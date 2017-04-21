using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EZCourse.Models.Entities
{
	public partial class EZCourseContext : DbContext
	{
		public virtual DbSet<Permission> Permission { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<UserCredential> UserCredential { get; set; }
		public virtual DbSet<UserPermission> UserPermission { get; set; }

		public EZCourseContext(DbContextOptions<EZCourseContext> options)
		: base(options)
		{ } 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Permission>(entity =>
			{
				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Title)
					.IsRequired()
					.HasMaxLength(100);
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.CreationDate).HasColumnType("datetime");

				entity.Property(e => e.Email)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.FirstName)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.LastName)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<UserCredential>(entity =>
			{
				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.HashedPassword)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.PasswordSalt)
					.IsRequired()
					.HasMaxLength(50);

				entity.HasOne(d => d.IdNavigation)
					.WithOne(p => p.UserCredential)
					.HasForeignKey<UserCredential>(d => d.Id)
					.OnDelete(DeleteBehavior.Restrict)
					.HasConstraintName("FK_UserCredential_User");
			});

			modelBuilder.Entity<UserPermission>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.PermissionId })
					.HasName("PK_UserPermissions");

				entity.HasOne(d => d.Permission)
					.WithMany(p => p.UserPermission)
					.HasForeignKey(d => d.PermissionId)
					.OnDelete(DeleteBehavior.Restrict)
					.HasConstraintName("FK_UserPermissions_Permissions");

				entity.HasOne(d => d.User)
					.WithMany(p => p.UserPermission)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.Restrict)
					.HasConstraintName("FK_UserPermissions_Users");
			});
		}
	}
}