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
    /// Lógica de interacción para IB_CTRL_PanelAeropuerto.xaml
    /// </summary>
    public partial class IB_CTRL_PanelAeropuerto : UserControl
    {
        private SICOAV_A.Modelos.QuickType.IbModAeropuerto m_Aeropuerto;

        public SICOAV_A.Modelos.QuickType.IbModAeropuerto Aeropuerto
        {
            get
            {
                return m_Aeropuerto;
            }
            set
            {
                 

                m_Aeropuerto = value;

                this.txt_lat.Text = Latitud_Cadena(value.Latitude);
                this.txt_lon.Text = Longitud_Cadena(value.Longitude);

                this.txt_airport.Text = value.Airport;
                this.txt_AirportName.Text = value.AirportName;

            }
        }

        public IB_CTRL_PanelAeropuerto()
        {
            InitializeComponent();
        }

        public IB_CTRL_PanelAeropuerto(SICOAV_A.Modelos.QuickType.IbModAeropuerto aeropuerto)
        {
            InitializeComponent();
            Aeropuerto = aeropuerto;
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
    }
}
