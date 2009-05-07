using System;
using FubuMVC.Core.Controller.Config;

namespace FubuMVC.Framework.Spark
{
    public class SparkConventions
    {
        private readonly FubuConventions _conventions;

        public SparkConventions(FubuConventions conventions)
        {
            _conventions = conventions;
            ViewFileBasePath = _conventions.ViewFileBasePath;
            DefaultLayoutName = "~/Views/Shared/Application.spark";
            LayoutViewFileBasePath = "~/Views/Layouts";
        }

        public string ViewFileBasePath { get; set; }
        public string DefaultLayoutName { get; set; }
        public string LayoutViewFileBasePath { get; set; }
    }
}