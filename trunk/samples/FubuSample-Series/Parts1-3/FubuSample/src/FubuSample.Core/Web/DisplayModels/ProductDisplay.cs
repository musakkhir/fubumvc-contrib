using FubuSample.Core.Domain;

namespace FubuSample.Core.Web.DisplayModels
{
    public class ProductDisplay
    {
        public ProductDisplay(Product product)
        {
            Name = product.Name;
            Description = product.Description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}