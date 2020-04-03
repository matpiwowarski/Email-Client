﻿using System;
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

        // hashset because email adresses should be unique
        public HashSet<String> Receivers = new HashSet<String>();

        public List<String> Attachments = new List<String>();

        // one receiver
        public Mail(String author, String receiver, String topic, String content)
        {
            Author = author;
            Receivers.Add(receiver);
            Topic = topic;
            Text = content;
        }
        // more receivers
        public Mail(String author, HashSet<String> receivers, String topic, String content)
        {
            Author = author;
            Receivers = receivers;
            Topic = topic;
            Text = content;
        }

        public void AddAttachment(String attachment)
        {
            this.Attachments.Add(attachment);
        }
    }
}
