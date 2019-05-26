using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using IB_CTRL_SICOAV.controles;
using SICOAV_A.API;
using SICOAV_A.Controles;
using SICOAV_A.Info;
using SICOAV_A.Marcas;
using SICOAV_A.Modelos;
using SICOAV_A.Recursos;
using SICOAV_A.Singletons;
using SICOAV_A.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

namespace SICOAV_A
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region - Ventanas de informacion 

        IB_VISTA_Planes_de_vuelo m_VentanaPlanesDeVuelo;
        IB_VISTA_Planes_de_vuelo m_VentanaColiciones;
        IB_VISTA_Planes_de_vuelo m_VentanaVuelos;
        IB_VISTA_Planes_de_vuelo m_VentanaAeropuertos;

        IB_CTRL_VISTA_Reloj m_VentanaRelog;

        #endregion - Ventanas de informacion

        GMapPolygon polygonFIR_MADRID;
        GMapPolygon polygonFIR_BARCELONA;
        GMapPolygon polygonFIR_CANARIAS;

        List<GMapRouteMapa> polygonFIR_mapa;

        BackgroundWorker FIR_Worker = new BackgroundWorker();
        BackgroundWorker PLANES_Worker = new BackgroundWorker();

        BackgroundWorker Colision_Worker = new BackgroundWorker();

        //readonly GMapOverlay top = new GMapOverlay();
        //internal readonly GMapOverlay objects = new GMapOverlay("objects");
        //internal readonly GMapOverlay routes = new GMapOverlay("routes");
        //internal readonly GMapOverlay polygons = new GMapOverlay("polygons");
        private ObservableCollection<GMapMarker> objects = new ObservableCollection<GMapMarker>();

        
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;

            CultureInfo ci = new CultureInfo("us-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            DateTime oldDate = new DateTime(2019, 12, 23);
            DateTime newDate = DateTime.Now;

            // Difference in days, hours, and minutes.
            TimeSpan ts = newDate - oldDate;

            // Difference in days.
            int differenceInDays = ts.Days;

            if (differenceInDays > 0) this.Close();

            CargaConfiguracion();

            InicializaEventos();

            InicializaControles();

            InicializaErrores();

            InicializaMapas();

            InicializaBotonera();
           
            
        }

        private void CargaConfiguracion()
        {
            
        }

        private void InicializaBotonera()
        {
            this.wrapBonotonera.Children.Clear();

            IB_CTRL_BTN_MenuPP PaisBoton = new IB_CTRL_BTN_MenuPP(tipoBotonPP.SELECT_PAIS);
            PaisBoton.MouseDown += PaisBoton_MouseDown;
            this.wrapBonotonera.Children.Add(PaisBoton);

            IB_CTRL_BTN_MenuPP AvionesBoton = new IB_CTRL_BTN_MenuPP(tipoBotonPP.AVIONES);
            AvionesBoton.MouseDown += PaisBoton_MouseDown;
            this.wrapBonotonera.Children.Add(AvionesBoton);

            IB_CTRL_BTN_MenuPP AeropuertoBoton = new IB_CTRL_BTN_MenuPP(tipoBotonPP.AIRPORT);
            AeropuertoBoton.MouseDown += PaisBoton_MouseDown;
            this.wrapBonotonera.Children.Add(AeropuertoBoton);

            IB_CTRL_BTN_MenuPP ColisionesBoton = new IB_CTRL_BTN_MenuPP(tipoBotonPP.COLISIONES);
            ColisionesBoton.MouseDown += PaisBoton_MouseDown;
            this.wrapBonotonera.Children.Add(ColisionesBoton);

            IB_CTRL_BTN_MenuPP PlanesBoton = new IB_CTRL_BTN_MenuPP(tipoBotonPP.PLAN_VUENO);
            PlanesBoton.MouseDown += PaisBoton_MouseDown;
            this.wrapBonotonera.Children.Add(PlanesBoton);


            IB_CTRL_BTN_MenuPP ConfigBoton = new IB_CTRL_BTN_MenuPP(tipoBotonPP.CONFIGURACION);
            ConfigBoton.MouseDown += PaisBoton_MouseDown;
            this.wrapBonotonera.Children.Add(ConfigBoton);

            this.TipoInfo = tipoBotonPP.NINGUNA;

            this.wrapPanel_vuelos.Visibility = Visibility.Collapsed;
            this.wrapPanel_aeropuertos.Visibility = Visibility.Collapsed;
            this.wrapPanel_configuracion.Visibility = Visibility.Collapsed;

            this.wrapPanel_configuracion.Children.Add(new IB_CTRL_CNF_Colisiones());

        }

        private void PaisBoton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.wrapPanel_vuelos.Visibility = Visibility.Collapsed;
            this.wrapPanel_aeropuertos.Visibility = Visibility.Collapsed;
            this.wrapPanel_configuracion.Visibility = Visibility.Collapsed;

            IB_CTRL_BTN_MenuPP aux = (IB_CTRL_BTN_MenuPP)sender;

            foreach (IB_CTRL_BTN_MenuPP elemento in this.wrapBonotonera.Children)
            {
                elemento.IsSelected = false;
            }

            if(this.TipoInfo == aux.TipoBoton)
            {
                aux.IsSelected = false;
                this.TipoInfo = tipoBotonPP.NINGUNA;
            }
            else
            {
                aux.IsSelected = true;
                this.TipoInfo = aux.TipoBoton;
            }

            

            switch(this.TipoInfo)
            {
                case tipoBotonPP.AVIONES:
                    this.wrapPanel_vuelos.Visibility = Visibility.Visible;
                    break;
                case tipoBotonPP.AIRPORT:
                    this.wrapPanel_aeropuertos.Visibility = Visibility.Visible;
                    break;
                case tipoBotonPP.CONFIGURACION:
                    this.wrapPanel_configuracion.Visibility = Visibility.Visible;
                    break;
                case tipoBotonPP.NINGUNA:
                    this.wrapPanel_vuelos.Visibility = Visibility.Collapsed;
                    this.wrapPanel_aeropuertos.Visibility = Visibility.Collapsed;
                    this.wrapPanel_configuracion.Visibility = Visibility.Collapsed;
                    break;

            }

        }


        private void DIVIDIR_REGION()
        {
            var MADRID = EspaciosAereos["LECM"];

            System.Windows.Media.Geometry Geo = MADRID.GeoPolygon;

            var A = this.MainMap.FromLatLngToLocal(new PointLatLng(53.4083714, -2.9915726));
            var B = this.MainMap.FromLatLngToLocal(new PointLatLng(33.589886, -7.603869)); // 33.589886, -7.603869.

            GMapLineaTexto LineaParti = new GMapLineaTexto(new PointLatLng(53.4083714, -2.9915726), new PointLatLng(33.589886, -7.603869), "Hola");
            LineaParti.RegenerateShape(MainMap);
            MainMap.Markers.Add(LineaParti);

            Point P1 = new Point(A.X, A.Y);
            Point P2 = new Point(B.X, B.Y);
            List<Point> Geo1 = null;
            List<Point> Geo2 = null;
            bool draw;
            //PointLatLng[] intersections = Matematicas.Matematicas.ClipLineWithPolygon(out draw, new PointLatLng(53.4083714, -2.9915726), new PointLatLng(33.589886, -7.603869), MADRID.Points);
            List<Point> Puntos = new List<Point>();
            foreach (var puntossss in MADRID.Points)
            {
                var puntod = this.MainMap.FromLatLngToLocal(puntossss);
                Puntos.Add(new Point(puntod.X, puntod.Y));
            }
            Point[] PuntosPuntos = Matematicas.Matematicas.ClipLineWithPolygon(out draw, new Point(A.X, A.Y), new Point(B.X, B.Y), Puntos);

            

            foreach(Point punto in PuntosPuntos)
            {
                IB_CTRL_COLISION DibujoColision = new IB_CTRL_COLISION();
                var puntotoca1 = this.MainMap.FromLocalToLatLng(punto.X, punto.Y);
                GMapMarker Colision = new GMapMarker(puntotoca1);
                Colision.Shape = DibujoColision;
                Colision.Offset = new System.Windows.Point(-DibujoColision.Height / 2, -DibujoColision.Width / 2);

                MainMap.Markers.Add(Colision);
            }
            

           






            //Matematicas.Matematicas.SplitGeometry(Geo, new Point(Punto1.X,Punto1.Y), new Point(Punto2.X, Punto2.Y), out Geo1, out Geo2);
            //List<PointLatLng> ListaA = new List<PointLatLng>();
            //List<PointLatLng> ListaB = new List<PointLatLng>();
            //foreach (Point punto in Geo1)
            //{
            //    ListaA.Add(this.MainMap.FromLocalToLatLng((int)punto.X, (int)punto.Y));
            //}

            //foreach (Point punto in Geo2)
            //{
            //    ListaB.Add(this.MainMap.FromLocalToLatLng((int)punto.X, (int)punto.Y));
            //}

            //GMapPolygon G1 = new GMapPolygon(ListaA);
            //G1.RegenerateShape(MainMap);

            //MainMap.Markers.Add(G1);




            //GMapPolygon G2 = new GMapPolygon(ListaB);

            //G2.RegenerateShape(MainMap);
            //MainMap.Markers.Add(G2);


        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InicializaVentanasDeInformacion();

            m_VentanaRelog = new IB_CTRL_VISTA_Reloj();
            m_VentanaRelog.Show();
            m_VentanaRelog.Owner = this;
            m_VentanaRelog.Show();
        }

        private void InicializaVentanasDeInformacion()
        {
            m_VentanaPlanesDeVuelo = new IB_VISTA_Planes_de_vuelo();
            m_VentanaPlanesDeVuelo.TituloVentana.Text = "Planes de vuelo";
            m_VentanaPlanesDeVuelo.Show();
            m_VentanaPlanesDeVuelo.Owner = this;
            m_VentanaPlanesDeVuelo.Show();

            m_VentanaColiciones = new IB_VISTA_Planes_de_vuelo();
            m_VentanaColiciones.TituloVentana.Text = "Colisiones de vuelos";
            m_VentanaColiciones.Show();
            m_VentanaColiciones.Owner = this;
            m_VentanaColiciones.Show();

            m_VentanaVuelos = new IB_VISTA_Planes_de_vuelo();
            m_VentanaVuelos.TituloVentana.Text = "Aeronaves seleccionadas";
            m_VentanaVuelos.Show();
            m_VentanaVuelos.Owner = this;
            m_VentanaVuelos.Show();

            m_VentanaAeropuertos = new IB_VISTA_Planes_de_vuelo();
            m_VentanaAeropuertos.TituloVentana.Text = "Aeropuertos Locales";
            m_VentanaAeropuertos.Show();
            m_VentanaAeropuertos.Owner = this;
            m_VentanaAeropuertos.Show();

           
        }

        private void InicializaMapas()
        {
            
             
            polygonFIR_mapa = IB_SGLT_CARTOGRAFIA.Instance.GetMapaCosta();
            foreach(GMapRouteMapa Linea in polygonFIR_mapa)
            {
                Linea.RegenerateShape(MainMap);
                Linea.Shape.Visibility = Visibility.Visible;
                MainMap.Markers.Add(Linea);
            }
           
           
        }

        private void InicializaErrores()
        {
            IB_SGLT_ERRORES.Carga_win(this);
        }

        private void InicializaControles()
        {
            GMapProvider.WebProxy = WebRequest.DefaultWebProxy;

            MainMap.MapProvider = GMapProviders.GoogleMap;
            MainMap.Position = new PointLatLng(40.127062133198081, -5.7009474188089371);
            MainMap.Zoom = 13;
            MainMap.Manager.Mode = AccessMode.ServerAndCache;
            MainMap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            MainMap.MouseDoubleClick += MainMap_MouseDoubleClick;

            GMapLineaTexto A = new GMapLineaTexto(new PointLatLng(40.127062133198081, -5.7009474188089371), new PointLatLng(40.197062133198081, -5.6009474188089371), "hola");
            A.RegenerateShape(MainMap);
            MainMap.Markers.Add(A);

            SIVOAV_GIS_CTRL_A MapInicial= (SIVOAV_GIS_CTRL_A)panel_zoom.Children[0];
            MapInicial.Active = true;

            ActualizaPanelInfo();

            InicializaVuelos();

            List<Point> Puntos = new List<Point>();

            InicializaCargaRegiones();

            InicializaWorkPlanes();
            
        }

        private void MainMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void InicializaWorkPlanes()
        {
            {
                PLANES_Worker.DoWork += new DoWorkEventHandler(PLANES_Worker_DoWork);
                PLANES_Worker.ProgressChanged += new ProgressChangedEventHandler(PLANES_Worker_ProgressChanged);
                PLANES_Worker.WorkerSupportsCancellation = true;
                PLANES_Worker.WorkerReportsProgress = true;

                
            }
        }

        private void InicializaWorkColision()
        {
            {
                Colision_Worker.DoWork += new DoWorkEventHandler(COLISION_Worker_DoWork);
                Colision_Worker.ProgressChanged += new ProgressChangedEventHandler(COLISION_Worker_ProgressChanged);
                Colision_Worker.WorkerSupportsCancellation = true;
                Colision_Worker.WorkerReportsProgress = true;
            }
        }

        private void InicializaCargaRegiones()
        {
            // flight demo
            {
                FIR_Worker.DoWork += new DoWorkEventHandler(FIR_DoWork);
                FIR_Worker.ProgressChanged += new ProgressChangedEventHandler(FIR_ProgressChanged);
                FIR_Worker.WorkerSupportsCancellation = true;
                FIR_Worker.WorkerReportsProgress = true;

                if (!FIR_Worker.IsBusy)
                {

                    FIR_Worker.RunWorkerAsync();
                }
            }
        }

        private void InicializaVuelos()
        {
            // flight demo
            {
                flightWorker.DoWork += new DoWorkEventHandler(flight_DoWork);
                flightWorker.ProgressChanged += new ProgressChangedEventHandler(flight_ProgressChanged);
                flightWorker.WorkerSupportsCancellation = true;
                flightWorker.WorkerReportsProgress = true;
            }
        }

        #region - INICIALIZACIÓN -

        private void InicializaEventos()
        {
            

            MainMap.OnPositionChanged += new PositionChanged(MainMap_OnCurrentPositionChanged);
            MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
            MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
            MainMap.MouseMove += new System.Windows.Input.MouseEventHandler(MainMap_MouseMove);
            MainMap.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainMap_MouseLeftButtonDown);
            MainMap.MouseEnter += new MouseEventHandler(MainMap_MouseEnter);

            ActualizaPanelInfo();
        }

        #region - ENVENTOS INICIALES -

        void MainMap_MouseEnter(object sender, MouseEventArgs e)
        {
            MainMap.Focus();
        }

        void MainMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(MainMap);
            //currentMarker.Position = MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
        }

        void MainMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point p = e.GetPosition(MainMap);

                
                //currentMarker.Position = MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
            }


        }

        void MainMap_OnMapTypeChanged(GMapProvider type)
        {
            //sliderZoom.Minimum = MainMap.MinZoom;
            //sliderZoom.Maximum = MainMap.MaxZoom;
        }

        void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            ActualizaPanelInfo();
        }

        void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

            //System.Windows.Forms.MethodInvoker m = delegate ()
            //{
            //    progressBar1.Visibility = Visibility.Hidden;
            //    groupBox3.Header = "loading, last in " + MainMap.ElapsedMilliseconds + "ms";
            //};

            //try
            //{
            //    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
            //}
            //catch
            //{
            //}
        }

        // tile louading starts
        void MainMap_OnTileLoadStart()
        {
            //System.Windows.Forms.MethodInvoker m = delegate ()
            //{
            //    progressBar1.Visibility = Visibility.Visible;
            //};

            //try
            //{
            //    this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
            //}
            //catch
            //{
            //}
        }


        #endregion

        #endregion


        private void VerificaControlesMapas(object source)
        {
            //foreach (object elemento in panel_mapas.Children)
            //{
            //    if (source is SIVOAV_GIS_CTRL_B &&
            //        elemento is SIVOAV_GIS_CTRL_B)
            //    {
            //        SIVOAV_GIS_CTRL_B aux = (SIVOAV_GIS_CTRL_B)source;
            //        SIVOAV_GIS_CTRL_B aux2 = (SIVOAV_GIS_CTRL_B)elemento;

            //        if (aux == aux2)
            //        {
            //            aux2.Active = true;
            //        }
            //        else
            //        {
            //            aux2.Active = false;
            //        }
            //    }
            //}
        }

        private void VerificaControles(object source)
        {
            foreach (object elemento in panel_zoom.Children)
            {
                if (source is SIVOAV_GIS_CTRL_A &&
                    elemento is SIVOAV_GIS_CTRL_A)
                {
                    SIVOAV_GIS_CTRL_A aux = (SIVOAV_GIS_CTRL_A)source;
                    SIVOAV_GIS_CTRL_A aux2 = (SIVOAV_GIS_CTRL_A)elemento;
                    if (aux == aux2)
                    {
                        aux2.Active = true;
                    }
                    else
                    {
                        aux2.Active = false;
                    }
                }
            }
        }

        private void ActualizaPanelInfo()
        {


            lbl_latitud.Text = Latitud_Cadena(MainMap.Position.Lat);
            lbl_longitud.Text = Longitud_Cadena(MainMap.Position.Lng);

            GeoCoderStatusCode status = GeoCoderStatusCode.Unknow;
            Placemark? pos = GMapProviders.GoogleMap.GetPlacemark(MainMap.Position, out status);
            if (pos != null && status == GeoCoderStatusCode.G_GEO_SUCCESS)
            {

            }
        }

        private string Longitud_Cadena(double longitud)
        {

            
            double lon = longitud;

            string lonDir = (lon >= 0 ? "E" : "O");
            lon = Math.Abs(lon);
            double lonMinPart = ((lon - Math.Truncate(lon) / 1) * 60);
            double lonSecPart = ((lonMinPart - Math.Truncate(lonMinPart) / 1) * 60);

          
            return string.Format("{0:00}", Math.Truncate(lon)) + "º " + string.Format("{0:00}", Math.Truncate(lonMinPart)) + "' " + string.Format("{0:00.00}", lonSecPart) + "'' " + lonDir;
        }

        private string Latitud_Cadena(double latitud)
        {

            double lat = latitud;


            string latDir = (lat >= 0 ? "N" : "S");
            lat = Math.Abs(lat);
            double latMinPart = ((lat - Math.Truncate(lat) / 1) * 60);
            double latSecPart = ((latMinPart - Math.Truncate(latMinPart) / 1) * 60);

            
            return string.Format("{0:00}", Math.Truncate(lat)) +"º " +string.Format("{0:00}", Math.Truncate(latMinPart)) +"' " + string.Format("{0:00.00}", latSecPart) +"'' " + latDir;
        }

        private void SIVOAV_GIS_CTRL_A_OnMaximum(object source, IB_CTRL_SICOAV.controles.MyEventArgs e)
        {
            MainMap.MapProvider = GMapProviders.GoogleMap;
            VerificaControles(source);
            ActualizaPanelInfo();
        }

        private void SIVOAV_GIS_CTRL_A_OnMaximum_1(object source, IB_CTRL_SICOAV.controles.MyEventArgs e)
        {
            MainMap.MapProvider = GMapProviders.GoogleSatelliteMap;
            VerificaControles(source);
            ActualizaPanelInfo();
        }

        private void SIVOAV_GIS_CTRL_A_OnMaximum_2(object source, IB_CTRL_SICOAV.controles.MyEventArgs e)
        {
            MainMap.MapProvider = GMapProviders.GoogleHybridMap;
            VerificaControles(source);
            ActualizaPanelInfo();
        }

        private void SIVOAV_GIS_CTRL_A_OnMaximum_3(object source, MyEventArgs e)
        {
            MainMap.MapProvider = GMapProviders.GoogleTerrainMap;
            VerificaControles(source);
            ActualizaPanelInfo();
        }

        private void SIVOAV_GIS_CTRL_A_OnMaximum_4(object source, MyEventArgs e)
        {
            MainMap.MapProvider = GMapProviders.EmptyProvider;


            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(-25.969562, 32.585789));
            points.Add(new PointLatLng(-25.966205, 32.588171));
            points.Add(new PointLatLng(-25.968134, 32.591647));
            points.Add(new PointLatLng(-25.971684, 32.589759));

            GMapPolygon polygon = new GMapPolygon(points);
            polygon.RegenerateShape(MainMap);
            MainMap.Markers.Add(polygon);

           



            VerificaControles(source);
            ActualizaPanelInfo();
        }

        private void SIVOAV_GIS_CTRL_B_OnMaximum(object source, MyEventArgs e)
        {
            SIVOAV_GIS_CTRL_B aux = (SIVOAV_GIS_CTRL_B)source;

            if (aux.Active)
            {
                if (!flightWorker.IsBusy)
                {
                    firstLoadFlight = true;
                    flightWorker.RunWorkerAsync();
                }
            }
            else
            {
                if (flightWorker.IsBusy)
                {
                    flightWorker.CancelAsync();
                }
            }


        }

        private void SIVOAV_GIS_CTRL_B_OnMaximum_1(object source, MyEventArgs e)
        {
            // FIR_MADRID

            SIVOAV_GIS_CTRL_B aux = (SIVOAV_GIS_CTRL_B)source;

            if (aux.Active)
            {
                this.polygonFIR_MADRID.Shape.Visibility = Visibility.Visible;
            }
            else
            {
                this.polygonFIR_MADRID.Shape.Visibility = Visibility.Hidden;
            }
                

        }

        private void SIVOAV_GIS_CTRL_B_OnMaximum_2(object source, MyEventArgs e)
        {

            SIVOAV_GIS_CTRL_B aux = (SIVOAV_GIS_CTRL_B)source;

            if (aux.Active)
            {
                this.polygonFIR_BARCELONA.Shape.Visibility = Visibility.Visible;
            }
            else
            {
                this.polygonFIR_BARCELONA.Shape.Visibility = Visibility.Hidden;
            }

        }

        private void SIVOAV_GIS_CTRL_B_OnMaximum_3(object source, MyEventArgs e)
        {

            SIVOAV_GIS_CTRL_B aux = (SIVOAV_GIS_CTRL_B)source;

            if (aux.Active)
            {
                this.polygonFIR_CANARIAS.Shape.Visibility = Visibility.Visible;
            }
            else
            {
                this.polygonFIR_CANARIAS.Shape.Visibility = Visibility.Hidden;
            }

             
        }

        private void SIVOAV_GIS_CTRL_B_OnMaximum_4(object source, MyEventArgs e)
        {
            // -------------------------------------------------------------
            // Activamos colisiones si esta a true.
            SIVOAV_GIS_CTRL_B aux = (SIVOAV_GIS_CTRL_B)source;

            if (aux.Active)
            {
                _Colisiones = true;

            }
            else
            {
                _Colisiones = false;

                BorraColisiones();

            }

            // -----------------------------------------------------------
        }

        #region -- flight demo --
        BackgroundWorker flightWorker = new BackgroundWorker();

        #region [] Diccionarios
        readonly List<FlightRadarData> flights = new List<FlightRadarData>();
        readonly Dictionary<int, GMapMarker> flightMarkers = new Dictionary<int, GMapMarker>();
        readonly Dictionary<int, GMapMarker> flightMarkers_data = new Dictionary<int, GMapMarker>();
        readonly Dictionary<int, GMapMarker> flightMarkers_link = new Dictionary<int, GMapMarker>();
        readonly Dictionary<string, GMapPolygon> EspaciosAereos = new Dictionary<string, GMapPolygon>();
        #endregion 

        bool firstLoadFlight = true;
        GMapMarker currentFlight;
        RectLatLng flightBounds ;// new RectLatLng(54.4955675218741, -0.966796875, 28.916015625, 13.3830987326932);
        private Application loadedApp;
        private bool _Colisiones;

        public FlightRadarData TrackFlight;

        public tipoBotonPP TipoInfo { get; private set; }

        void PLANES_Worker_ProgressChanged (object sender, ProgressChangedEventArgs e)
        {

        }

        void FIR_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        void flight_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            

            // stops immediate marker/route/polygon invalidations;
            // call Refresh to perform single refresh and reset invalidation state
            MainMap.HoldInvalidation = true;
            flightBounds = MainMap.ViewArea;
            lock (flights)
            {
                //if (flightBounds != MainMap.ViewArea)
                //{
                //    flightBounds = MainMap.ViewArea;
                //    foreach (var m in objects)
                //    {
                //        if (!flightBounds.Contains(m.Position))
                //        {
                //            m.IsVisible = false;
                //        }
                //        else
                //        {
                //            m.IsVisible = true;
                //        }
                //    }
                //}
                wrapPanel_vuelos.Children.Clear();

                foreach (FlightRadarData d in flights)
                {
                    IB_CTRL_PanelAvion_A aux;

                    if (this.TrackFlight.Id == d.Id)
                    {
                        aux = new IB_CTRL_PanelAvion_A(d,true);
                        this.MainMap.Position = d.point;
                    }
                    else
                    {
                        aux = new IB_CTRL_PanelAvion_A(d, false);
                    }
                    
                    aux.RaiseCustomEvent += Aux_RaiseCustomEvent; 
                    wrapPanel_vuelos.Children.Add(aux);

                    // Actualizamos la lista de vuelos seleccionados.
                    // ----------------------------------------------
                    foreach (InfoVuelo_A ElementoLista in this.m_VentanaVuelos.WrapPanel_Principal.Children)
                    {
                        if(ElementoLista.DatosVuelo.Id == d.Id)
                        {
                            ElementoLista.DatosVuelo = d;
                        }
                    }

                    GMapMarker marker = null;

                    if (!flightMarkers.TryGetValue(d.Id, out marker))
                    {

                        marker = new GMapMarker(d.point);
                        {
                            marker.Shape = new CustomMarkerRed(this, marker,d);
                            marker.Offset = new System.Windows.Point(0, 0);
                            marker.ZIndex = int.MaxValue;
                            ((CustomMarkerRed)(marker.Shape)).Vuelo = d;
                            MainMap.Markers.Add(marker);
                        }

                        flightMarkers[d.Id] = marker;
                        objects.Add(marker);
                    }
                    else
                    {
                        ((CustomMarkerRed)(marker.Shape)).Angulo = d.bearing;
                        ((CustomMarkerRed)(marker.Shape)).Vuelo = d;



                        marker.Position = d.point;
                        //(marker as GMarkerArrow).Bearing = d.bearing;
                    }

                    ((CustomMarkerRed)(marker.Shape)).RevisaRecuadro();


                    continue;

                    #region # Codigo antiguo #
                    //if (!flightMarkers_data.TryGetValue(d.Id, out marker_vuelo))
                    //{

                    //    marker_vuelo = new GMapMarker(d.point);
                    //    {
                    //        marker_vuelo.Shape = new IB_DATOS_VUELO_CTRL(this, marker_vuelo, marker, marker_link, d);
                    //        ((IB_DATOS_VUELO_CTRL)(marker_vuelo.Shape)).RaiseCustomEvent += MainWindow_RaiseCustomEvent;
                    //        marker_vuelo.Offset = new System.Windows.Point(0, 0);
                    //        marker_vuelo.ZIndex = int.MaxValue;

                    //        MainMap.Markers.Add(marker_vuelo);
                    //    }

                    //    flightMarkers_data[d.Id] = marker_vuelo;

                    //    objects.Add(marker_vuelo);
                    //}
                    //else
                    //{


                    //    IB_DATOS_VUELO_CTRL Antiguo = (IB_DATOS_VUELO_CTRL)marker_vuelo.Shape;
                    //    Antiguo.Title = d;

                    //    d.point.Offset(marker_vuelo.Position);
                    //    marker_vuelo.Position = d.point - Antiguo.IncrementoLatLon;

                    //    //(marker as GMarkerArrow).Bearing = d.bearing;
                    //}

                    //if (!flightMarkers_link.TryGetValue(d.Id, out marker_link))
                    //{
                    //    List<PointLatLng> route = new List<PointLatLng>();

                    //    route.Add(marker.Position);

                    //    double A = 26 / 2;
                    //    double B = 151 / 2;

                    //    var PC = this.MainMap.FromLatLngToLocal(marker_vuelo.Position);
                    //    var PM = this.MainMap.FromLatLngToLocal(marker.Position);

                    //    double CX = PC.X;
                    //    double CY = PC.Y;

                    //    double AX = PM.X;
                    //    double AY = PM.Y;

                    //    Vector A1 = new Vector(CX - B, CY + A);
                    //    Vector A2 = new Vector(CX + B, CY + A);
                    //    Vector A3 = new Vector(CX + B, CY - A);
                    //    Vector A4 = new Vector(CX - B, CY - A);

                    //    Vector P1 = new Vector(CX, CY);
                    //    Vector P2 = new Vector(AX, AY);

                    //    var Point = Matematicas.Matematicas.PuntoRectangulo(A1, A2, A3, A4, P1, P2);

                    //    route.Add(this.MainMap.FromLocalToLatLng((int)Point.Value.X, (int)Point.Value.Y));


                    //    marker_link = new GMapRoute(route);
                    //    {



                    //        marker_link.ZIndex = int.MaxValue;

                    //        MainMap.Markers.Add(marker_link);
                    //    }

                    //    IB_DATOS_VUELO_CTRL aux = (IB_DATOS_VUELO_CTRL)marker_vuelo.Shape;
                    //    aux.Marker_Line = marker_link;

                    //    flightMarkers_link[d.Id] = marker_link;

                    //    objects.Add(marker_link);
                    //}
                    //else
                    //{

                    //    MainMap.Markers.Remove(flightMarkers_link[d.Id]);
                    //    List<PointLatLng> route = new List<PointLatLng>();

                    //    route.Add(marker.Position);

                    //    //var Point = Matematicas.Matematicas.PuntoRectangulo(this.MainMap.FromLatLngToLocal(marker.Position), this.MainMap.FromLatLngToLocal(marker_vuelo.Position), 151.0, 26.3);

                    //    double A = 26 / 2;
                    //    double B = 151 / 2;

                    //    var PC = this.MainMap.FromLatLngToLocal(marker_vuelo.Position);
                    //    var PM = this.MainMap.FromLatLngToLocal(marker.Position);

                    //    double CX = PC.X;
                    //    double CY = PC.Y;

                    //    double AX = PM.X;
                    //    double AY = PM.Y;

                    //    Vector A1 = new Vector(CX - B, CY + A);
                    //    Vector A2 = new Vector(CX + B, CY + A);
                    //    Vector A3 = new Vector(CX + B, CY - A);
                    //    Vector A4 = new Vector(CX - B, CY - A);

                    //    Vector P1 = new Vector(CX, CY);
                    //    Vector P2 = new Vector(AX, AY);

                    //    var Point = Matematicas.Matematicas.PuntoRectangulo(A1, A2, A3, A4, P1, P2);

                    //    route.Add(this.MainMap.FromLocalToLatLng((int)Point.Value.X, (int)Point.Value.Y));




                    //    marker_link = new GMapRoute(route);
                    //    {



                    //        marker_link.ZIndex = int.MaxValue;

                    //        MainMap.Markers.Add(marker_link);
                    //    }

                    //    IB_DATOS_VUELO_CTRL aux = (IB_DATOS_VUELO_CTRL)marker_vuelo.Shape;
                    //    aux.Marker_Line = marker_link;

                    //    flightMarkers_link[d.Id] = marker_link;

                    //    //(marker as GMarkerArrow).Bearing = d.bearing;
                    //}





                    //if (currentFlight != null && currentFlight == marker)
                    //{
                    //    MainMap.Position = marker.Position;
                    //    MainMap.Bearing = (float)d.bearing;
                    //}

                    #endregion # Codigo antiguo #
                }
                COLISION_Worker_ProgressChanged(null, null);
                // arranca proceso de verificacion de colisiones
            }

            //if (firstLoadFlight)
            //{
            //    MainMap.Zoom = 5;
            //    MainMap.SetZoomToFitRect(new RectLatLng(54.4955675218741, -0.966796875, 28.916015625, 13.3830987326932));
            //    firstLoadFlight = false;
            //}
            //MainMap.Refresh();
        }

        private void Aux_RaiseCustomEvent(object sender, FlightRadarData e)
        {

            IB_CTRL_PanelAvion_A aux = (IB_CTRL_PanelAvion_A)sender;

            foreach (IB_CTRL_PanelAvion_A elemento in this.wrapPanel_vuelos.Children)
            {

                elemento.IsSelected = false;

            }

            if (this.TrackFlight.Id == e.Id)
            {
                this.TrackFlight.Id = -1;
                aux.IsSelected = false;
            } 
            else
            {
                this.TrackFlight = e;
                this.MainMap.Position = e.point;
                aux.IsSelected = true;
                 
            }
        }

        public void MainWindow_RaiseCustomEvent(object sender, FlightRadarData e)
        {
            if (Application.Current != null)
            {
                loadedApp = Application.Current;

                loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                   new Action(delegate ()
                   {
                       InfoVuelo_A aux = null;

                       foreach (InfoVuelo_A p in this.m_VentanaVuelos.WrapPanel_Principal.Children)
                       {
                           if(p.DatosVuelo.Id == e.Id)
                           {
                               aux = p;
                               break;
                           }
                       }

                       if (aux == null)
                       {
                           aux = new InfoVuelo_A(e, this.MainMap);
                           aux.RaisePlanEvent += Aux_RaisePlanEvent;
                           this.m_VentanaVuelos.WrapPanel_Principal.Children.Add(aux);
                       }
                       else
                       {
                           this.m_VentanaVuelos.WrapPanel_Principal.Children.Remove((InfoVuelo_A)aux);
                       }
                   }
                   ));
            }
        }

        private void Aux_RaisePlanEvent(object sender, planEvent e)
        {
            if (!PLANES_Worker.IsBusy)
            {

                PLANES_Worker.RunWorkerAsync(e);
            }
        }

        #region - DOWORK COLISIONES -
        void COLISION_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!Colision_Worker.CancellationPending)
            {

            }
        }

        void COLISION_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!_Colisiones) return;

            lock (flights)
            {
                BorraColisiones();

                foreach (var elemento in flights)
                {
                    foreach (var elemento2 in flights)
                    {
                        AnalizaColision(elemento, elemento2);
                    }
                }
            }

        }

        private void BorraColisiones()
        {
            if(this.m_VentanaColiciones != null)
              this.m_VentanaColiciones.WrapPanel_Principal.Children.Clear();

            List<GMapLineaTexto> ListaAborrar = new List<GMapLineaTexto>();
            List<GMapMarker> ListaMarcasColisiones = new List<GMapMarker>();

            foreach (GMapMarker marker in MainMap.Markers)
            {
                if (marker is GMapLineaTexto)
                {
                    ListaAborrar.Add((GMapLineaTexto)marker);
                }

                if(marker.Shape is IB_CTRL_COLISION)
                {
                    ListaMarcasColisiones.Add(marker);
                }
            }

            foreach (GMapLineaTexto marker in ListaAborrar)
            {
                MainMap.Markers.Remove(marker);
            }

            foreach (GMapMarker marker in ListaMarcasColisiones)
            {
                MainMap.Markers.Remove(marker);
            }
        }

        private void AnalizaColision(FlightRadarData elemento, FlightRadarData elemento2)
        {

            if (elemento.Id == elemento2.Id) return;

            var Diferencia = elemento.point - elemento2.point;

            if (Diferencia.WidthLng == 0 && Diferencia.HeightLat == 0) return;

            var DAltitud = Math.Abs(int.Parse(elemento.altitude) - int.Parse(elemento2.altitude));
            var AltitudSingleton = IB_SGLT_Configuracion.Instance.GetAlturaMinimaColicion();

            if (DAltitud >= AltitudSingleton) return; 

            var sCoord = new GeoCoordinate(elemento.point.Lat, elemento.point.Lng);
            var eCoord = new GeoCoordinate(elemento2.point.Lat, elemento2.point.Lng);

            var Distancia = sCoord.GetDistanceTo(eCoord);
            var DistanciaConfig = IB_SGLT_Configuracion.Instance.GetDistanciaEntreAvionesMinimaColicion();

            if (Distancia >= DistanciaConfig) return;

            Point A1 = new Point(this.MainMap.FromLatLngToLocal(elemento.point).X, this.MainMap.FromLatLngToLocal(elemento.point).Y);
            Point B1 = Matematicas.Matematicas.RadioCirculo(10, elemento.bearing - 90, A1);

            Point A2 = new Point(this.MainMap.FromLatLngToLocal(elemento2.point).X, this.MainMap.FromLatLngToLocal(elemento2.point).Y);
            Point B2 = Matematicas.Matematicas.RadioCirculo(10, elemento2.bearing - 90, A2);

            Point P1;
            Point P2;

            bool esLineaInterseccion, esSegmento;

            Point PointInterseccion;

            Matematicas.Matematicas.FindIntersection(A1, B1, A2, B2, out esLineaInterseccion, out esSegmento, out PointInterseccion, out P1, out P2);

            if (B1 == P1 && B2 == P2)
            {
                PointLatLng CoordInterseccion = this.MainMap.FromLocalToLatLng((int)PointInterseccion.X, (int)PointInterseccion.Y);
                PointLatLng CoordAvion = this.MainMap.FromLocalToLatLng((int)B1.X, (int)B1.Y);

                var sCoord1 = new GeoCoordinate(CoordAvion.Lat, CoordAvion.Lng);
                var eCoord1 = new GeoCoordinate(CoordInterseccion.Lat, CoordInterseccion.Lng);

                var Distancia1 = sCoord1.GetDistanceTo(eCoord1) / 1000;
                int Distanciaint = (int)Distancia1;

                if (((int) (Distanciaint)) >= IB_SGLT_Configuracion.Instance.GetDistanciaMinimaColicion()) return;

                var Tiempo = (Distancia1 / double.Parse(elemento.speed.Replace("km/h", ""))) * 60;

                if (double.IsInfinity(Tiempo)) return;
                if (double.IsNaN(Tiempo)) return;
                
                var TiempoLlegada = DateTime.Now.AddMinutes(Tiempo);

                GMapLineaTexto A = new GMapLineaTexto(CoordAvion, CoordInterseccion, TiempoLlegada.ToLongTimeString());
                A.RegenerateShape(MainMap);
                MainMap.Markers.Add(A);

             

                IB_MOD_COLISION Col = new IB_MOD_COLISION();

                Col.m_callsing_V1 = elemento.name;
                Col.m_callsing_V2 = elemento2.name;
                Col.m_distanciaV1 = string.Format("{0:0.00}", Distancia1) + " km"; //  Distancia1.ToString("0:0.00") + " km";
                Col.m_TimeV1 = TiempoLlegada;

                IB_CTRL_COLISION_INFO ColisionItem = new IB_CTRL_COLISION_INFO(Col);

                this.m_VentanaColiciones.WrapPanel_Principal.Children.Add(ColisionItem);

                #region + Añadimos la marca de la colision
                IB_CTRL_COLISION DibujoColision = new IB_CTRL_COLISION(Col);

                GMapMarker Colision = new GMapMarker(CoordInterseccion);
                Colision.Shape = DibujoColision;
                Colision.Offset = new System.Windows.Point(-DibujoColision.Height / 2, -DibujoColision.Width / 2);
                MainMap.Markers.Add(Colision);

                #endregion + Añadimos la marca de la colision
                //GMapLineaTexto B = new GMapLineaTexto(this.MainMap.FromLocalToLatLng((int)B2.X, (int)B2.Y), this.MainMap.FromLocalToLatLng((int)PointInterseccion.X, (int)PointInterseccion.Y), "hola");
                //B.RegenerateShape(MainMap);
                //MainMap.Markers.Add(B);
            }

        }

        #endregion - DOWORK COLISIONES -

        void PLANES_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Application.Current != null)
                {
                    loadedApp = Application.Current;

                    loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                       new Action(delegate ()
                       {
                           this.m_VentanaPlanesDeVuelo.WrapPanel_Principal.Children.Clear();
                           this.m_VentanaPlanesDeVuelo.PanelPrincipal.Children.Clear();

                           var datos = API_WEB.GetPlanesDeVuelo(((planEvent)(e.Argument)).origenIATA, ((planEvent)(e.Argument)).destinoIATA);
                           foreach (var planes in datos)
                           {
                               this.m_VentanaPlanesDeVuelo.PanelPrincipal.Children.Add(new IB_CTRL_PLANDEVUELO(planes, this.MainMap));
                           }

                       }
                       ));
                }
            }
            catch(Exception ex)
            {
                string Error = ex.Message;
            }
        }
        

        void FIR_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Application.Current != null)
                {
                    loadedApp = Application.Current;

                    loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                       new Action(delegate ()
                       {
                           try
                           {
                               //bool restartSesion = true;
                               polygonFIR_MADRID = new GMapPolygon(API_WEB.GetEspaciosAereos("LECM"));
                               polygonFIR_MADRID.RegenerateShape(MainMap);
                               polygonFIR_MADRID.Shape.Visibility = Visibility.Hidden;
                               MainMap.Markers.Add(polygonFIR_MADRID);
                               EspaciosAereos.Add("LECM", polygonFIR_MADRID);

                               //DIVIDIR_REGION();

                               polygonFIR_BARCELONA = new GMapPolygon(API_WEB.GetEspaciosAereos("LECB"));
                               polygonFIR_BARCELONA.RegenerateShape(MainMap);
                               polygonFIR_BARCELONA.Shape.Visibility = Visibility.Hidden;
                               MainMap.Markers.Add(polygonFIR_BARCELONA);
                               EspaciosAereos.Add("LECB", polygonFIR_BARCELONA);

                               polygonFIR_CANARIAS = new GMapPolygon(API_WEB.GetEspaciosAereos("GCCC"));
                               polygonFIR_CANARIAS.RegenerateShape(MainMap);
                               polygonFIR_CANARIAS.Shape.Visibility = Visibility.Hidden;
                               MainMap.Markers.Add(polygonFIR_CANARIAS);
                               EspaciosAereos.Add("GCCC", polygonFIR_CANARIAS);

                               foreach (SICOAV_A.Modelos.QuickType.IbModAeropuerto aereopuerto in API_WEB.GetAeropuertos("ESP")) //(IB_SGLT_LOCALIZACION.ISORegionName)))
                               {
                                   //this.m_VentanaAeropuertos.WrapPanel_Principal.Children.Add(new IB_CTRL_AEROPUERTO(MainMap, aereopuerto));

                                   IB_CTRL_PanelAeropuerto aux = new IB_CTRL_PanelAeropuerto(aereopuerto);
                                   aux.MouseDown += Aux_MouseDown;
                                   this.wrapPanel_aeropuertos.Children.Add(aux);

                                   GMapMarker marker = new GMapMarker(new PointLatLng(aereopuerto.Latitude, aereopuerto.Longitude));
                                   // CTRL_AEROPUERTO_A ObjGraf = new CTRL_AEROPUERTO_A(null,aereopuerto);
                                   IB_CTRL_MARCA_Posicion ObjGraf = new IB_CTRL_MARCA_Posicion();
                                   marker.Shape = ObjGraf;
                                  
                                   marker.Offset = new Point(-ObjGraf.Width / 2, -ObjGraf.Height / 3);
                                   MainMap.Markers.Add(marker);
                               }
                           }
                           catch(Exception ex)
                           {
                               IB_SGLT_ERRORES.MuestraError_API(this, "API_WEB.GetEspaciosAereos", ex.Message);
                           }
                       }
                       ));
                }
            }
            catch(Exception ex)
            {

            }

           

        }

        private void Aux_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IB_CTRL_PanelAeropuerto aux = (IB_CTRL_PanelAeropuerto)sender;

            MainMap.Position = new PointLatLng(aux.Aeropuerto.Latitude,aux.Aeropuerto.Longitude);

        }

        void flight_DoWork(object sender, DoWorkEventArgs e)
        {
            //bool restartSesion = true;
            DateTime Tiempo = DateTime.Now;

            while (!flightWorker.CancellationPending)
            {
                try
                {

                    
                    lock (flights)
                    {
                        //Stuff.GetFlightRadarData(flights, lastPosition, lastZoom, restartSesion);
                        TimeSpan span = DateTime.Now.Subtract(Tiempo);
                        if (span.TotalMinutes > 1)
                        {
                            Tiempo = DateTime.Now;
                            if (Application.Current != null)
                            {
                               

                                loadedApp = Application.Current;

                                loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                                   new Action(delegate ()
                                   {
                                       List<GMapMarker> BorrarVuelos = new List<GMapMarker>();

                                       foreach (KeyValuePair<int, GMapMarker> entry in flightMarkers)
                                       {
                                           if (entry.Value.Shape is CustomMarkerRed)
                                           {
                                               CustomMarkerRed aux = (CustomMarkerRed)entry.Value.Shape;
                                               
                                               if (DateTime.Now.Subtract(aux.Vuelo.Tiempo).TotalMinutes > 1)
                                               {
                                                   BorrarVuelos.Add(entry.Value);
                                               }
                                           }
                                          
                                       }
                                      

                                      foreach(GMapMarker FlightRadarData in BorrarVuelos)
                                       {
                                           objects.Remove(FlightRadarData);
                                           if (FlightRadarData.Shape is CustomMarkerRed)
                                           {
                                               CustomMarkerRed ptr = (CustomMarkerRed)FlightRadarData.Shape;
                                               if (ptr.Vuelo.Id != 0)
                                               {
                                                   for (int index = 0; index < this.m_VentanaVuelos.WrapPanel_Principal.Children.Count; index++)
                                                   {
                                                       InfoVuelo_A Aux = (InfoVuelo_A)this.m_VentanaVuelos.WrapPanel_Principal.Children[index];
                                                       if (Aux.DatosVuelo.Id == ptr.Vuelo.Id)
                                                       {
                                                           this.m_VentanaVuelos.WrapPanel_Principal.Children.Remove(Aux);
                                                       }
                                                   }

                                                  // MainMap.Markers.Remove(flightMarkers_link[ptr.Vuelo.Id]);
                                                   MainMap.Markers.Remove(flightMarkers[ptr.Vuelo.Id]);
                                                 //  MainMap.Markers.Remove(flightMarkers_data[ptr.Vuelo.Id]);

                                                  // flightMarkers_link.Remove(ptr.Vuelo.Id);
                                                   flightMarkers.Remove(ptr.Vuelo.Id);
                                                  // flightMarkers_data.Remove(ptr.Vuelo.Id);
                                               }
                                           }

                                       }

                                       BorrarVuelos.Clear();
                                       BorrarVuelos = null;

                                   }
                                   ));
                            }
                        }
                        else
                        {
                            Stuff.GetFlightRadarData(flights, flightBounds);
                        }
                        //if(flights.Count > 0 && restartSesion)
                        //{
                        //   restartSesion = false;
                        //}
                    }

                    flightWorker.ReportProgress(100);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("flight_DoWork: " + ex.ToString());
                }
                Thread.Sleep(5 * 1000);
            }

            flightMarkers.Clear();

            if (Application.Current != null)
            {
                loadedApp = Application.Current;

                loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                   new Action(delegate ()
                   {
                       //MainMap.Markers.Clear();
                   }
                   ));
            }

           
        }


        #endregion

        private void SIVOAV_GIS_CTRL_A_OnMaximum_5(object source, MyEventArgs e)
        {
            if (source is SIVOAV_GIS_CTRL_A)
            {
                SIVOAV_GIS_CTRL_A ptr = (SIVOAV_GIS_CTRL_A)source;

                IB_SGLT_Configuracion.Instance.SetVisualizarDatosAvion(ptr.Active);
            }
            
        }

        List<PointLatLng> m_Poligono = new List<PointLatLng>();

        private void MainMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ex = (MouseEventArgs)e;
            var punto = ex.GetPosition(this.MainMap);

            double X = MainMap.FromLocalToLatLng(punto.X, punto.Y).Lng;
            double Y = MainMap.FromLocalToLatLng(punto.X, punto.Y).Lat;

            m_Poligono.Add(new PointLatLng(Y, X));

            GMapLineaTexto tyipo;

            List<GMapLineaTexto> lista_puntos = new List<GMapLineaTexto>(); 

            foreach (GMapMarker elemento in MainMap.Markers)
            {
                if (elemento is GMapLineaTexto)
                    lista_puntos.Add((GMapLineaTexto)elemento);
            }

            foreach (GMapLineaTexto elemento in lista_puntos)
            {
                MainMap.Markers.Remove(elemento);
            }

           
            if (m_Poligono.Count > 1)
            {
                for(int index=0; index < m_Poligono.Count - 1; index ++)
                {
                    GMapLineaTexto LineaParti = new GMapLineaTexto(m_Poligono[index], m_Poligono[index+1], "Hola");
                    LineaParti.RegenerateShape(MainMap);
                    MainMap.Markers.Add(LineaParti);
                }
            }

             
        }

        private void MainMap_MouseMove_1(object sender, MouseEventArgs e)
        {
             
        }
    }
}
