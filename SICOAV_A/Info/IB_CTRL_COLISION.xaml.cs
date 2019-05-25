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
    /// Lógica de interacción para IB_CTRL_COLISION.xaml
    /// </summary>
    public partial class IB_CTRL_COLISION : UserControl
    {
        IB_MOD_COLISION m_Colision;



        public IB_CTRL_COLISION()
        {
            InitializeComponent();
        }

        public IB_CTRL_COLISION(IB_MOD_COLISION p_colision)
        {
            InitializeComponent();

            m_Colision = p_colision;

            ActualControl();

        }

        private void ActualControl()
        {
            if(m_Colision != null)
            {
                this.txt_v1.Text = m_Colision.m_callsing_V1;
                this.txt_V2.Text = m_Colision.m_callsing_V2;

                this.txt_TiempoV1.Text = m_Colision.m_TimeV1.ToLongTimeString();
                this.txt_TiempoV2.Text = m_Colision.m_TimeV2.ToLongTimeString();

                this.txt_DistanciaV1.Text = m_Colision.m_distanciaV1;
                this.txt_DistanciaV2.Text = m_Colision.m_distanciaV2;

            }
        }
    }
}
