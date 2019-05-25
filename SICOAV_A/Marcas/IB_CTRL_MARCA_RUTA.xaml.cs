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

namespace SICOAV_A.Marcas
{
    /// <summary>
    /// Lógica de interacción para IB_CTRL_MARCA_RUTA.xaml
    /// </summary>
    public partial class IB_CTRL_MARCA_RUTA : UserControl
    {
        public IB_CTRL_MARCA_RUTA()
        {
            InitializeComponent();
        }

        public IB_CTRL_MARCA_RUTA(string texto)
        {
            InitializeComponent();
            this.txt_texto.Text = texto;
        }
    }
}
