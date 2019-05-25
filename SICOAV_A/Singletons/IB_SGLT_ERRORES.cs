using SICOAV_A.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SICOAV_A.Singletons
{
    public sealed class IB_SGLT_ERRORES
    {
        private static IB_SGLT_ERRORES instance = null;
        private static readonly object padlock = new object();
        private static MainWindow win;

        private IB_SGLT_ERRORES() { }

        public static IB_SGLT_ERRORES Instance
        {

            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new IB_SGLT_ERRORES();

                    return instance;
                }
            }
        }

        public static void Carga_win(MainWindow p_win)
        {
            win = p_win;
        }

        public static void MuestraError_API(MainWindow p_win, string API, string txt_Error)
        {
            if (p_win == null) p_win = win;

            foreach(var elemento in p_win.panel_error.Children)
            {
                if(elemento is IB_CTRL_ALERTA )
                {
                    IB_CTRL_ALERTA aux = (IB_CTRL_ALERTA)elemento;

                    aux.txt_error.Text = txt_Error;
                    aux.txt_titulo.Text = API;

                    aux.Visibility = Visibility.Visible;

                }
            }
        }
    }
}
