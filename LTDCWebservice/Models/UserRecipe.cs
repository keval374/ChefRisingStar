using System;
using System.Collections.Generic;

namespace LTDCWebservice.Models;

public partial class UserRecipe
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string SpoonacularId { get; set; }

    public virtual User User { get; set; }
}
