using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOAV_A.Ficheros
{
    public class IB_FICH_PARSE_GEO_SACTA
    {
        string m_Path;
        List<PointLatLng> points;
        Recursos.Map m_MainMap;

        public IB_FICH_PARSE_GEO_SACTA(string path, Recursos.Map mainMap)
        {
            m_Path = path;
            m_MainMap = mainMap;

        }

        public static double ConvertDegreeAngleToDouble(double degrees, double minutes, double seconds)
        {
            //Decimal degrees = 
            //   whole number of degrees, 
            //   plus minutes divided by 60, 
            //   plus seconds divided by 3600

            return degrees + (minutes / 60) + (seconds / 3600);
        }

        public bool Ejecutar()
        {
            string line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(m_Path);
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(m_Path + ".gps");
                //Read the first line of text
                line = sr.ReadLine();
                points = new List<PointLatLng>();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    if (line.Length == 26)
                    {
                        // 201522N0021557E
                        //write the lie to console window
                        double LAT = 0.0;
                        string LAT_grados = line.Substring(0, 2);
                        string LAT_minutos = line.Substring(2, 2);
                        string LAT_segundos = line.Substring(4, 6);
                        string LAT_Simbol = line.Substring(11, 1);
                        if (LAT_Simbol == "N")
                        {
                            LAT = ConvertDegreeAngleToDouble(double.Parse(LAT_grados), double.Parse(LAT_minutos), double.Parse(LAT_segundos));
                        }

                        double LON = 0.0;
                        string LON_grados = line.Substring(13, 3);
                        string LON_minutos = line.Substring(16, 2);
                        string LON_segundos = line.Substring(18, 6);
                        string LON_Simbol = line.Substring(25, 1);
                        if (LON_Simbol == "W")
                        {
                            LON = ConvertDegreeAngleToDouble(double.Parse(LON_grados), double.Parse(LON_minutos), double.Parse(LON_segundos));
                            LON = LON * -1;
                        }
                        else
                        {
                            LON = ConvertDegreeAngleToDouble(double.Parse(LON_grados), double.Parse(LON_minutos), double.Parse(LON_segundos));
                        }
                        
                        points.Add(new PointLatLng(LAT, LON));

                    }
                    else
                    {
                        if (points.Count >= 2)
                        {
                            GMapPolygon polygon = new GMapPolygon(points);
                            polygon.RegenerateShape(m_MainMap);
                            m_MainMap.Markers.Add(polygon);

                            points = new List<PointLatLng>();
                        }
                    }
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                sw.Close();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
                
            }

            
        }
    }
}
