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
        private double[] x; // Добавляем поле для хранения значений выборки

        public Form1()
        {
            InitializeComponent();
        }

        public void solving(int k)
        {
            int seed = int.Parse(textBox1.Text); // параметр рандома
            Random r = new Random(seed);
            int N = int.Parse(textBox2.Text); // объем выборки

            double lambda = double.Parse(textBox3.Text); // параметр распределения

            r = new Random();
            x = new double[N]; // Инициализируем массив значений выборки
            for (int i = 0; i < N; i++)
            {
                double X = r.NextDouble() * seed;
                x[i] = seed *lambda* Math.Pow(Math.E, -lambda * X);
            }
            double x_min = x.Min();
            double x_max = x.Max();

            double spread = x_max - x_min;

            double delta_x = spread / k;

            //-----------------------------
            double[] intervals = new double[k];
            double delta_xx = delta_x;

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (x[j] < delta_xx && x[j] > delta_xx - delta_x) { intervals[i]++; }
                }
                delta_xx += delta_x;
            }
            delta_xx = delta_x;
            for (int i = 0; i < k; i++)
            {
                chart1.Series[0].Points.AddXY(delta_xx, intervals[i] / N);
                delta_xx += delta_x;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int k = int.Parse(textBox4.Text); // количество интервалов
            solving(k);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int k = int.Parse(textBox4.Text); // количество интервалов

            if (x == null) // Если массив значений выборки не инициализирован
            {
                MessageBox.Show("Сначала нужно построить гистограмму с помощью кнопки 'button1'.");
                return;
            }

            double x_min = x.Min();
            double x_max = x.Max();

            double spread = x_max - x_min;

            double delta_x = spread / k;

            //-----------------------------
            double[] intervals = new double[k];
            double delta_xx = delta_x;

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    if (x[j] < delta_xx && x[j] > delta_xx - delta_x) { intervals[i]++; }
                }
                delta_xx += delta_x;
            }
            delta_xx = delta_x;
            for (int i = 0; i < k; i++)
            {
                chart1.Series[0].Points.AddXY(delta_xx, intervals[i] / x.Length);
                delta_xx += delta_x;
            }
        }
    }
}