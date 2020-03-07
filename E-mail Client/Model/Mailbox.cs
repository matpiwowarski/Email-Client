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

        public Folder Inbox = new Folder("Inbox");
        public Folder Sent = new Folder("Sent items");
        public Folder Deleted = new Folder("Deleted items");
        public Folder Starred = new Folder("Starred");

        public Mailbox(string mailAddress)
        {
            this.MailAddress = mailAddress;

            this.Inbox = new Folder("Inbox");
            this.Sent = new Folder("Sent items");
            this.Deleted = new Folder("Deleted items");
            this.Starred = new Folder("Starred");


            // TreeViewItem properties
            this.Header = mailAddress;
            this.Foreground = Brushes.White;
            this.Items.Add(Inbox);
            this.Items.Add(Sent);
            this.Items.Add(Deleted);
            this.Items.Add(Starred);
        }
    }
}
