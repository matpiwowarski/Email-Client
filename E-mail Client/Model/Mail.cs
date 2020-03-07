using System;
using System.Collections.Generic;
using System.Text;

namespace E_mail_Client.Model
{
    public class Mail
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Receiver { get; set; }
        public Mail(string author, string receiver, string topic, string content)
        {
            Author = author;
            Receiver = receiver;
            Topic = topic;
            Content = content;
        }
    }
}
