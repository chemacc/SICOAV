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

namespace SICOAV_A.Vistas
{
    /// <summary>
    /// Lógica de interacción para IB_CTRL_VISTA_Reloj.xaml
    /// </summary>
    public partial class IB_CTRL_VISTA_Reloj : Window
    {
        static private Application loadedApp;

        public IB_CTRL_VISTA_Reloj()
        {
            InitializeComponent();
            ActivarReloj();
        }

        private void Grid_Ventana_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ActivarReloj()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
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
                       this.txt_hora.Text = DateTime.Now.ToLongTimeString();
                   }
                   ));
            }
        }
    }
}
