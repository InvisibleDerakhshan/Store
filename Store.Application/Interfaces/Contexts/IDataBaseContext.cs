using Bugeto_Store.Domain.Entities.Carts;
using Bugeto_Store.Domain.Entities.Finances;
using Bugeto_Store.Domain.Entities.HomePages;
using Bugeto_Store.Domain.Entities.Products;
using Bugeto_Store.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Strore.Application.Interfaces.Contexts
{
    public interface IDataBaseContext 
    {
         DbSet<User> Users { get; set; }
         DbSet<Role> Roles { get; set; }
         DbSet<UserInRole> UserInRoles { get; set; }
         DbSet<Category> Categories { get; set; }
         DbSet<Product> Products { get; set; }
         DbSet<ProductImages> ProductImages { get; set; }
         DbSet<ProductFeatures> ProductFeatures { get; set; }
         DbSet<Slider> Sliders { get; set; }
         DbSet<HomePagesImages> HomePagesImages { get; set; }
         DbSet<Cart> Carts { get; set; }
         DbSet<CartItem> CartItems { get; set; }
         DbSet<RequestPay> RequestPays { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,CancellationToken cancellationToken =new CancellationToken ());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken ());
    }
}
