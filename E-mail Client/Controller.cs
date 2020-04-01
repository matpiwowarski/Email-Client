using E_mail_Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_mail_Client
{
    public class Controller
    {
        static private MainWindow _window;
        static private Mail _currentMail;
        static private Folder _currentFolder;

        public Controller()
        {

        }
        public Controller(MainWindow window)
        {
            _window = window;
        }

        public void LoadTreeViewEmails(params Mailbox[] mailboxes)
        {
            _window.EmailTreeView.Items.Clear();

            foreach(Mailbox m in mailboxes)
            {
                _window.EmailTreeView.Items.Add(m);
            }
        }

        public void AddMailsToInbox(Mailbox mailbox, params Mail[] mails)
        {
            mailbox.Inbox.AddMails(mails);
        }
        public void AddMailsToSentItems(Mailbox mailbox, params Mail[] mails)
        {
            mailbox.Sent.AddMails(mails);
        }
        public void AddMailsToDeleted(Mailbox mailbox, params Mail[] mails)
        {
            mailbox.Deleted.AddMails(mails);
        }
        public void AddMailsToStarred(Mailbox mailbox, params Mail[] mails)
        {
            mailbox.Starred.AddMails(mails);
        }

        public void LoadMails(Folder folder)
        {
            _currentFolder = folder;
            _window.MessagesListView.Items.Clear();

            foreach(Mail m in folder.Mails)
            {
                _window.MessagesListView.Items.Add(m);
            }
            _window.MessageTextBlock.Text = "";
            _window.AuthorLabel.Content = "";
            _window.TopicLabel.Content = "";
        }
        public void LoadMail(Mail mail)
        {
            _window.MessageTextBlock.Text = mail.Text;
            _window.AuthorLabel.Content = mail.Author;
            _window.TopicLabel.Content = mail.Topic;
        }

        public void DeleteCurrentMail()
        {
            if(_currentMail != null)
            {
                if (_currentFolder.FolderName == "Deleted items")
                {
                    MessageBoxResult result = MessageBox.Show("Do you really wish to delete the message?", "Delete message", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _currentFolder.Mails.Remove(_currentMail);
                            _currentMail = null;
                            DisableDeleteButton();
                            LoadMails(_currentFolder);
                            
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                else // other folder = move to Deleted items
                {
                    Mailbox currentMailbox = _currentFolder.Email;

                    Mail mailCopy = new Mail(_currentMail.Author, _currentMail.Receiver, _currentMail.Topic, _currentMail.Text);

                    currentMailbox.Deleted.Mails.Add(mailCopy);

                    _currentFolder.Mails.Remove(_currentMail);
                    _currentMail = null;
                    DisableDeleteButton();
                    LoadMails(_currentFolder);
                }
            }
        }

        public void SetCurrentMail(Mail mail)
        {
            _currentMail = mail;
        }

        public void EnableDeleteButton()
        {
            _window.DeleteButton.IsEnabled = true;
        }

        public void DisableDeleteButton()
        {
            _window.DeleteButton.IsEnabled = false;
        }
    }
}
