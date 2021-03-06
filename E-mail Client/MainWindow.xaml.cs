﻿using E_mail_Client.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace E_mail_Client
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Mailbox> Mailboxes;
        private Mailbox _currentMailbox = new Mailbox();
        private Folder _currentFolder = new Folder();
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public Storyboard Story = new Storyboard();
        public MainWindow()
        {
            InitializeComponent();

            Mailboxes = new ObservableCollection<Mailbox>();

            // to not import data every program run
            ///
            Deserialize("../../../Data/data.xml");
            CreateTreeViewForMailboxList(Mailboxes);
            /// generate random message every 10 seconds
            dispatcherTimer.Tick += new EventHandler(SendRandomMessage);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            // new message animation
            StringAnimationUsingKeyFrames frames = new StringAnimationUsingKeyFrames();
            frames.Duration = new Duration(new TimeSpan(0, 0, 6));
            frames.FillBehavior = FillBehavior.HoldEnd;
            frames.KeyFrames.Add(new DiscreteStringKeyFrame("New ", TimeSpan.FromSeconds(1)));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame("New Message", TimeSpan.FromSeconds(2)));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame("New Message!", TimeSpan.FromSeconds(3)));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame("", TimeSpan.FromSeconds(6)));

            Storyboard.SetTargetName(frames, NewMessageLabel.Name);
            Storyboard.SetTargetProperty(frames, new PropertyPath(Label.ContentProperty));
            Story.Children.Add(frames);
        }
        private void CreateTreeViewForMailboxList(ObservableCollection<Mailbox> mailboxList)
        {
            EmailTreeView.Items.Clear();
            foreach (Mailbox m in mailboxList)
            {
                CreateTreeViewItemForMailbox(m.EmailAddress);
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
        private Mailbox FindCurrentMailbox(string emailAddress)
        {
            foreach(Mailbox m in Mailboxes)
            {
                if(m.EmailAddress == emailAddress)
                {
                    return m;
                }
            }
            return null;
        }
        private void Folder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedFolder = (StackPanel)EmailTreeView.SelectedItem;
            TreeViewItem selectedMailbox = (TreeViewItem)selectedFolder.Parent;

            _currentMailbox = FindCurrentMailbox(selectedMailbox.Header.ToString());

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
        private void LoadMails(ObservableCollection<Mail> mails)
        {
            MessagesListView.ItemsSource = mails;

            ClearAllDisplayedInfo();
        }
        private void LoadMail(int mailIndex)
        {
            MessageView.AttachmentListBox.Items.Clear();
            MessageView.AttachmentListBox.Visibility = Visibility.Collapsed;

            // read mail
            _currentFolder[mailIndex].Read = true;

            LoadFormattedText(_currentFolder[mailIndex]);
            MessageView.AuthorLabel.Content = "By: " + _currentFolder[mailIndex].Author;
            MessageView.TopicLabel.Content = "Subject: " + _currentFolder[mailIndex].Topic;
            MessageView.TimeLabel.Content = _currentFolder[mailIndex].Time;

            // loading receivers
            String Receivers = "";
            foreach(String r in _currentFolder[mailIndex].Receivers)
            {
                Receivers += r;
                Receivers += "; ";
            }

            MessageView.ReceiverLabel.Content = "To: " + Receivers;
            // loading attachments
            if (_currentFolder[mailIndex].Attachments.Count > 0)
            {
                MessageView.AttachmentListBox.Visibility = Visibility.Visible;
                foreach (String a in _currentFolder[mailIndex].Attachments)
                {
                    MessageView.AttachmentListBox.Items.Add(a);
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
                            //
                            EllipseStoryboard.Begin(Dot);
                            VisibilityStoryboard.Begin(DeletedMessageLabel);
                            VisibilityStoryboard.Begin(Dot);

                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                else // other folder = move to Deleted items
                {
                    var currentMail = GetCurrentMail();

                    Mail mailCopy = new Mail(currentMail);

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

                Mail mailCopy = new Mail(currentMail);

                _currentMailbox.Starred.Add(mailCopy);

                _currentFolder.RemoveAt(mailIndex);

                DisableAllButtons();
                LoadMails(_currentFolder);
            }
        }
        private void NewMessage_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);

            // passing email addresses
            
            PassEmailAdresses(messageWindow, Mailboxes);

            ShowWindow(messageWindow);
        }
        private void PassEmailAdresses(NewMessageWindow messageWindow, ObservableCollection<Mailbox> mailboxes)
        {
            foreach(Mailbox m in mailboxes)
            {
                ComboBoxItem emailAddress = new ComboBoxItem();
                emailAddress.Content = m.EmailAddress;
                messageWindow.MailInfo.MailboxComboBox.Items.Add(emailAddress);

                messageWindow.MailInfo.MailboxComboBox.SelectedIndex = 0;
            }
        }
        private void PassEmailAdress(NewMessageWindow messageWindow, string emailAddress)
        {
            ComboBoxItem emailAddressItem = new ComboBoxItem();
            emailAddressItem.Content = emailAddress;
            messageWindow.MailInfo.MailboxComboBox.Items.Add(emailAddressItem);

            messageWindow.MailInfo.MailboxComboBox.SelectedIndex = 0;
        }
        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            ChangeNewMessageWindowTitle("Forward", messageWindow);

            // passing email address
            PassEmailAdress(messageWindow, _currentMailbox.EmailAddress);

            // find selected mail
            var currentMail = GetCurrentMail();

            LoadFormattedText(currentMail, messageWindow);

            messageWindow.MailInfo.SubjectTextBox.Text = "Fwd: " + currentMail.Topic;
            // rewrite previous message
            const string breakLine = "\n- - - - - - - - - - - - - - - - - - - - - -\n";
            string message = breakLine + currentMail.Time +
                "\n" + currentMail.Author + ":";

            AddStringAtBeginning(message, messageWindow);
            
            ShowWindow(messageWindow);
        }
        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            NewMessageWindow messageWindow = new NewMessageWindow(this);
            // find selected mail
            var currentMail = GetCurrentMail();

            Reply(messageWindow, currentMail);

            messageWindow.MailInfo.RecipientTextBox.Text = currentMail.Author;

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
                if (r != _currentMailbox.EmailAddress)
                {
                    messageWindow.MailInfo.RecipientTextBox.Text += ";" + r;
                }
            }

            ShowWindow(messageWindow);
        }
        private void Reply(NewMessageWindow messageWindow, Mail currentMail)
        {
            ChangeNewMessageWindowTitle("Reply", messageWindow);

            RewritePreviousMessage(messageWindow, currentMail);

            // passing email address
            PassEmailAdress(messageWindow, _currentMailbox.EmailAddress);

            // rewrite author to recipient in message window
            messageWindow.MailInfo.RecipientTextBox.Text = currentMail.Author;
            messageWindow.MailInfo.SubjectTextBox.Text = "Re: " + currentMail.Topic;
        }
        private void Mail_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableAllButtons();
            LoadMail(MessagesListView.SelectedIndex);
        }
        private void Mail_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // inbox or sent folders
            if(_currentFolder.Type == FolderType.INBOX || _currentFolder.Type == FolderType.SENT || _currentFolder.Type == FolderType.STARRED)
            {
                NewMessageWindow messageWindow = new NewMessageWindow(this);
                ChangeNewMessageWindowTitle("Message View", messageWindow);

                // load selected mail
                var currentMail = GetCurrentMail();
                // Read mail
                currentMail.Read = true;
                messageWindow.MailInfo.MailboxComboBox.Items.Add(currentMail.Author);
                messageWindow.MailInfo.MailboxComboBox.SelectedIndex = 0;
                foreach (string receiver in currentMail.Receivers)
                {
                    messageWindow.MailInfo.RecipientTextBox.Text += receiver + ";";
                }
                messageWindow.MailInfo.SubjectTextBox.Text = currentMail.Topic;

                // loading formatted text from string
                LoadFormattedText(currentMail, messageWindow);

                // display mail time
                const string breakLine = "\n- - - - - - - - - - - - - - - - - - - - - -";
                string message = currentMail.Time + breakLine;

                AddStringAtBeginning(message, messageWindow);

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

            messageWindow.MailInfo.MailboxComboBox.IsReadOnly = true;
            messageWindow.MailInfo.RecipientTextBox.IsReadOnly = true;
            messageWindow.MailInfo.SubjectTextBox.IsReadOnly = true;
            messageWindow.TextEditor.IsReadOnly = true;
            messageWindow.AttachmentButton.Visibility = Visibility.Collapsed;
            messageWindow.SendButton.Visibility = Visibility.Collapsed;

            // make textboxes background look non editable
            messageWindow.MailInfo.RecipientTextBox.Background = Brushes.LightGray;
            messageWindow.MailInfo.SubjectTextBox.Background = Brushes.LightGray;
            messageWindow.TextEditor.ContentBox.Background = Brushes.LightGray;
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
            // clear content
            TextRange txt = new TextRange(MessageView.ContentBox.Document.ContentStart,
                MessageView.ContentBox.Document.ContentEnd);
            txt.Text = "";
            // clear the rest
            MessageView.AuthorLabel.Content = "";
            MessageView.TopicLabel.Content = "";
            MessageView.ReceiverLabel.Content = "";
            MessageView.TimeLabel.Content = "";
            MessageView.AttachmentListBox.Items.Clear();
            MessageView.AttachmentListBox.Visibility = Visibility.Collapsed;
        }
        private void RewritePreviousMessage(NewMessageWindow messageWindow, Mail currentMail)
        {
            // rewrite previous message
            const string breakLine = "\n- - - - - - - - - - - - - - - - - - - - - -\n";
            string message = breakLine + currentMail.Time +
                "\n" + currentMail.Author + ":";

            LoadFormattedText(currentMail, messageWindow);
            AddStringAtBeginning(message, messageWindow);
        }
        private Mail GetCurrentMail()
        {
            // find selected mail
            int mailIndex = MessagesListView.SelectedIndex;
            var currentMail = _currentFolder[mailIndex];
    
            return currentMail;
        }
        private void Serialize(string filePath)
        {
            using(Stream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Mailbox>));
                serializer.Serialize(fs, Mailboxes);
            }
        }
        private void Deserialize(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Mailbox>));

            using (FileStream fs = File.OpenRead(filePath))
            {
                Mailboxes = (ObservableCollection<Mailbox>)deserializer.Deserialize(fs);
            }
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Import XML file...";
            var filter = "XML files (*.XML)|*.XML";

            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                Deserialize(filePath);
            }

            CreateTreeViewForMailboxList(Mailboxes);
        }
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();

            openFileDialog.Title = "Export XML file...";
            var filter = "XML files (*.XML)|*.XML";

            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                Serialize(filePath);
            }
        }

        public static void AddStringAtBeginning(string text, NewMessageWindow newMessageWindow)
        {
            Paragraph p = new Paragraph(new Run(text));

            newMessageWindow.TextEditor.ContentBox.Document.Blocks.InsertBefore(
                newMessageWindow.TextEditor.ContentBox.Document.Blocks.FirstBlock, p);
        }
        public static void LoadFormattedText(Mail m, NewMessageWindow newMessageWindow)
        {
            // loading formatted text from string
            string xmlString = m.Text;
            byte[] byteArray = Encoding.ASCII.GetBytes(xmlString);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(newMessageWindow.TextEditor.ContentBox.Document.ContentStart,
                    newMessageWindow.TextEditor.ContentBox.Document.ContentEnd);
                tr.Load(ms, DataFormats.Xaml);
            }
        }
        private void LoadFormattedText(Mail m)
        {
            // loading formatted text from string
            string xmlString = m.Text;
            byte[] byteArray = Encoding.ASCII.GetBytes(xmlString);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(MessageView.ContentBox.Document.ContentStart,
                    MessageView.ContentBox.Document.ContentEnd);
                tr.Load(ms, DataFormats.Xaml);
            }
        }
        private void SendRandomMessage(object sender, EventArgs e)
        {
            MailGenerator generator = new MailGenerator();
            Mail mail = generator.GenerateMail();

            // generate mail every 10 seconds for 3rd maiilbox
            Mailboxes[2].Inbox.Add(mail);
            Story.Begin(this);
        }
    }
}
