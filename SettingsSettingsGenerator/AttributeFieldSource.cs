using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SettingsSettingsGenerator
{
    internal class AttributeFieldSource
    {
        internal enum FieldDeclarationType { BuiltIn, String, /*Enum*/ }

        internal string FieldName { get; }
        internal string TypeName { get; }
        internal FieldDeclarationType FieldType { get; }
        internal string Key { get; }

        private AttributeFieldSource(string name, string type, FieldDeclarationType decl, string key)
            => (FieldName, TypeName, FieldType, Key) = (name, type, decl, key);

        internal static AttributeFieldSource? Create(SemanticModel semanticModel, FieldDeclarationSyntax fieldDeclaration)
        {
            var fieldSymbol = fieldDeclaration.Declaration.Variables.Select(v => semanticModel.GetDeclaredSymbol(v) as IFieldSymbol).FirstOrDefault();
            if (fieldSymbol is null)
                return null;

            var typeName = fieldSymbol.Type.ToString(); //.Split('.').Last();
            var fieldDecl = typeName.Contains("string") ? FieldDeclarationType.String : FieldDeclarationType.BuiltIn;

            // target is only Nullable type.
            if (typeName[typeName.Length - 1] != '?')
                return null;

            var key = GetKeyName(fieldDeclaration);
            if (key is null)
                return null;

            return new(fieldSymbol.Name, typeName, fieldDecl, key);
        }

        private static string? GetKeyName(FieldDeclarationSyntax fieldDeclaration)
        {
            var attr = fieldDeclaration.AttributeLists.SelectMany(x => x.Attributes)
                .FirstOrDefault(x => x.Name.ToString() is nameof(SettingsGenerator) or SettingsGenerator.AttributeName);

            IReadOnlyList<AttributeArgumentSyntax>? args = attr?.ArgumentList?.Arguments;
            if (args is not null && args.Count >= 1)
            {
                var key = args.Take(2).Select(x => x.ToString()).FirstOrDefault() ?? "";
                return key;
            }
            return null;
        }

    }
}
