﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace Enrollment.Forms.View.Properties
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Enrollment.Forms.View.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Unknown validation class..
        /// </summary>
        internal static string unknownValidationClass
        {
            get
            {
                return ResourceManager.GetString("unknownValidationClass", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to A validation message is required. Validation Function: {0}, Variable Name: {1}..
        /// </summary>
        internal static string validationFunctionMessageRequiredFormat
        {
            get
            {
                return ResourceManager.GetString("validationFunctionMessageRequiredFormat", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The validation function: &quot;{0}&quot; does not exist in the class &quot;{1}&quot;..
        /// </summary>
        internal static string validationFunctionNotFoundFormat
        {
            get
            {
                return ResourceManager.GetString("validationFunctionNotFoundFormat", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Validation messages are requred for variable {0}..
        /// </summary>
        internal static string validationMessagesRequiredFormat
        {
            get
            {
                return ResourceManager.GetString("validationMessagesRequiredFormat", resourceCulture);
            }
        }
    }
}
