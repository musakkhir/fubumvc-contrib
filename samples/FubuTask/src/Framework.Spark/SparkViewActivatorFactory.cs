using System;
using Spark;

namespace FubuMVC.Framework.Spark
{
    public class SparkViewActivatorFactory : IViewActivatorFactory
    {
        public IViewActivator Register(Type type)
        {
            throw new NotImplementedException();
        }

        public void Unregister(Type type, IViewActivator activator)
        {
            throw new NotImplementedException();
        }
    }

    public class SparkViewActivator : IViewActivator
    {
        public ISparkView Activate(Type type)
        {
            throw new NotImplementedException();
        }

        public void Release(Type type, ISparkView view)
        {
            throw new NotImplementedException();
        }
    }
}