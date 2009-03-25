using System;
using System.Collections.Generic;
using System.Linq;
using FubuSample.Core.Domain;
using FubuSample.Core.Persistence;
using FubuSample.Core.Web.DisplayModels;

namespace FubuSample.Core.Web.Controllers
{
    public class HomeController
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IndexViewModel Index(IndexSetupViewModel inModel)
        {
            var prod1 = new Product {Name = "TestProduct1", Description = "This is a test product"};

            _repository.Save(prod1);

            var prod2 = new Product {Name = "TestProduct2", Description = "This is a test product"};

            _repository.Save(prod2);

            var outModel = new IndexViewModel();

            var productList = _repository.Query<Product>();

            outModel.Products = productList.ToList().Select(x => new ProductDisplay(x));

            return outModel;
        }
    }

    public class IndexSetupViewModel : ViewModel
    {
    }

    [Serializable]
    public class IndexViewModel : ViewModel
    {
        public IEnumerable<ProductDisplay> Products { get; set; }
    }
}