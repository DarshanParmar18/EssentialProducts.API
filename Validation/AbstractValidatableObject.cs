using System.ComponentModel.DataAnnotations;

namespace EssentialProducts.API.Validation
{
    public class AbstractValidatableObject : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            var task = ValidateAsync(validationContext, tokenSource.Token);
            Task.WaitAll(task);
            return task.Result;
        }

        public virtual Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext, CancellationToken token) 
        {
            return Task.FromResult((IEnumerable<ValidationResult>)new List<ValidationResult>());
        }
    }
}
