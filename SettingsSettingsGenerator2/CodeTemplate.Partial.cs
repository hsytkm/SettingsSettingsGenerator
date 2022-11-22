using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SettingsSettingsGenerator;

public partial class CodeTemplate
{
    internal ClassDeclarationSyntax ClassDecl { get; }
    internal IReadOnlyList<AttributeFieldSource> AttributeFieldSources { get; }
    internal INamedTypeSymbol TypeSymbol { get; }

    internal CodeTemplate(ClassDeclarationSyntax classDecl, IReadOnlyList<AttributeFieldSource> fieldSources, INamedTypeSymbol typeSymbol)
    {
        ClassDecl = classDecl;
        AttributeFieldSources = fieldSources;
        TypeSymbol = typeSymbol;
    }

    internal string ClassName
    {
        get
        {
            _className ??= ClassDecl.GetGenericTypeName();
            return _className;
        }
    }
    string? _className;

    internal string CodeFilename
    {
        get
        {
            _codeFilename ??= TypeSymbol.GenerateHintName();
            return _codeFilename;
        }
    }
    string? _codeFilename;

    internal string Namespace
    {
        get
        {
            _namespace ??= TypeSymbol.ContainingNamespace.ToDisplayString();
            return _namespace;
        }
    }
    string? _namespace;

    internal string SettingsClassName
    {
        get
        {
            _settingsClassName ??= GetSettingClassName(GetOptionKey(AttributeFieldSources.FirstOrDefault()));
            return _settingsClassName;
        }
    }
    string? _settingsClassName;

    internal static string GetFieldTypeFullName(AttributeFieldSource source)
    {
        var typeName = source.TypeName.ToString();
        var length = typeName.Length;
        return (typeName[length - 1] == '?') ? typeName.Substring(0, length - 1) : typeName;
    }

    internal static string GetBackingFieldName(AttributeFieldSource source) => source.SymbolName;

    internal static string GetBackingFieldValue(AttributeFieldSource source)
    {
        return source.DeclType is AttributeFieldSource.FieldDeclarationType.NullableValue
            ? source.SymbolName + ".Value"
            : source.SymbolName;
    }

    internal static string GetPropertyName(AttributeFieldSource source)
    {
        var fieldName = source.SymbolName;
        var propName = (fieldName[0] == '_') ? fieldName.Substring(1) : fieldName;
        return propName.ToUpperOnlyFirst();
    }

    internal static string GetResourceKey(string argument)
    {
        var match = Regex.Match(argument.Trim(), @"^nameof\((?<name>.+)\)");
        return match.Success ? match.Groups[1].Value : argument;
    }

    internal static string GetOptionKey(AttributeFieldSource source) => GetResourceKey(source.Argument);

    internal static string GetSettingClassName(string? text) => text is null ? "" : GetResourceKey(text).Split('.')[0];
}
