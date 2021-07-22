using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Notebook.Infrastructure.Data
{
  class DesignTimeContextFactory : IDesignTimeDbContextFactory<NotebookContext>
  {
    #region IDesignTimeDbContextFactory
    public NotebookContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<NotebookContext>();
      optionsBuilder.UseSqlServer("Server=w608w10;Database=NotebookCore;Trusted_Connection=True;");
      return new NotebookContext(optionsBuilder.Options);
    }
    #endregion
  }
}
