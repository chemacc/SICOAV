using GMap.NET;
using SICOAV_A.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SICOAV_A.Matematicas
{
    public sealed class Matematicas
    {

        private static Matematicas instance = null;
        private static readonly object padlock = new object();

        private Matematicas() { }

        public static Matematicas Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Matematicas();

                    return instance;
                }
            }
        }

        public static Point RadioCirculo(double p_radio, int alpha, Point p_inicio)
        {
            double r_ = p_radio;
            
            
            double beta = alpha * Math.PI / 180.0;

            double x_ini = (p_inicio.X) + r_ * Math.Cos(beta);
            double y_ini = (p_inicio.Y) + r_ * Math.Sin(beta);
           
            return new Point(x_ini, y_ini);
        }

        public static Point RadioCirculo (double p_radio, Point p_inicio, Point p_final)
        {
            double r_ = p_radio;
            double alpha = Math.Atan2(p_final.Y - p_inicio.Y, p_final.X - p_inicio.X);
            double offset = 0;
            double beta = alpha;
            double x_ = p_final.X - p_inicio.X;
            double y_ = p_final.Y - p_inicio.Y;

            if (x_ >= 0 && y_ >= 0 || x_ > 0 && y_ < 0)
            {
                beta = alpha - offset * Math.PI / 180.0;
            }
            if (x_ < 0 && y_ > 0 || x_ < 0 && y_ < 0)
            {
                beta = alpha + offset * Math.PI / 180.0;
            }

            double x_ini = (p_inicio.X) + r_ * Math.Cos(beta);
            double y_ini = (p_inicio.Y) + r_ * Math.Sin(beta);
            //double x_fin = (xgDest) + r_ * Math.Cos(beta);
            //double y_fin = (ygDest) + r_ * Math.Sin(beta);

            return new Point(x_ini, y_ini);

        }

        public static Vector? PuntoRectangulo(Vector RP1, Vector RP2, Vector RP3, Vector RP4, Vector RA, Vector RB)
        {
            Vector? A1 = Intersects(RP1, RP2, RA, RB);
            if (A1 != null) 
                return A1;

            A1 = Intersects(RP2, RP3, RA, RB);
            if (A1 != null)
                return A1;

            A1 = Intersects(RP3, RP4, RA, RB);
            if (A1 != null)
                return A1;

            A1 = Intersects(RP4, RP1, RA, RB);
            if (A1 != null)
                return A1;

            return RA;


        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static Vector? Intersects(Vector a1, Vector a2, Vector b1, Vector b2)
        {
            Vector b = a2 - a1;
            Vector d = b2 - b1;
            var bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return null;

            Vector c = b1 - a1;
            var t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
            {
                return null;
            }

            var u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
            {
                return null;
            }

            return a1 + t * b;
        }

        private static Shape DrawLinkArrow(Point p1, Point p2)
        {
            GeometryGroup lineGroup = new GeometryGroup();
            double theta = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            Point p = new Point(p1.X + ((p2.X - p1.X) / 1.35), p1.Y + ((p2.Y - p1.Y) / 1.35));
            pathFigure.StartPoint = p;

            Point lpoint = new Point(p.X + 6, p.Y + 15);
            Point rpoint = new Point(p.X - 6, p.Y + 15);
            LineSegment seg1 = new LineSegment();
            seg1.Point = lpoint;
            pathFigure.Segments.Add(seg1);

            LineSegment seg2 = new LineSegment();
            seg2.Point = rpoint;
            pathFigure.Segments.Add(seg2);

            LineSegment seg3 = new LineSegment();
            seg3.Point = p;
            pathFigure.Segments.Add(seg3);

            pathGeometry.Figures.Add(pathFigure);
            RotateTransform transform = new RotateTransform();
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            pathGeometry.Transform = transform;
            lineGroup.Children.Add(pathGeometry);

            LineGeometry connectorGeometry = new LineGeometry();
            connectorGeometry.StartPoint = p1;
            connectorGeometry.EndPoint = p2;
            lineGroup.Children.Add(connectorGeometry);
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.Data = lineGroup;
            path.StrokeThickness = 2;
            path.Stroke = path.Fill = Brushes.Black;

            return path;
        }

        internal static Vector? IntersecRectangulo(double height, double width, Point point1, Point point2)
        {
            double A = height / 2;
            double B = width / 2;

            double CX = point2.X;
            double CY = point2.Y;

            double AX = point1.X;
            double AY = point1.Y;

            Vector A1 = new Vector(CX - B, CY + A);
            Vector A2 = new Vector(CX + B, CY + A);
            Vector A3 = new Vector(CX + B, CY - A);
            Vector A4 = new Vector(CX - B, CY - A);

            Vector P1 = new Vector(CX, CY);
            Vector P2 = new Vector(AX, AY);

            return Matematicas.PuntoRectangulo(A1, A2, A3, A4, P1, P2);
        }

        #region - Determinar si un punto está dentro de un poligono - 
        public static bool PointInPolygon(List<Point> Points, double X, double Y)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = Points.Count - 1;
            float total_angle = GetAngle(
                Points[max_point].X, Points[max_point].Y,
                X, Y,
                Points[0].X, Points[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    Points[i].X, Points[i].Y,
                    X, Y,
                    Points[i + 1].X, Points[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 0.000001);
        }

        // Return the angle ABC.
        // Return a value between PI and -PI.
        // Note that the value is the opposite of what you might
        // expect because Y coordinates increase downward.
        private static float GetAngle(double Ax, double Ay,
            double Bx, double By, double Cx, double Cy)
        {
            // Get the dot product.
            double dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            double cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(cross_product, dot_product);
        }

        // Return the dot product AB · BC.
        // Note that AB · BC = |AB| * |BC| * Cos(theta).
        private static double DotProduct(double Ax, double Ay,
            double Bx, double By, double Cx, double Cy)
        {
            // Get the vectors' coordinates.
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        // Return the cross product AB x BC.
        // The cross product is a vector perpendicular to AB
        // and BC having length |AB| * |BC| * Sin(theta) and
        // with direction given by the right-hand rule.
        // For two vectors in the X-Y plane, the result is a
        // vector with X and Y components 0 so the Z component
        // gives the vector's length and direction.
        private static double CrossProductLength(double Ax, double Ay,
            double Bx, double By, double Cx, double Cy)
        {
            // Get the vectors' coordinates.
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        #endregion - Determinar si un punto está dentro de un poligono -

        #region - Determinar el area de un polígono 

        // Return the polygon's area in "square units."
        // The value will be negative if the polygon is
        // oriented clockwise.
        private static Double SignedPolygonArea(List<Point> Points)
        {
            // Add the first point to the end.
            int num_points = Points.Count;
            Point[] pts = new Point[num_points + 1];
            Points.CopyTo(pts, 0);
            pts[num_points] = Points[0];

            // Get the areas.
            Double area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

        // Return the polygon's area in "square units."
        public static double PolygonArea(List<Point> Points)
        {
            // Return the absolute value of the signed area.
            // The signed area is negative if the polyogn is
            // oriented clockwise.
            return Math.Abs(SignedPolygonArea(Points));
        }

        #endregion - Determinar el área de un polígono

        #region - Determinar el centro de un poligono 
        // Find the polygon's centroid.
        public Point FindCentroid(List<Point> Points)
        {
            // Add the first point at the end of the array.
            int num_points = Points.Count;
            Point[] pts = new Point[num_points + 1];
            Points.CopyTo(pts, 0);
            pts[num_points] = Points[0];

            // Find the centroid.
            double X = 0;
            double Y = 0;
            double second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor =
                    pts[i].X * pts[i + 1].Y -
                    pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            double polygon_area = PolygonArea(Points);
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0)
            {
                X = -X;
                Y = -Y;
            }

            return new Point(X, Y);
        }

        #endregion - Determinar el centro de un poligono 

        #region - Determinar la interseccion de dos lineas
        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        public static void FindIntersection(Point p1, Point p2, Point p3, Point p4,
            out bool lines_intersect, out bool segments_intersect,
            out Point intersection, out Point close_p1, out Point close_p2)
        {
            // Get the segments' parameters.
            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);
            double t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            if (double.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new Point(float.NaN, float.NaN);
                close_p1 = new Point(float.NaN, float.NaN);
                close_p2 = new Point(float.NaN, float.NaN);
                return;
            }
            lines_intersect = true;

            double t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new Point(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new Point(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new Point(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }
        #endregion - Determinar la interseccion de dos lineas.

        #region - Determinar la distancia minima entre dos lineas

        // Return the shortest distance between the two segments
        // p1 --> p2 and p3 --> p4.
        public static double FindDistanceBetweenSegments(
            Point p1, Point  p2, Point p3, Point p4,
            out Point close1, out Point close2)
        {
            // See if the segments intersect.
            bool lines_intersect, segments_intersect;
            Point intersection;
            FindIntersection(p1, p2, p3, p4,
                out lines_intersect, out segments_intersect,
                out intersection, out close1, out close2);
            if (segments_intersect)
            {
                // They intersect.
                close1 = intersection;
                close2 = intersection;
                return 0;
            }

            // Find the other possible distances.
            Point closest;
            double best_dist = double.MaxValue, test_dist;

            // Try p1.
            test_dist = FindDistanceToSegment(p1, p3, p4, out closest);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
                close1 = p1;
                close2 = closest;
            }

            // Try p2.
            test_dist = FindDistanceToSegment(p2, p3, p4, out closest);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
                close1 = p2;
                close2 = closest;
            }

            // Try p3.
            test_dist = FindDistanceToSegment(p3, p1, p2, out closest);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
                close1 = closest;
                close2 = p3;
            }

            // Try p4.
            test_dist = FindDistanceToSegment(p4, p1, p2, out closest);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
                close1 = closest;
                close2 = p4;
            }

            return best_dist;
        }

        // Calculate the distance between
        // point pt and the segment p1 --> p2.
        private static double FindDistanceToSegment(Point pt, Point p1, Point p2, out Point closest)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            double t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new Point(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new Point(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new Point(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }

        #endregion

        #region - Determinar los puntos de corte de un poligono y una recta

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        public static void FindIntersection(PointLatLng p1, PointLatLng p2, PointLatLng p3, PointLatLng p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointLatLng intersection, out PointLatLng close_p1, out PointLatLng close_p2)
        {
            // Get the segments' parameters.
            double dx12 = p2.Lat - p1.Lat;
            double dy12 = p2.Lng - p1.Lng;
            double dx34 = p4.Lat - p3.Lat;
            double dy34 = p4.Lng - p3.Lng;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);
            double t1 = ((p1.Lat - p3.Lat) * dy34 + (p3.Lng - p1.Lng) * dx34) / denominator;
            if (double.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointLatLng(double.NaN, double.NaN);
                close_p1 = new PointLatLng(double.NaN, double.NaN);
                close_p2 = new PointLatLng(double.NaN, double.NaN);
                return;
            }
            lines_intersect = true;

            double t2 = ((p3.Lat - p1.Lat) * dy12 + (p1.Lng - p3.Lng) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new PointLatLng(p1.Lat + dx12 * t1, p1.Lng + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new PointLatLng(p1.Lat + dx12 * t1, p1.Lng + dy12 * t1);
            close_p2 = new PointLatLng(p3.Lat + dx34 * t2, p3.Lng + dy34 * t2);
        }

        #region Puntos 

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private static void FindIntersection(Point p1, Point p2, Point p3, Point p4,
            out bool lines_intersect, out bool segments_intersect,
            out Point intersection, out Point close_p1, out Point close_p2,
            out double t1, out double t2)
        {
            // Get the segments' parameters.
            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);
            t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            if (double.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new Point(float.NaN, float.NaN);
                close_p1 = new Point(float.NaN, float.NaN);
                close_p2 = new Point(float.NaN, float.NaN);
                t2 = float.PositiveInfinity;
                return;
            }
            lines_intersect = true;

            t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new Point(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0) t1 = 0;
            else if (t1 > 1) t1 = 1;

            if (t2 < 0) t2 = 0;
            else if (t2 > 1) t2 = 1;

            close_p1 = new Point(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new Point(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }
        // Return points where the segment enters and leaves the polygon.
        public static Point[] ClipLineWithPolygon(
            out bool starts_outside_polygon,
            Point point1, Point point2,
            List<Point> polygon_points)
        {
            // Make lists to hold points of
            // intersection and their t values.
            List<Point> intersections = new List<Point>();
            List<double> t_values = new List<double>();

            // Add the segment's starting point.
            intersections.Add(point1);
            t_values.Add(0f);
            starts_outside_polygon =
                !PointIsInPolygon(point1.X, point1.Y,
                    polygon_points.ToArray());

            // Examine the polygon's edges.
            for (int i1 = 0; i1 < polygon_points.Count; i1++)
            {
                // Get the end points for this edge.
                int i2 = (i1 + 1) % polygon_points.Count;

                // See where the edge intersects the segment.
                bool lines_intersect, segments_intersect;
                Point intersection, close_p1, close_p2;
                double t1, t2;
                FindIntersection(point1, point2,
                    polygon_points[i1], polygon_points[i2],
                    out lines_intersect, out segments_intersect,
                    out intersection, out close_p1, out close_p2,
                    out t1, out t2);

                // See if the segment intersects the edge.
                if (segments_intersect)
                {
                    // See if we need to record this intersection.

                    // Record this intersection.
                    intersections.Add(intersection);
                    t_values.Add(t1);
                }
            }

            // Add the segment's ending point.
            intersections.Add(point2);
            t_values.Add(1f);

            // Sort the points of intersection by t value.
            Point[] intersections_array = intersections.ToArray();
            double[] t_array = t_values.ToArray();
            Array.Sort(t_array, intersections_array);

            // Return the intersections.
            return intersections_array;
        }

        // Return true if the point is in the polygon.
        public static bool PointIsInPolygon(double X, double Y, Point[] polygon_points)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon_points.Length - 1;
            float total_angle = GetAngle(
                polygon_points[max_point].X, polygon_points[max_point].Y,
                X, Y,
                polygon_points[0].X, polygon_points[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    polygon_points[i].X, polygon_points[i].Y,
                    X, Y,
                    polygon_points[i + 1].X, polygon_points[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 0.000001);
        }
        #endregion Puntos

        public static PointLatLng[] ClipLineWithPolygon(
           out bool starts_outside_polygon,
           PointLatLng point1, PointLatLng point2,
           List<PointLatLng> polygon_points)
        {
            // Make lists to hold points of
            // intersection and their t values.
            List<PointLatLng> intersections = new List<PointLatLng>();
            List<double> t_values = new List<double>();

            // Add the segment's starting point.
            intersections.Add(point1);
            t_values.Add(0d);
            starts_outside_polygon =
                !PointIsInPolygon(point1.Lat, point1.Lng,
                    polygon_points.ToArray());

            // Examine the polygon's edges.
            for (int i1 = 0; i1 < polygon_points.Count; i1++)
            {
                // Get the end points for this edge.
                int i2 = (i1 + 1) % polygon_points.Count;

                // See where the edge intersects the segment.
                bool lines_intersect, segments_intersect;
                PointLatLng intersection, close_p1, close_p2;
                double t1, t2;

                FindIntersection(point1, point2,
                    polygon_points[i1], polygon_points[i2],
                    out lines_intersect, out segments_intersect,
                    out intersection, out close_p1, out close_p2,
                    out t1, out t2);

                // See if the segment intersects the edge.
                if (segments_intersect)
                {
                    // See if we need to record this intersection.

                    // Record this intersection.
                    intersections.Add(intersection);
                    t_values.Add(t1);
                }
            }

            // Add the segment's ending point.
            intersections.Add(point2);
            t_values.Add(1f);

            // Sort the points of intersection by t value.
            PointLatLng[] intersections_array = intersections.ToArray();
            double[] t_array = t_values.ToArray();
            Array.Sort(t_array, intersections_array);

            // Return the intersections.
            return intersections_array;
        }

        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        private static void FindIntersection(PointLatLng p1, PointLatLng p2, PointLatLng p3, PointLatLng p4,
            out bool lines_intersect, out bool segments_intersect,
            out PointLatLng intersection, out PointLatLng close_p1, out PointLatLng close_p2,
            out double t1, out double t2)
        {
            // Get the segments' parameters.
            double dx12 = p2.Lat - p1.Lat;
            double dy12 = p2.Lng - p1.Lng;
            double dx34 = p4.Lat - p3.Lat;
            double dy34 = p4.Lng - p3.Lng;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);
            t1 = ((p1.Lat - p3.Lat) * dy34 + (p3.Lng - p1.Lng) * dx34) / denominator;
            if (double.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new PointLatLng(double.NaN, double.NaN);
                close_p1 = new PointLatLng(double.NaN, double.NaN);
                close_p2 = new PointLatLng(double.NaN, double.NaN);
                t2 = double.PositiveInfinity;
                return;
            }
            lines_intersect = true;

            t2 = ((p3.Lat - p1.Lat) * dy12 + (p1.Lng - p3.Lng) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new PointLatLng(p1.Lat + dx12 * t1, p1.Lng + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0) t1 = 0;
            else if (t1 > 1) t1 = 1;

            if (t2 < 0) t2 = 0;
            else if (t2 > 1) t2 = 1;

            close_p1 = new PointLatLng(p1.Lat + dx12 * t1, p1.Lng + dy12 * t1);
            close_p2 = new PointLatLng(p3.Lat + dx34 * t2, p3.Lng + dy34 * t2);
        }

        // Return true if the point is in the polygon.
        public static bool PointIsInPolygon(double X, double Y, PointLatLng[] polygon_points)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon_points.Length - 1;
            float total_angle = GetAngle(
                polygon_points[max_point].Lat, polygon_points[max_point].Lng,
                X, Y,
                polygon_points[0].Lat, polygon_points[0].Lng);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    polygon_points[i].Lat, polygon_points[i].Lng,
                    X, Y,
                    polygon_points[i + 1].Lat, polygon_points[i + 1].Lng);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 0.000001);
        }
        #endregion - Determinar los puntos de corte de un poligono y una recta
        
        #region - Separar Gemometria

        public static void SplitGeometry(Geometry geo, Point pt1, Point pt2, out List<Point> leftGeo, out List<Point> rightGeo)
        {


            leftGeo = new List<Point>();
            rightGeo = new List<Point>();

            double c = 360.0 + 90.0 - (180.0 / Math.PI * Math.Atan2(pt2.Y - pt1.Y, pt2.X - pt1.X));
            var t = new TransformGroup();
            t.Children.Add(new TranslateTransform(-pt1.X, -pt1.Y));
            t.Children.Add(new RotateTransform(c));
            var i = t.Inverse;
            
            foreach (var figure in geo.GetFlattenedPathGeometry().Figures)
            {
                var lastPt = t.Transform(figure.StartPoint);
                foreach (PolyLineSegment segment in figure.Segments)
                {
                    foreach (var currentPtOrig in segment.Points)
                    {
                        var currentPt = t.Transform(currentPtOrig);
                        ProcessLine(lastPt, currentPt, leftGeo, rightGeo);
                        lastPt = currentPt;
                    }
                }
            }
        }

        public static void SplitGeometry(Geometry geo, Point pt1, Point pt2, out PathGeometry leftGeo, out PathGeometry rightGeo)
        {
            double c = 360.0 + 90.0 - (180.0 / Math.PI * Math.Atan2(pt2.Y - pt1.Y, pt2.X - pt1.X));
            var t = new TransformGroup();
            t.Children.Add(new TranslateTransform(-pt1.X, -pt1.Y));
            t.Children.Add(new RotateTransform(c));
            var i = t.Inverse;
            leftGeo = new PathGeometry();
            rightGeo = new PathGeometry();
            foreach (var figure in geo.GetFlattenedPathGeometry().Figures)
            {
                var left = new List<Point>();
                var right = new List<Point>();
                var lastPt = t.Transform(figure.StartPoint);
                foreach (PolyLineSegment segment in figure.Segments)
                {
                    foreach (var currentPtOrig in segment.Points)
                    {
                        var currentPt = t.Transform(currentPtOrig);
                        ProcessLine(lastPt, currentPt, left, right);
                        lastPt = currentPt;
                    }
                }

                ProcessFigure(left, i, leftGeo);
                ProcessFigure(right, i, rightGeo);
            }
        }

        private static void ProcessFigure(List<Point> points, GeneralTransform transform, PathGeometry geometry)
        {
            if (points.Count == 0) return;
            var result = new PolyLineSegment();
            var prev = points[0];
            for (int i = 1; i < points.Count; ++i)
            {
                var current = points[i];
                if (current == prev) continue;
                result.Points.Add(transform.Transform(current));
                prev = current;
            }
            if (result.Points.Count == 0) return;
            geometry.Figures.Add(new PathFigure(transform.Transform(points[0]), new PathSegment[] { result }, true));
        }

        private static void ProcessLine(Point pt1, Point pt2, List<Point> left, List<Point> right)
        {
            if (pt1.X >= 0 && pt2.X >= 0)
            {
                right.Add(pt1);
                right.Add(pt2);
            }
            else if (pt1.X < 0 && pt2.X < 0)
            {
                left.Add(pt1);
                left.Add(pt2);
            }
            else if (pt1.X < 0)
            {
                double c = (Math.Abs(pt1.X) * Math.Abs(pt2.Y - pt1.Y)) / Math.Abs(pt2.X - pt1.X);
                double y = pt1.Y + c * Math.Sign(pt2.Y - pt1.Y);
                var p = new Point(0, y);
                left.Add(pt1);
                left.Add(p);
                right.Add(p);
                right.Add(pt2);
            }
            else
            {
                double c = (Math.Abs(pt1.X) * Math.Abs(pt2.Y - pt1.Y)) / Math.Abs(pt2.X - pt1.X);
                double y = pt1.Y + c * Math.Sign(pt2.Y - pt1.Y);
                var p = new Point(0, y);
                right.Add(pt1);
                right.Add(p);
                left.Add(p);
                left.Add(pt2);
            }
        }

        #endregion - Separar Geometria



    }
}
