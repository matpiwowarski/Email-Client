using E_mail_Client.Model;
using System;
using System.Collections;
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
        enum EmailType { EMAIL1, EMAIL2 };

        private EmailType _currentEmail = EmailType.EMAIL1;
        private List<Mail> _currentFolder;

        List<Mail> inbox1 = new List<Mail>();
        List<Mail> sent1 = new List<Mail>();
        List<Mail> deleted1 = new List<Mail>();
        List<Mail> starred1 = new List<Mail>();

        List<Mail> inbox2 = new List<Mail>();
        List<Mail> sent2 = new List<Mail>();
        List<Mail> deleted2 = new List<Mail>();
        List<Mail> starred2 = new List<Mail>();

        public MainWindow()
        {
            InitializeComponent();

            // mails for 1st mailbox
            Mail mail1 = new Mail("author1", "receiver1", "topic1", "content1");
            Mail mail2 = new Mail("author2", "receiver2", "topic2", "content2");
            Mail mail3 = new Mail("author3", "receiver3", "topic3", "content3");
            Mail mail4 = new Mail("author4", "receiver4", "topic4", "content4");
            Mail mail5 = new Mail("author5", "receiver5", "topic5", "content5");
            Mail mail6 = new Mail("author6", "receiver6", "topic6", "content6");
            Mail mail7 = new Mail("author7", "receiver7", "topic7", "content7");
            Mail mail8 = new Mail("author8", "receiver8", "topic8", "content8");
            Mail mail9 = new Mail("author9", "receiver9", "topic9", "content9");
            Mail mail10 = new Mail("author10", "receiver10", "topic10", "content10");
            Mail mail11 = new Mail("author11", "receiver11", "topic11", "content11");
            Mail mail12 = new Mail("author12", "receiver12", "topic12", "content12");
            Mail mail13 = new Mail("author13", "receiver13", "topic13", "content13");
            Mail mail14 = new Mail("author14", "receiver14", "topic14", "content14");
            Mail mail15 = new Mail("author15", "receiver15", "topic15", "content15");
            Mail mail16 = new Mail("author16", "receiver16", "topic16", "content16");
            // mails for 2nd mailbox
            Mail mail17 = new Mail("author17", "receiver17", "topic17", "content17");
            Mail mail18 = new Mail("author18", "receiver18", "topic18", "content18");
            Mail mail19 = new Mail("author19", "receiver19", "topic19", "content19");
            Mail mail20 = new Mail("author20", "receiver20", "topic20", "content20");
            Mail mail21 = new Mail("author21", "receiver21", "topic21", "content21");
            Mail mail22 = new Mail("author22", "receiver22", "topic22", "content22");
            Mail mail23 = new Mail("author23", "receiver23", "topic23", "content23");
            Mail mail24 = new Mail("author24", "receiver24", "topic24", "content24");
            Mail mail25 = new Mail("author25", "receiver25", "topic25", "content25");
            Mail mail26 = new Mail("author26", "receiver26", "topic26", "content26");
            Mail mail27 = new Mail("author27", "receiver27", "topic27", "content27");
            Mail mail28 = new Mail("author28", "receiver28", "topic28", "content28");
            Mail mail29 = new Mail("author29", "receiver29", "topic29", "content29");
            Mail mail30 = new Mail("author30", "receiver30", "topic30", "content30");
            Mail mail31 = new Mail("author31", "receiver31", "topic31", "content31");
            Mail mail32 = new Mail("author32", "receiver32", "topic32", "content32");

            // load mails for mailbox1
            AddMailsToList(inbox1, mail1, mail2, mail3, mail4);
            AddMailsToList(sent1, mail5, mail6, mail7, mail8);
            AddMailsToList(deleted1, mail9, mail10, mail11, mail12);
            AddMailsToList(starred1, mail13, mail14, mail15, mail16);
            // load mails for mailbox2
            AddMailsToList(inbox2, mail17, mail18, mail19, mail20);
            AddMailsToList(sent2, mail21, mail22, mail23, mail24);
            AddMailsToList(deleted2, mail25, mail26, mail27, mail28);
            AddMailsToList(starred2, mail29, mail30, mail31, mail32);
        }

        private void AddMailsToList(List<Mail> list, params Mail[] mails)
        {
            foreach (Mail m in mails)
            {
                list.Add(m);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessagesListView.SelectedItems.Count > 0)
            {
                DisableDeleteButton();
                DeleteMail(MessagesListView.SelectedIndex);
            }
        }

        private void Email_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DisableDeleteButton();
        }

        private void Inbox1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL1;
            _currentFolder = inbox1;
            DisableDeleteButton();
            LoadMails(inbox1);
        }

        private void Sent1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL1;
            _currentFolder = sent1;
            DisableDeleteButton();
            LoadMails(sent1);
        }

        private void Starred1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL1;
            _currentFolder = starred1;
            DisableDeleteButton();
            LoadMails(starred1);
        }

        private void Deleted1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL1;
            _currentFolder = deleted1;
            DisableDeleteButton();
            LoadMails(deleted1);
        }

        private void Inbox2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL2;
            _currentFolder = inbox2;
            DisableDeleteButton();
            LoadMails(inbox2);
        }

        private void Sent2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL2;
            _currentFolder = sent2;
            DisableDeleteButton();
            LoadMails(sent2);
        }

        private void Starred2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL2;
            _currentFolder = starred2;
            DisableDeleteButton();
            LoadMails(starred2);
        }

        private void Deleted2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentEmail = EmailType.EMAIL2;
            _currentFolder = deleted2;
            DisableDeleteButton();
            LoadMails(deleted2);
        }

        private void Mail_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableDeleteButton();

            LoadMail(MessagesListView.SelectedIndex);
        }

        private void DisableDeleteButton()
        {
            DeleteButton.IsEnabled = false;
        }
        private void EnableDeleteButton()
        {
            DeleteButton.IsEnabled = true;
        }
        private void LoadMails(List<Mail> mails)
        {
            this.MessagesListView.Items.Clear();

            foreach (Mail m in mails)
            {
                ListViewItem mailItem = new ListViewItem();
                mailItem.Content = m.Topic + " by " + m.Author;
                mailItem.MouseLeftButtonUp += Mail_MouseLeftButtonUp;
                MessagesListView.Items.Add(mailItem);
            }
            MessageTextBlock.Text = "";
            AuthorLabel.Content = "";
            TopicLabel.Content = "";
        }
        private void LoadMail(int mailIndex)
        {
            MessageTextBlock.Text = _currentFolder[mailIndex].Text;
            AuthorLabel.Content = _currentFolder[mailIndex].Author;
            TopicLabel.Content = _currentFolder[mailIndex].Topic;
        }
        public void DeleteMail(int mailIndex)
        {
            if (_currentFolder[mailIndex] != null)
            {
                if (_currentFolder == deleted1 || _currentFolder == deleted2)
                {
                    MessageBoxResult result = MessageBox.Show("Do you really wish to delete the message?", "Delete message", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _currentFolder.RemoveAt(mailIndex);
                            DisableDeleteButton();
                            LoadMails(_currentFolder);

                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                else // other folder = move to Deleted items
                {
                    var currentMail = _currentFolder[mailIndex];
                    Mail mailCopy = new Mail(currentMail.Author, currentMail.Receiver, currentMail.Topic, currentMail.Text);

                    if (_currentEmail == EmailType.EMAIL1)
                        deleted1.Add(mailCopy);
                    else // EmailType.EMAIL2
                        deleted2.Add(mailCopy);

                    _currentFolder.RemoveAt(mailIndex);

                    DisableDeleteButton();
                    LoadMails(_currentFolder);
                }
            }
        }
    }
}
