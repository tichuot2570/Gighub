using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gighub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Identify DbSet Gig, as Gig has a reference of Genre in it so EF can find Genre reference but if you want 
        //to query it, you need to declare a DBSet for it too
        public DbSet<Gig> Gigs { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet< Following> Followings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }
        public ApplicationDbContext()
            : base("¨NotificationConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //remove the delete cascade in the circle of attendancem gig and user
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany(g => g.Attendances)
                .WillCascadeOnDelete(false);
        
            //config Following properties
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);

            //using FluentAPI
            base.OnModelCreating(modelBuilder);
        }
    }
}