using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FplPriceNotificator.Models;

namespace FplPriceNotificator.Data
    {
    public class ApplicationDbContext : IdentityDbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
            {
            }
    
        public DbSet<EmailInfo> EmailInfo { get; set; }
        }
    }
