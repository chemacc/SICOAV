using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOAV_A.Modelos
{
    public class IB_MOD_COLISION
    {
        public string m_callsing_V1;
        public string m_callsing_V2;

        public string m_distanciaV1;
        public string m_distanciaV2;

        public DateTime m_TimeV1;
        public DateTime m_TimeV2;

        public PointLatLng m_PointInterserccion;
        public PointLatLng m_PointV1;
        public PointLatLng m_PointV2;

        public IB_MOD_COLISION()
        {

        }
    }
}
