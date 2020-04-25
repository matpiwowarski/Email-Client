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
        private List<Mail> _currentFolder;

        public MainWindow()
        {
            InitializeComponent();

            Mailboxes = new List<Mailbox>();
            Mailboxes.Add(new Mailbox("mateusz.piwowarski@student.um.si"));
            Mailboxes.Add(new Mailbox("matpiwowarski7@gmail.com"));
            Mailboxes.Add(new Mailbox("test@test.pl"));
            CreateTreeViewItemForMailbox(Mailboxes[0].EmailAdress);
        }

        private void CreateTreeViewItemForMailbox(string emailAddress)
        {
            // create mailbox to xaml
            TreeViewItem mailbox = new TreeViewItem();
            mailbox.Header = emailAddress;
            mailbox.MouseLeftButtonUp += Email_MouseLeftButtonUp;
            mailbox.FontSize = 15;
            // create subfolders to xaml
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            stackPanel.MouseLeftButtonUp += Inbox1_MouseLeftButtonUp;
            mailbox.Items.Add(stackPanel);
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("Resources/folder.png", UriKind.Relative));
            img.Width = 20;
            img.Height = 20;
            Label label = new Label() { Content = "Inbox" };
            stackPanel.Children.Add(img);
            stackPanel.Children.Add(label);

            // adding ready mailbox into TreeView
            EmailTreeView.Items.Add(mailbox);



                 /*
                                 < StackPanel Orientation = "Horizontal" MouseLeftButtonUp = "Sent1_MouseLeftButtonUp" >
                    
                                        < Image Source = "Resources/folder.png" Width = "20" Height = "20" ></ Image >
                         
                                             < Label Content = "Sent items" ></ Label >
                          
                                          </ StackPanel >
                          
                                          < StackPanel Orientation = "Horizontal" MouseLeftButtonUp = "Deleted1_MouseLeftButtonUp" >
                             
                                                 < Image Source = "Resources/deleted.png" Width = "20" Height = "20" ></ Image >
                                  
                                                      < Label Content = "Deleted items" ></ Label >
                                   
                                                   </ StackPanel >
                                   
                                                   < StackPanel Orientation = "Horizontal" MouseLeftButtonUp = "Starred1_MouseLeftButtonUp" >
                                      
                                                          < Image Source = "Resources/starred.png" Width = "20" Height = "20" ></ Image >
                                           
                                                               < Label Content = "Starred" ></ Label >
                                            
                                                            </ StackPanel >
                                                            */
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
            //_currentFolder = Mailbox1.Inbox;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Sent1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox1.Sent;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Starred1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox1.Starred;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Deleted1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox1.Deleted;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Inbox2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox2.Inbox;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Sent2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox2.Sent;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Starred2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox2.Starred;
            DisableAllButtons();
            LoadMails(_currentFolder);
        }

        private void Deleted2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_currentFolder = Mailbox2.Deleted;
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
            /*
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
                    var currentMail = GetCurrentMail();

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
            */
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (MessagesListView.SelectedItems.Count > 0)
            { 
                int mailIndex = MessagesListView.SelectedIndex;

                var currentMail = GetCurrentMail();

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
            */
        }

        // 3rd assignment
        private void NewMessage_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);

            // passing email addresses
            
            ////////////////PassEmailAdresses(messageWindow, Mailbox1.EmailAdress, Mailbox2.EmailAdress);

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
            /*
            if(_currentFolder == Mailbox1.Inbox || _currentFolder == Mailbox1.Sent || _currentFolder == Mailbox1.Starred || _currentFolder == Mailbox1.Deleted)
            {
                return Mailbox1.EmailAdress;
            }
            else if(_currentFolder == Mailbox2.Inbox || _currentFolder == Mailbox2.Sent || _currentFolder == Mailbox2.Starred || _currentFolder == Mailbox2.Deleted)
            {
                return Mailbox2.EmailAdress;
            }
            */
            return ""; 
        }

        private void Mail_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*
            // inbox or sent folders
            if(_currentFolder == Mailbox1.Inbox || _currentFolder == Mailbox2.Inbox || _currentFolder == Mailbox1.Sent || _currentFolder == Mailbox2.Sent)
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
            */
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

        Mail GetCurrentMail()
        {
            // find selected mail
            int mailIndex = MessagesListView.SelectedIndex;
            var currentMail = _currentFolder[mailIndex];
            return currentMail;
        }
    }
}
