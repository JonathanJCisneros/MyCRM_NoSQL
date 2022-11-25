#pragma warning disable CS8618

namespace MyCRMNoSQL.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public User Author { get; set; }

        public List<Purchase> PurchaseList { get; set; }
    }
}
