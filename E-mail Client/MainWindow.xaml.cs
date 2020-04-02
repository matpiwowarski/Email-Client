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
    public partial class MainWindow : Window
    {
        private List<Mail> _currentFolder;

        public Mailbox _mailbox1 = new Mailbox("arsenalfc@london.com");
        public Mailbox _mailbox2 = new Mailbox("matpiwowarski7@gmail.com");

        public MainWindow()
        {
            InitializeComponent();

            this.Email1.Header = _mailbox1.EmailAdress;
            this.Email2.Header = _mailbox2.EmailAdress;

            // mails for 1st mailbox
            Mail mail1 = new Mail("author1", "receiver1", "topic1", "LONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONGLONG");
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
            AddMailsToList(_mailbox1.Inbox, mail1, mail2, mail3, mail4);
            AddMailsToList(_mailbox1.Sent, mail5, mail6, mail7, mail8);
            AddMailsToList(_mailbox1.Deleted, mail9, mail10, mail11, mail12);
            AddMailsToList(_mailbox1.Starred, mail13, mail14, mail15, mail16);
            // load mails for mailbox2
            AddMailsToList(_mailbox2.Inbox, mail17, mail18, mail19, mail20);
            AddMailsToList(_mailbox2.Sent, mail21, mail22, mail23, mail24);
            AddMailsToList(_mailbox2.Deleted, mail25, mail26, mail27, mail28);
            AddMailsToList(_mailbox2.Starred, mail29, mail30, mail31, mail32);
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
                DisableAllButtons();
                DeleteMail(MessagesListView.SelectedIndex);
            }
        }

        private void Email_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DisableAllButtons();
        }

        private void Inbox1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox1.Inbox;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Sent1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox1.Sent;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Starred1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox1.Starred;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Deleted1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox1.Deleted;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Inbox2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox2.Inbox;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Sent2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox2.Sent;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Starred2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox2.Starred;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Deleted2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = _mailbox2.Deleted;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Mail_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableAllButtons();
            LoadMail(MessagesListView.SelectedIndex);
        }

        private void DisableAllButtons()
        {
            DeleteButton.IsEnabled = false;
            StarButton.IsEnabled = false;
            ForwardButton.IsEnabled = false;
            ReplyAllButton.IsEnabled = false;
            ReplyButton.IsEnabled = false;
            
        }
        private void EnableAllButtons()
        {
            DeleteButton.IsEnabled = true;
            StarButton.IsEnabled = true;
            ForwardButton.IsEnabled = true;
            ReplyAllButton.IsEnabled = true;
            ReplyButton.IsEnabled = true;

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
            AuthorLabel.Content = "by: " + _currentFolder[mailIndex].Author;
            TopicLabel.Content = "topic: " + _currentFolder[mailIndex].Topic;
        }
        public void DeleteMail(int mailIndex)
        {
            if (_currentFolder[mailIndex] != null)
            {
                if (_currentFolder == _mailbox1.Deleted || _currentFolder == _mailbox2.Deleted)
                {
                    MessageBoxResult result = MessageBox.Show("Do you really wish to delete the message?", "Delete message", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _currentFolder.RemoveAt(mailIndex);
                            DisableAllButtons();
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

                    if (_currentFolder == _mailbox1.Inbox || _currentFolder == _mailbox1.Sent || _currentFolder == _mailbox1.Starred)
                    {
                        // 1st mailbox
                        _mailbox1.Deleted.Add(mailCopy);
                    }
                    else // 2nd mailbox
                    {
                        _mailbox2.Deleted.Add(mailCopy);
                    }

                    _currentFolder.RemoveAt(mailIndex);

                    DisableAllButtons();
                    LoadMails(_currentFolder);
                }
            }
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            if (MessagesListView.SelectedItems.Count > 0)
            { 
                int mailIndex = MessagesListView.SelectedIndex;

                var currentMail = _currentFolder[mailIndex];

                Mail mailCopy = new Mail(currentMail.Author, currentMail.Receiver, currentMail.Topic, currentMail.Text);

                if (_currentFolder == _mailbox1.Inbox || _currentFolder == _mailbox1.Sent || _currentFolder == _mailbox1.Deleted)
                {
                    // 1st mailbox
                    _mailbox1.Starred.Add(mailCopy);
                }
                else if(_currentFolder == _mailbox2.Inbox || _currentFolder == _mailbox2.Sent || _currentFolder == _mailbox2.Deleted) // 2nd mailbox
                {
                    // 2nd mailbox
                    _mailbox2.Starred.Add(mailCopy);
                }
                else
                {
                    return;
                }

                _currentFolder.RemoveAt(mailIndex);

                DisableAllButtons();
                LoadMails(_currentFolder);
            }
        }

        // 3rd assignment
        private void NewMessage_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow nmWindow = new NewMessageWindow();

            nmWindow.Email1.Content = _mailbox1.EmailAdress;
            nmWindow.Email2.Content = _mailbox2.EmailAdress;

            if(nmWindow.ShowDialog() == true)
            {
                
            }

        }

    }
}
