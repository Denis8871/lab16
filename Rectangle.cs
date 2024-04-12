using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_16_OOP_Shostya
{
    public class Rectangle
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }

        public Rectangle(Point location, Size size, Color color)
        {
            Location = location;
            Size = size;
            Color = color;
        }

        public void Move(int deltaX, int deltaY)
        {
            Location = new Point(Location.X + deltaX, Location.Y + deltaY);
        }

        public void Resize(int newWidth, int newHeight)
        {
            Size = new Size(newWidth, newHeight);
        }

        public Rectangle Union(Rectangle other)
        {
            int x = Math.Min(Location.X, other.Location.X);
            int y = Math.Min(Location.Y, other.Location.Y);
            int width = Math.Max(Location.X + Size.Width, other.Location.X + other.Size.Width) - x;
            int height = Math.Max(Location.Y + Size.Height, other.Location.Y + other.Size.Height) - y;

            return new Rectangle(new Point(x, y), new Size(width, height), Color);
        }

        public Rectangle Intersection(Rectangle other)
        {
            int x = Math.Max(Location.X, other.Location.X);
            int y = Math.Max(Location.Y, other.Location.Y);
            int width = Math.Min(Location.X + Size.Width, other.Location.X + other.Size.Width) - x;
            int height = Math.Min(Location.Y + Size.Height, other.Location.Y + other.Size.Height) - y;

            if (width <= 0 || height <= 0)
                return null; // No intersection

            return new Rectangle(new Point(x, y), new Size(width, height), Color);
        }

        public static Rectangle CreateEnclosingRectangle(Rectangle[] rectangles)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (var rect in rectangles)
            {
                minX = Math.Min(minX, rect.Location.X);
                minY = Math.Min(minY, rect.Location.Y);
                maxX = Math.Max(maxX, rect.Location.X + rect.Size.Width);
                maxY = Math.Max(maxY, rect.Location.Y + rect.Size.Height);
            }

            int width = maxX - minX;
            int height = maxY - minY;

            return new Rectangle(new Point(minX, minY), new Size(width, height), Color.Transparent);
        }
    }
}
