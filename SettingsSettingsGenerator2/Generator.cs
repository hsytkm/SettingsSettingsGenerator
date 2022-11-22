using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SettingsSettingsGenerator;

// https://zenn.dev/pcysl5edgo/articles/6d9be0dd99c008
// https://qiita.com/WiZLite/items/48f37278cf13be899e40

[Generator(LanguageNames.CSharp)]
public sealed class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            //System.Diagnostics.Debugger.Launch();
        }
#endif
        context.RegisterPostInitializationOutput(GenerateInitialCode);

        IncrementalValuesProvider<CodeTemplate> syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (node, token) =>
            {
                token.ThrowIfCancellationRequested();

                // 超高頻度に呼ばれるため class宣言 のみを判定します
                return node is ClassDeclarationSyntax;
            },
            transform: static (context, token) =>
            {
                token.ThrowIfCancellationRequested();
                ClassDeclarationSyntax classDecl = (context.Node as ClassDeclarationSyntax)!;

                if (!classDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
                    return null;

                IEnumerable<AttributeFieldSource> fieldSource = classDecl.Members.OfType<FieldDeclarationSyntax>()
                    .Select(fieldDecl => CreateAttributeFieldSource(context.SemanticModel, fieldDecl))
                    .Where(static x => x is not null)!;

                var filedSourceArray = fieldSource.ToArray();
                if (filedSourceArray.Length is 0)
                    return null;

                INamedTypeSymbol? typeSymbol = Microsoft.CodeAnalysis.CSharp.CSharpExtensions.GetDeclaredSymbol(context.SemanticModel, classDecl);
                if (typeSymbol is null)
                    return null;

                return new CodeTemplate(classDecl, filedSourceArray, typeSymbol);
            })
            .Where(static x => x is not null)!;

        context.RegisterSourceOutput(syntaxProvider, static (context, coteTemplate) =>
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                context.AddSource(coteTemplate.CodeFilename, coteTemplate.TransformText());
            });
    }

    static void GenerateInitialCode(IncrementalGeneratorPostInitializationContext context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
        context.AddSource(GenerateHelper.AttributeFilename, GenerateHelper.AttributeCs);
    }

    static AttributeFieldSource? CreateAttributeFieldSource(SemanticModel semanticModel, FieldDeclarationSyntax fieldDecl)
    {
        foreach (var variable in fieldDecl.Declaration.Variables)
        {
            var symbol = semanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
            if (symbol is not null)
            {
                var isTarget = symbol.GetAttributes().Any(static x => GenerateHelper.ContainsGeneratorName(x.AttributeClass?.Name));
                if (isTarget)
                    return AttributeFieldSource.Create(fieldDecl, symbol);
            }
        }
        return null;
    }
}
