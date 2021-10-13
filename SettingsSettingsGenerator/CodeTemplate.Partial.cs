#pragma warning disable IDE0057
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SettingsSettingsGenerator
{
    public partial class CodeTemplate
    {
        internal string Namespace { get; set; } = "";
        internal string ClassName { get; }
        internal string SettingsClassName { get; }
        internal IReadOnlyList<AttributeFieldSource> AttributeFieldSources { get; }

        internal CodeTemplate(ClassDeclarationSyntax classDeclaration, IEnumerable<AttributeFieldSource> fieldSources)
        {
            ClassName = classDeclaration.GetGenericTypeName();
            AttributeFieldSources = fieldSources.ToArray();
            SettingsClassName = GetSettingClassName(GetOptionKey(AttributeFieldSources.FirstOrDefault()));
            ;
        }

        internal static string GetFieldTypeFullName(AttributeFieldSource source)
        {
            var typeName = source.TypeName.ToString();
            var length = typeName.Length;
            return (typeName[length - 1] == '?') ? typeName.Substring(0, length - 1) : typeName;
        }

        internal static string GetBackingFieldName(AttributeFieldSource source) => source.FieldName;

        internal static string GetBackingFieldValue(AttributeFieldSource source)
        {
            return source.FieldType == AttributeFieldSource.FieldDeclarationType.BuiltIn
                ? source.FieldName + ".Value"
                : source.FieldName;
        }

        internal static string GetPropertyName(AttributeFieldSource source)
        {
            var fieldName = source.FieldName;
            var propName = (fieldName[0] == '_') ? fieldName.Substring(1) : fieldName;
            return propName.ToUpperOnlyFirst();
        }

        internal static string GetOptionKey(string text)
        {
            var match = Regex.Match(text.Trim(), @"^nameof\((?<name>.+)\)");
            if (match.Success)
                text = match.Groups[1].Value;
            return text;
        }

        internal static string GetOptionKey(AttributeFieldSource source) => GetOptionKey(source.Key);

        internal static string GetSettingClassName(string? text) => text is null ? "" : GetOptionKey(text).Split('.')[0];
    }
}
