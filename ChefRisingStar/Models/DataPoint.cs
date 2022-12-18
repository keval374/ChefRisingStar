using System.Drawing;

namespace ChefRisingStar.Models
{
    public class DataPoint
    {
        public string Name { get; }
        public Color Color { get; }
        public float[] Values { get; }
        public Point Point { get; }

        public PointF[] Points = null;

        public DataPoint(string name, Color color, params float[] values)
        {
            Name = name;
            Color = color;
            Values = (float[])values.Clone();
        }
    }
}
