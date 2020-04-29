using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;

namespace E_mail_Client.Model
{
    [Serializable()]
    public class Mail : ISerializable, INotifyPropertyChanged
    {
        public String Topic { get; set; }
        public String Text { get; set; }
        public String Author { get; set; }
        public bool Read { get; set; } = false;
        public DateTime Time { get; set; }

        // hashset because email adresses should be unique
        public ObservableCollection<String> Receivers = new ObservableCollection<String>();

        public ObservableCollection<String> Attachments = new ObservableCollection<String>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Mail()
        {

        }

        public Mail(SerializationInfo info, StreamingContext context)
        {
            Topic = (String)info.GetValue("Topic", typeof(String));
            Text = (String)info.GetValue("Text", typeof(String));
            Author = (String)info.GetValue("Author", typeof(String));
            Time = (DateTime)info.GetValue("Time", typeof(DateTime));
            Read = (bool)info.GetValue("Read", typeof(bool));
            Receivers = (ObservableCollection<String>)info.GetValue("Receivers", typeof(ObservableCollection<String>));
            Attachments = (ObservableCollection<String>)info.GetValue("Attachments", typeof(ObservableCollection<String>));
        }

        // one receiver
        public Mail(String author, String receiver, String topic, String content)
        {
            Author = author;
            Receivers.Add(receiver);
            Topic = topic;
            Text = content;
            Time = DateTime.Now;
        }
        // more receivers
        public Mail(String author, ObservableCollection<String> receivers, String topic, String content)
        {
            Author = author;
            Receivers = receivers;
            Topic = topic;
            Text = content;
            Time = DateTime.Now;
        }

        public Mail(String author, ObservableCollection<String> receivers, String topic, String content, DateTime time)
        {
            Author = author;
            Receivers = receivers;
            Topic = topic;
            Text = content;
            Time = time;
        }

        public void AddAttachment(String attachment)
        {
            this.Attachments.Add(attachment);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Topic", Topic);
            info.AddValue("Text", Text);
            info.AddValue("Author", Author);
            info.AddValue("Time", Time);
            info.AddValue("Receivers", Receivers);
            info.AddValue("Attachments", Attachments);
            info.AddValue("Read", Read);
        }
    }
}
