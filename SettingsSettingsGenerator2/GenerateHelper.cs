using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SettingsSettingsGenerator
{
    internal static class GenerateHelper
    {
        internal const string GeneratorName = "SettingsGenerator";
        internal const string GeneratorVersion = "1.0.0.0";
        internal const string Namespace = nameof(SettingsSettingsGenerator);

        internal const string AttributeFilename = "SettingsGeneratorAttribute.cs";
        internal const string AttributeCs = @$"namespace {Namespace}
{{
    [global::System.CodeDom.Compiler.GeneratedCode(""{GeneratorName}"", ""{GeneratorVersion}"")]
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.AttributeUsage(global::System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class SettingsGeneratorAttribute : global::System.Attribute
    {{
        public string Key {{ get; }}

        public SettingsGeneratorAttribute(string key)
        {{
            Key = key;
        }}
    }}
}}
";

        internal static bool ContainsGeneratorName(string? name) => name is GeneratorName or $"{GeneratorName}Attribute";

        internal static string ToUpperOnlyFirst(this string str) => char.ToUpper(str[0]) + str.Substring(1);
    }

    internal static class RoslynExtension
    {
        // Code from: https://github.com/YairHalberstadt/stronginject/blob/779a38e7e74b92c87c86ded5d1fef55744d34a83/StrongInject/Generator/RoslynExtensions.cs#L166
        internal static string FullName(this INamespaceSymbol @namespace) =>
            @namespace.ToDisplayString(new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces));

        // Code from: https://github.com/YairHalberstadt/stronginject/blob/779a38e7e74b92c87c86ded5d1fef55744d34a83/StrongInject/Generator/RoslynExtensions.cs#L69
        internal static IEnumerable<INamedTypeSymbol> GetContainingTypesAndThis(this INamedTypeSymbol? namedType)
        {
            var current = namedType;
            while (current is not null)
            {
                yield return current;
                current = current.ContainingType;
            }
        }

        // Code from: https://github.com/YairHalberstadt/stronginject/blob/779a38e7e74b92c87c86ded5d1fef55744d34a83/StrongInject/Generator/SourceGenerator.cs#L87
        internal static string GenerateHintName(this INamedTypeSymbol container)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(container.ContainingNamespace.FullName());
            foreach (var type in container.GetContainingTypesAndThis().Reverse())
            {
                stringBuilder.Append('.');
                stringBuilder.Append(type.Name);
                if (type.TypeParameters.Length > 0)
                {
                    stringBuilder.Append('_');
                    stringBuilder.Append(type.TypeParameters.Length);
                }
            }
            stringBuilder.Append(".g.cs");
            return stringBuilder.ToString();
        }

        //public static string? NullIfEmpty(this string? value) => string.IsNullOrEmpty(value) ? null : value;
    }

    internal static class SyntaxExtension
    {
        internal static string GetGenericTypeName(this TypeDeclarationSyntax typeDecl)
        {
            if (typeDecl.TypeParameterList is null)
                return typeDecl.Identifier.Text;

            var param = string.Join(", ", typeDecl.TypeParameterList.Parameters.Select(p => p.Identifier.Text));
            return typeDecl.Identifier.Text + "<" + param + ">";
        }
    }
}

namespace System.CodeDom.Compiler
{
    public class CompilerError
    {
        public string? ErrorText { get; set; }
        public bool IsWarning { get; set; }
    }

    public class CompilerErrorCollection
    {
        public void Add(CompilerError error) { }
    }
}
