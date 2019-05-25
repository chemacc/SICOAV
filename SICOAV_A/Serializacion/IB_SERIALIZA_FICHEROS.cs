
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using SICOAV_A.Singletons;
using MODELO.PLANDEVUELO;
using Newtonsoft.Json;
using SICOAV_A.Modelos;

namespace SICOAV_A.Serializacion
{
    public class IB_SERIALIZA_FICHEROS : IDisposable
    {

        string m_PathSerielizer;

        private string PathSerielizer
        {
            get { return m_PathSerielizer; }
            set { m_PathSerielizer = value; }
        }

        private string LogPath_PlaneVuelo
        {
            get { return _LogPath_PlaneVuelo ?? (_LogPath_PlaneVuelo = AppDomain.CurrentDomain.BaseDirectory + "\\PLANES_VUELO"); }
            set { _LogPath_PlaneVuelo = value; }
        }

        private string _LogPath_PlaneVuelo;

        private string LogPath_Sectores
        {
            get { return _LogPath_Sectores ?? (_LogPath_Sectores = AppDomain.CurrentDomain.BaseDirectory + "SECTORES"); }
            set { _LogPath_Sectores = value; }
        }

        private string _LogPath_Sectores;


        private string LogPath_LocalAeropuerto
        {
            get { return _LogPath_LocalAeropuerto ?? (_LogPath_LocalAeropuerto = AppDomain.CurrentDomain.BaseDirectory + "LOCAL"); }
            set { _LogPath_LocalAeropuerto = value; }
        }

        private string _LogPath_LocalAeropuerto;

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                var Types = serializableObject.GetType();
                XmlSerializer serializer = new XmlSerializer(Types);
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                IB_SGLT_ERRORES.MuestraError_API(null, "IB_SERIALIZA_FICHEROS", ex.Message.ToString());
                
            }
        }



        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                IB_SGLT_ERRORES.MuestraError_API(null, "IB_SERIALIZA_FICHEROS", ex.Message.ToString());
            }

            return objectOut;
        }

        #region PLANES DE VUELO

        internal bool existePlanDeVuelo(string iata_A, string iata_B)
        {
            return File.Exists(ficheroPlanDeVuelo(iata_A, iata_B));
        }

        internal string ficheroPlanDeVuelo(string iata_A, string iata_B)
        {
            int index = string.Compare(iata_A, iata_B);

            if(index == -1)
            {
                string aux = iata_B;
                iata_B = iata_A;
                iata_A = aux;
            } else if (index == 1)
            {
                string aux = iata_A;
                iata_A = iata_B;
                iata_B = aux;
            }

            string Archivo = "PLAN_DE_VUELO_" + iata_A + "_" + iata_B;

            return (LogPath_PlaneVuelo +"\\"+ Archivo + ".json");
        }

        internal void EscribePlandeVuelo(string Archivo, List<IB_MOD_PLANDEVUELO> lista)
        {
            if (!Directory.Exists(LogPath_PlaneVuelo))
            {
                Directory.CreateDirectory(LogPath_PlaneVuelo);
            }

            JsonSerializer serializer = new JsonSerializer();
            

            using (StreamWriter sw = new StreamWriter(Archivo))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, lista);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

           // this.SerializeObject< List<IB_MOD_PLANDEVUELO>>(lista, Archivo);
        }

        internal string CargaPlandeVuelo(string Archivo)
        {
            if (!Directory.Exists(LogPath_PlaneVuelo))
            {
                Directory.CreateDirectory(LogPath_PlaneVuelo);
            }

            return System.IO.File.ReadAllText(Archivo);  
        }

        #endregion PLANES DE VUELO


        #region SECTORES 

        internal bool existeSector(string FIR)
        {
            return File.Exists(ficheroSector(FIR));
        }

        internal string ficheroSector(string FIR)
        {

            string Archivo = FIR;

            return (LogPath_Sectores + "\\" + Archivo + ".json");
        }

        internal void EscribeSector(string Archivo, List<IbModApiRegion> lista)
        {
            if (!Directory.Exists(LogPath_Sectores))
            {
                Directory.CreateDirectory(LogPath_Sectores);
            }

            JsonSerializer serializer = new JsonSerializer();


            using (StreamWriter sw = new StreamWriter(Archivo))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, lista);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

            // this.SerializeObject< List<IB_MOD_PLANDEVUELO>>(lista, Archivo);
        }

        internal string CargaSector(string Archivo)
        {
            if (!Directory.Exists(LogPath_PlaneVuelo))
            {
                Directory.CreateDirectory(LogPath_PlaneVuelo);
            }

            return System.IO.File.ReadAllText(Archivo);
        }

        #endregion SECTORES.

        #region LOCAL_AEROPUERTOS

        internal bool existeLocalAeropuerto(string FIR)
        {
            return File.Exists(ficheroLocalAeropuerto(FIR));
        }

        internal string ficheroLocalAeropuerto(string FIR)
        {

            string Archivo = FIR;

            return (LogPath_LocalAeropuerto + "\\" + Archivo + ".json");
        }

        internal void EscribeLocalAeropuerto(string Archivo, List<SICOAV_A.Modelos.QuickType.IbModAeropuerto> lista)
        {
            if (!Directory.Exists(LogPath_LocalAeropuerto))
            {
                Directory.CreateDirectory(LogPath_LocalAeropuerto);
            }

            JsonSerializer serializer = new JsonSerializer();


            using (StreamWriter sw = new StreamWriter(Archivo))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, lista);
                 
            }
        }

        internal void EscribeLocalAeropuerto(string Archivo, MODELO.AEROPUERTO.IbModAeropuerto lista)
        {
            if (!Directory.Exists(LogPath_LocalAeropuerto))
            {
                Directory.CreateDirectory(LogPath_LocalAeropuerto);
            }

            JsonSerializer serializer = new JsonSerializer();


            using (StreamWriter sw = new StreamWriter(Archivo))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, lista);

            }
        }

        internal string CargaLocalAeropuerto(string Archivo)
        {
            if (!Directory.Exists(LogPath_LocalAeropuerto))
            {
                Directory.CreateDirectory(LogPath_LocalAeropuerto);
            }

            return System.IO.File.ReadAllText(Archivo);
        }

        #endregion LOCAL_AEROPUERTOS

        public void Dispose()
        {

        }
    }
}

