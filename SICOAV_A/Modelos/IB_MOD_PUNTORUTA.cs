using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOAV_A.Modelos
{
    public class IB_MOD_PUNTORUTA
    {
        public DateTime hora;
        public double Latitud;
        public double Longitud;
        public double Altitud;
        public double Rumbo;


        public IB_MOD_PUNTORUTA(string token)
        {
            var elementos = token.Split(',');
            int index = 0;

            foreach(string elmento in elementos)
            {
                switch(index)
                {
                    case 0: hora = UnixTimeStampToDateTime(double.Parse(elmento.Replace('[',' ').Replace(':',' ')));
                        break;
                    case 1:
                        Latitud = double.Parse(elmento);//.Replace(".",","));
                        break;
                    case 2:
                        Longitud = double.Parse(elmento);//.Replace(".", ","));
                        break;
                    case 3:
                        Altitud = double.Parse(elmento);//.Replace(".", ","));
                        break;
                    case 4:
                        Rumbo = double.Parse(elmento);//.Replace(".", ","));
                        break;
                  

                }

                index++;
            }

        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
             
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
