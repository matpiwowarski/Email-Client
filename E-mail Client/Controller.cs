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
            _window.MessagesListView.Items.Clear();

            foreach(Mail m in folder.Mails)
            {
                _window.MessagesListView.Items.Add(m);
            }
        }
    }
}
