using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOAV_A.Singletons
{

    public sealed class IB_SGLT_Configuracion
    {
        private static volatile IB_SGLT_Configuracion instance = null;
        private static readonly object padlock = new object();

        private static double m_distanciamincolision = 100;
        private static int m_distanciaminentreaviones = 46500;
        private static int m_alturaminimacolision = 9600;
        private static bool m_visualiza_datos_avion = true;

        private static string m_pais = "NO_PAIS";

        private IB_SGLT_Configuracion() { }

        public static IB_SGLT_Configuracion Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new IB_SGLT_Configuracion();
                    }
                }

                return instance;
            }
        }

        #region Colisión 

        internal void SetDistanciaMinimaColicion(double value)
        {

            m_distanciamincolision = value;
        }

        internal double GetDistanciaMinimaColicion()
        {
            
            return m_distanciamincolision;
        }

        internal void SetDistanciaEntreAvionesMinimaColicion(int value)
        {

            m_distanciaminentreaviones = value;
        }

        internal int GetDistanciaEntreAvionesMinimaColicion()
        {
            return m_distanciaminentreaviones;
        }

        internal void SetAlturaMinimaColicion(int value)
        {

            m_alturaminimacolision = value;
        }

        internal int GetAlturaMinimaColicion()
        {
            
            return m_alturaminimacolision;
        }

        #endregion Colisión

        #region - MAPA -

        internal bool GetVisualizarDatosAvion()
        {
            return m_visualiza_datos_avion;
        }

        internal void SetVisualizarDatosAvion(bool p_value)
        {
            m_visualiza_datos_avion = p_value;
        }

        #endregion
    }

    
}
