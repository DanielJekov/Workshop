namespace Workshop.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Workshop.Data.Models;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> appUser)
        {
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser.HasMany(e => e.NotificationsReceive)
                   .WithOne(e => e.Receiver);

            appUser.HasMany(e => e.NotificationsSend)
                   .WithOne(e => e.Sender);

            appUser.HasMany(e => e.MessagesReceive)
                  .WithOne(e => e.Receiver);

            appUser.HasMany(e => e.MessagesSend)
                   .WithOne(e => e.Sender);
        }
    }
}
