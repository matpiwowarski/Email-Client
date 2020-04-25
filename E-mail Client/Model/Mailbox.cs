using System;
using System.Collections.Generic;
using System.Text;

namespace E_mail_Client.Model
{
    public class Mailbox
    {
        public String EmailAdress { get; set; }

        public Folder Inbox = new Folder(FolderType.INBOX);
        public Folder Sent = new Folder(FolderType.SENT);
        public Folder Deleted = new Folder(FolderType.DELETED);
        public Folder Starred = new Folder(FolderType.STARRED);

        public Mailbox(String emailAddress)
        {
            EmailAdress = emailAddress;
        }
    }
}
