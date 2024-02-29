using Microsoft.EntityFrameworkCore;
using PatientManagement_frontEnd_.Models;

namespace DevApplication.Connection
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

      
        public virtual DbSet<Patient> Patients { get; set; }

    }
}
