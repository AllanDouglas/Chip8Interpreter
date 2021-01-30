using System;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
namespace Chip8Console.Video
{
    public class SkiaDisplay : Form, IDisplay
    {
        private const int SCALER = 10;
        private readonly SKControl skiaView;
        private readonly IGPU gpu;
        private readonly SKBitmap bitmap;

        public SkiaDisplay(IGPU gpu)
        {

            SuspendLayout();
            this.gpu = gpu;

            var width = gpu.Columns;
            var height = gpu.Rows;

            this.skiaView = new SKControl();
            this.SuspendLayout();

            var clientSize = new System.Drawing.Size(width * SCALER, height * SCALER);
            bitmap = new SKBitmap(width, height);
            // 
            // skiaView
            // 
            this.skiaView.Dock = DockStyle.Fill;
            this.skiaView.Location = new System.Drawing.Point(0, 0);
            this.skiaView.Name = "skiaView";
            this.skiaView.Size = clientSize;
            this.skiaView.ClientSize = clientSize;
            this.skiaView.TabIndex = 0;
            this.skiaView.Text = "skControl1";
            this.skiaView.PaintSurface += new EventHandler<SKPaintSurfaceEventArgs>(SkiaView_PaintSurface);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = clientSize;
            this.Controls.Add(this.skiaView);
            this.ResumeLayout(false);

        }

        private void SkiaView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            canvas.Scale(SCALER);
            // make sure the canvas is blank
            canvas.Clear(SKColors.Black);

            for (var y = 0; y < gpu.Rows; y++)
            {
                for (var x = 0; x < gpu.Columns; x++)
                {
                    var index = x + (y * gpu.Columns);
                    var bit = gpu.Read((ushort)index);
                    bitmap.SetPixel(x, y, bit > 0 ? SKColors.DarkGreen : SKColors.Black);
                }
            }

            canvas.DrawBitmap(bitmap, new SKPoint(0, 0));
        }
        public void Draw()
        {
            Invoke((Action)this.skiaView.Refresh);
        }
    }
}