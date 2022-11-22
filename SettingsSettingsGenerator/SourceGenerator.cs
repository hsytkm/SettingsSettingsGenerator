using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingsSettingsGenerator;

[Generator]
public sealed class SettingsGenerator : ISourceGenerator
{
    internal const string AttributeName = nameof(SettingsGenerator) + "Attribute";

    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            //System.Diagnostics.Debugger.Launch();
        }
#endif
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var attrCode = new SettingsGeneratorAttributeTemplate().TransformText();
        context.AddSource(AttributeName + ".cs", attrCode);

        try
        {
            if (context.SyntaxReceiver is not SyntaxReceiver receiver) return;

            foreach (var classDeclaration in receiver.Targets)
            {
                var model = context.Compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                var typeSymbol = model.GetDeclaredSymbol(classDeclaration);
                if (typeSymbol is null) continue;

                var candidateFields = classDeclaration.Members.OfType<FieldDeclarationSyntax>()
                    .Select(field => (field, model))
                    .SelectMany(x => x.field.Declaration.Variables.Select(v => (x.field, symbol: x.model.GetDeclaredSymbol(v) as IFieldSymbol)))
                    .Where(x => x.symbol?.GetAttributes().Any(y => y.AttributeClass?.Name is nameof(SettingsGenerator) or AttributeName) ?? false)
                    .ToArray();

                var fieldSource = candidateFields.Select(x => AttributeFieldSource.Create(model, x.field)).ToArray();
                if (fieldSource is null) continue;

                var template = new CodeTemplate(classDeclaration, fieldSource!)
                {
                    Namespace = typeSymbol.ContainingNamespace.ToDisplayString(),
                };

                var text = template.TransformText();
                context.AddSource(typeSymbol.GenerateHintName(), text);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.WriteLine(ex.ToString());
        }
    }

    private sealed class SyntaxReceiver : ISyntaxReceiver
    {
        internal List<ClassDeclarationSyntax> Targets { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not ClassDeclarationSyntax classDeclaration) return;

            var fields = classDeclaration.Members.OfType<FieldDeclarationSyntax>();
            if (fields is null) return;

            var attr = fields.SelectMany(x => x.AttributeLists)?.SelectMany(x => x.Attributes)
                .FirstOrDefault(x => x.Name.ToString() is nameof(SettingsGenerator) or AttributeName);
            if (attr is null) return;

            Targets.Add(classDeclaration);
        }
    }
}
