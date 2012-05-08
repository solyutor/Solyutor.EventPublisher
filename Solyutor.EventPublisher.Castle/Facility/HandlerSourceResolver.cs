using Castle.MicroKernel;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class HandlerSourceResolver : CollectionResolver
    {
        public HandlerSourceResolver(IKernel kernel) : base(kernel, true)
        {
        }

        protected override bool CanSatisfy(System.Type itemType)
        {
            return itemType == typeof(IHandlerSource) && base.CanSatisfy(itemType);
        }
    }
}