using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCalc;

namespace MathFunction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            // Получаем функцию от пользователя
            string function = textBoxFunction.Text;

            // Создаем битмап и объект Graphics
            Bitmap bitmap = new Bitmap(pictureBoxGraph.Width, pictureBoxGraph.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Задаем систему координат и масштабирование
            double minX = -10;
            double maxX = 10;
            double minY = -10;
            double maxY = 10;

            double scaleX = bitmap.Width / (maxX - minX);
            double scaleY = bitmap.Height / (maxY - minY);

            // Создаем перо для рисования (можете выбрать другой цвет)
            Pen pen = new Pen(Color.Black);

            double step = 0.01; // Шаг для построения точек

            for (double x = minX; x <= maxX; x += step)
            {
                // Здесь вычисляем y в зависимости от x и функции пользователя
                double y = CalculateY(x, function);

                // Переводим координаты и рисуем точку
                int xPix = (int)((x - minX) * scaleX);
                int yPix = (int)((maxY - y) * scaleY);
                graphics.DrawRectangle(pen, xPix, yPix, 1, 1);
            }

            // Отображаем график на PictureBox
            pictureBoxGraph.Image = bitmap;
        }

        private double CalculateY(double x, string function)
        {
            // Здесь используем библиотеку NCalc для вычисления значения функции
            NCalc.Expression expression = new NCalc.Expression(function);
            expression.Parameters["x"] = x;
            double y = (double)expression.Evaluate();
            return y;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Очистить текстовое поле с функцией
            textBoxFunction.Clear();

            // Очистить PictureBox с графиком
            pictureBoxGraph.Image = null;
        }
    }
}
