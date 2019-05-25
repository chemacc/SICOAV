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

namespace SICOAV_A.Vistas
{
    /// <summary>
    /// Lógica de interacción para IB_CTRL_VISTA_Cerrar.xaml
    /// </summary>
    public partial class IB_CTRL_VISTA_Cerrar : UserControl
    {

        public IB_CTRL_VISTA_Cerrar()
        {
            InitializeComponent();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = null;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            this.Background = (Brush)bc.ConvertFrom("#FF686868");
        }
    }
}
