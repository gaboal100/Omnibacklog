﻿#pragma checksum "..\..\..\..\Paginas\Generos.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23BC7A995448817A3B3632257161A91D5281B6C1"
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
    /// Generos
    /// </summary>
    public partial class Generos : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridGeneros;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView LVGenerosT;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBNombreEdit;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditarNombre;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BorrarGenero;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridGeneroNuevo;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBGenAñadir;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AñadirGen;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DGLibros;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkDefault;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView LVGenerosCambiar;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\Paginas\Generos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CambiarGeneros;
        
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
            System.Uri resourceLocater = new System.Uri("/OmniBacklog;component/paginas/generos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Paginas\Generos.xaml"
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
            this.GridGeneros = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.LVGenerosT = ((System.Windows.Controls.ListView)(target));
            
            #line 41 "..\..\..\..\Paginas\Generos.xaml"
            this.LVGenerosT.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LVGenerosT_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TBNombreEdit = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.EditarNombre = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\Paginas\Generos.xaml"
            this.EditarNombre.Click += new System.Windows.RoutedEventHandler(this.EditarNombre_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BorrarGenero = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\Paginas\Generos.xaml"
            this.BorrarGenero.Click += new System.Windows.RoutedEventHandler(this.BorrarGenero_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GridGeneroNuevo = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.TBGenAñadir = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.AñadirGen = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\..\Paginas\Generos.xaml"
            this.AñadirGen.Click += new System.Windows.RoutedEventHandler(this.AñadirGen_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DGLibros = ((System.Windows.Controls.DataGrid)(target));
            
            #line 92 "..\..\..\..\Paginas\Generos.xaml"
            this.DGLibros.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DGLibros_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.checkDefault = ((System.Windows.Controls.CheckBox)(target));
            
            #line 110 "..\..\..\..\Paginas\Generos.xaml"
            this.checkDefault.Checked += new System.Windows.RoutedEventHandler(this.checkDefault_Checked);
            
            #line default
            #line hidden
            
            #line 110 "..\..\..\..\Paginas\Generos.xaml"
            this.checkDefault.Unchecked += new System.Windows.RoutedEventHandler(this.checkDefault_Unchecked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.LVGenerosCambiar = ((System.Windows.Controls.ListView)(target));
            
            #line 111 "..\..\..\..\Paginas\Generos.xaml"
            this.LVGenerosCambiar.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LVGenerosCambiar_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.CambiarGeneros = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\..\Paginas\Generos.xaml"
            this.CambiarGeneros.Click += new System.Windows.RoutedEventHandler(this.CambiarGeneros_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

