using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOAV_A.Modelos
{
    public class IB_MOD_NODORUTA
    {
        public string tipo;
        public string ident;
        public string nombre;
        public double latitud;
        public double longitud;
        public string altura;
        public string via;

        public IB_MOD_NODORUTA(string node)
        {
            var tokens = node.Split(',');
            int index = 0;

            foreach (string token in tokens)
            {
                var elemento = token.Split(':');
                switch(index)
                {
                    case 0:
                        tipo = elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 1:
                        ident = elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 2:
                        nombre = elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 3:
                        latitud = double.Parse(elemento[1].Replace('"', ' ').Trim().Replace('.','.'));
                        break;
                    case 4:
                        longitud = double.Parse(elemento[1].Replace('"', ' ').Trim().Replace('.', '.'));
                        break;
                    case 5:
                        altura = elemento[1].Replace('"', ' ').Trim();
                        break;
                    case 6:
                        altura = elemento[1].Replace('"', ' ').Trim();
                        break;
                }

                index++;
            }
           
        }
    }
}
