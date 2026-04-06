using ECommerceNew.Domain.Entities.ProductSide;
using ECommerceNew.Domain.Entities.StoreInfo;
using ECommerceNew.Domain.Entities.UserSide;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNew.Infrastructure.EfCore
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        //User side
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }

        //Product side 
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }

        //Store Info
        public DbSet<StoreInfo> StoreInfos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasData(
                    // Parent categories
                    new ProductCategory { CategoryId = 1, CategoryName = "Gaming Consoles & Accessories", ParentCategoryId = null },
                    new ProductCategory { CategoryId = 2, CategoryName = "PC Hardware & Components", ParentCategoryId = null },
                    new ProductCategory { CategoryId = 3, CategoryName = "PC & Gaming Peripherals", ParentCategoryId = null },
                    new ProductCategory { CategoryId = 4, CategoryName = "Gaming PCs & Laptops", ParentCategoryId = null },
                    new ProductCategory { CategoryId = 5, CategoryName = "Services", ParentCategoryId = null },

                    // Children of Gaming Consoles & Accessories
                    new ProductCategory { CategoryId = 101, CategoryName = "Consoles", ParentCategoryId = 1 },
                    new ProductCategory { CategoryId = 102, CategoryName = "Gamepads & Controllers", ParentCategoryId = 1 },
                    new ProductCategory { CategoryId = 103, CategoryName = "Controller Accessories", ParentCategoryId = 1 },
                    new ProductCategory { CategoryId = 104, CategoryName = "Gift Cards / Subscriptions", ParentCategoryId = 1 },

                    // Children of PC Hardware & Components
                    new ProductCategory { CategoryId = 201, CategoryName = "Processors (CPU)", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 202, CategoryName = "Graphics Cards (GPU)", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 203, CategoryName = "Motherboards", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 204, CategoryName = "Memory (RAM)", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 205, CategoryName = "Storage (SSD / HDD / NVMe)", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 206, CategoryName = "Power Supplies (PSU)", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 207, CategoryName = "PC Cases", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 208, CategoryName = "Cooling Solutions", ParentCategoryId = 2 },
                    new ProductCategory { CategoryId = 209, CategoryName = "Mining / Crypto Gear", ParentCategoryId = 2 },

                    // Children of PC & Gaming Peripherals
                    new ProductCategory { CategoryId = 301, CategoryName = "Monitors", ParentCategoryId = 3 },
                    new ProductCategory { CategoryId = 302, CategoryName = "Keyboards", ParentCategoryId = 3 },
                    new ProductCategory { CategoryId = 303, CategoryName = "Mice", ParentCategoryId = 3 },
                    new ProductCategory { CategoryId = 304, CategoryName = "Headsets", ParentCategoryId = 3 },
                    new ProductCategory { CategoryId = 305, CategoryName = "Speakers", ParentCategoryId = 3 },
                    new ProductCategory { CategoryId = 306, CategoryName = "Other Accessories", ParentCategoryId = 3 },

                    // Children of Gaming PCs & Laptops
                    new ProductCategory { CategoryId = 401, CategoryName = "Desktop PCs", ParentCategoryId = 4 },
                    new ProductCategory { CategoryId = 402, CategoryName = "Gaming Laptops", ParentCategoryId = 4 },
                    new ProductCategory { CategoryId = 403, CategoryName = "Laptop Accessories", ParentCategoryId = 4 },

                    // Children of Services
                    new ProductCategory { CategoryId = 501, CategoryName = "PC / Laptop Assembly", ParentCategoryId = 5 },
                    new ProductCategory { CategoryId = 502, CategoryName = "PC / Laptop Repair", ParentCategoryId = 5 },
                    new ProductCategory { CategoryId = 503, CategoryName = "Upgrades & Custom Builds", ParentCategoryId = 5 },
                    new ProductCategory { CategoryId = 504, CategoryName = "Data Recovery", ParentCategoryId = 5 },
                    new ProductCategory { CategoryId = 505, CategoryName = "Consultation & Troubleshooting", ParentCategoryId = 5 }
                );

                modelBuilder.Entity<StoreInfo>().HasData(
                new StoreInfo
                {
                    Id = 1,
                    Name = "EkkoShop",
                    Address = "123 Tech Street, Tbilisi, Georgia",
                    Phone = "+995 599 931 611",
                    Email = "temomeshveliani777@gmail.com",
                    WorkingHours = "Mon-Fri 9:00-18:00",
                    LogoUrl = "/images/logo.png"
                }
                    );

                modelBuilder.Entity<WishListItem>(entity =>
                {
                    entity.HasKey(e => e.WishListItemId);
                    entity.HasOne(e => e.User)
                          .WithMany(u => u.WishListItems)
                          .HasForeignKey(e => e.UserId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.HasOne(e => e.Product)
                          .WithMany(p => p.WishListItems)
                          .HasForeignKey(e => e.ProductId)
                          .OnDelete(DeleteBehavior.NoAction);
                });


                modelBuilder.Entity<Product>(entity =>
                {
                    entity.HasKey(entity => entity.ProductId);
                    entity.Property(e => e.Name)
                          .IsRequired()
                          .HasMaxLength(200);
                    entity.Property(e => e.Description)
                            .IsRequired()
                            .HasMaxLength(1000);
                    entity.Property(e => e.Price)
                            .HasPrecision(18, 2)
                            .IsRequired();
                    entity.Property(e => e.Amount)
                            .IsRequired();
                    entity.HasOne(p => p.User)
                          .WithMany(u => u.Products)
                          .HasForeignKey(p => p.UserId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.HasOne(p => p.ProductCategory)
                          .WithMany(c => c.Products)
                          .HasForeignKey(p => p.CategoryId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.Property(e => e.CreationDate)
                          .IsRequired();
                    entity.Property(e => e.ModifiedDate);

                });

                modelBuilder.Entity<ProductImage>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasOne(e => e.Product)
                          .WithMany(p => p.ProductImages)
                          .HasForeignKey(p => p.ProductId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.Property(e => e.ImagePath)
                          .IsRequired()
                          .HasMaxLength(500);
                });



                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.UserId);
                    entity.Property(e => e.FirstName)
                            .IsRequired()
                            .HasMaxLength(100);
                    entity.Property(e => e.LastName)
                            .IsRequired()
                            .HasMaxLength(200);
                    entity.HasIndex(e => e.Email)
                            .IsUnique();
                    entity.Property(e => e.Email)
                            .IsRequired()
                            .HasMaxLength(255);
                    entity.Property(e => e.PasswordHash)
                            .IsRequired()
                            .HasMaxLength(500);
                    entity.Property(e => e.IsActive)
                            .IsRequired();
                    entity.HasOne(u => u.Role)
                            .WithMany(r => r.Users)
                            .HasForeignKey(u => u.RoleId)
                            .OnDelete(DeleteBehavior.NoAction);

                    // Admin User Seeding 

                    entity.HasData(
                    new User
                    {
                        UserId = 1,
                        FirstName = "System",
                        LastName = "Administrator",
                        Email = "admin@system.local",
                        PasswordHash = "AQAAAAEAACcQAAAAEIM9Gb9aogKUXsuO5FOBcvftv4BETrT9VGpGfYB7ER2nxW8eV2+oCBpkAZt8oxnXNw==", //Admin@123
                        IsActive = true,
                        CreatedAt = new DateTime(2025, 1, 1),
                        UpdatedAt = null,
                        RoleId = 2
                    }
                );

                });


                modelBuilder.Entity<Role>(entity =>
                {
                    entity.HasKey(e => e.RoleId);
                    entity.Property(e => e.RoleName)
                            .IsRequired()
                            .HasMaxLength(100);

                    entity.HasData(
                            new Role { RoleId = 1, RoleName = "Client" },
                            new Role { RoleId = 2, RoleName = "Admin" }
                    );
                });

                modelBuilder.Entity<EmailVerification>(entity =>
                {
                    entity.HasKey(e => e.VerificationId);
                    entity.Property(e => e.VerificationCode)
                           .IsRequired();
                    entity.Property(e => e.Email)
                          .IsRequired()
                          .HasMaxLength(255);
                    entity.Property(e => e.ExpirationTime)
                          .IsRequired();
                });

                modelBuilder.Entity<Review>(entity =>
                {
                    entity.HasKey(e => e.ReviewId);
                    entity.Property(e => e.Rating)
                          .IsRequired();
                    entity.Property(e => e.Comment)
                          .HasMaxLength(1000);
                    entity.HasOne(r => r.Product)
                          .WithMany(p => p.Reviews)
                          .HasForeignKey(r => r.ProductId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.HasOne(r => r.User)
                          .WithMany(u => u.Reviews)
                          .HasForeignKey(r => r.UserId)
                          .OnDelete(DeleteBehavior.NoAction);
                });



                modelBuilder.Entity<Cart>(entity =>
                {
                    entity.HasKey(c => c.CartId);
                    entity.HasOne(c => c.User)
                          .WithOne(u => u.Cart)
                          .HasForeignKey<Cart>(c => c.UserId)
                          .OnDelete(DeleteBehavior.NoAction);
                });

                //modelBuilder.Entity<Cart>().HasData(new Cart
                //{
                //    UserId = 1,
                //})

                modelBuilder.Entity<CartItem>(entity =>
                {
                    entity.HasKey(ci => ci.CartItemId);
                    entity.HasOne(ci => ci.Product)
                          .WithMany(c => c.CartItems)
                          .HasForeignKey(ci => ci.ProductId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.HasOne(ci => ci.Cart)
                          .WithMany(c => c.CartItems)
                          .HasForeignKey(ci => ci.CartId)
                          .OnDelete(DeleteBehavior.NoAction);
                    entity.Property(ci => ci.ItemQuantity)
                          .IsRequired();
                });
            });
        }
           
    } 
}
