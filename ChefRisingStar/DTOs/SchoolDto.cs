using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChefRisingStar.DTOs
{
    public partial class SchoolDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public long? ContactId { get; set; }

        public virtual ICollection<UserDto> Users { get; } = new List<UserDto>();
    }
}
