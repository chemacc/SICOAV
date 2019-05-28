using SICOAV_A.Singletons;
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
    /// Lógica de interacción para IB_CTRL_CNF_Colisiones.xaml
    /// </summary>
    public partial class IB_CTRL_CNF_Colisiones : UserControl
    {
        public IB_CTRL_CNF_Colisiones()
        {
            InitializeComponent();

            this.txt_altura.Text = IB_SGLT_Configuracion.Instance.GetAlturaMinimaColicion().ToString();
            this.txt_entre.Text = IB_SGLT_Configuracion.Instance.GetDistanciaEntreAvionesMinimaColicion().ToString();
            this.txt_distancia.Text = IB_SGLT_Configuracion.Instance.GetAlturaMinimaColicion().ToString();
        }

        private void cmd_plus_entre_Click(object sender, RoutedEventArgs e)
        {
            var altura = IB_SGLT_Configuracion.Instance.GetDistanciaEntreAvionesMinimaColicion();

            altura = altura + 100;

            if(altura >= 100000)
            {
                altura = 100000;
            }

            IB_SGLT_Configuracion.Instance.SetDistanciaEntreAvionesMinimaColicion(altura);

            this.txt_entre.Text = altura.ToString();

        }

        private void cmd_min_entre_Click(object sender, RoutedEventArgs e)
        {
            var altura = IB_SGLT_Configuracion.Instance.GetDistanciaEntreAvionesMinimaColicion();

            altura = altura - 100;

            if (altura <= 0)
            {
                altura = 0;
            }

            IB_SGLT_Configuracion.Instance.SetDistanciaEntreAvionesMinimaColicion(altura);

            this.txt_entre.Text = altura.ToString();
        }

        private void cmd_plus_altura_Click(object sender, RoutedEventArgs e)
        {
            var altura = IB_SGLT_Configuracion.Instance.GetAlturaMinimaColicion();

            altura = altura + 100;

            if (altura >=100000)
            {
                altura = 100000;
            }

            IB_SGLT_Configuracion.Instance.SetAlturaMinimaColicion(altura);

            this.txt_altura.Text = altura.ToString();
        }

        private void cmd_min_altura_Click(object sender, RoutedEventArgs e)
        {
            var altura = IB_SGLT_Configuracion.Instance.GetAlturaMinimaColicion();

            altura = altura - 100;

            if (altura <= 0)
            {
                altura = 0;
            }

            IB_SGLT_Configuracion.Instance.SetAlturaMinimaColicion(altura);

            this.txt_altura.Text = altura.ToString();
        }

        private void cmd_plus_distancia_Click(object sender, RoutedEventArgs e)
        {
            var altura = IB_SGLT_Configuracion.Instance.GetDistanciaMinimaColicion();

            altura = altura + 1;

            if (altura >= 100000)
            {
                altura = 100000;
            }

            IB_SGLT_Configuracion.Instance.SetDistanciaMinimaColicion(altura);

            this.txt_distancia.Text = altura.ToString();
        }

        private void cmd_min_distancia_Click(object sender, RoutedEventArgs e)
        {
            var altura = IB_SGLT_Configuracion.Instance.GetDistanciaMinimaColicion();

            altura = altura - 1;

            if (altura <= 0)
            {
                altura = 0;
            }

            IB_SGLT_Configuracion.Instance.SetDistanciaMinimaColicion(altura);

            this.txt_distancia.Text = altura.ToString();
        }
    }
}
