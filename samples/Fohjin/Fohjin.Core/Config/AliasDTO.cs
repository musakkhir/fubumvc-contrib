using Fohjin.Core.Domain;

namespace Fohjin.Core.Config
{
    public class AliasDTO
    {
        public string host { get; set; }
        public bool redirect { get; set; }

        public Alias ToAlias()
        {
            return new Alias
                {
                    Host = host,
                    Redirect = redirect
                };
        }
    }
}