using Microsoft.EntityFrameworkCore;
using ToDoListProjectJWT.Models;

namespace ToDoListProjectJWT.Context

{
    public class OrganizerContext : DbContext
{
    public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options) { }

    public DbSet<ToDoTask> Tasks { get; set; }
}
}