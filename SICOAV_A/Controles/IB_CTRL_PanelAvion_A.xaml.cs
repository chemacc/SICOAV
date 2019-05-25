using SICOAV_A.Recursos;
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
    /// <summary>
    /// Lógica de interacción para IB_CTRL_PanelAvion_A.xaml
    /// </summary>
    public partial class IB_CTRL_PanelAvion_A : UserControl
    {
        public delegate void CustomEventHandler(object sender, FlightRadarData a);

        public event EventHandler<FlightRadarData> RaiseCustomEvent;

        FlightRadarData m_flightRadarData;

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

        public FlightRadarData RadarData
        {
            get
            {
                return m_flightRadarData;
            }
            set
            {
                m_flightRadarData = value;

                this.txt_altura.Text = value.altitude;
                this.txt_lat.Text = Latitud_Cadena(value.point.Lat);
                this.txt_lon.Text = Longitud_Cadena(value.point.Lng);
                this.txt_rumbo.Text = value.bearing.ToString();
                this.txt_callsing.Text = value.name;
                this.txt_Num.Text = value.NVuelo;
            }
        }

        public IB_CTRL_PanelAvion_A()
        {
            InitializeComponent();
        }

        public IB_CTRL_PanelAvion_A(FlightRadarData title, bool selected)
        {
            InitializeComponent();

            m_flightRadarData = title;
            isSelected = selected;
            if(isSelected)
            {
                Seccionado();
            }
            else
            {
                NoSeccionado();
            }

            RadarData = title;
        }

        private void Seccionado()
        {
            BrushConverter bc = new BrushConverter();
            this.grid_pp.Background = (Brush)bc.ConvertFrom("#FF024744");
            this.RectangleSelected.Visibility = Visibility.Visible;
        }

        private void NoSeccionado()
        {
            BrushConverter bc = new BrushConverter();
            this.grid_pp.Background = (Brush)bc.ConvertFrom("#FF222323");
            this.RectangleSelected.Visibility = Visibility.Hidden;
        }

        protected virtual void OnRaiseCustomEvent(FlightRadarData e)
        {
            EventHandler<FlightRadarData> handler = RaiseCustomEvent;

            if (handler != null)
            {

                handler(this, e);
            }
        }

        private string Longitud_Cadena(double longitud)
        {


            double lon = longitud;

            string lonDir = (lon >= 0 ? "E" : "O");
            lon = Math.Abs(lon);
            double lonMinPart = ((lon - Math.Truncate(lon) / 1) * 60);
            double lonSecPart = ((lonMinPart - Math.Truncate(lonMinPart) / 1) * 60);


            return string.Format("{0:00}", Math.Truncate(lon)) + "º " + string.Format("{0:00}", Math.Truncate(lonMinPart)) + "' " + string.Format("{0:00.00}", lonSecPart) + "'' " + lonDir;
        }

        private string Latitud_Cadena(double latitud)
        {

            double lat = latitud;


            string latDir = (lat >= 0 ? "N" : "S");
            lat = Math.Abs(lat);
            double latMinPart = ((lat - Math.Truncate(lat) / 1) * 60);
            double latSecPart = ((latMinPart - Math.Truncate(latMinPart) / 1) * 60);


            return string.Format("{0:00}", Math.Truncate(lat)) + "º " + string.Format("{0:00}", Math.Truncate(latMinPart)) + "' " + string.Format("{0:00.00}", latSecPart) + "'' " + latDir;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (!isSelected)
            {

                isSelected = true;
                Seccionado();
            }
            else
            {
                isSelected = false;
                NoSeccionado();
            }

            OnRaiseCustomEvent(this.m_flightRadarData);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Seccionado();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isSelected) return;

            NoSeccionado();
        }
    }
}
