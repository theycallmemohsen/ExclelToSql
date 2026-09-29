using System;
using System.Collections.Generic;

namespace ExclelToSql.Models;

public partial class Test
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? PassWord1 { get; set; }

    public string Auth { get; set; } = null!;

    public virtual UserLevel AuthNavigation { get; set; } = null!;
}
