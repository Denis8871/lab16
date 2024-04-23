using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_16_OOP_Dikiy
{
    public partial class Form1 : Form
    {
        private List<Rectangle> rectangles = new List<Rectangle>();
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            pictureBox1.Paint += pictureBox1_Paint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = random.Next(0, pictureBox1.Width);
            int y = random.Next(0, pictureBox1.Height);
            int width = random.Next(20, 100);
            int height = random.Next(20, 100);
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            Rectangle rectangle = new Rectangle(new Point(x, y), new Size(width, height), color);
            rectangles.Add(rectangle);

            listBox1.Items.Add("Прямокутник " + rectangles.Count); // Додаємо запис про прямокутник у listBox

            pictureBox1.Refresh(); // Явно викликаємо перерисовку pictureBox1
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var rectangle in rectangles)
            {
                // Задаємо колір контура прямокутника
                Pen pen = Pens.Black;
                // Задаємо колір заливки прямокутника
                SolidBrush brush = new SolidBrush(rectangle.Color);

                // Малюємо прямокутник з заданими кольорами контура та заливки
                g.FillRectangle(brush, rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width, rectangle.Size.Height);
                g.DrawRectangle(pen, rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width, rectangle.Size.Height);
            }

            // Перевіряємо перетин прямокутників по індексах textBox7 і textBox8
            int index1, index2;
            if (int.TryParse(textBox7.Text, out index1) && int.TryParse(textBox8.Text, out index2))
            {
                index1--; // Зменшуємо індекси на 1, так як вони починаються з 1 у listBox
                index2--;

                if (index1 >= 0 && index1 < rectangles.Count && index2 >= 0 && index2 < rectangles.Count)
                {
                    Rectangle rectangle1 = rectangles[index1];
                    Rectangle rectangle2 = rectangles[index2];

                    // Перевіряємо перетин прямокутників
                    Rectangle intersection = rectangle1.Intersection(rectangle2);
                    if (intersection != null)
                    {
                        // Малюємо червоний прямокутник на перетині двох прямокутників
                        g.FillRectangle(Brushes.Red, intersection.Location.X, intersection.Location.Y, intersection.Size.Width, intersection.Size.Height);
                    }
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Отримання обраного прямокутника
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < rectangles.Count)
            {
                Rectangle selectedRectangle = rectangles[selectedIndex];

                // Відображення елементів управління при виборі прямокутника
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;
                textBox8.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;


                // Встановлення поточних значень координат і розмірів прямокутника в textBox
                textBox1.Text = selectedRectangle.Size.Width.ToString(); // Ширина
                textBox2.Text = selectedRectangle.Size.Height.ToString(); // Висота
                textBox3.Text = selectedRectangle.Location.X.ToString(); // X
                textBox4.Text = selectedRectangle.Location.Y.ToString(); // Y
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Зміна розміру прямокутника
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < rectangles.Count)
            {
                int newWidth, newHeight;
                if (int.TryParse(textBox1.Text, out newWidth) && int.TryParse(textBox2.Text, out newHeight))
                {
                    Rectangle selectedRectangle = rectangles[selectedIndex];

                    // Перевіряємо, чи не виходить прямокутник за межі pictureBox1
                    if (selectedRectangle.Location.X + newWidth <= pictureBox1.Width && selectedRectangle.Location.Y + newHeight <= pictureBox1.Height)
                    {
                        selectedRectangle.Resize(newWidth, newHeight);
                        pictureBox1.Refresh(); // Перерисовуємо pictureBox1
                    }
                    else
                    {
                        MessageBox.Show("Прямокутник виходить за межі області!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Некоректне значення для ширини та/або висоти.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Зміна положення прямокутника
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < rectangles.Count)
            {
                int newX, newY;
                if (int.TryParse(textBox3.Text, out newX) && int.TryParse(textBox4.Text, out newY))
                {
                    Rectangle selectedRectangle = rectangles[selectedIndex];

                    // Перевіряємо, чи не виходить прямокутник за межі pictureBox1
                    if (newX >= 0 && newY >= 0 && newX + selectedRectangle.Size.Width <= pictureBox1.Width && newY + selectedRectangle.Size.Height <= pictureBox1.Height)
                    {
                        selectedRectangle.Move(newX - selectedRectangle.Location.X, newY - selectedRectangle.Location.Y);
                        pictureBox1.Refresh(); // Перерисовуємо pictureBox1
                    }
                    else
                    {
                        MessageBox.Show("Прямокутник виходить за межі області!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                    }
                }
                else
                {
                    MessageBox.Show("Некоректне значення для координат X та/або Y.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // Парсимо індекси обраних прямокутників з textBox5 і textBox6
            int index1, index2;
            if (!int.TryParse(textBox5.Text, out index1) || !int.TryParse(textBox6.Text, out index2))
            {
                MessageBox.Show("Введіть коректні числові значення для індексів прямокутників.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Перевіряємо, що індекси належать допустимому діапазону
            if (index1 <= 0 || index1 > rectangles.Count || index2 <= 0 || index2 > rectangles.Count)
            {
                MessageBox.Show("Введені недійсні індекси прямокутників.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Створюємо прямокутник, що містить обрані прямокутники
            Rectangle[] selectedRectangles = { rectangles[index1 - 1], rectangles[index2 - 1] };
            Rectangle enclosingRectangle = Rectangle.CreateEnclosingRectangle(selectedRectangles);

            // Додаємо створений прямокутник до списку
            rectangles.Add(enclosingRectangle);

            // Додаємо запис про прямокутник у listBox
            listBox1.Items.Add("Прямокутник " + rectangles.Count);

            // Перерисовуємо pictureBox
            pictureBox1.Refresh();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // Перевіряємо, що індекси обрані коректно
            int index1, index2;
            if (int.TryParse(textBox7.Text, out index1) && int.TryParse(textBox8.Text, out index2))
            {
                index1--; // Зменшуємо індекси на 1, так як вони починаються з 1 у listBox
                index2--;

                if (index1 >= 0 && index1 < rectangles.Count && index2 >= 0 && index2 < rectangles.Count)
                {
                    Rectangle rectangle1 = rectangles[index1];
                    Rectangle rectangle2 = rectangles[index2];

                    // Перевіряємо перетин прямокутників
                    Rectangle intersection = rectangle1.Intersection(rectangle2);
                    if (intersection != null)
                    {
                        // Створюємо новий прямокутник на перетині двох прямокутників і додаємо його до списку
                        Rectangle intersectingRectangle = new Rectangle(intersection.Location, intersection.Size, Color.Red);
                        rectangles.Add(intersectingRectangle);
                        listBox1.Items.Add("Прямокутник " + rectangles.Count);
                        // Перерисовуємо pictureBox1, щоб відобразити новий прямокутник
                        pictureBox1.Refresh();

                        // Виводимо повідомлення про перетин прямокутників
                        label7.Text = "Перетинаються";
                    }
                    else
                    {
                        label7.Text = "Не перетинаються";
                    }
                }
                else
                {
                    MessageBox.Show("Некоректний індекс прямокутника.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Введіть коректні індекси прямокутників.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
