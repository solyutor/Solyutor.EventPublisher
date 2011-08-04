namespace Solyutor.EventPublisher
{
    public interface IAssignee
    {
        void Subscribe(object subcriber);

        void Unsubscribe(object subcriber);
    }
}
