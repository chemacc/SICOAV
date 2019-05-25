using GMap.NET;
using SICOAV_A.Modelos;
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

namespace SICOAV_A.Info
{
    /// <summary>
    /// Lógica de interacción para IB_CTRL_AEROPUERTO.xaml
    /// </summary>
    public partial class IB_CTRL_AEROPUERTO : UserControl
    {
        SICOAV_A.Modelos.QuickType.IbModAeropuerto m_aereopuerto;
        Recursos.Map m_mainMap;
        PointLatLng m_point;

        public IB_CTRL_AEROPUERTO()
        {
            InitializeComponent();
        }

        public IB_CTRL_AEROPUERTO(Recursos.Map mainMap, SICOAV_A.Modelos.QuickType.IbModAeropuerto p_aereopuerto)
        {
            InitializeComponent();

            this.m_mainMap = mainMap;

            this.m_aereopuerto = p_aereopuerto;
            this.txt_icao.Text = m_aereopuerto.Airport;
            this.ToolTip = m_aereopuerto.AirportName;
            this.m_point = new PointLatLng(p_aereopuerto.Latitude, p_aereopuerto.Longitude);

            this.MouseDown += IB_CTRL_AEROPUERTO_MouseDown;
        }

        private void IB_CTRL_AEROPUERTO_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_mainMap.Position = m_point;
        }
    }
}
