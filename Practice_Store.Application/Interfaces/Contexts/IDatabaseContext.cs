using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Domain.Entities.Carts;
using Practice_Store.Domain.Entities.LandingPage;
using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Products;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<IdtUser> Users { get; }
        DbSet<IdtRole> Roles { get; }
        DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        DbSet<IdtUsertokens> UserTokens { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImages> ProductImages { get; set; }
        DbSet<ProductSizes> ProductSizes { get; set; }
        DbSet<LandingPageImages> LandingPageImages { get; set; }
        DbSet<ProductOff> ProductOffs { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartProduct> CartProducts { get; set; }
        DbSet<OrderRequest> OrderRequests { get; set; }
        DbSet<OrderRequestExtraInfo> OrderRequestExtraInfos { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Review> Reviews { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        DbSet<T> Set<T>() where T : class;
    }
}
