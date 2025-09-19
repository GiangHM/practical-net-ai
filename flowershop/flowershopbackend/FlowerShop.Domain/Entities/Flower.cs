namespace FlowerShop.Domain.Entities
{
    public class Flower
    {
        public float Id { get; set; }
        public float CategoryId { get; set; }
        public FlowerPricing UnitPrice { get; set; }
        public FlowerStock Stock { get; set; }
        public string FlowerName { get; set; }
        public string FlowerImageUrl { get; set; }
        public string FlowerDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
