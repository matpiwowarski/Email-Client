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
        private bool _read = false;
        private DateTime _time = DateTime.Now;
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
            _topic = (String)info.GetValue("Topic", typeof(String));
            _text = (String)info.GetValue("Text", typeof(String));
            _author = (String)info.GetValue("Author", typeof(String));
            _time = (DateTime)info.GetValue("Time", typeof(DateTime));
            _read = (bool)info.GetValue("Read", typeof(bool));
            _receivers = (ObservableCollection<String>)info.GetValue("Receivers", typeof(ObservableCollection<String>));
            _attachments = (ObservableCollection<String>)info.GetValue("Attachments", typeof(ObservableCollection<String>));
        }
        public Mail(Mail m)
        {
            _topic = m.Topic;
            _text = m.Text;
            _author = m.Author;
            _time = m.Time;
            _read = m.Read;
            _receivers = m.Receivers;
            _attachments = m.Attachments;
        }
        public Mail(String topic, String text, String author, DateTime time, bool read, ObservableCollection<String> receivers)
        {
            _topic = topic;
            _text = text;
            _author = author;
            _time = time;
            _read = read;
            _receivers = receivers;
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
