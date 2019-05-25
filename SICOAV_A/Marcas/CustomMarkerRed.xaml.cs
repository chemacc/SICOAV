using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using GMap.NET.WindowsPresentation;
using SICOAV_A;
using SICOAV_A.Recursos;

namespace SICOAV_A.Marcas
{
    /// <summary>
    /// Interaction logic for CustomMarkerDemo.xaml
    /// </summary>
    public partial class CustomMarkerRed
    {

        Popup Popup;
        Label Label;
        GMapMarker Marker;
        MainWindow MainWindow;
        FlightRadarData m_Vuelo;
        private int myAngulo;

        #region Propiedad Angulo 
        public static readonly DependencyProperty CurrentReadingProperty = DependencyProperty.Register(
    "Angulo",
    typeof(double),
    typeof(CustomMarkerRed),
    new FrameworkPropertyMetadata(
        double.NaN,
        FrameworkPropertyMetadataOptions.AffectsMeasure,
        new PropertyChangedCallback(OnCurrentReadingChanged),
        new CoerceValueCallback(CoerceCurrentReading)
    ),
    new ValidateValueCallback(IsValidReading));

        public double Angulo
        {
            get { return (double)GetValue(CurrentReadingProperty); }
            set { SetValue(CurrentReadingProperty, value); }
        }

        public FlightRadarData Vuelo { get => m_Vuelo; set => m_Vuelo = Rectangulo.Title = value;  }

        public static bool IsValidReading(object value)
        {
            double v = (double)value;
            return (!v.Equals(double.NegativeInfinity) && !v.Equals(double.PositiveInfinity));
        }

        private static void OnCurrentReadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             
        }

        private static object CoerceCurrentReading(DependencyObject d, object value)
        {
            CustomMarkerRed g = (CustomMarkerRed)d;
            double current = (double)value - 90;
            
            return current;
        }

        #endregion Propiedad Angulo

    

        public CustomMarkerRed(MainWindow window, GMapMarker marker, FlightRadarData title)
        {
            this.InitializeComponent();

            

            
            m_Linea.StrokeThickness = 2;

            m_Linea.X2 = marker.LocalPositionX;
            m_Linea.Y2 = marker.LocalPositionY;

            m_Linea.X1 = marker.LocalPositionX+10;
            m_Linea.Y1 = marker.LocalPositionY;

            Rectangulo.Title = title;


            DataContext = this;
            this.MainWindow = window;
            this.Marker = marker;

            this.Angulo = title.bearing;

            this.Rectangulo.MainWindow = window;

            //this.txt_nombre.Text = title.name;
            //this.txt_altura.Text = title.altitude;
            //this.txt_rumbo.Text = title.bearing.ToString();
            Popup = new Popup();
            Label = new Label();

            this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);

            this.Rectangulo.MouseLeave -= Rectangulo_MouseLeave;
            this.Rectangulo.PreviewMouseMove -= Rectangulo_PreviewMouseMove;

            

            //this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
            //this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
            //this.MouseMove += new MouseEventHandler(CustomMarkerDemo_MouseMove);
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
            //this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);



            //Popup.Placement = PlacementMode.Mouse;
            //{
            //   Label.Background = Brushes.Blue;
            //   Label.Foreground = Brushes.White;
            //   Label.BorderBrush = Brushes.WhiteSmoke;
            //   Label.BorderThickness = new Thickness(2);
            //   Label.Padding = new Thickness(5);
            //   Label.FontSize = 22;
            //   Label.Content = title;
            //}
            //Popup.Child = Label;
        }

        
        private void MainMap_OnMapZoomChanged()
        {
            if (this.MainWindow.MainMap.Zoom == 12)
            {
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Hidden;
            }
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
                Marker.Position = MainWindow.MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
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
            Popup.IsOpen = false;
        }

        void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker.ZIndex += 10000;
            Popup.IsOpen = true;
        }

        private bool _isMoving;
        private Point? _buttonPosition;
        private double deltaX;
        private double deltaY;
        private TranslateTransform _currentTT;

        private void Rectangulo_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                

                return;
            }

            this.Rectangulo.PreviewMouseMove += Rectangulo_PreviewMouseMove;
            this.Rectangulo.MouseLeave -= Rectangulo_MouseLeave;

            if (_buttonPosition == null)
                _buttonPosition = Rectangulo.TransformToAncestor(MyGrid).Transform(new Point(0, 0));
            var mousePosition = Mouse.GetPosition(MyGrid);
            deltaX = mousePosition.X - _buttonPosition.Value.X;
            deltaY = mousePosition.Y - _buttonPosition.Value.Y;
            _isMoving = true;
        }

       
        private void Rectangulo_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentTT = Rectangulo.RenderTransform as TranslateTransform;
            _isMoving = false;

            this.Rectangulo.MouseLeave += Rectangulo_MouseLeave;
            this.Rectangulo.PreviewMouseMove -= Rectangulo_PreviewMouseMove;
        }

        private void Rectangulo_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMoving) return;

            var mousePoint = Mouse.GetPosition(this.MyGrid);

            var offsetX = (_currentTT == null ? _buttonPosition.Value.X : _buttonPosition.Value.X - _currentTT.X) + deltaX - mousePoint.X;
            var offsetY = (_currentTT == null ? _buttonPosition.Value.Y : _buttonPosition.Value.Y - _currentTT.Y) + deltaY - mousePoint.Y;

           

            this.Rectangulo.RenderTransform = new TranslateTransform(-offsetX, -offsetY);

            m_Linea.X1 = (_buttonPosition.Value.X + Rectangulo.Width /  2)  - offsetX;
            m_Linea.Y1 = (_buttonPosition.Value.Y + Rectangulo.Height / 2) - offsetY;

            Vector? PuntoRectangulo = Matematicas.Matematicas.IntersecRectangulo( Rectangulo.Height, Rectangulo.Width, new Point(0.0, 0.0), new Point(m_Linea.X1 - 10 , m_Linea.Y1 - 10));
            m_Linea.X1 = PuntoRectangulo.Value.X;
            m_Linea.Y1 = PuntoRectangulo.Value.Y;

            Point circulo = Matematicas.Matematicas.RadioCirculo(10, new Point(0.0, 0.0), new Point(m_Linea.X1, m_Linea.Y1));

            m_Linea.X2 = circulo.X;
            m_Linea.Y2 = circulo.Y;
        }

        private void Rectangulo_MouseLeave(object sender, MouseEventArgs e)
        {
            _isMoving = false;
            this.Rectangulo.MouseLeave -= Rectangulo_MouseLeave;
        }

        private void Rectangulo_RaiseCustomEvent(object sender, FlightRadarData e)
        {
            this.MainWindow.MainWindow_RaiseCustomEvent(sender, e);
        }
    }
}