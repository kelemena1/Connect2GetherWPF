﻿#pragma checksum "..\..\..\AdminHome.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D5F048E13A03A9BB94498484159F0077F864AC59"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Connect2GetherWPF;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Connect2GetherWPF {
    
    
    /// <summary>
    /// AdminHome
    /// </summary>
    public partial class AdminHome : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Displaydg;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Postcountlbl;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UserCountlbl;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_Post;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn SP1;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Delete_btn;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\AdminHome.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Change_data;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Connect2GetherWPF;component/adminhome.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AdminHome.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Displaydg = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\..\AdminHome.xaml"
            this.Displaydg.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Displaydg_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Postcountlbl = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.UserCountlbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.dg_Post = ((System.Windows.Controls.DataGrid)(target));
            
            #line 34 "..\..\..\AdminHome.xaml"
            this.dg_Post.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SP1 = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 6:
            
            #line 67 "..\..\..\AdminHome.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 68 "..\..\..\AdminHome.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Delete_btn = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\AdminHome.xaml"
            this.Delete_btn.Click += new System.Windows.RoutedEventHandler(this.Change_data_btn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Change_data = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\AdminHome.xaml"
            this.Change_data.Click += new System.Windows.RoutedEventHandler(this.Change_data_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

