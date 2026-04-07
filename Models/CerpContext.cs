using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CERP.Models;

public partial class CerpContext : DbContext
{
    public CerpContext()
    {
    }

    public CerpContext(DbContextOptions<CerpContext> options): base(options)
    {
    }
  
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
