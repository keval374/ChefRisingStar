namespace ChefRisingStar.Models
{
    public class AxisInfo
    {
        public string Name { get; }
        public string Description { get; }
        public float Min { get; }
        public float Max { get; }

        public AxisInfo(string name, string description, float min, float max)
        {
            Name = name;
            Description = description;
            Min = min;
            Max = max;
        }
    }
}
