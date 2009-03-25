using FubuSample.Core.Web.Controllers;
using FubuSample.Core.Web.DisplayModels;
using FubuSample.Core.Web.WebForms;

public class SiteMasterView : FubuSampleMasterPage { }

public class HomeIndexView : FubuSamplePage<IndexViewModel> { }

public class ProductInfo : FubuSampleUserControl<ProductDisplay> { }