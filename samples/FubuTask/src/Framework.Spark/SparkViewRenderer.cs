using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.View;
using Spark;
using Spark.FileSystem;

namespace FubuMVC.Framework.Spark
{
    public class SparkViewRenderer : IViewRenderer
    {
        private readonly IControllerConfigContext _context;
        private readonly ISparkViewEngine _viewEngine;
        private readonly FubuConventions _fubuConventions;
        private readonly SparkConventions _sparkConventions;

        public SparkViewRenderer(
            FubuConventions fubuConventions, 
            SparkConventions sparkConventions, 
            IControllerConfigContext context,
            ISparkViewEngine viewEngine)
        {
            _fubuConventions = fubuConventions;
            _sparkConventions = sparkConventions;
            _context = context;
            _viewEngine = viewEngine;
        }

        public virtual string RenderView<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var viewPath = _fubuConventions.DefaultPathToViewForAction(_context.CurrentConfig);

            var descriptor = GetTheViewDescriptor(viewPath, _sparkConventions.DefaultLayoutName);
            var view = InstantiateTheView(descriptor, viewModel);
            return RenderTheView(view);
        }

        protected virtual SparkViewDescriptor GetTheViewDescriptor(string viewPath, string layoutPath)
        {
            return new SparkViewDescriptor
            {
                Templates = new List<string> { viewPath, layoutPath }
            };
        }

        protected virtual ISparkView InstantiateTheView<TViewModel>(SparkViewDescriptor descriptor, TViewModel viewModel)
            where TViewModel : class
        {
            _viewEngine.Settings.PageBaseType = typeof(SparkView<>).MakeGenericType(viewModel.GetType()).ToPrettyString();

            ISparkViewEntry entry = _viewEngine.CreateEntry(descriptor);
            var view = (SparkView<TViewModel>)entry.CreateInstance();
            view.ResourcePathManager = _viewEngine.ResourcePathManager;
            view.Model = viewModel;
            return view;
        }

        protected virtual string RenderTheView(ISparkView view)
        {
            var writer = new StringWriter();
            view.RenderView(writer);
            writer.Flush();
            return writer.GetStringBuilder().ToString();
        }
    }

    public static class ViewExtensions
    {
        public static string ToPrettyString(this Type type)
        {
            return type.ToString().Replace('[', '<').Replace(']', '>').RegexReplace(@"`\d", string.Empty).Replace('+', '.');
        }

        public static string RegexReplace(this string input, string pattern, string replacement)
        {
            return input.RegexReplace(pattern, replacement, RegexOptions.None);
        }

        public static string RegexReplace(this string input, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(input, pattern, replacement, options);
        }
    }
}