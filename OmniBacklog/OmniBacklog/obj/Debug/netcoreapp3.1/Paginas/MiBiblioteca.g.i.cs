﻿#pragma checksum "..\..\..\..\Paginas\MiBiblioteca.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4660C6E84E4F4DDBC597228E6B9BE097BF612A7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using OmniBacklog;
using OmniBacklog.DAL;
using OmniBacklog.MODEL;
using OmniBacklog.Paginas;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace OmniBacklog.Paginas {
    
    
    /// <summary>
    /// MiBiblioteca
    /// </summary>
    public partial class MiBiblioteca : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DGLeyendo;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtNoLeyendo;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBLibro;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBSaga;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBNombreBuscar;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTBuscar;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkFav;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DGBiblio;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTLeer;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTFav;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTFueraBiblio;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\..\Paginas\MiBiblioteca.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTAñadirABiblio;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OmniBacklog;V1.0.0.0;component/paginas/mibiblioteca.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.17.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DGLeyendo = ((System.Windows.Controls.DataGrid)(target));
            
            #line 47 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.DGLeyendo.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DGLeyendo_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtNoLeyendo = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.BtNoLeyendo.Click += new System.Windows.RoutedEventHandler(this.BtNoLeyendo_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RBLibro = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.RBSaga = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.TBNombreBuscar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.BTBuscar = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.BTBuscar.Click += new System.Windows.RoutedEventHandler(this.BTBuscar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.checkFav = ((System.Windows.Controls.CheckBox)(target));
            
            #line 108 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.checkFav.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 108 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.checkFav.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DGBiblio = ((System.Windows.Controls.DataGrid)(target));
            
            #line 117 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.DGBiblio.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DGBiblio_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BTLeer = ((System.Windows.Controls.Button)(target));
            
            #line 145 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.BTLeer.Click += new System.Windows.RoutedEventHandler(this.BTLeer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.BTFav = ((System.Windows.Controls.Button)(target));
            
            #line 153 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.BTFav.Click += new System.Windows.RoutedEventHandler(this.BTFav_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.BTFueraBiblio = ((System.Windows.Controls.Button)(target));
            
            #line 161 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.BTFueraBiblio.Click += new System.Windows.RoutedEventHandler(this.BTFueraBiblio_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.BTAñadirABiblio = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\..\..\Paginas\MiBiblioteca.xaml"
            this.BTAñadirABiblio.Click += new System.Windows.RoutedEventHandler(this.BTAñadirABiblio_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

