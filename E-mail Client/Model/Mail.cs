using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace E_mail_Client.Model
{
    public class Mail: ListViewItem
    {
        public string Topic { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string Receiver { get; set; }
        public Mail(string author, string receiver, string topic, string content)
        {
            Author = author;
            Receiver = receiver;
            Topic = topic;
            Text = content;
            // ListViewItem properties
            Content = content;
            MouseLeftButtonUp += Mail_MouseLeftButtonUp;
        }

        public Mail(string topic, string content)
        {
            Text = content;
            Topic = topic;
            // ListViewItem properties
            Content = topic;
            MouseLeftButtonUp += Mail_MouseLeftButtonUp;
        }

        public Mail()
        {
            // ListViewItem properties
            MouseLeftButtonUp += Mail_MouseLeftButtonUp;
        }
        private void Mail_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Controller controller = new Controller();
            controller.SetCurrentMail(this);
            controller.EnableDeleteButton();
            controller.LoadMail(this);
        }
    }
}
