#pragma checksum "..\..\LoginSignupPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4F8E704690A74751CFAA0766847D74842506891CBCE55FDA075C2709DA7CA61F"
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
    /// LoginSignupPage
    /// </summary>
    public partial class LoginSignupPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas LoginCanvas;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ApplicationLabel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewAccountButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessage;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\LoginSignupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Footer;
        
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
            System.Uri resourceLocater = new System.Uri("/RemoteCodeAnalyzer;component/loginsignuppage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LoginSignupPage.xaml"
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
            this.LoginCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 12 "..\..\LoginSignupPage.xaml"
            this.LoginCanvas.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\LoginSignupPage.xaml"
            this.LoginCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.Canvas_KeyDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\LoginSignupPage.xaml"
            this.LoginCanvas.Loaded += new System.Windows.RoutedEventHandler(this.Canvas_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ApplicationLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.NewAccountButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\LoginSignupPage.xaml"
            this.NewAccountButton.Click += new System.Windows.RoutedEventHandler(this.NewAccountButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EmailTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\LoginSignupPage.xaml"
            this.EmailTextBox.IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.EmailTextBox_ActivateOnClick);
            
            #line default
            #line hidden
            
            #line 24 "..\..\LoginSignupPage.xaml"
            this.EmailTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.EmailTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 26 "..\..\LoginSignupPage.xaml"
            this.PasswordTextBox.IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.PasswordTextBox_ActivateOnClick);
            
            #line default
            #line hidden
            
            #line 26 "..\..\LoginSignupPage.xaml"
            this.PasswordTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.PasswordTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\LoginSignupPage.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.LoginButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ErrorMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.Footer = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

