﻿using System;
using Microsoft.EntityFrameworkCore;
using Web3.EF.FirstCode.Models;

namespace Web3.EF.FirstCode.DAL
{
    public class CustomerDbContext : DbContext
    {
     
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
        
    }
}

