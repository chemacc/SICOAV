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
using SICOAV_A.Modelos;

namespace SICOAV_A.Marcas
{
    /// <summary>
    /// Lógica de interacción para CTRL_AEROPUERTO_A.xaml
    /// </summary>
    public partial class CTRL_AEROPUERTO_A : UserControl
    {
        private SICOAV_A.Modelos.QuickType.IbModAeropuerto aereopuerto;

        public CTRL_AEROPUERTO_A()
        {
            InitializeComponent();
        }

        public CTRL_AEROPUERTO_A(WrapPanel panel ,SICOAV_A.Modelos.QuickType.IbModAeropuerto aereopuerto)
        {
            InitializeComponent();

            this.aereopuerto = aereopuerto;

            this.txt_icao.Text = aereopuerto.Airport;
        }

       
    }
}
