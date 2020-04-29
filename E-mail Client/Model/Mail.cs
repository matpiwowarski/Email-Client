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
        // private
        private String _topic;
        private String _text;
        private String _author;
        private bool _read;
        private DateTime _time;
        private ObservableCollection<String> _receivers = new ObservableCollection<String>();
        private ObservableCollection<String> _attachments = new ObservableCollection<String>();

        // public
        public String Topic 
        {
            get => _topic;
            set
            {
                _topic = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Title"));
            }
        }
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Text"));
            }
        }
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Author"));
            }
        }
        public bool Read
        {
            get => _read;
            set
            {
                _read = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Read"));
            }
        }
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
        }
        public ObservableCollection<string> Receivers
        {
            get => _receivers;
            set
            {
                _receivers = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Receivers"));
            }
        }
        public ObservableCollection<string> Attachments
        {
            get => _attachments;
            set
            {
                _attachments = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Attachments"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

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
