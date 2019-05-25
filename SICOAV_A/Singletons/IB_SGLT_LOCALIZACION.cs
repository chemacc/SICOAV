using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SICOAV_A.Singletons
{
   
    public sealed class IB_SGLT_LOCALIZACION
    {
        private static IB_SGLT_LOCALIZACION instance = null;
        private static readonly object padlock = new object();
        

        private static string m_CultureName;
        private static string m_CultureUIName;
        private static string m_CultureInstaleUIName;
        private static string m_ISORegionName;
        private static uint m_LCID;
        
        private IB_SGLT_LOCALIZACION() { }

        public static IB_SGLT_LOCALIZACION Instance
        {

            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IB_SGLT_LOCALIZACION();

                        Thread.CurrentThread.CurrentCulture.ClearCachedData();
                        Thread.CurrentThread.CurrentUICulture.ClearCachedData();
                        var thread = new Thread(() => ((Action)(() =>
                        {
                            m_CultureName = Thread.CurrentThread.CurrentCulture.Name;
                            m_CultureUIName = Thread.CurrentThread.CurrentUICulture.Name;
                            m_CultureInstaleUIName =  CultureInfo.InstalledUICulture.Name;
                            m_ISORegionName = RegionInfo.CurrentRegion.ThreeLetterISORegionName;
                            m_LCID = GetSystemDefaultLCID();

                        }))());
                        thread.Start();
                        thread.Join();
                    }
                    return instance;
                }
            }
        }

        public static string CultureName { get => m_CultureName; set => m_CultureName = value; }
        public static string CultureUIName { get => m_CultureUIName; set => m_CultureUIName = value; }
        public static string CultureInstaleUIName { get => m_CultureInstaleUIName; set => m_CultureInstaleUIName = value; }
        public static string ISORegionName { get => RegionInfo.CurrentRegion.ThreeLetterISORegionName;  }
        public static uint LCID { get => GetSystemDefaultLCID();}

        #region Constants

        private const int GEO_FRIENDLYNAME = 8;

        #endregion

        #region Private Enums

        private enum GeoClass : int
        {
            Nation = 16,
            Region = 14,
        };

        #endregion

        #region Win32 Declarations

        [DllImport("kernel32.dll", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int GetUserGeoID(GeoClass geoClass);

        [DllImport("kernel32.dll")]
        private static extern int GetUserDefaultLCID();

        [DllImport("kernel32.dll")]
        private static extern int GetGeoInfo(int geoid, int geoType, System.Text.StringBuilder lpGeoData, int cchData, int langid);

        [DllImport("kernel32.dll")]
        private static extern uint GetSystemDefaultLCID();

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns machine current location as specified in Region and Language settings.
        /// </summary>
        /// <param name="geoFriendlyname"></param>
        public static string GetMachineCurrentLocation(int geoFriendlyname)
        {
            int geoId = GetUserGeoID(GeoClass.Nation); ;
            int lcid = GetUserDefaultLCID();
            StringBuilder locationBuffer = new StringBuilder(100);
            GetGeoInfo(geoId, geoFriendlyname, locationBuffer, locationBuffer.Capacity, lcid);

            return locationBuffer.ToString().Trim();
        }

        #endregion

       

    }
}
