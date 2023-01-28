using System;
using System.Collections.Generic;

namespace ChefRisingStar.DTOs
{
    public partial class TeamDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? CaptainId { get; set; }

        public long? Active { get; set; }

        public virtual ICollection<UserDto> Users { get; } = new List<UserDto>();
    }
}
