using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace terver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            
            //double func = lambda * Math.Pow(Math.E, -x*lambda);
            int seed = int.Parse(textBox1.Text); //параметр рандома
            Random r = new Random(seed);
            int N  = int.Parse(textBox2.Text); // объем выборки
            double spread = r.NextDouble() * seed; // разброс
            double lambda = double.Parse(textBox3.Text); // параметр распределения

            int k = (int)Math.Log(N, 2) + 1; // количество интервалов

            double delta_x = spread / k; // длина интервала

            double[] middle_interval = new double[k]; // середины интервалов
            double delta_xx = delta_x / 2;

            for (int i = 0; i < k; i++) {
                middle_interval[i] = delta_xx;
                delta_xx += delta_x;
            }

            double[] theory = new double[k];  // массив значений ожидаемых количеств событий 

            for (int i = 0; i < k; i++) {
                double n_theory = N * lambda * Math.Pow(Math.E, -middle_interval[i] * lambda)*delta_x;
                theory[i] = n_theory;
            }

            double[] middle_val = new double[k]; // значения в серединах интервалов

            for (int i = 0; i < k; i++)
            {
                middle_val[i] = lambda * Math.Pow(Math.E, -middle_interval[i] * lambda);
            }

            double[] func_middle_val = new double[k]; // вероятности

            for (int i = 0; i < k; i++)
            {
                func_middle_val[i] = lambda * Math.Pow(Math.E, -middle_interval[i] * lambda)*delta_x;
            }

            double summ = 0;
            for (int i = 0;i < k; i++)
            {
                summ += func_middle_val[i];
            }

            if (summ > 0.9 && summ <= 1) {
                MessageBox.Show("Работает корректно");
            }
            double x = delta_xx;
            for (int i = 0; i < k; i++) {
                chart1.Series[0].Points.AddXY(x, func_middle_val[i]);
                x += delta_x;
            }

        }
    }
}
