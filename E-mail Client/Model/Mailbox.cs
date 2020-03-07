using System;
using System.Collections.Generic;
using System.Text;

namespace E_mail_Client.Model
{
    public class Mailbox
    {
        public string MailAddress;

        public List<Folder> Folders { get; set; }

        public Mailbox(string mailAddress)
        {
            this.Folders = new List<Folder>();
            this.MailAddress = mailAddress;

            Folder inbox = new Folder("Inbox");
            Folder sent = new Folder("Sent items");
            Folder deleted = new Folder("Deleted items");
            Folder starred = new Folder("Starred");

            AddFolders(inbox, sent, deleted, starred);
        }
        public void AddFolders(params Folder[] folders)
        {
            foreach (Folder folder in folders)
            {
                Folders.Add(folder);
            }
        }
    }
}
