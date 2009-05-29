//--------------------------------------------
// FUBU NOTICE: Portions of this file come from
// the Spark source code (Spark.Web.Mvc.SparkView to be exact)
// Where applicable, those sections adhere to the following
// license below
// http://sparkviewengine.com/
//--------------------------------------------
//
// Copyright 2008-2009 Louis DeJardin - http://whereslou.com
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System;
using System.Linq.Expressions;
using System.Web;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Html;
using FubuMVC.Core.Html.Expressions;
using FubuMVC.Core.View;
using FubuMVC.Framework.Presentation.WebForms;
using Microsoft.Practices.ServiceLocation;
using Spark;

namespace FubuMVC.Framework.Spark
{
    public abstract class SparkView<TViewModel> : AbstractSparkView, IFubuView<TViewModel>, IFubuViewPage
        where TViewModel : class
    {
        private string _siteRoot;

        public void SetModel(object model)
        {
            
            Model = (TViewModel)model;
        }

        public TViewModel GetModel()
        {
            return Model;
        }

        public IResourcePathManager ResourcePathManager { get; set; }

        public string SiteResource(string page)
        {
            return ResourcePathManager.GetResourcePath(SiteRoot, page);
        }

        public TViewModel Model { get; set; }
        object IFubuViewPage.Model { get { return Model; } }
        
        public string SiteRoot
        {
            get
            {
                if (_siteRoot == null)
                {
                    _siteRoot = UrlContext.GetUrl("~/").TrimEnd('/');
                }
                return _siteRoot;
            }
        }
 
        public string H(object value)
        {
            return HttpUtility.HtmlEncode(Convert.ToString(value));
        }

        public TextBoxExpression<TViewModel> TextBoxFor(Expression<Func<TViewModel, object>> expression)
        {
            return new TextBoxExpression<TViewModel>(Model, expression, "");
        }

        public FormExpression FormFor<TController>(Expression<Func<TController, object>> actionExpression)
            where TController : class
        {
            var resolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            var url = resolver.UrlFor(actionExpression);
            return new FormExpression(url);
        }
    }
}