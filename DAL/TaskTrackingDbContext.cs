using DAL.Enitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class TaskTrackingDbContext : DbContext
    {
        public TaskTrackingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentStatus> AssignmentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }

}
