﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms.GoogleMaps.Clustering
{
    public class ClusterOptions
    {
        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <value>The algorithm.</value>
        public ClusterAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Xamarin.Forms.GoogleMaps.Clustering.ClusterOptions"/> enable buckets grouping.
        /// </summary>
        /// <value><c>true</c> if enable buckets; otherwise, <c>false</c>.</value>
        /// <example>
        ///     Instead of 136 pins groups, the cluster will show "100+" (since that's the nearest bucket declared).
        ///     Please note that the usage of buckets is HIGHLY RECOMMENDED, since the system caches the cluster icons.
        ///     So, instead of having 1...N icons being generated, you'll mave maximum {buckets.Length} icons.
        /// </example>
        public bool EnableBuckets { get; set; }

        /// <summary>
        /// Gets the buckets.
        /// </summary>
        /// <value>The buckets.</value>
        /// <example>new int[] { 10, 50, 100, 500, 1000, 5000, 10000 }</example>
        public int[] Buckets { get; private set; }

        /// <summary>
        /// Gets the bucket colors.
        /// </summary>
        /// <value>The bucket colors.</value>
        public Color[] BucketColors { get; set; }

        /// <summary>
        /// Gets or sets the color of the renderer text.
        /// </summary>
        /// <value>The color of the renderer text.</value>
        internal Color RendererTextColor { get; set; }

        /// <summary>
        /// Gets or sets the renderer image.
        /// </summary>
        /// <value>The renderer image.</value>
        internal BitmapDescriptor RendererImage { get; set; }

        /// <summary>
        /// Gets or sets the renderer callback.
        /// </summary>
        /// <value>The renderer callback.</value>
        internal Func<string, BitmapDescriptor> RendererCallback { get; set; }

        public ClusterOptions()
        {
            Algorithm = ClusterAlgorithm.NonHierarchicalDistanceBased;
            EnableBuckets = true;
            RendererImage = null;
            RendererCallback = null;
            ResetBuckets();
        }

        /// <summary>
        /// Sets the buckets.
        /// </summary>
        /// <param name="buckets">Buckets.</param>
        /// <param name="colors">Colors.</param>
        public void SetBuckets(int[] buckets, Color[] colors)
        {
            if (buckets.Length != colors.Length)
                throw new ArgumentException("The buckets length must be equal to the colors length.");

            Buckets = buckets;
            BucketColors = colors;
        }

        /// <summary>
        /// Sets the buckets.
        /// </summary>
        /// <param name="buckets">Buckets.</param>
        public void SetBuckets(Dictionary<int, Color> buckets)
        {
            Buckets = buckets.Keys.ToArray();
            BucketColors = buckets.Values.ToArray();
        }

        /// <summary>
        /// Resets the buckets.
        /// </summary>
        public void ResetBuckets()
        {
            Buckets = new int[] { 10, 50, 100, 200, 1000 };
            BucketColors = new Color[]
            {
                Color.FromHex("#A22244"),
                Color.FromHex("#A22244"),
                Color.FromHex("#A22244"),
                Color.FromHex("#A22244"),
                Color.FromHex("#A22244")
            };

            RendererTextColor = Color.White;
            RendererImage = null;
            RendererCallback = null;
        }

        /// <summary>
        /// Sets the rendering method to use default colors.
        /// </summary>
        public void SetRenderUsingColors()
        {
            SetRenderUsingColors(Color.White);
        }

        /// <summary>
        /// Sets the rendering method to use default colors.
        /// </summary>
        /// <param name="textColor">The text color.</param>
        public void SetRenderUsingColors(Color textColor)
        {
            RendererTextColor = textColor;
            RendererImage = null;
            RendererCallback = null;
        }

        /// <summary>
        /// Sets the rendering method to use custom bitmap image.
        /// </summary>
        /// <param name="image">The BitmapDescriptor image. The number of items will be drawn automatically on top of this image.</param>
        public void SetRenderUsingImage(BitmapDescriptor image)
        {
            SetRenderUsingImage(image, Color.White);
        }

        /// <summary>
        /// Sets the rendering method to use custom bitmap image.
        /// </summary>
        /// <param name="image">The BitmapDescriptor image. The number of items will be drawn automatically on top of this image.</param>
        /// <param name="textColor">The text color.</param>
        public void SetRenderUsingImage(BitmapDescriptor image, Color textColor)
        {
            RendererTextColor = textColor;
            RendererImage = image;
            RendererCallback = null;
        }


        /// <summary>
        /// Sets the rendering method to use custom lambda action.
        /// </summary>
        /// <param name="callback">A callback which recieves the value to display (a string or bucket value) and renders it into a BitmapDescriptor.</param>
        public void SetRenderUsingCustomAction(Func<string, BitmapDescriptor> callback)
        {
            RendererImage = null;
            RendererCallback = callback;
        }
    }
}