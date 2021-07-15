using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class AchievementStep : IEquatable<AchievementStep>
    {
        public const string CompleteImage = "checkmark64.png";
        public const string IncompleteImage = "emptycheckbox64.png";


        public AchievementStep(int id, string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));
            }

            Id = id;
            Name = name;
            Description = description;
        }
        
        public AchievementStep(int id, string name, string description, DateTime completionDate) : this(id, name, description)
        {
            CompletionDate = completionDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CompletionDate { get; set; }
        
        public string DateEarnedString
        {
            get
            {
                return CompletionDate == DateTime.MinValue ? string.Empty : CompletionDate.ToShortDateString();
            }
        }

        public string ImageSrc 
        {
            get { return CompletionDate == DateTime.MinValue ? IncompleteImage : CompleteImage; }
        }
        
        public bool IsComplete 
        {
            get { return CompletionDate == DateTime.MinValue ? false : true; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AchievementStep);
        }

        public bool Equals(AchievementStep other)
        {
            return other != null &&
                   Id == other.Id &&
                   Name == other.Name &&
                   Description == other.Description && 
                   CompletionDate == other.CompletionDate;
        }

        public override int GetHashCode()
        {
            int hashCode = 1829809407;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime>.Default.GetHashCode(CompletionDate);
            return hashCode;
        }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Name}";
        }
    }
}
