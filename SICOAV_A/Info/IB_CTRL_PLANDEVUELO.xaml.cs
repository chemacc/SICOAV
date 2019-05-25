using GMap.NET;
using GMap.NET.WindowsPresentation;
using MODELO.PLANDEVUELO;
using SICOAV_A.API;
using SICOAV_A.Marcas;
using SICOAV_A.Modelos;
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

namespace SICOAV_A.Info
{
    /// <summary>
    /// Lógica de interacción para IB_CTRL_PLANDEVUELO.xaml
    /// </summary>
    public partial class IB_CTRL_PLANDEVUELO : UserControl
    {
        IB_MOD_PLANDEVUELO m_modelo;
        Map m_mainMap;
        GMapRoutePlanVuelo m_GMapRoute = null;

        public IB_CTRL_PLANDEVUELO()
        {
            InitializeComponent();
        }

        public IB_CTRL_PLANDEVUELO(IB_MOD_PLANDEVUELO p_modelo, Map mainMap)
        {
            InitializeComponent();

            this.txt_distancia.Text = p_modelo.Distance.ToString();
            this.txt_ICAO_Destino.Text = p_modelo.ToIcao;
            this.txt_ICAO_ORIGEN.Text = p_modelo.FromIcao;
            this.txt_nombreOrigen.Text = p_modelo.FromName;
            this.txt_nombre_destino.Text = p_modelo.ToName;

            m_modelo = p_modelo;
            m_mainMap = mainMap;
        }

        private void imgaRuta_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // dibujar ruta.
            List<PointLatLng> points = new List<PointLatLng>();

            foreach(IB_MOD_NODORUTA node in API_WEB.GetRutaPlandeVuelo(m_modelo.Id.ToString()))
            {
                PointLatLng point = new PointLatLng(node.latitud, node.longitud);
                points.Add(point);

                GMapMarker marker = new GMapMarker(point);
                {
                    marker.Shape = new IB_CTRL_MARCA_RUTA(node.ident);
                    marker.Offset = new System.Windows.Point(-10, -10);
                    marker.ZIndex = int.MaxValue;

                    m_mainMap.Markers.Add(marker);
                }
            }

            this.m_GMapRoute = new GMapRoutePlanVuelo(points);
            this.m_GMapRoute.RegenerateShape(m_mainMap);

            m_mainMap.Markers.Add(this.m_GMapRoute);
        }
    }
}
