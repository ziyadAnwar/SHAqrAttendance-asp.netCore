using Microsoft.EntityFrameworkCore;
using SHA_QrAttendanceSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHA_QrAttendanceSystem.Models
{
    public class QrContext : DbContext
    {
        public QrContext(DbContextOptions<QrContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers  { get; set; }

    }
} 
