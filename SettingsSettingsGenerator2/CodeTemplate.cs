﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 17.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace SettingsSettingsGenerator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class CodeTemplate : CodeTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("// <auto-generated>\r\n// THIS (.cs) FILE IS GENERATED USING IIncrementalGenerator." +
                    " DO NOT CHANGE IT.\r\n// </auto-generated>\r\n#nullable enable\r\n\r\n");
 if (!string.IsNullOrEmpty(Namespace)) { 
            this.Write("namespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            this.Write("\r\n");
 } 
            this.Write("{\r\n    [global::System.CodeDom.Compiler.GeneratedCode(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerateHelper.GeneratorName));
            this.Write("\", \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(GenerateHelper.GeneratorVersion));
            this.Write("\")]\r\n    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]\r\n    p" +
                    "artial class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            this.Write(" : global::System.ComponentModel.INotifyPropertyChanged\r\n    {\r\n");
 foreach (var item in AttributeFieldSources) { 
            this.Write("        /// <summary>\r\n        /// Property of ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOptionKey(item)));
            this.Write(".\r\n        /// </summary>\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetFieldTypeFullName(item)));
            this.Write(" ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetPropertyName(item)));
            this.Write("\r\n        {\r\n            // Created from ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldName(item)));
            this.Write("\r\n            get\r\n            {\r\n                ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldName(item)));
            this.Write(" ??= ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOptionKey(item)));
            this.Write(";\r\n                return ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldValue(item)));
            this.Write(";\r\n            }\r\n            set\r\n            {\r\n                ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldName(item)));
            this.Write(" ??= ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOptionKey(item)));
            this.Write(";\r\n                SetProperty(ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldName(item)));
            this.Write(", value);\r\n            }\r\n        }\r\n\r\n");
 } 
            this.Write(@"        // whether or not value has changed.
        bool _isChanged;

        /// <summary>
        /// Write the current configuration to *.settings.
        /// </summary>
        public void Save()
        {
            if (!_isChanged)
                return;

            // Write back the value if you update the value in your app.
");
 foreach (var item in AttributeFieldSources) { 
            this.Write("            if (");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldName(item)));
            this.Write(" != null) {\r\n                ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetOptionKey(item)));
            this.Write(" = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GetBackingFieldValue(item)));
            this.Write(";\r\n            }\r\n");
 } 
            this.Write("\r\n            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(SettingsClassName));
            this.Write(@".Default.Save();
            _isChanged = false;
        }

        public event global::System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

        bool SetProperty<T>(ref T field, T value, [global::System.Runtime.CompilerServices.CallerMemberName] string propertyName = """")
        {
            if (global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            _isChanged = true;

            NotifyPropertyChanged(propertyName);
            return true;
        }

        void NotifyPropertyChanged([global::System.Runtime.CompilerServices.CallerMemberName] string propertyName = """")
        {
            PropertyChanged?.Invoke(this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}
");
            return this.GenerationEnvironment.ToString();
        }
    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class CodeTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
