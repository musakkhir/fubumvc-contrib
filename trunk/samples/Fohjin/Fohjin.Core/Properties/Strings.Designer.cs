﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fohjin.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Fohjin.Core.Properties.Strings", typeof(Strings).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} comment.
        /// </summary>
        internal static string COMMENT_LINK_TEXT {
            get {
                return ResourceManager.GetString("COMMENT_LINK_TEXT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} comments.
        /// </summary>
        internal static string COMMENT_LINK_TEXT_PLURAL {
            get {
                return ResourceManager.GetString("COMMENT_LINK_TEXT_PLURAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Filed under {0}{1}.
        /// </summary>
        internal static string TAG_LINK_LIST_FORMAT {
            get {
                return ResourceManager.GetString("TAG_LINK_LIST_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Filed under &lt;ul class=\&quot;tags\&quot;&gt;{0}&lt;/ul&gt;{1}.
        /// </summary>
        internal static string TAG_LINK_LIST_UNORDERED_LIST_FORMAT {
            get {
                return ResourceManager.GetString("TAG_LINK_LIST_UNORDERED_LIST_FORMAT", resourceCulture);
            }
        }
    }
}