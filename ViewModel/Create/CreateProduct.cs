using EssentialProducts.API.ViewModel.Get;
using EssentialProducts.Service;
using System.ComponentModel.DataAnnotations;

namespace EssentialProducts.API.ViewModel.Create
{
    public class CreateProduct:ProductViewModel
    {
        /*public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext, CancellationToken cancellationToken)
        {
            var errors = new List<ValidationResult>();
            var productService = validationContext.GetService<IProductService>();

            var isProductNameExist = await productService.IsProductNameExist(Name);
            if(isProductNameExist)
            {
                errors.Add(new ValidationResult($"Product with name {Name} exist, provide a different name", new[] { nameof(Name) }));
            }
            return errors;
        }*/
    }
}
