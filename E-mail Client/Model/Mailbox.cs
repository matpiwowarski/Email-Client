using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_mail_Client.Model
{
    public class Mailbox: TreeViewItem
    {
        public string MailAddress;

        public Folder Inbox;
        public Folder Sent;
        public Folder Deleted;
        public Folder Starred;

        public Mailbox(string mailAddress)
        {
            MailAddress = mailAddress;

            Inbox = new Folder("Inbox", this);
            Sent = new Folder("Sent items", this);
            Deleted = new Folder("Deleted items", this);
            Starred = new Folder("Starred", this);


            // TreeViewItem properties
            Header = mailAddress;
            Foreground = Brushes.White;
            Items.Add(Inbox);
            Items.Add(Sent);
            Items.Add(Deleted);
            Items.Add(Starred);
            MouseLeftButtonUp += Mailbox_MouseLeftButtonUp;
        }

        private void Mailbox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Controller controller = new Controller();
            controller.DisableDeleteButton();
        }
    }
}
