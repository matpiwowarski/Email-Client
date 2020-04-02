using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace E_mail_Client.Model
{
    public class Mail
    {
        public String Topic { get; set; }
        public String Text { get; set; }
        public String Author { get; set; }
        public String Receiver { get; set; }
        public Mail(string author, string receiver, string topic, string content)
        {
            Author = author;
            Receiver = receiver;
            Topic = topic;
            Text = content;
        }
    }
}
