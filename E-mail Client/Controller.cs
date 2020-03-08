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

        public void AddMails(Mailbox mailbox, params Mail[] mails)
        {
            mailbox.Inbox.AddMails(mails);
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
        }
        public void LoadMail(Mail mail)
        {
            _currentMail = mail;
            _window.MessageTextBlock.Text = mail.Text;
        }

        public void DeleteCurrentMail()
        {
            if(_currentMail != null)
            {
                if (_currentFolder.FolderName == "Deleted items")
                {
                    MessageBoxResult result = MessageBox.Show("“Do you really wish to delete the message?", "Delete message", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            _currentFolder.Mails.Remove(_currentMail);
                            _currentMail = null;
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
                    LoadMails(_currentFolder);
                }
            }
        }

        public void setCurrentMail(Mail mail)
        {
            _currentMail = mail;
        }
    }
}
