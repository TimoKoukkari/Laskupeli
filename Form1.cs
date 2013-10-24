using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laskupeli
{
    public partial class Form1 : Form
    {
        Random r = new Random();

        int vastaus = 0;
        int pisteet = 0;
        int levelCounter = 0;
        int rlevel = 0;

        public Form1()
        {
            InitializeComponent();
            TeeLasku();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Top = label4.Top + 1;
            if (label4.Top == Height)
            {
                timer1.Enabled = false;
                label9.ForeColor = Color.Red;
                label9.Text = "Game over";
                label9.Visible = true;
                textBox1.Enabled = false;
            }
        }

        private void TeeLasku()
        {
            label4.Top = 100;
            label4.Text = "";
            char operand = ' ';

            rlevel = r.Next(0, levelCounter/10 + 1);
            /*  Levelin vaikutus:
             *      0: a + b
             *      1: a - b
             *      2: a * b
             *      3: a / b
             *      4: aa + bb
             *      5: aa - bb
             *      6: aa * b0
             *      7: aa / b0
             */
            int a = 0, b = 0;
            switch (rlevel)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    a = r.Next(10);
                    b = r.Next(1, 10);
                    break;

                case 4:
                case 5:
                    a = r.Next(100);
                    b = r.Next(1, 100);
                    break;

                case 6:
                case 7:
                    a = r.Next(100);
                    b = r.Next(1, 10) * 10;
                    break;
            }  // switch luvut

            switch (rlevel)
            {
                case 0:
                case 4:
                    operand = '+';
                    vastaus = a + b;
                break;

                case 1:
                case 5:
                    operand = '-';
                    vastaus = a - b;
                break;

                case 2:
                case 6:
                    operand = '*';
                    vastaus = a * b;
                break;

                case 3:
                case 7:
                    operand = '/';
                    vastaus = a;
                    a = a * b;
                break;
            } // switch operand

            label4.Text = Convert.ToString(a) + " " + operand + " " +
                Convert.ToString(b);
            timer1.Enabled = true;
        } // TeeLasku

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return) return;
            if (textBox1.Text == "") return;

            int tulos = 0;
            try
            {
                tulos = Convert.ToInt32(textBox1.Text);
            } catch {
                return;
            }

            timer1.Enabled = false;

            int dp = (rlevel + 1) * (Height - label4.Top) * 10;
            if (tulos == vastaus)
            {
                pisteet += dp;
                levelCounter += 1;
                if (timer1.Interval > 5)
                    if (levelCounter % 10 == 0)
                        timer1.Interval -= 5;
                    else
                        timer1.Interval -= 1;
                label8.Text = Convert.ToString(levelCounter / 10);
                label9.ForeColor = Color.Green;
                label9.Text = "+";
            }
            else
            {
                pisteet -= dp;
                label9.ForeColor = Color.Red;
                label9.Text = "-";
            }
            label6.Text = Convert.ToString(pisteet);
            textBox1.Text = "";
            label9.Text += Convert.ToString(dp);
            label9.Visible = true;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label9.Visible = false;
            timer2.Enabled = false;
            TeeLasku();
        }
    }
}
