﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SICOAV_A.Recursos
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using GMap.NET.WindowsPresentation;
    using System.Globalization;

    using System.Windows;
    using System;
    using GMap.NET;
    using System.Windows.Input;

    /// <summary>
    /// the custom map f GMapControl 
    /// </summary>
    public class Map : GMapControl
    {
        public long ElapsedMilliseconds;

#if DEBUG
        DateTime start;
        DateTime end;
        int delta;

        private int counter;
        internal bool HoldInvalidation;
        readonly Typeface tf = new Typeface("GenericSansSerif");
        readonly System.Windows.FlowDirection fd = new System.Windows.FlowDirection();

        /// <summary>
        /// any custom drawing here
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            //start = DateTime.Now;

            base.OnRender(drawingContext);

            //end = DateTime.Now;
            //delta = (int)(end - start).TotalMilliseconds;

            //FormattedText text = new FormattedText(string.Format(CultureInfo.InvariantCulture, "{0:0.0}", Zoom) + "z, " + MapProvider + ", refresh: " + counter++ + ", load: " + ElapsedMilliseconds + "ms, render: " + delta + "ms", CultureInfo.InvariantCulture, fd, tf, 20, Brushes.Blue);
            //drawingContext.DrawText(text, new Point(text.Height, text.Height));
            //text = null;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

#endif
    }
}
