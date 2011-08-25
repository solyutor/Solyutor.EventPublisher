using System.Windows.Forms;

namespace Solyutor.EventPublisher.Sample
{
    public partial class MainForm : Form, IListener<HelloMessage>
    {
        private readonly IAssignee _assignee;
        private readonly IPublisher _publisher;

        protected MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IAssignee assignee, IPublisher publisher) : this()
        {
            _assignee = assignee;
            _publisher = publisher;

            _assignee.Subscribe(this);
        }

        public void ListenTo(HelloMessage message)
        {
            listBox1.Items.Add(message.Content);
        }

        private void OnSendHello(object sender, System.EventArgs e)
        {
            _publisher.Publish(new HelloMessage(textBox1.Text));
        }
    }
}