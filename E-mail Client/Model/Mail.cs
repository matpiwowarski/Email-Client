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

        public List<String> Receivers = new List<String>();

        // one receiver
        public Mail(String author, String receiver, String topic, String content)
        {
            Author = author;
            Receivers.Add(receiver);
            Topic = topic;
            Text = content;
        }
        // more receivers
        public Mail(String author, List<String> receivers, String topic, String content)
        {
            Author = author;
            Receivers = receivers;
            Topic = topic;
            Text = content;
        }
    }
}
