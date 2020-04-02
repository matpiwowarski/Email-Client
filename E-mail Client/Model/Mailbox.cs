using System;
using System.Collections.Generic;
using System.Text;

namespace E_mail_Client.Model
{
    public class Mailbox
    {
        public String EmailAdress { get; set; }

        public List<Mail> Inbox = new List<Mail>();
        public List<Mail> Sent = new List<Mail>();
        public List<Mail> Deleted = new List<Mail>();
        public List<Mail> Starred = new List<Mail>();

        public Mailbox(String emailAddress)
        {
            EmailAdress = emailAddress;
        }
    }
}
