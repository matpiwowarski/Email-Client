using E_mail_Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_mail_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Controller controller = new Controller(this);

            Mailbox myMailbox = new Mailbox("matpiwowarski7@gmail.com");
            Mailbox fakeMailbox = new Mailbox("test@test.pl");

            controller.LoadTreeViewEmails(myMailbox, fakeMailbox);
            
            Mail mail1 = new Mail("content1");
            Mail mail2 = new Mail("content2");
            Mail mail3 = new Mail("content3");
            Mail mail4 = new Mail("content4");
            Mail mail5 = new Mail("content5");
            Mail mail6 = new Mail("content6");

            controller.AddMails(myMailbox, mail1, mail2, mail3);
            controller.AddMails(fakeMailbox, mail4, mail5, mail6);
        }

        private void Email1TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Inbox1TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Sent1TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Deleted1TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Starred1TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Email2TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Inbox2TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Sent2TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Deleted2TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }

        private void Starred2TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessagesListView.Items.Clear();
        }
    }
}
