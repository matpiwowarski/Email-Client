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
            String mail1 = "mail1";
            String mail2 = "mail2";
            String mail3 = "mail3";
            String mail4 = "mail4";
            String mail5 = "mail5";
            String mail6 = "mail6";
            String mail7 = "mail7";
            String mail8 = "mail8";
            String mail9 = "mail9";
            String mail10 = "mail10";

            List<String> email1 = new List<String>();
            List<String> email2 = new List<String>();

            email1.Add(mail1);
            email1.Add(mail2);
            email1.Add(mail3);
            email1.Add(mail4);
            email1.Add(mail5);

            email2.Add(mail6);
            email2.Add(mail7);
            email2.Add(mail8);
            email2.Add(mail9);
            email2.Add(mail10);

            InitializeComponent();
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
