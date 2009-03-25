using System;
using System.Collections.Generic;
using FubuSample.Core.Domain;
using FubuSample.Core.Web.DisplayModels;
using FubuMVC.Core;

namespace FubuSample.Core.Web.Controllers
{
    public class HomeController
    {
        
        public IndexViewModel Index(IndexSetupViewModel inModel)
        {
            var outModel = new IndexViewModel();

            var productList = new List<ProductDisplay>();

            productList.Add(new ProductDisplay(new Product { Name = "TestProduct1", Description = "This is a test product"}));
            productList.Add(new ProductDisplay(new Product { Name = "TestProduct2", Description = "This is a test product"}));
            productList.Add(new ProductDisplay(new Product { Name = "TestProduct3", Description = "This is a test product"}));

            outModel.Products = productList;

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