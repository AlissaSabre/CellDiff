﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CellDiff.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CellDiff.Properties.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Two sellected areas contain different number of cells.  Try selecting two areas of same size, or one contiguous area whose width (number of columns) or height (number of rows) is two..
        /// </summary>
        internal static string ERROR_Areas_have_different_sizes {
            get {
                return ResourceManager.GetString("ERROR_Areas_have_different_sizes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Three or more separate areas are selected.  Try selecting two areas of same size, or one contiguous area whose width (number of columns) or height (number of rows) is two..
        /// </summary>
        internal static string ERROR_Invalid_selected_areas {
            get {
                return ResourceManager.GetString("ERROR_Invalid_selected_areas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Selection is ambiguous to compre.  Try selecting two areas of same size, or one contiguous area whose width (number of columns) or height (number of rows) is two..
        /// </summary>
        internal static string ERROR_Invalid_selected_range {
            get {
                return ResourceManager.GetString("ERROR_Invalid_selected_range", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Selected cells contain one or more merged cells.  Use Compare with Advanced Options to compare texts in merged cells..
        /// </summary>
        internal static string ERROR_MergedCells {
            get {
                return ResourceManager.GetString("ERROR_MergedCells", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple cells are required to compare.  Try selecting two or more cells..
        /// </summary>
        internal static string ERROR_One_cell {
            get {
                return ResourceManager.GetString("ERROR_One_cell", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No cells are currently selected.  Try selecting cells that contain texts to compare..
        /// </summary>
        internal static string ERROR_Selection_was_not_a_Range {
            get {
                return ResourceManager.GetString("ERROR_Selection_was_not_a_Range", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Compare.
        /// </summary>
        internal static string ErrorCaption {
            get {
                return ResourceManager.GetString("ErrorCaption", resourceCulture);
            }
        }
    }
}
