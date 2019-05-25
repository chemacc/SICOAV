using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SICOAV_A.Marcas
{
    /// <summary>
    /// Lógica de interacción para IB_CTRL_COLISION.xaml
    /// </summary>
    public partial class IB_CTRL_COLISION : UserControl
    {
        static private Application loadedApp;
        static bool activo;
        public IB_CTRL_COLISION()
        {
            InitializeComponent();

        }

        public IB_CTRL_COLISION(Modelos.IB_MOD_COLISION p_colision)
        {
            InitializeComponent();
            
            string value = p_colision.m_distanciaV1.Replace("km", "");

            if (double.Parse(value) < 20)
                ActivarReloj();
        }


        private void ActivarReloj()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 500;
            aTimer.Enabled = true;


        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (Application.Current != null)
            {
                loadedApp = Application.Current;

                loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                   new Action(delegate ()
                   {
                       if (activo)
                       {
                           this.Visibility = Visibility.Visible;
                           activo = false;
                       }
                       else
                       {
                           this.Visibility = Visibility.Hidden;
                           activo = true;
                       }
                           
                   }
                   ));
            }
        }

        ~IB_CTRL_COLISION()
        {

        }
    }
        
}
    
