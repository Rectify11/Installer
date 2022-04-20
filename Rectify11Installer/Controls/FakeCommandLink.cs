using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Controls
{
    public partial class FakeCommandLink : UserControl
    {
        private static readonly Color DefaultText = Color.White;//Color.FromArgb(65, 65, 65);
        private static readonly Color PressedText = Color.FromArgb(96, 96, 96);

        public string Note
        {
            get
            {
                return lblBody.Text;
            }
            set
            {
                lblBody.Text = value;
            }
        }
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        public new EventHandler? Click;

       // bool fadeIn = false;
        public FakeCommandLink()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            foreach (Control control in Controls)
            {
                control.MouseEnter += new EventHandler(TheMouseEnter);
                control.MouseLeave += new EventHandler(TheMouseLeave);
                control.MouseDown += new MouseEventHandler(TheMouseDown);
                control.MouseUp += new MouseEventHandler(TheMouseUp);
                control.Click += new EventHandler(TheMouseClick);
            }
            this.MouseEnter += new EventHandler(TheMouseEnter);
            this.MouseLeave += new EventHandler(TheMouseLeave);
            this.MouseDown += new MouseEventHandler(TheMouseDown);
            this.MouseUp += new MouseEventHandler(TheMouseUp);
            base.Click += new EventHandler(TheMouseClick);

            lblTitle.ForeColor = DefaultText;
            lblBody.ForeColor = DefaultText;
            BackColor = Color.Transparent;

            pictureBox1.BackColor = Color.Transparent;
            lblTitle.BackColor = Color.Transparent;
            lblBody.BackColor = Color.Transparent;

            if (Theme.IsUsingDarkMode)
            {
                pictureBox1.Image = Properties.Resources.Dark_CommandLinkGlyph_Normal; 
            }
        }

        private void TheMouseClick(object? sender, EventArgs e)
        {
            if (Click != null)
                Click.Invoke(sender, e);
        }

        private void TheMouseUp(object? sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                BackColor = Color.Transparent;

                if (Theme.IsUsingDarkMode)
                {
                    pictureBox1.Image = Properties.Resources.Dark_CommandLinkGlyph_Normal;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.CommandLinkGlyph_Normal;
                }
                Color forecolor = Theme.IsUsingDarkMode ? DefaultBackColor : Color.Black;

                lblTitle.ForeColor = forecolor;
                lblBody.ForeColor = forecolor;
            }
        }
        private void TheMouseDown(object? sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                BackColor = Color.FromArgb(64, 20, 20, 20);
                if (Theme.IsUsingDarkMode)
                {
                    pictureBox1.Image = Properties.Resources.Dark_CommandLinkGlyph_Pressed;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.CommandLinkGlyph_Pressed;
                }
              

                lblTitle.ForeColor = PressedText;
                lblBody.ForeColor = PressedText;
            }
        }

        private void TheMouseLeave(object? sender, EventArgs e)
        {
            //fader.Stop();
            //fadeIn = true;
            //fader.Start();
            BackColor = Color.Transparent;
        }

        private void TheMouseEnter(object? sender, EventArgs e)
        {
            //fader.Stop();



            int t = 64;
            if (Theme.IsUsingDarkMode)
                t = 255;
            BackColor = Color.FromArgb(t, 40, 40, 40);
            //fadeIn = false;
            //fader.Start();
        }

        //private void fader_Tick(object sender, EventArgs e)
        //{
            //if (fadeIn)
            //{
            //    var basee = BackColor;

            //    BackColor = Color.FromArgb(basee.R - 1, basee.G - 1, basee.B - 1);

            //    if (BackColor.R <= 0)
            //    {
            //        fader.Stop();
            //    }

            //  //  Wait(10);
            //}
            //else
            //{
            //    var basee = BackColor;// Color.FromArgb(40, 40, 40);

            //    BackColor = Color.FromArgb(basee.R + 1, basee.G + 1, basee.B + 1);

            //    if (BackColor.R >= 40)
            //    {
            //        fader.Stop();
            //    }

            //    Wait(10);
            //}
        //}

        public static void Wait(int time)
        {
            Thread thread = new(delegate ()
            {
                System.Threading.Thread.Sleep(time);
            });
            thread.Start();
            while (thread.IsAlive)
                Application.DoEvents();
        }
    }
}