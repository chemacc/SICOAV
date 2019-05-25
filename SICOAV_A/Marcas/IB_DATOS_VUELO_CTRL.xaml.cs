using GMap.NET;
using GMap.NET.WindowsPresentation;
using SICOAV_A.Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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

namespace SICOAV_A.Marcas
{
    /// <summary>
    /// Lógica de interacción para IB_DATOS_VUELO_CTRL.xaml
    /// </summary>
    public partial class IB_DATOS_VUELO_CTRL : UserControl
    {
        public delegate void CustomEventHandler(object sender, FlightRadarData a);

        public event EventHandler<FlightRadarData> RaiseCustomEvent;

        #region - Variables miembro privadas -

        GMapMarker Marker;
        GMapMarker Marker_link;
        public GMapMarker Marker_Line;
        public MainWindow MainWindow;
        FlightRadarData m_title;

        BackgroundWorker flightWorker = new BackgroundWorker();
        private Application loadedApp;

        List<Modelos.IB_MOD_PUNTORUTA> historicoRuta = new List<Modelos.IB_MOD_PUNTORUTA>();

        GMapRoute m_Ruta;

        bool estaSeccionado = false;

        #endregion

        #region - Propiedades -


        public FlightRadarData Title
        {
            get
            {
                return m_title;
            }
            set
            {
                m_title = value;

                chkValue(this.txt_altidud_vuelo.Text, m_title.altitude, this.txt_altidud_vuelo);
                chkValue(this.txt_velocidad_vuelo.Text, m_title.speed, this.txt_velocidad_vuelo);
                chkValue(this.txt_rumbo_vuelo.Text, m_title.bearing.ToString(), this.txt_rumbo_vuelo);

                this.txt_altidud_vuelo.Text = m_title.altitude;
                this.txt_nombre_vuelo.Text = m_title.name;
                this.txt_rumbo_vuelo.Text = m_title.bearing.ToString();
                this.txt_velocidad_vuelo.Text = m_title.speed;
                this.txt_tiempo.Text = DateTime.Now.ToShortTimeString();
                
            }
        }

        private void chkValue(string textoViejo, string textoNuevo, TextBlock elemento)
        {
          
            if (textoViejo != textoNuevo)
            {
                elemento.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00C5FF"));
            }
            else
            {
                elemento.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFB900"));
            }
        }

        protected virtual void OnRaiseCustomEvent(FlightRadarData e)
        {
            EventHandler<FlightRadarData> handler = RaiseCustomEvent;

            if (handler != null)
            {            
                              
                handler(this, e);
            }
        }

        public SizeLatLng IncrementoLatLon { get; private set; }


        #endregion

        public IB_DATOS_VUELO_CTRL()
        {
            InitializeComponent();

            InicializaPintaRuta();

       
            this.MouseDown += IB_DATOS_VUELO_CTRL_MouseDown;
        }
        public IB_DATOS_VUELO_CTRL(MainWindow window, GMapMarker marker, GMapMarker marker_link, GMapMarker marker_line_link,FlightRadarData title)
        {
            InitializeComponent();

            InicializaPintaRuta();

            this.RectangleSeleccion.Visibility = Visibility.Collapsed;
            this.MainWindow = window;
            this.Marker = marker;
            this.Marker_link = marker_link;
            this.m_title = title;
            this.Marker_Line = marker_line_link;

            this.txt_altidud_vuelo.Text = title.altitude;
            this.txt_nombre_vuelo.Text = title.name;
            this.txt_rumbo_vuelo.Text = title.bearing.ToString() + "º";
            this.txt_velocidad_vuelo.Text = title.speed;

            this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
            this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
            this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);
            this.MouseRightButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseRightButtonDown);
            this.MouseWheel += IB_DATOS_VUELO_CTRL_MouseWheel;
            this.MouseDown += IB_DATOS_VUELO_CTRL_MouseDown;
        }

        private void IB_DATOS_VUELO_CTRL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                estaSeccionado = !estaSeccionado;
                if (estaSeccionado)
                {
                    RectangleSeleccion.Visibility = Visibility.Visible;
                    OnRaiseCustomEvent(this.m_title);

                    // muestro la ruta historica.

                    if (!flightWorker.IsBusy)
                    {

                        flightWorker.RunWorkerAsync();
                    }




                }
                else
                {
                    RectangleSeleccion.Visibility = Visibility.Collapsed;
                    OnRaiseCustomEvent(this.m_title);

                    if (flightWorker.IsBusy)
                    {
                        flightWorker.CancelAsync();
                    }
                }
            }
        }

        private void IB_DATOS_VUELO_CTRL_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }

        private void InicializaPintaRuta()
        {
            flightWorker.DoWork += new DoWorkEventHandler(flight_DoWork);
            flightWorker.ProgressChanged += new ProgressChangedEventHandler(flight_ProgressChanged);
            flightWorker.WorkerSupportsCancellation = true;
            flightWorker.WorkerReportsProgress = true;
        }

        bool isUpdate;

        void flight_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (historicoRuta)
            {
                
                if (m_Ruta == null)
                {
                    List<PointLatLng> lista = new List<PointLatLng>();

                    foreach (Modelos.IB_MOD_PUNTORUTA d in historicoRuta)
                    {
                        lista.Add(new PointLatLng(d.Latitud, d.Longitud));
                    }

                    m_Ruta = new GMapRoute(lista);
                    m_Ruta.RegenerateShape(this.MainWindow.MainMap);
                    this.MainWindow.MainMap.Markers.Add(m_Ruta);

                }
                else
                {
                    m_Ruta.Points.Add(this.m_title.point);
                    m_Ruta.RegenerateShape(this.MainWindow.MainMap);
                }
                
                //if (m_Ruta != null)
                //{
                //    this.MainWindow.MainMap.Markers.Remove(m_Ruta);
                //    m_Ruta = null;
                   
                //}

               
            }
        }

        void flight_DoWork(object sender, DoWorkEventArgs e)
        {
            //bool restartSesion = true;

            while (!flightWorker.CancellationPending)
            {
                try
                {
                    lock (historicoRuta)
                    {

                        if(!isUpdate)
                        {
                            historicoRuta = API.API_WEB.GetTrackAvion(this.m_title.ssr);
                            isUpdate = true;
                            if(historicoRuta.Count > 0)
                                if (Application.Current != null)
                                {
                                    loadedApp = Application.Current;

                                    loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                                       new Action(delegate ()
                                       {

                                           grid_historico.Visibility = Visibility.Collapsed;
                                       }
                                       ));
                                }
                                else
                                if (Application.Current != null)
                                {
                                    loadedApp = Application.Current;

                                    loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                                       new Action(delegate ()
                                       {

                                           grid_historico.Visibility = Visibility.Visible;
                                       }
                                       ));
                                }
                        }
                          


                    }

                    flightWorker.ReportProgress(100);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("flight_DoWork: " + ex.ToString());
                    isUpdate = true;
                    if (Application.Current != null)
                    {
                        loadedApp = Application.Current;

                        loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                           new Action(delegate ()
                           {

                               grid_historico.Visibility = Visibility.Visible;
                           }
                           ));
                    }
                }

                Thread.Sleep(5 * 1000);
            }

            historicoRuta.Clear();

            if (Application.Current != null)
            {
                loadedApp = Application.Current;

                loadedApp.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle,
                   new Action(delegate ()
                   {
                       this.MainWindow.MainMap.Markers.Remove(m_Ruta);
                       m_Ruta = null;
                       isUpdate = false;
                       
                   }
                   ));
            }


        }

        void CustomMarkerDemo_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.RightButton == MouseButtonState.Pressed)
            
        }

        void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            //if(icon.Source.CanFreeze)
            //{
            //   icon.Source.Freeze();
            //}
        }

        void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // De esta manera tenemos el punto en el centro.

            Marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height / 2);
        }

        void CustomMarkerDemo_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed && IsMouseCaptured)
            {
                Point p = e.GetPosition(MainWindow.MainMap);
                IncrementoLatLon = this.Marker.Position - MainWindow.MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
                this.Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
                
            }

            if (Marker_Line != null)
            {
                GMapRoute aux = (GMapRoute)Marker_Line;

                aux.Points.Clear();

                List<PointLatLng> route = new List<PointLatLng>();
                double A = 26 / 2;
                double B = 151 / 2;



                route.Add(this.Marker_link.Position);

                var PC = this.MainWindow.MainMap.FromLatLngToLocal(this.Marker.Position);
                var PM = this.MainWindow.MainMap.FromLatLngToLocal(this.Marker_link.Position);

                double CX = PC.X;
                double CY = PC.Y;

                double AX = PM.X;
                double AY = PM.Y;

                Vector A1 = new Vector(CX - B, CY + A); 
                Vector A2 = new Vector(CX + B, CY + A);
                Vector A3 = new Vector(CX + B, CY - A);
                Vector A4 = new Vector(CX - B, CY - A);

                Vector P1 = new Vector(CX, CY);
                Vector P2 = new Vector(AX, AY);

                var Point = Matematicas.Matematicas.PuntoRectangulo(A1,A2,A3,A4,P1,P2);

                route.Add(MainWindow.MainMap.FromLocalToLatLng((int)Point.Value.X, (int)Point.Value.Y));

                 

                aux.Points = route;
                aux.RegenerateShape(this.MainWindow.MainMap);
            }
            
        }

        void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            if (!IsMouseCaptured)
            {
                Mouse.Capture(this);
            }
        }

        void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Marker.ZIndex -= 10000;
            IncrementoLatLon = Marker.Position - Marker_link.Position;

            

        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker.ZIndex += 10000;
             
        }
    }

}

