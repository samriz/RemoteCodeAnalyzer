﻿#pragma checksum "..\..\LoginOrCreateAccount.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5E14136EC4205B395C8CE8632E2BCB96B27C138E48BA3E5207E258EF72C9E9DF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RemoteCodeAnalyzer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace RemoteCodeAnalyzer {
    
    
    /// <summary>
    /// LoginOrCreateAccount
    /// </summary>
    public partial class LoginOrCreateAccount : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\LoginOrCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox loginBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\LoginOrCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\LoginOrCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\LoginOrCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox newAccountBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RemoteCodeAnalyzer;component/loginorcreateaccount.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LoginOrCreateAccount.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\LoginOrCreateAccount.xaml"
            ((System.Windows.Controls.DockPanel)(target)).IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.DockPanel_IsMouseDirectlyOverChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.loginBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 3:
            this.EmailTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\LoginOrCreateAccount.xaml"
            this.EmailTextBox.IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.EmailTextBox_IsMouseDirectlyOverChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PasswordTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\LoginOrCreateAccount.xaml"
            this.PasswordTextBox.IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.PasswordTextBox_IsMouseDirectlyOverChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 28 "..\..\LoginOrCreateAccount.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Login_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.newAccountBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 7:
            
            #line 35 "..\..\LoginOrCreateAccount.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

