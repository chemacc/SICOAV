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
    /// Lógica de interacción para IB_CTRL_ALERTA.xaml
    /// </summary>
    public partial class IB_CTRL_ALERTA : UserControl
    {
        string m_alerta;

        public string Alerta
        {
            set
            {
               this.txt_error.Text = m_alerta = value ;
            }
        }

        public IB_CTRL_ALERTA()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
