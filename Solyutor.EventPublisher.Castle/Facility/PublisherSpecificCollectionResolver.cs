using Castle.MicroKernel;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Solyutor.EventPublisher.Impl;

namespace Solyutor.EventPublisher.Windsor.Facility
{
    public class PublisherSpecificCollectionResolver : CollectionResolver
    {
        public PublisherSpecificCollectionResolver(IKernel kernel) : base(kernel, true)
        {
        }

        protected override bool CanSatisfy(System.Type itemType)
        {
            return (itemType == typeof(IHandlerSource) || itemType == typeof(IDispatcher)) && base.CanSatisfy(itemType);
        }
    }
}