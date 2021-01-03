using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Chip8Console.Video
{
    public class WindowsVideo : Form, IDisplay
    {

        private readonly IGPU gpu;
        private readonly Bitmap screen;
        private readonly PictureBox pictureBox;
        private bool isLocked;

        public WindowsVideo(IGPU gpu)
        {
            this.gpu = gpu;
            screen = new Bitmap(64, 32);

            pictureBox = new MyBox(this)
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "Screen",
                Size = new Size(screen.Width * 10, screen.Height * 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabIndex = 0,
                TabStop = false
            };

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = pictureBox.Size;

            Controls.Add(pictureBox);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Screen";
            Text = "Chip8Console";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);

            pictureBox.Image = screen;

        }

        public void Draw()
        {
            isLocked = true;
            var bits = screen.LockBits(new Rectangle(0, 0, screen.Width, screen.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* pointer = (byte*)bits.Scan0;

                for (var y = 0; y < screen.Height; y++)
                {
                    for (var x = 0; x < screen.Width; x++)
                    {
                        var index = x + (y * gpu.Columns);
                        pointer[0] = 0; // Blue
                        pointer[1] = gpu.Read((ushort)index) > 0 ? (byte)0x64 : (byte)0; // Green
                        pointer[2] = 0; // Red
                        pointer[3] = 255; // Alpha

                        pointer += 4; // 4 bytes per pixel
                    }
                }
            }

            screen.UnlockBits(bits);
            isLocked = false;
            Invoke((Action)pictureBox.Refresh);
            
        }

        private class MyBox : PictureBox
        {
            private readonly WindowsVideo video;

            public MyBox(WindowsVideo video)
            {
                this.video = video;
            }

            protected override void OnPaint(PaintEventArgs paintEventArgs)
            {
                if (video.isLocked) return;

                paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                paintEventArgs.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

                base.OnPaint(paintEventArgs);
            }
        }

    }

}
