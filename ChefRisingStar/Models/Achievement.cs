using System;
using System.Collections.Generic;
using System.Text;

namespace ChefRisingStar.Models
{
    public class Achievement : IEquatable<Achievement>
    {
        public Achievement(int id, int value, string name, string description, string imageSrc, AchievementTypes achievementType)
        {
            Id = id;
            Value = value;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ImageSrc = imageSrc ?? throw new ArgumentNullException(nameof(imageSrc));
            AchievementType = achievementType;
        }
        
        public Achievement(int id, int value, string name, string description, string imageSrc, AchievementTypes achievementType, DateTime dateEarned): this (id, value, name, description, imageSrc, achievementType)
        {
            DateEarned = dateEarned;
        }

        public int Id { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSrc { get; set; }

        public DateTime DateEarned { get; set; }
        public string DateEarnedString 
        { 
            get
            {
                return DateEarned == DateTime.MinValue ? "Never" : DateEarned.ToShortDateString();
            }
        }
        
        public double ImageOpacity 
        { 
            get
            {
                return DateEarned == DateTime.MinValue ? 0.3 : 1;
            }
        }
        public AchievementTypes AchievementType { get; set; }
        public List<AchievementCondition> AchievementConditions { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Achievement);
        }

        public bool Equals(Achievement other)
        {
            return other != null &&
                   Id == other.Id &&
                   Value == other.Value &&
                   Name == other.Name &&
                   Description == other.Description &&
                   ImageSrc == other.ImageSrc &&
                   DateEarned == other.DateEarned &&
                   AchievementType == other.AchievementType &&
                   EqualityComparer<List<AchievementCondition>>.Default.Equals(AchievementConditions, other.AchievementConditions);
        }

        public override int GetHashCode()
        {
            int hashCode = -627139147;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ImageSrc);
            hashCode = hashCode * -1521134295 + DateEarned.GetHashCode();
            hashCode = hashCode * -1521134295 + AchievementType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<AchievementCondition>>.Default.GetHashCode(AchievementConditions);
            return hashCode;
        }
    }

    public enum AchievementTypes
    {
        Skill,
        Progress,
        Social
    }
}
