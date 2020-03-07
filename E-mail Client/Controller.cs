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
        private MainWindow _window;

        public Controller(MainWindow window)
        {
            _window = window;
        }

        public void LoadTreeViewEmails(params Mailbox[] mailboxes)
        {
            _window.EmailTreeView.Items.Clear();

            foreach(Mailbox m in mailboxes)
            {
                TreeViewItem mailbox = new TreeViewItem();
                mailbox.Header = m.MailAddress;
                mailbox.Foreground = Brushes.White;

                _window.EmailTreeView.Items.Add(mailbox);
            }
        }
    }
}
