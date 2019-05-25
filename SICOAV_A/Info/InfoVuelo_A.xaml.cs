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
    public struct planEvent
    {
        public string origenIATA, destinoIATA;

        public planEvent(string p1, string p2)
        {
            origenIATA = p1;
            destinoIATA = p2;
        }
    }
    /// <summary>
    /// Lógica de interacción para InfoVuelo_A.xaml
    /// </summary>
    public partial class InfoVuelo_A : UserControl
    {
        public event EventHandler<planEvent> RaisePlanEvent;

        FlightRadarData m_DatosVuelo;
        Map m_mainMap;
        public FlightRadarData DatosVuelo
        {
            get { return m_DatosVuelo; }
            set
            {
                this.txt_inidicativo.Text = value.name;
                this.txt_llegada.Text = value.llegada;
                this.txt_numvuelo.Text = value.NVuelo;
                this.txt_radar.Text = value.radar;
                this.txt_radar_Copy.Text = value.ssr;
                this.txt_salida.Text = value.salida;
                this.txt_numvuelo.Text = value.NVuelo;
                m_DatosVuelo = value;
            }
        }

        protected virtual void OnRaiseCustomEvent(planEvent e)
        {
            EventHandler<planEvent> handler = RaisePlanEvent;

            if (handler != null)
            {

                handler(this, e);
            }
        }

        public InfoVuelo_A(FlightRadarData p_DatosVuelo, Map mainMap)
        {
            InitializeComponent();
            this.MouseDown += InfoVuelo_A_MouseDown;
            this.DatosVuelo = p_DatosVuelo;
            m_mainMap = mainMap;
        }

        private void InfoVuelo_A_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void imagen_avion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_mainMap.Position = m_DatosVuelo.point;
        }

        private void img_plandeVuelo_MouseDown(object sender, MouseButtonEventArgs e)
        {

            OnRaiseCustomEvent(new planEvent(m_DatosVuelo.salida, m_DatosVuelo.llegada));
        }
    }
}
