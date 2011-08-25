using System.Windows.Forms;

namespace Solyutor.EventPublisher.Sample
{
    public class HellowStrategy : IHellowStrategy
    {
        public void SayHello(string helloMessage)
        {
            MessageBox.Show(helloMessage);
        }
    }
}