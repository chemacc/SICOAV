﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0E8776A39CAC529680EA13A9A98BA996B29B899D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using GMap.NET.WindowsPresentation;
using IB_CTRL_SICOAV.controles;
using SICOAV_A;
using SICOAV_A.Controles;
using SICOAV_A.Info;
using SICOAV_A.Recursos;
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


namespace SICOAV_A {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SICOAV_A.Recursos.Map MainMap;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wrapPanel_vuelos;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wrapPanel_aeropuertos;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wrapPanel_configuracion;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbl_latitud;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbl_longitud;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel panel_zoom;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel panel_mapas;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel panel_error;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SICOAV_A.Info.IB_CTRL_ALERTA Panel_alerta;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wrapBonotonera;
        
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
            System.Uri resourceLocater = new System.Uri("/SICOAV_A;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.MainMap = ((SICOAV_A.Recursos.Map)(target));
            return;
            case 2:
            this.wrapPanel_vuelos = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 3:
            this.wrapPanel_aeropuertos = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 4:
            this.wrapPanel_configuracion = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 5:
            this.lbl_latitud = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.lbl_longitud = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.panel_zoom = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 8:
            
            #line 33 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_A)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_A_OnMaximum);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 34 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_A)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_A_OnMaximum_1);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 35 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_A)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_A_OnMaximum_2);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 36 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_A)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_A_OnMaximum_3);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 37 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_A)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_A_OnMaximum_4);
            
            #line default
            #line hidden
            return;
            case 13:
            this.panel_mapas = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 14:
            
            #line 41 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_B)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_B_OnMaximum);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 42 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_B)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_B_OnMaximum_1);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 43 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_B)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_B_OnMaximum_2);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 44 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_B)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_B_OnMaximum_3);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 45 "..\..\MainWindow.xaml"
            ((IB_CTRL_SICOAV.controles.SIVOAV_GIS_CTRL_B)(target)).OnMaximum += new IB_CTRL_SICOAV.controles.MyEventHandler(this.SIVOAV_GIS_CTRL_B_OnMaximum_4);
            
            #line default
            #line hidden
            return;
            case 19:
            this.panel_error = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 20:
            this.Panel_alerta = ((SICOAV_A.Info.IB_CTRL_ALERTA)(target));
            return;
            case 21:
            this.wrapBonotonera = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

