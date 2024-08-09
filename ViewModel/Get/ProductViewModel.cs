using System.ComponentModel.DataAnnotations;

namespace EssentialProducts.API.ViewModel.Get
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [MinLength(5), MaxLength(500)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(5, 100000)]
        public decimal Price { get; set; }

        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
    }
}
