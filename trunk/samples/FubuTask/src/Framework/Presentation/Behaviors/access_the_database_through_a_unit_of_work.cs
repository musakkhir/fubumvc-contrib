using System;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Controller;
using FubuMVC.Framework.Persistence;

namespace FubuMVC.Framework.Presentation.Behaviors
{
    public class access_the_database_through_a_unit_of_work : IControllerActionBehavior
    {
        private readonly IUnitOfWork _unitOfWork;

        public access_the_database_through_a_unit_of_work(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IControllerActionBehavior InsideBehavior { get; set; }
        public IInvocationResult Result { get; set; }

        public TOutput Invoke<TInput, TOutput>(TInput input, Func<TInput, TOutput> func) 
            where TInput : class
            where TOutput : class
        {
            _unitOfWork.Initialize();

            try
            {
                var output = InsideBehavior.Invoke(input, func);
                Result = InsideBehavior.Result;
                
                _unitOfWork.Commit();

                return output;
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}