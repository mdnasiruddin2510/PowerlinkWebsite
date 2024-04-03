using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace PosWebsite.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }

        public DbSet<ProductBrand> ProductBrand { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<ProductSetting> ProductSetting { get; set; }

        public DbSet<ProductSpecification> ProductSpecification { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<ProductUnit> ProductUnit { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesItem> SalesItem { get; set; }

        public DbSet<SalesPayment> SalesPayment { get; set; }

        public DbSet<SalesService> SalesService { get; set; }

        public DbSet<SalesSetting> SalesSetting { get; set; }

        public DbSet<Shelf> Shelf { get; set; }

        public DbSet<StockIssue> StockIssue { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<SupplierPayment> SupplierPayment { get; set; }

        public DbSet<SupplierPrice> SupplierPrice { get; set; }

        public DbSet<UserActivity> UserActivity { get; set; }

        public DbSet<VariantOption> VariantOption { get; set; }

        public DbSet<VariantValue> VariantValue { get; set; }

        public DbSet<Branch> Branch { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<InventoryHistory> InventoryHistory { get; set; }

        public DbSet<InventoryLocation> InventoryLocation { get; set; }

        public DbSet<ItemPurchaseHistory> ItemPurchaseHistory { get; set; }

        public DbSet<ItemSalesHistory> ItemSalesHistory { get; set; }

        public DbSet<Purchase> Purchase { get; set; }

        public DbSet<PurchaseItem> PurchaseItem { get; set; }

        public DbSet<PurchasePayment> PurchasePayment { get; set; }

        public DbSet<PurchaseSetting> PurchaseSetting { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<AccountGroup> AccountGroup { get; set; }

        public DbSet<AccountLedger> AccountLedger { get; set; }

        public DbSet<AccountSubLedger> AccountSubLedger { get; set; }

        public DbSet<AccountingSetting> AccountingSetting { get; set; }

        public DbSet<AccountingTransaction> AccountingTransaction { get; set; }

        public DbSet<Area> Area { get; set; }

        public DbSet<CashRegister> CashRegister { get; set; }

        public DbSet<DumpHistory> DumpHistory { get; set; }

        public DbSet<DumpStock> DumpStock { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<FiscalYear> FiscalYear { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Otp> Otp { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<DynamicBodyTemplate> DynamicBodyTemplate { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<Solutions> Solutions { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Clients> Clients { get; set; }
        
    }
}
