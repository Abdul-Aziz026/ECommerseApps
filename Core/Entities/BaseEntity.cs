
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Price { get; set; }
        public required string PictureUrl { get; set; }
        public required string Type { get; set; }
        public required string Brand { get; set; }
        public int QuantityInStock { get; set; }
    }
}
