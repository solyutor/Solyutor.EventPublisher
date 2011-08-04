namespace Solyutor.EventPublisher
{
    public interface IAssignee
    {
        void Subscribe(object listener);

        void Unsubscribe(object listener);
    }
}
