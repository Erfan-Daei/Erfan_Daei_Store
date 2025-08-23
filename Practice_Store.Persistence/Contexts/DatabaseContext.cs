using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.LandingPage;
using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Products;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.Contexts
{
    public class DatabaseContext : IdentityDbContext<IdtUser, IdtRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdtUsertokens>, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings
                .Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        public override DbSet<IdtUser> Users { get; set; }
        public override DbSet<IdtRole> Roles { get; set; }
        public override DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public override DbSet<IdtUsertokens> UserTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductSizes> ProductSizes { get; set; }
        public DbSet<LandingPageImages> LandingPageImages { get; set; }
        public DbSet<ProductOff> ProductOffs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<OrderRequest> OrderRequests { get; set; }
        public DbSet<OrderRequestExtraInfo> OrderRequestExtraInfos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);

            IdentitDatabaseConfiguration(modelBuilder);

            modelBuilder.Entity<IdtUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<IdtUser>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<IdtUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<IdtUser>()
                .Property(u => u.UserName)
                .IsRequired()
                .IsUnicode(true);

            modelBuilder.Entity<IdtUsertokens>()
                .HasIndex(ut => ut.LoginProvider)
                .IsUnique(false);

            modelBuilder.Entity<IdtUsertokens>()
                .HasIndex(ut => ut.UserId)
                .IsUnique(false);

            modelBuilder.Entity<Order>().HasOne(p => p.User)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetail>().HasOne(p => p.Order)
                .WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetail>().HasOne(p => p.Product)
                .WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>().HasOne(p => p.Product)
                .WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>().HasOne(p => p.User)
                .WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.NoAction);

            SetQueryFilter(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdtRole>().HasData(new IdtRole { Name = nameof(Common.UserRoles.Admin), NormalizedName = nameof(Common.UserRoles.Admin).ToUpper() });
            modelBuilder.Entity<IdtRole>().HasData(new IdtRole { Name = nameof(Common.UserRoles.Customer), NormalizedName = nameof(Common.UserRoles.Customer).ToUpper() });
            modelBuilder.Entity<IdtRole>().HasData(new IdtRole { Name = nameof(Common.UserRoles.UserManagement_Admin), NormalizedName = nameof(Common.UserRoles.UserManagement_Admin).ToUpper() });
            modelBuilder.Entity<IdtRole>().HasData(new IdtRole { Name = nameof(Common.UserRoles.ProductManagement_Admin), NormalizedName = nameof(Common.UserRoles.ProductManagement_Admin).ToUpper() });
            modelBuilder.Entity<IdtRole>().HasData(new IdtRole { Name = nameof(Common.UserRoles.OrderManagement_Admin), NormalizedName = nameof(Common.UserRoles.OrderManagement_Admin).ToUpper() });
            modelBuilder.Entity<IdtRole>().HasData(new IdtRole { Name = nameof(Common.UserRoles.SiteManagement_Admin), NormalizedName = nameof(Common.UserRoles.SiteManagement_Admin).ToUpper() });
        }

        private void SetQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdtUser>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<IdtRole>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ProductImages>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ProductSizes>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<LandingPageImages>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ProductOff>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<CartProduct>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<OrderRequest>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<OrderRequestExtraInfo>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Review>().HasQueryFilter(p => !p.IsDeleted);
        }

        private void IdentitDatabaseConfiguration(ModelBuilder builder)
        {
            builder.Entity<IdtUser>(b =>
            {
                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
                b.ToTable("AspNetUsers");
            });

            builder.Entity<IdtRole>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
                b.ToTable("AspNetRoles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("AspNetRoleClaims");
            });

            builder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("AspNetUserRoles");
            });

            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(ul => new { ul.ProviderKey, ul.LoginProvider });
                b.ToTable("AspNetUserLogins");
            });

            builder.Entity<IdtUsertokens>(b =>
            {
                b.HasKey(ut => new { ut.TokenId, ut.UserId, ut.LoginProvider });
                b.ToTable("AspNetUserTokens");
            });

            builder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.ToTable("AspNetUserClaims");
            });
        }
    }
}
