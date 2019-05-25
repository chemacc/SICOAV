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
using System.Windows.Shapes;

namespace SICOAV_A.Vistas
{
    /// <summary>
    /// Lógica de interacción para IB_VISTA_Planes_de_vuelo.xaml
    /// </summary>
    public partial class IB_VISTA_Planes_de_vuelo : Window
    {
        public WrapPanel PanelPrincipal;
        private bool m_minimiza;
        public TextBlock TituloVentana;
        public IB_VISTA_Planes_de_vuelo()
        {
            InitializeComponent();

            PanelPrincipal = this.WrapPanel_Principal;
            TituloVentana = this.Titulo_ventana;
        }

        private void Grid_Ventana_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Cmd_cerrar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void cmd_maximiza_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_minimiza = false;
            this.Height = 396.625;
        }

        private void Cmd_minimiza_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!m_minimiza)
            {
                this.Height = this.Grid_Ventana.Height;
                m_minimiza = true;
            }
        }
    }
}
