using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SICOAV_A.Controles
{
    public enum tipoBotonPP
        {
        SELECT_PAIS,
        PLAN_VUENO,
        COLISIONES,
        AVIONES,
        CONFIGURACION,
        AIRPORT,
        NINGUNA,

        };
    /// <summary>
    /// Lógica de interacción para IB_CTRL_BTN_MenuPP.xaml
    /// </summary>
    public partial class IB_CTRL_BTN_MenuPP : UserControl
    {
        public delegate void CustomEventHandler(object sender, tipoBotonPP a);

        public event EventHandler<tipoBotonPP> RaiseCustomEvent;

        tipoBotonPP m_TipoBoton;

        bool isSelected;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (value) Seccionado();
                else NoSeccionado();

                isSelected = value;

            }
        }

        public tipoBotonPP TipoBoton { get => m_TipoBoton;  }

        public IB_CTRL_BTN_MenuPP()
        {
            InitializeComponent();
            this.txt_titulo.Text = "Aviones";
            Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-automatic-filled-100.png"));
        }

        public IB_CTRL_BTN_MenuPP(tipoBotonPP tipo)
        {
            InitializeComponent();
            m_TipoBoton = tipo;

            switch(tipo)
            {
                case tipoBotonPP.AVIONES:
                    this.txt_titulo.Text = "Aviones";
                    Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-automatic-filled-100.png"));
                    break;
                case tipoBotonPP.COLISIONES:
                    this.txt_titulo.Text = "Colisiones";
                    Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-strategy-filled-100.png"));
                    break;
                case tipoBotonPP.CONFIGURACION: 
                    this.txt_titulo.Text= "Config";
                    Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-automatic-filled-100.png"));
                    break;
                case tipoBotonPP.SELECT_PAIS:
                    this.txt_titulo.Text = "Ubicación";
                    Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-globe-filled-100.png"));
                    break;
                case tipoBotonPP.PLAN_VUENO:
                    this.txt_titulo.Text = "Planes";
                    Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-search-property-filled-100.png"));
                    break;
                case tipoBotonPP.AIRPORT:
                    this.txt_titulo.Text = "Airports";
                    Imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SICOAV_A;component/imagenes/icons8-runway-filled-100.png"));
                    break;
            }
        }

        private void Seccionado()
        {
            BrushConverter bc = new BrushConverter();
            this.gridpp.Background = (Brush)bc.ConvertFrom("#FFB6B6B6");
            this.Rectangulo.Visibility = Visibility.Visible;
        }

        private void NoSeccionado()
        {
            BrushConverter bc = new BrushConverter();
            this.gridpp.Background = (Brush)bc.ConvertFrom("#FF727272");
            this.Rectangulo.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Seccionado();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isSelected)
                NoSeccionado();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(isSelected)
            {
                NoSeccionado();
                isSelected = false;
            }
            else
            {
                Seccionado();
                isSelected = true;
            }
        }
    }
}
