namespace Solyutor.EventPublisher.Castle.Tests.Facility
{
    public class MultiHandler : IHandler<int>, IHandler<string>
    {
        #region ITransientHandler<int> Members

        public void Handle(int message)
        {
        }

        #endregion

        #region ITransientHandler<string> Members

        public void Handle(string message)
        {
        }

        #endregion
    }
}