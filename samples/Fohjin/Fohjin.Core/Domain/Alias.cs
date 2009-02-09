using Fohjin.Core.Domain.Validation;

namespace Fohjin.Core.Domain
{
    public class Alias : DomainEntity
    {
        [Required]
        public string Host{ get; set; }
        public bool Redirect { get; set; }
    }
}