using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SICOAV_A.Singletons
{

    public class IB_SGLT_CARTOGRAFIA
    {

        #region Singleton

        private static readonly Lazy<IB_SGLT_CARTOGRAFIA> Lazy = new Lazy<IB_SGLT_CARTOGRAFIA>(() => new IB_SGLT_CARTOGRAFIA());

        public static IB_SGLT_CARTOGRAFIA Instance { get { return Lazy.Value; } }

        internal IB_SGLT_CARTOGRAFIA()
        {
            MapaCosta = "costa";
            MapaFileExtension = ".gps";
        }

        #endregion

        public StreamWriter Writer { get; set; }

        public string MapasPath
        {
            get { return _LogPath ?? (_LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\MAPAS"); }
            set { _LogPath = value; }
        }

        private string _LogPath;

        public string MapaCosta { get; set; }

        public string MapaFileExtension { get; set; }

        public string FileMapaCosta { get { return MapaCosta + MapaFileExtension; } }

        public string MapaCostaFullPath { get { return Path.Combine(MapasPath, FileMapaCosta); } }

        public bool MapaCostaExists { get { return File.Exists(MapaCostaFullPath); } }

        public  List<GMapRouteMapa> GetMapaCosta()
        {
            List<PointLatLng> aux = new List<PointLatLng>();
            List<GMapRouteMapa> route = new List<GMapRouteMapa>();

            if (MapaCostaExists)
            {
                StreamReader sr = new StreamReader(MapaCostaFullPath);
                string line = sr.ReadLine();
                CultureInfo myCI = new CultureInfo("en-US", false);
                //Continue to read until you reach end of file -6.99111111111111,39.725
                while (line != null)
                {
                    if(line.Length != 0)
                    {
                        var Coordenadas = line.Split(',');
                        if(Coordenadas.Length == 2)
                        {
                            PointLatLng A = new PointLatLng(double.Parse(Coordenadas[1].Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)), 
                                                            double.Parse(Coordenadas[0].Replace(",", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)));

                            aux.Add(A);
                        }
                    }
                    else if (line.Length == 0)
                    {
                        if (aux.Count > 1)
                        {
                            GMapRouteMapa Polilinea = new GMapRouteMapa(aux);
                            route.Add(Polilinea);
                            aux = new List<PointLatLng>();
                        }
                    }


                    line = sr.ReadLine();
                }

                sr.Close();

            }

            return route;
        }
        

       
    }

    public class IB_SGLT_LOG_A
    {

        #region Singleton

        private static readonly Lazy<IB_SGLT_LOG_A> Lazy = new Lazy<IB_SGLT_LOG_A>(() => new IB_SGLT_LOG_A());

        public static IB_SGLT_LOG_A Instance { get { return Lazy.Value; } }

        internal IB_SGLT_LOG_A()
        {
            LogFileName = "LOG_TRACK_" + DateTime.Now.ToShortDateString().Replace("/","_");
            LogFileExtension = ".log";
        }

        #endregion

        public StreamWriter Writer { get; set; }

        public string LogPath
        {
            get { return _LogPath ?? (_LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\LOG") ; }
            set { _LogPath = value; }
        }

        private string _LogPath;

        public string LogFileName { get; set; }

        public string LogFileExtension { get; set; }

        public string LogFile { get { return LogFileName + LogFileExtension; } }

        public string LogFullPath { get { return Path.Combine(LogPath, LogFile); } }

        public bool LogExists { get { return File.Exists(LogFullPath); } }

        public void WriteLineToLog(string inLogMessage)
        {
            WriteToLog(inLogMessage + Environment.NewLine);
        }

        public void WriteToLog(string inLogMessage)
        {
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            if (Writer == null)
            {
                Writer = new StreamWriter(LogFullPath, true);
            }

            Writer.Write(inLogMessage);
            Writer.Flush();
        }

        public static void WriteLine(string inLogMessage)
        {
            Instance.WriteLineToLog(inLogMessage);
        }

        public static void Write(string inLogMessage)
        {
            Instance.WriteToLog(inLogMessage);
        }
    }

    public class IB_SGLT_LOG_B
    {

        #region Singleton

        private static readonly Lazy<IB_SGLT_LOG_B> Lazy = new Lazy<IB_SGLT_LOG_B>(() => new IB_SGLT_LOG_B());

        public static IB_SGLT_LOG_B Instance { get { return Lazy.Value; } }

        internal IB_SGLT_LOG_B()
        {
            LogFileName = "LOG_SICOAV_" + DateTime.Now.ToShortDateString().Replace("/", "_");
            LogFileExtension = ".log";
            _LogPathLineas = 0;
        }

        #endregion

        public StreamWriter Writer { get; set; }

        public string LogPath
        {
            get { return _LogPath ?? (_LogPath = AppDomain.CurrentDomain.BaseDirectory + "\\LOG"); }
            set { _LogPath = value; }
        }

        private int _LogPathLineas;

        static private string _LogPath;
        private int cnt;

        public string LogFileName { get; set; }

        public string LogFileExtension { get; set; }

        public string LogFile { get { return LogFileName + LogFileExtension; } }

        public string LogFullPath { get { return Path.Combine(LogPath, LogFile); } }

        public bool LogExists { get { return File.Exists(LogFullPath); } }

        public void WriteLineToLog(string inLogMessage)
        {
            WriteToLog(inLogMessage + Environment.NewLine);
        }

        public void WriteToLog(string inLogMessage)
        {
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
            if (Writer == null)
            {
                Writer = new StreamWriter(LogFullPath, true);
            }

            Writer.Write(inLogMessage);
            Writer.Flush();

            _LogPathLineas++;

            if(_LogPathLineas > 10000)
            {
                _LogPathLineas = 0;
                LogFileName = LogFileName + (cnt++).ToString();
            }
        }

        private static string FechaLog ()
        {
            return DateTime.Now.ToLongTimeString();
        }
        public static void WriteLine(string inLogMessage)
        {
            Instance.WriteLineToLog(inLogMessage);
        }

        public static void Write(string inLogMessage)
        {
            Instance.WriteToLog(inLogMessage);
        }
    }
}
