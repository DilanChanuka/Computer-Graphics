using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Point[] points = new Point[100];
        int count = 0;

        private void DrawPoint(float x,float y)
        {
            Graphics g = panel1.CreateGraphics();
            Pen myPen = new Pen(Color.Red, 2);
            g.DrawLine(myPen, x, y, x + 0.5F, y);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DrawPoint(e.X, e.Y);
                points[count].X = e.X;
                points[count].Y = e.Y;
                count++;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (sfd_1.ShowDialog() == DialogResult.OK)
            {
                string txt = "";

                for (int i = 0; i < count; i++)
                {
                    txt += points[i].X + "," + points[i].Y + "\n";                    
                }
                File.WriteAllText(sfd_1.FileName, txt);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            count = 0;
            panel1.Refresh();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            if (ofd_1.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(ofd_1.FileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] split = lines[i].Split(',');
                    points[count].X = int.Parse(split[0]);
                    points[count].Y = int.Parse(split[1]);
                    DrawPoint(points[count].X, points[count].Y);
                    count++;
                }
            }
        }

        private void btn_draw_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            
            for (int i = 1; i < count; i++)
            {
                g.DrawLine(Pens.BlueViolet,points[i-1].X,points[i-1].Y,points[i].X,points[i].Y);
            }
        }
        // "D:\\SC10278\\test.txt" //obsolte path
        // "test.txt"  //relative path
        private void btn_plus_Click(object sender, EventArgs e)
        {
            points[count].X = int.Parse(txt_x.Text);
            points[count].Y = int.Parse(txt_y.Text);

            DrawPoint(points[count].X, points[count].Y);
            count++;
            txt_x.Clear();
            txt_y.Clear();
        }
    }
}
