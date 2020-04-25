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
        public List<Mailbox> Mailboxes;
        private Mailbox _currentMailbox = new Mailbox();
        private Folder _currentFolder = new Folder();

        public MainWindow()
        {
            InitializeComponent();

            Mailboxes = new List<Mailbox>();
            Mailboxes.Add(new Mailbox("mateusz.piwowarski@student.um.si"));
            Mailboxes.Add(new Mailbox("matpiwowarski7@gmail.com"));
            Mailboxes.Add(new Mailbox("test@test.pl"));

            // loading data
            Mail mail1 = new Mail("author1", "receiver1", "topic1", "content1");
            Mailboxes[2].Inbox.Add(mail1);
            Mail mail2 = new Mail("author2", "receiver2", "topic2", "content2");
            Mailboxes[2].Starred.Add(mail2);
            Mail mail3 = new Mail("author3", "receiver3", "topic3", "content3");
            Mailboxes[2].Sent.Add(mail3);
            Mail mail4 = new Mail("author4", "receiver4", "topic4", "content4");
            Mailboxes[2].Deleted.Add(mail4);

            CreateTreeViewForMailboxList(Mailboxes);
        }

        private void CreateTreeViewForMailboxList(List<Mailbox> mailboxList)
        {
            foreach(Mailbox m in mailboxList)
            {
                CreateTreeViewItemForMailbox(m.EmailAdress);
            }
        }

        private void CreateTreeViewItemForMailbox(string emailAddress)
        {
            // create mailbox to xaml
            TreeViewItem mailbox = new TreeViewItem();
            mailbox.Header = emailAddress;
            mailbox.MouseLeftButtonUp += Email_MouseLeftButtonUp;
            mailbox.FontSize = 15;
            // create subfolders to xaml
            CreateSubfolderStackPanelForMailbox("Inbox", "Resources/folder.png", mailbox);
            CreateSubfolderStackPanelForMailbox("Sent items", "Resources/folder.png", mailbox);
            CreateSubfolderStackPanelForMailbox("Deleted items", "Resources/deleted.png", mailbox);
            CreateSubfolderStackPanelForMailbox("Starred", "Resources/starred.png", mailbox);

            // adding ready mailbox into TreeView
            EmailTreeView.Items.Add(mailbox);                  
        }
        private void CreateSubfolderStackPanelForMailbox(string subfolderName, string iconPath, TreeViewItem parentMailbox)
        {
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            stackPanel.MouseLeftButtonUp += Folder_MouseLeftButtonUp;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(iconPath, UriKind.Relative));
            img.Width = 20;
            img.Height = 20;
            Label label = new Label() { Content = subfolderName };
            stackPanel.Children.Add(img);
            stackPanel.Children.Add(label);

            // adding ready subfolder stackpanel into mailbox treeviewitem
            parentMailbox.Items.Add(stackPanel);
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

        private void Folder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedFolder = (StackPanel)EmailTreeView.SelectedItem;
            TreeViewItem selectedMailbox = (TreeViewItem)selectedFolder.Parent;

            _currentMailbox = Mailboxes.Find(a => a.EmailAdress == selectedMailbox.Header);

            if (sender is StackPanel)
            {
                string folderName = ""; // name of folder that we clicked

                StackPanel folder = (StackPanel)sender;
                if(folder.Children[1] is Label) // [0] is image
                {
                    Label label = (Label)folder.Children[1];
                    folderName = label.Content.ToString(); // we are getting name of the clicked folder
                }

                if(folderName == "Inbox")
                {
                    _currentFolder = _currentMailbox.Inbox;
                }
                else if (folderName == "Sent items")
                {
                    _currentFolder = _currentMailbox.Sent;
                }
                else if (folderName == "Deleted items")
                {
                    _currentFolder = _currentMailbox.Deleted;
                }
                else if (folderName == "Starred")
                {
                    _currentFolder = _currentMailbox.Starred;
                }     
            }
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
                if (_currentFolder.Type == FolderType.DELETED)
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
                    var currentMail = GetCurrentMail();

                    Mail mailCopy = new Mail(currentMail.Author, currentMail.Receivers, currentMail.Topic, currentMail.Text, currentMail.Time);

                    _currentMailbox.Deleted.Add(mailCopy);

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

                var currentMail = GetCurrentMail();

                Mail mailCopy = new Mail(currentMail.Author, currentMail.Receivers, currentMail.Topic, currentMail.Text, currentMail.Time);

                _currentMailbox.Starred.Add(mailCopy);

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
            
            PassEmailAdresses(messageWindow, Mailboxes);

            ShowWindow(messageWindow);
        }

        private void PassEmailAdresses(NewMessageWindow messageWindow, List<Mailbox> mailboxes)
        {
            foreach(Mailbox m in mailboxes)
            {
                ComboBoxItem emailAddress = new ComboBoxItem();
                emailAddress.Content = m.EmailAdress;
                messageWindow.MailboxComboBox.Items.Add(emailAddress);
            }

            messageWindow.MailboxComboBox.SelectedIndex = 0;
        }

        private void PassEmailAdress(NewMessageWindow messageWindow, string emailAddress)
        {
            ComboBoxItem emailAddressItem = new ComboBoxItem();
            emailAddressItem.Content = emailAddress;
            messageWindow.MailboxComboBox.Items.Add(emailAddressItem);

            messageWindow.MailboxComboBox.SelectedIndex = 0;
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            ChangeNewMessageWindowTitle("Forward", messageWindow);

            // passing email address
            PassEmailAdress(messageWindow, _currentMailbox.EmailAdress);

            // find selected mail
            var currentMail = GetCurrentMail();

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
            var currentMail = GetCurrentMail();

            Reply(messageWindow, currentMail);

            messageWindow.RecipientTextBox.Text = currentMail.Author;

            ShowWindow(messageWindow);
        }
        private void ReplyAllButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            var currentMail = GetCurrentMail();

            Reply(messageWindow, currentMail);

            // rewrite all the recipients except this email address
            foreach (String r in currentMail.Receivers)
            {
                if (r != _currentMailbox.EmailAdress)
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
            PassEmailAdress(messageWindow, _currentMailbox.EmailAdress);

            // rewrite author to recipient in message window
            messageWindow.RecipientTextBox.Text = currentMail.Author;
            messageWindow.SubjectTextBox.Text = "Re: " + currentMail.Topic;
        }

        private void Mail_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // inbox or sent folders
            if(_currentFolder.Type == FolderType.INBOX || _currentFolder.Type == FolderType.SENT)
            {
                NewMessageWindow messageWindow = new NewMessageWindow(this);
                ChangeNewMessageWindowTitle("Message View", messageWindow);

                // load selected mail
                var currentMail = GetCurrentMail();
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

        private Mail GetCurrentMail()
        {
            // find selected mail
            int mailIndex = MessagesListView.SelectedIndex;
            var currentMail = _currentFolder[mailIndex];
    
            return currentMail;
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
