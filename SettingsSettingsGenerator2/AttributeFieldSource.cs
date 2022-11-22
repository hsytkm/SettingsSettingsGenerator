using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SettingsSettingsGenerator;

internal sealed class AttributeFieldSource
{
    internal enum FieldDeclarationType { NullableValue, String, /*Enum*/ }

    internal IFieldSymbol Symbol { get; }
    internal string TypeName { get; }
    internal FieldDeclarationType DeclType { get; }
    internal string Argument { get; }

    internal string SymbolName => Symbol.Name;
    internal string Namespace => Symbol.ContainingNamespace.ToDisplayString();

    private AttributeFieldSource(IFieldSymbol symbol, string type, FieldDeclarationType declType, string argument) =>
        (Symbol, TypeName, DeclType, Argument) = (symbol, type, declType, argument);

    internal static AttributeFieldSource? Create(FieldDeclarationSyntax fieldDeclaration, IFieldSymbol fieldSymbol)
    {
        var argument = GetArgument(fieldDeclaration);
        if (argument is null)
            return null;

        var typeName = fieldSymbol.Type.ToString(); //.Split('.').Last();

        // target is only Nullable type.
        if (typeName[typeName.Length - 1] != '?')
            return null;

        var declType = typeName.Contains("string") ? FieldDeclarationType.String : FieldDeclarationType.NullableValue;

        return new(fieldSymbol, typeName, declType, argument);
    }

    static string? GetArgument(FieldDeclarationSyntax fieldDeclaration)
    {
        var attr = fieldDeclaration.AttributeLists.SelectMany(static x => x.Attributes)
            .FirstOrDefault(static x => GenerateHelper.ContainsGeneratorName(x.Name.ToString()));

        IReadOnlyList<AttributeArgumentSyntax>? args = attr?.ArgumentList?.Arguments;
        if (args is not null && args.Count >= 1)
        {
            var key = args.Take(2).Select(x => x.ToString()).FirstOrDefault();
            return key is null ? "" : key;
        }
        return null;
    }
}
