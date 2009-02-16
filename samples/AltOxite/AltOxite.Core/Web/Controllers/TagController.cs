
using System;
using System.Collections.Generic;
using System.Linq;
using AltOxite.Core.Domain;
using AltOxite.Core.Persistence;
using FubuMVC.Core;

namespace AltOxite.Core.Web.Controllers
{
    public class TagController
    {
        private readonly IRepository _repository;

        public TagController(IRepository repository)
        {
            _repository = repository;
        }

        public TagViewModel Index(TagSetupViewModel inModel)
        {
            if (inModel.Tag.IsEmpty()) return new TagViewModel();

            var tag = _repository.Query<Tag>().Where(p => p.Name == inModel.Tag).FirstOrDefault(); // TODO: Currently tags are not unique 

            if (tag == null) return new TagViewModel();

            return new TagViewModel
            {
                Tag = tag
            };
        }

        public TagListViewModel AllTags(TagSearchViewModel model)
        {
            IQueryable<Tag> tags;
            if (model.SuggestQuery.IsEmpty())
            {
                tags = from t in _repository.Query<Tag>()
                       select t;
            }
            else
            {
                tags = from t in _repository.Query<Tag>()
                       where t.Name.StartsWith(model.SuggestQuery)
                       select t;
            }

            var outModel = new TagListViewModel
            {
                Tags = tags.ToList().Select(item => item.Name)
            };
            return outModel;
        }
    }

    public class TagSetupViewModel : ViewModel
    {
        public string Tag { get; set; }
    }

    [Serializable]
    public class TagViewModel : ViewModel
    {
        public Tag Tag { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }

    [Serializable]
    public class TagListViewModel : ViewModel
    {
        public IEnumerable<string> Tags { get; set; }
    }

    [Serializable]
    public class TagSearchViewModel : ViewModel
    {
        public string q { get; set; }

        public string SuggestQuery { get { return q; } }
    }
}