using AutoMapper;
using Go2Climb.API.Agencies.Domain.Models;
using Go2Climb.API.Domain.Models;
using Go2Climb.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Go2Climb.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<AgencyReview> AgencyReviews { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HiredService> HideServices { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        
        protected readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //Constrains
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Customer>().HasKey(p => p.Id);
            builder.Entity<Customer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(p => p.LastName).IsRequired().HasMaxLength(75);
            builder.Entity<Customer>().Property(p => p.Email).IsRequired().HasMaxLength(250);
            builder.Entity<Customer>().Property(p => p.PasswordHash).IsRequired().HasMaxLength(200);
            builder.Entity<Customer>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Entity<Customer>().Property(p => p.Photo);

            //Relationship
            builder.Entity<Customer>()
                .HasMany(p => p.AgencyReviews)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);
            builder.Entity<Customer>()
                .HasMany(p => p.ServiceReviews)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);
            builder.Entity<Customer>()
                .HasMany(p => p.HideServices)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            //Constrains
            builder.Entity<AgencyReview>().ToTable("AgencyReviews");
            builder.Entity<AgencyReview>().HasKey(p => p.Id);
            builder.Entity<AgencyReview>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<AgencyReview>().Property(p => p.Date).IsRequired();
            builder.Entity<AgencyReview>().Property(p => p.Comment).IsRequired().HasMaxLength(200);
            builder.Entity<AgencyReview>().Property(p => p.ProfessionalismScore).IsRequired();
            builder.Entity<AgencyReview>().Property(p => p.SecurityScore).IsRequired();
            builder.Entity<AgencyReview>().Property(p => p.QualityScore).IsRequired();
            builder.Entity<AgencyReview>().Property(p => p.CostScore).IsRequired();
            
            //Relationships

            //Constrains
            builder.Entity<ServiceReview>().ToTable("ServiceReviews");
            builder.Entity<ServiceReview>().HasKey(p => p.Id);
            builder.Entity<ServiceReview>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ServiceReview>().Property(p => p.Date).IsRequired();
            builder.Entity<ServiceReview>().Property(p => p.Comment).IsRequired().HasMaxLength(200);
            builder.Entity<ServiceReview>().Property(p => p.Score).IsRequired();

            //Relationships
            //does not need relationships

            //Agency Entity
            builder.Entity<Agency>().ToTable("Agencies");
            builder.Entity<Agency>().HasKey(p => p.Id);
            builder.Entity<Agency>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Agency>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Agency>().Property(p => p.Email).IsRequired();
            builder.Entity<Agency>().Property(p => p.PasswordHash).IsRequired().HasMaxLength(200);
            builder.Entity<Agency>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(10);
            builder.Entity<Agency>().Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Entity<Agency>().Property(p => p.Location).IsRequired().HasMaxLength(50);
            builder.Entity<Agency>().Property(p => p.Ruc).IsRequired();
            builder.Entity<Agency>().Property(p => p.Photo);
            builder.Entity<Agency>().Property(p => p.Score);

            builder.Entity<Agency>()
                .HasMany(p => p.Services)
                .WithOne(p => p.Agency)
                .HasForeignKey(p => p.AgencyId);
            builder.Entity<Agency>()
                .HasMany(p => p.AgencyReviews)
                .WithOne(p => p.Agency)
                .HasForeignKey(p => p.AgencyId);
            
            //Activity Entity
            builder.Entity<Activity>().ToTable("Activities");
            builder.Entity<Activity>().HasKey(p => p.Id);
            builder.Entity<Activity>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Activity>().Property(p => p.Name);
            builder.Entity<Activity>().Property(p => p.Description).IsRequired().HasMaxLength(50);
            
            //Service Entity
            builder.Entity<Service>().ToTable("Services");
            builder.Entity<Service>().HasKey(p => p.Id);
            builder.Entity<Service>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Service>().Property(p => p.Name).IsRequired().HasMaxLength(35);
            builder.Entity<Service>().Property(p => p.Score);
            builder.Entity<Service>().Property(p => p.Price).IsRequired();
            builder.Entity<Service>().Property(p => p.NewPrice);
            builder.Entity<Service>().Property(p => p.Location).IsRequired();
            builder.Entity<Service>().Property(p => p.CreationDate).IsRequired();
            builder.Entity<Service>().Property(p => p.Photos).HasMaxLength(500);
            builder.Entity<Service>().Property(p => p.Description).IsRequired().HasMaxLength(300);
            builder.Entity<Service>().Property(p => p.IsOffer);

            builder.Entity<Service>()
                .HasMany(p => p.Activities)
                .WithOne(p => p.Service)
                .HasForeignKey(p => p.ServiceId);
            builder.Entity<Service>()
                .HasMany(p => p.ServiceReviews)
                .WithOne(p => p.Service)
                .HasForeignKey(p => p.ServiceId);

            //Constrains
            builder.Entity<HiredService>().ToTable("HiredServices");
            builder.Entity<HiredService>().HasKey(p => p.Id);
            builder.Entity<HiredService>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<HiredService>().Property(p => p.Amount).IsRequired();
            builder.Entity<HiredService>().Property(p => p.Price).IsRequired();
            builder.Entity<HiredService>().Property(p => p.ScheduledDate).IsRequired().HasMaxLength(15);
            builder.Entity<HiredService>().Property(p => p.Status).IsRequired().HasMaxLength(30);

            //Constraints
            builder.Entity<Subscription>().ToTable("Subscriptions");
            builder.Entity<Subscription>().HasKey(p => p.Id);
            builder.Entity<Subscription>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Subscription>().Property(p => p.Name).IsRequired();
            builder.Entity<Subscription>().Property(p => p.Price).IsRequired();
            builder.Entity<Subscription>().Property(p => p.Description).IsRequired();

            builder.UseSnakeCaseNamingConventions();
        }
        
    }
}