﻿#pragma checksum "..\..\UserPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EF5B072911252ACD9B33640D28D44DB1EF9F2641A70194D8649251AC332DB6AB"
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
    /// UserPage
    /// </summary>
    public partial class UserPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FullNameLabel;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchFiles;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FolderPathLabel;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UploadFiles;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProjectNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView AnalysisResultsGrid;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessage;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox UsersComboBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView UsersProjectsTreeView;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RelativePathBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AnalyzeButton;
        
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
            System.Uri resourceLocater = new System.Uri("/RemoteCodeAnalyzer;component/userpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserPage.xaml"
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
            this.FullNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.SearchFiles = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\UserPage.xaml"
            this.SearchFiles.Click += new System.Windows.RoutedEventHandler(this.SearchFiles_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FolderPathLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.UploadFiles = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\UserPage.xaml"
            this.UploadFiles.Click += new System.Windows.RoutedEventHandler(this.UploadFiles_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ProjectNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\UserPage.xaml"
            this.ProjectNameTextBox.IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.ProjectNameTextBox_ActivateOnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AnalysisResultsGrid = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            this.ErrorMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.UsersComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\UserPage.xaml"
            this.UsersComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.UsersComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.UsersProjectsTreeView = ((System.Windows.Controls.TreeView)(target));
            return;
            case 10:
            this.RelativePathBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.AnalyzeButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\UserPage.xaml"
            this.AnalyzeButton.Click += new System.Windows.RoutedEventHandler(this.AnalyzeButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

