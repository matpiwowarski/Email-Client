using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace E_mail_Client.Model
{
    [Serializable()]
    public class Mailbox : ISerializable
    {
        public String EmailAdress { get; set; }

        public Folder Inbox = new Folder(FolderType.INBOX);
        public Folder Sent = new Folder(FolderType.SENT);
        public Folder Deleted = new Folder(FolderType.DELETED);
        public Folder Starred = new Folder(FolderType.STARRED);
        public Mailbox()
        {

        }
        public Mailbox(String emailAddress)
        {
            EmailAdress = emailAddress;
        }

        public Mailbox(SerializationInfo info, StreamingContext context)
        {
            EmailAdress = (String)info.GetValue("EmailAddress", typeof(String));
            Inbox = (Folder)info.GetValue("Inbox", typeof(Folder));
            Sent = (Folder)info.GetValue("Sent", typeof(Folder));
            Deleted = (Folder)info.GetValue("Deleted", typeof(Folder));
            Starred = (Folder)info.GetValue("Starred", typeof(Folder));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EmailAddress", EmailAdress);
            info.AddValue("Inbox", Inbox);
            info.AddValue("Sent", Sent);
            info.AddValue("Deleted", Deleted);
            info.AddValue("Starred", Starred);   
        }
    }
}
