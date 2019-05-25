using GMap.NET;
using GMap.NET.MapProviders;
using MODELO.AEROPUERTO;
using MODELO.PLANDEVUELO;
using MODELO.TRACK;
using Newtonsoft.Json;
using SICOAV_A.Modelos;
using SICOAV_A.Recursos;
using SICOAV_A.Serializacion;
using SICOAV_A.Singletons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


/// <summary>
///  https://www.icao.int/safety/istars/pages/api-data-service.aspx
/// </summary>
namespace SICOAV_A.API
{
    public sealed class API_WEB
    {

        private static API_WEB instance = null;
        private static readonly object padlock = new object();

        private API_WEB() { }

        public static API_WEB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new API_WEB();

                    return instance;
                }
            }
        }

        // https://v4p4sz5ijk.execute-api.us-east-1.amazonaws.com/anbdata/airspaces/zones/fir-list?api_key=d3e78d90-4de4-11e8-bb63-21ad2a999159&firs=LECM&format=json

        public static List<PointLatLng> GetEspaciosAereos(string ICAOCODE_FIR)
        {
            List<PointLatLng> aux = new List<PointLatLng>();
            string cadena = "";
            bool existe = false;
            try
            {
                IB_SERIALIZA_FICHEROS seri = new IB_SERIALIZA_FICHEROS();

                if(seri.existeSector(ICAOCODE_FIR))
                {
                    cadena = seri.CargaSector(seri.ficheroSector(ICAOCODE_FIR));
                    existe = true;
                }
                else
                {
                    cadena = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, "https://v4p4sz5ijk.execute-api.us-east-1.amazonaws.com/anbdata/airspaces/zones/fir-list?api_key=d3e78d90-4de4-11e8-bb63-21ad2a999159&firs={0}&format=json", ICAOCODE_FIR));
                    existe = false;
                }

               

                List<IbModApiRegion> A = IbModApiRegion.FromJson(cadena).ToList();
                if(!existe)
                {
                    seri.EscribeSector(seri.ficheroSector(ICAOCODE_FIR), A);
                }


                foreach (IbModApiRegion regiones in A)
                {
                    foreach (var it in A[0].Geometry.Coordinates[0])
                    {
                        double Lon = it[0];
                        double Lat = it[1];

                        PointLatLng Punto = new PointLatLng(Lat, Lon);
                        aux.Add(Punto);
                    }
                }
               
            }
            catch(Exception ex)
            {
                IB_SGLT_ERRORES.MuestraError_API(null, "GetEspaciosAereos", ex.Message);
            }

            return aux;
        }

        public static List<IB_MOD_PLANDEVUELO> GetPlanesDeVuelo(string iata_ORIGEN, string iata_DESTINO)
        {

            List<IB_MOD_PLANDEVUELO> aux = new List<IB_MOD_PLANDEVUELO>();
            try
            {
                IB_SERIALIZA_FICHEROS seri = new IB_SERIALIZA_FICHEROS();

                if (seri.existePlanDeVuelo(iata_ORIGEN, iata_DESTINO))
                {
                    return IB_MOD_PLANDEVUELO.FromJson(seri.CargaPlandeVuelo(seri.ficheroPlanDeVuelo(iata_ORIGEN, iata_DESTINO))).ToList(); 
                }
                else
                {
                    string cadenaICAO = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, "https://api.flightradar24.com/common/v1/airport.json?code={0}&page=1&limit=1&token=1", iata_ORIGEN));

                    var ibModAeropuerto_origen = IbModAeropuerto.FromJson(cadenaICAO);

                    var ini = cadenaICAO.IndexOf("icao");
                    var strICAO_ORIGEN = cadenaICAO.Substring(ini + 7, 4);

                    string cadenaICAO2 = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, "https://api.flightradar24.com/common/v1/airport.json?code={0}&page=1&limit=1&token=1", iata_DESTINO));

                    var ibModAeropuerto_destino = IbModAeropuerto.FromJson(cadenaICAO2);

                    var ini2 = cadenaICAO2.IndexOf("icao");
                    var strICAO_DESTINO = cadenaICAO2.Substring(ini2 + 7, 4);

                    string cadena = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, "https://api.flightplandatabase.com/search/plans?fromICAO={0}&to={1}&limit=15", strICAO_ORIGEN, strICAO_DESTINO));
                    var Values = cadena.Split(new string[] { "}," }, StringSplitOptions.RemoveEmptyEntries);

                    cadena = cadena.Replace("null", "0.0");

                    var PLANES = IB_MOD_PLANDEVUELO.FromJson(cadena);


                    seri.EscribeLocalAeropuerto(seri.ficheroLocalAeropuerto(iata_ORIGEN), ibModAeropuerto_origen);
                    seri.EscribeLocalAeropuerto(seri.ficheroLocalAeropuerto(iata_DESTINO), ibModAeropuerto_origen);

                    seri.EscribePlandeVuelo(seri.ficheroPlanDeVuelo(iata_ORIGEN, iata_DESTINO), PLANES.ToList());

                    return PLANES.ToList();
                }

                
            }
            catch(Exception ex)
            {
                IB_SGLT_ERRORES.MuestraError_API(null, "GetPlanesDeVuelo", ex.Message);
                return aux;
            }
            
        }

        public static List<IB_MOD_NODORUTA> GetRutaPlandeVuelo(string idplan)
        {
            List<IB_MOD_NODORUTA> aux = new List<IB_MOD_NODORUTA>();

            string cadenaICAO = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, "https://api.flightplandatabase.com/plan/{0}", idplan));
            var Values = cadenaICAO.Split(new string[] { "\"nodes\":[{" }, StringSplitOptions.RemoveEmptyEntries);
            var rutas = Values[1].Split(new string[] { "}," }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string item in rutas)
            {
                IB_MOD_NODORUTA node = new IB_MOD_NODORUTA(item);
                aux.Add(node);
            }
            return aux;
        }

        public static List<SICOAV_A.Modelos.QuickType.IbModAeropuerto> GetAeropuertos(string ISO3state)
        {
            
            try
            {
                IB_SERIALIZA_FICHEROS seri = new IB_SERIALIZA_FICHEROS();
                bool exite = false;
                string cadena = "";

                if (seri.existeLocalAeropuerto(ISO3state))
                {
                    cadena = seri.CargaLocalAeropuerto(seri.ficheroLocalAeropuerto(ISO3state));
                    exite = true;
                }
                else
                {
                    cadena = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, " https://v4p4sz5ijk.execute-api.us-east-1.amazonaws.com/anbdata/airports/weather/current-conditions-list?api_key=d3e78d90-4de4-11e8-bb63-21ad2a999159&airports=&states={0},{1}&format=json", ISO3state.ToLower(), ISO3state.ToUpper()));
                    exite = false;
                }

                var Datos =  SICOAV_A.Modelos.QuickType.IbModAeropuerto.FromJson(cadena).ToList();

                if(!exite)
                {
                    seri.EscribeLocalAeropuerto(seri.ficheroLocalAeropuerto(ISO3state), Datos);
                }

                return Datos;
            }
            catch (Exception ex)
            {
                IB_SGLT_ERRORES.MuestraError_API(null, "GetAeropuertos", ex.Message);
                
            }

            return null;
           
        }

        public static List<IB_MOD_PUNTORUTA> GetTrackAvion(string ICAO4)
        {
            List<IB_MOD_PUNTORUTA> aux = new List<IB_MOD_PUNTORUTA>();

            return aux;

            string cadena = GetFlightRadarContentUsingHttp(string.Format(CultureInfo.InvariantCulture, " https://opensky-network.org/api/tracks/all?icao24={0}&time=0", ICAO4.ToLower()));
            var ibModTrack = IbModTrack.FromJson(cadena);
            var cadenas = cadena.Split(new string[] { "path" }, StringSplitOptions.RemoveEmptyEntries); // 
            cadenas[1] = cadenas[1].Trim('[', ']', '"');
            var Puntos = cadenas[1].Split(new string[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string token in Puntos)
                aux.Add(new IB_MOD_PUNTORUTA(token));


            IB_SGLT_LOG_B.WriteLine("[API_WEB.cs] -> GetTrackAvion( " + ICAO4 + " ) = " + cadena);

            return aux;
        }


        private static string GetFlightRadarContentUsingHttp(string url)
        {
            string ret = string.Empty;

            try
            {
                

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.UserAgent = GMapProvider.UserAgent;
                request.Timeout = GMapProvider.TimeoutMs;
                request.ReadWriteTimeout = GMapProvider.TimeoutMs * 6;
                request.Accept = "*/*";
                request.Referer = "http://www.flightradar24.com/";
                request.KeepAlive = true;
                //request.Headers.Add("Cookie", string.Format(System.Globalization.CultureInfo.InvariantCulture, "map_lat={0}; map_lon={1}; map_zoom={2}; " + (!string.IsNullOrEmpty(sid) ? "PHPSESSID=" + sid + ";" : string.Empty) + "__utma=109878426.303091014.1316587318.1316587318.1316587318.1; __utmb=109878426.2.10.1316587318; __utmz=109878426.1316587318.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)", p.Lat, p.Lng, zoom));

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {


                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader read = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            var tmp = read.ReadToEnd();
                            //if(!string.IsNullOrEmpty(sid))
                            {
                                ret = tmp;
                            }
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                // ALARMA VENTANA.
                var Error = ex.Message;
            }
            return ret;
        }
    }
}
