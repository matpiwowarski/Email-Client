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

        public Mailbox Mailbox1 = new Mailbox("mateusz.piwowarski@student.um.si");
        public Mailbox Mailbox2 = new Mailbox("matpiwowarski7@gmail.com");
        public MainWindow()
        {
            InitializeComponent();

            this.Email1.Header = Mailbox1.EmailAdress;
            this.Email2.Header = Mailbox2.EmailAdress;

            // to test "reply all"
            HashSet<String> moreReceivers = new HashSet<String>();
            moreReceivers.Add(Mailbox1.EmailAdress); 
            moreReceivers.Add("receiver1.1");
            moreReceivers.Add("receiver1.2");

            // mails for 1st mailbox
            // mail1 is for testing
            Mail mail1 = new Mail("author1", moreReceivers, "topic1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus fringilla diam sed purus interdum cursus. Fusce commodo felis est, eu cursus velit lobortis nec. Pellentesque dictum tellus ac sapien tincidunt, a ullamcorper nisi aliquam. Fusce vel dolor convallis turpis rhoncus sodales ut et elit. Donec hendrerit fringilla ante, ac lacinia diam consectetur vitae. Maecenas molestie neque sit amet congue bibendum. Aenean vitae porta neque. Sed et elit sit amet nulla aliquet laoreet eget ac metus. Fusce id ullamcorper tellus. Pellentesque placerat, elit a porttitor consectetur, ante risus convallis ipsum, ut accumsan lacus nisi non felis. Integer nec tellus a risus.");
            mail1.AddAttachment("TestAttachment");
            // mail1 is for testing
            Mail mail2 = new Mail("author2", Mailbox1.EmailAdress, "topic2", "content2");
            Mail mail3 = new Mail("author3", Mailbox1.EmailAdress, "topic3", "content3");
            Mail mail4 = new Mail("author4", Mailbox1.EmailAdress, "topic4", "content4");
            Mail mail5 = new Mail("author5", Mailbox1.EmailAdress, "topic5", "content5");
            Mail mail6 = new Mail("author6", Mailbox1.EmailAdress, "topic6", "content6");
            Mail mail7 = new Mail("author7", Mailbox1.EmailAdress, "topic7", "content7");
            Mail mail8 = new Mail("author8", Mailbox1.EmailAdress, "topic8", "content8");
            Mail mail9 = new Mail("author9", Mailbox1.EmailAdress, "topic9", "content9");
            Mail mail10 = new Mail("author10", Mailbox1.EmailAdress, "topic10", "content10");
            Mail mail11 = new Mail("author11", Mailbox1.EmailAdress, "topic11", "content11");
            Mail mail12 = new Mail("author12", Mailbox1.EmailAdress, "topic12", "content12");
            Mail mail13 = new Mail("author13", Mailbox1.EmailAdress, "topic13", "content13");
            Mail mail14 = new Mail("author14", Mailbox1.EmailAdress, "topic14", "content14");
            Mail mail15 = new Mail("author15", Mailbox1.EmailAdress, "topic15", "content15");
            Mail mail16 = new Mail("author16", Mailbox1.EmailAdress, "topic16", "content16");
            // mails for 2nd mailbox
            Mail mail17 = new Mail("author17", Mailbox2.EmailAdress, "topic17", "content17");
            Mail mail18 = new Mail("author18", Mailbox2.EmailAdress, "topic18", "content18");
            Mail mail19 = new Mail("author19", Mailbox2.EmailAdress, "topic19", "content19");
            Mail mail20 = new Mail("author20", Mailbox2.EmailAdress, "topic20", "content20");
            Mail mail21 = new Mail("author21", Mailbox2.EmailAdress, "topic21", "content21");
            Mail mail22 = new Mail("author22", Mailbox2.EmailAdress, "topic22", "content22");
            Mail mail23 = new Mail("author23", Mailbox2.EmailAdress, "topic23", "content23");
            Mail mail24 = new Mail("author24", Mailbox2.EmailAdress, "topic24", "content24");
            Mail mail25 = new Mail("author25", Mailbox2.EmailAdress, "topic25", "content25");
            Mail mail26 = new Mail("author26", Mailbox2.EmailAdress, "topic26", "content26");
            Mail mail27 = new Mail("author27", Mailbox2.EmailAdress, "topic27", "content27");
            Mail mail28 = new Mail("author28", Mailbox2.EmailAdress, "topic28", "content28");
            Mail mail29 = new Mail("author29", Mailbox2.EmailAdress, "topic29", "content29");
            Mail mail30 = new Mail("author30", Mailbox2.EmailAdress, "topic30", "content30");
            Mail mail31 = new Mail("author31", Mailbox2.EmailAdress, "topic31", "content31");
            Mail mail32 = new Mail("author32", Mailbox2.EmailAdress, "topic32", "content32");

            // load mails for mailbox1
            AddMailsToList(Mailbox1.Inbox, mail1, mail2, mail3, mail4);
            AddMailsToList(Mailbox1.Sent, mail5, mail6, mail7, mail8);
            AddMailsToList(Mailbox1.Deleted, mail9, mail10, mail11, mail12);
            AddMailsToList(Mailbox1.Starred, mail13, mail14, mail15, mail16);
            // load mails for mailbox2
            AddMailsToList(Mailbox2.Inbox, mail17, mail18, mail19, mail20);
            AddMailsToList(Mailbox2.Sent, mail21, mail22, mail23, mail24);
            AddMailsToList(Mailbox2.Deleted, mail25, mail26, mail27, mail28);
            AddMailsToList(Mailbox2.Starred, mail29, mail30, mail31, mail32);
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
            _currentFolder = Mailbox1.Inbox;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Sent1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox1.Sent;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Starred1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox1.Starred;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Deleted1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox1.Deleted;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Inbox2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox2.Inbox;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Sent2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox2.Sent;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Starred2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox2.Starred;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Deleted2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentFolder = Mailbox2.Deleted;
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
                // display message in MainWindow on click
                mailItem.MouseLeftButtonUp += Mail_MouseLeftButtonUp;
                // display message in NewMessageWindow on double click
                mailItem.MouseDoubleClick += Mail_DoubleClick;

                MessagesListView.Items.Add(mailItem);
            }

            ClearAllDisplayedInfo();
        }

        private void LoadMail(int mailIndex)
        {
            AttachmentListBox.Items.Clear();
            AttachmentListBox.Visibility = Visibility.Hidden;

            MessageTextBlock.Text = _currentFolder[mailIndex].Text;
            AuthorLabel.Content = "By: " + _currentFolder[mailIndex].Author;
            TopicLabel.Content = "Subject: " + _currentFolder[mailIndex].Topic;
            TimeLabel.Content = _currentFolder[mailIndex].Time;

            // loading receivers
            String Receivers = "";
            foreach(String r in _currentFolder[mailIndex].Receivers)
            {
                Receivers += r;
                Receivers += "; ";
            }

            ReceiverLabel.Content = "To: " + Receivers;
            // loading attachments
            if (_currentFolder[mailIndex].Attachments.Count > 0)
            {
                AttachmentListBox.Visibility = Visibility.Visible;
                foreach (String a in _currentFolder[mailIndex].Attachments)
                {
                    AttachmentListBox.Items.Add(a);
                }
            }
        }
        public void DeleteMail(int mailIndex)
        {
            if (_currentFolder[mailIndex] != null)
            {
                if (_currentFolder == Mailbox1.Deleted || _currentFolder == Mailbox2.Deleted)
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

                    Mail mailCopy = new Mail(currentMail.Author, currentMail.Receivers, currentMail.Topic, currentMail.Text, currentMail.Time);

                    if (_currentFolder == Mailbox1.Inbox || _currentFolder == Mailbox1.Sent || _currentFolder == Mailbox1.Starred)
                    {
                        // 1st mailbox
                        Mailbox1.Deleted.Add(mailCopy);
                    }
                    else // 2nd mailbox
                    {
                        Mailbox2.Deleted.Add(mailCopy);
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

                Mail mailCopy = new Mail(currentMail.Author, currentMail.Receivers, currentMail.Topic, currentMail.Text, currentMail.Time);

                if (_currentFolder == Mailbox1.Inbox || _currentFolder == Mailbox1.Sent || _currentFolder == Mailbox1.Deleted)
                {
                    // 1st mailbox
                    Mailbox1.Starred.Add(mailCopy);
                }
                else if(_currentFolder == Mailbox2.Inbox || _currentFolder == Mailbox2.Sent || _currentFolder == Mailbox2.Deleted) // 2nd mailbox
                {
                    // 2nd mailbox
                    Mailbox2.Starred.Add(mailCopy);
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
            NewMessageWindow messageWindow = new NewMessageWindow(this);

            // passing email addresses
            PassEmailAdresses(messageWindow, Mailbox1.EmailAdress, Mailbox2.EmailAdress);

            ShowWindow(messageWindow);
        }

        private void PassEmailAdresses(NewMessageWindow messageWindow, params string[] emailAddresses)
        {
            foreach(string address in emailAddresses)
            {
                ComboBoxItem emailAddress = new ComboBoxItem();
                emailAddress.Content = address;
                messageWindow.MailboxComboBox.Items.Add(emailAddress);
            }

            messageWindow.MailboxComboBox.SelectedIndex = 0;
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            ChangeNewMessageWindowTitle("Forward", messageWindow);

            // passing email address
            PassEmailAdresses(messageWindow, GetCurrentMailboxAddress());

            // find selected mail
            int mailIndex = MessagesListView.SelectedIndex;
            var currentMail = _currentFolder[mailIndex];

            // rewrite content to message window
            messageWindow.SubjectTextBox.Text = "Fwd: " + currentMail.Topic;
            // rewrite previous message
            messageWindow.ContentTextBox.Text = "\n" + currentMail.Time +
                "\n" + currentMail.Author + ":" + "\n" + currentMail.Text;

            ShowWindow(messageWindow);
        }

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            // find selected mail
            int mailIndex = MessagesListView.SelectedIndex;
            var currentMail = _currentFolder[mailIndex];

            Reply(messageWindow, currentMail);

            messageWindow.RecipientTextBox.Text = currentMail.Author;

            ShowWindow(messageWindow);
        }
        private void ReplyAllButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            // find selected mail
            int mailIndex = MessagesListView.SelectedIndex;
            var currentMail = _currentFolder[mailIndex];

            Reply(messageWindow, currentMail);

            // rewrite all the recipients except this email address
            foreach (String r in currentMail.Receivers)
            {
                if (r != GetCurrentMailboxAddress())
                {
                    messageWindow.RecipientTextBox.Text += ";" + r;
                }
            }

            ShowWindow(messageWindow);
        }

        private void Reply(NewMessageWindow messageWindow, Mail currentMail)
        {
            ChangeNewMessageWindowTitle("Reply", messageWindow);

            RewritePreviousMessage(messageWindow, currentMail);

            // passing email address
            PassEmailAdresses(messageWindow, GetCurrentMailboxAddress());

            // rewrite author to recipient in message window
            messageWindow.RecipientTextBox.Text = currentMail.Author;
            messageWindow.SubjectTextBox.Text = "Re: " + currentMail.Topic;
        }

        private String GetCurrentMailboxAddress()
        {
            if(_currentFolder == Mailbox1.Inbox || _currentFolder == Mailbox1.Sent || _currentFolder == Mailbox1.Starred || _currentFolder == Mailbox1.Deleted)
            {
                return Mailbox1.EmailAdress;
            }
            else if(_currentFolder == Mailbox2.Inbox || _currentFolder == Mailbox2.Sent || _currentFolder == Mailbox2.Starred || _currentFolder == Mailbox2.Deleted)
            {
                return Mailbox2.EmailAdress;
            }

            return ""; 
        }

        private void Mail_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // inbox or sent folders
            if(_currentFolder == Mailbox1.Inbox || _currentFolder == Mailbox2.Inbox || _currentFolder == Mailbox1.Sent || _currentFolder == Mailbox2.Sent)
            {
                NewMessageWindow messageWindow = new NewMessageWindow(this);
                ChangeNewMessageWindowTitle("Message View", messageWindow);

                // load selected mail
                int mailIndex = MessagesListView.SelectedIndex;
                var currentMail = _currentFolder[mailIndex];
                messageWindow.MailboxComboBox.Items.Add(currentMail.Author);
                messageWindow.MailboxComboBox.SelectedIndex = 0;
                foreach (string receiver in currentMail.Receivers)
                {
                    messageWindow.RecipientTextBox.Text += receiver + ";";
                }
                messageWindow.SubjectTextBox.Text = currentMail.Topic;
                messageWindow.ContentTextBox.Text = currentMail.Time + "\n\n" + currentMail.Text;
                if (currentMail.Attachments.Count > 0)
                {
                    messageWindow.AttachmentListBox.Visibility = Visibility.Visible;
                    foreach (string attachment in currentMail.Attachments)
                    {
                        messageWindow.AttachmentListBox.Items.Add(attachment);
                    }
                }

                ReadOnlyMode(messageWindow);
                ShowWindow(messageWindow);
            }
        }
        private void ChangeNewMessageWindowTitle(string newTitle, NewMessageWindow nmWindow)
        {
            nmWindow.Title = newTitle;
            nmWindow.TitleLabel.Content = newTitle;
        }

        private void ReadOnlyMode(NewMessageWindow messageWindow)
        {
            // block/hide components in messageWindow (READONLY)

            messageWindow.MailboxComboBox.IsReadOnly = true;
            messageWindow.RecipientTextBox.IsReadOnly = true;
            messageWindow.SubjectTextBox.IsReadOnly = true;
            messageWindow.ContentTextBox.IsReadOnly = true;
            messageWindow.AttachmentButton.Visibility = Visibility.Collapsed;
            messageWindow.SendButton.Visibility = Visibility.Collapsed;

            // make textboxes background look non editable
            messageWindow.RecipientTextBox.Background = Brushes.LightGray;
            messageWindow.SubjectTextBox.Background = Brushes.LightGray;
            messageWindow.ContentTextBox.Background = Brushes.LightGray;
        }

        private void ShowWindow(Window window)
        {
            if (window.ShowDialog() == true)
            {
                // some changes in MainWindow if needed
            }
        }
        private void ClearAllDisplayedInfo()
        {
            MessageTextBlock.Text = "";
            AuthorLabel.Content = "";
            TopicLabel.Content = "";
            ReceiverLabel.Content = "";
            TimeLabel.Content = "";
            AttachmentListBox.Items.Clear();
            AttachmentListBox.Visibility = Visibility.Hidden;
        }

        private void RewritePreviousMessage(NewMessageWindow messageWindow, Mail currentMail)
        {
            // rewrite previous message
            const string breakLine = "\n\n\n- - - - - - - - - - - - - - - - - - - - - -";
            messageWindow.ContentTextBox.Text = breakLine + "\n" + currentMail.Time +
                "\n" + currentMail.Author + ":" + "\n" + currentMail.Text;
        }
    }
}
