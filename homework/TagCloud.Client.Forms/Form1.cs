using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TagCloud.Core;
using TagCloud.Core.Layouters;
using TagCloud.Core.Math;
using Rectangle = System.Drawing.Rectangle;

namespace TagCloud.Client.Forms
{
    public partial class Form1 : Form
    {
        private readonly List<RectangleView> _rectangles = new List<RectangleView>();
        private CircularCloudLayouter _cloudLayouter = new CircularCloudLayouter(new Vector(860, 540));
        private readonly ColorGenerator _colorGenerator = new ColorGenerator(Random);
        private static readonly Random Random = new Random(10);

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Size = new Size(1920, 1080);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            foreach (RectangleView rectangleView in _rectangles)
            {
                g.FillRectangle(new SolidBrush(rectangleView.Color), rectangleView.Rectangle);
                g.DrawRectangle(new Pen(Color.Black, 1), rectangleView.Rectangle);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _cloudLayouter = new CircularCloudLayouter(new Vector(860, 540));
            _rectangles.Clear();
            
            for (int i = 0; i < 40; i++)
            {
                Rectangle rect = _cloudLayouter.PutNextRectangle(new Vector(Random.Next(50, 150), Random.Next(20, 50))).ToRectangle();
                _rectangles.Add(new RectangleView(rect, _colorGenerator.GetRandomColor()));
            }
            
            Invalidate();
        }
    }
}
