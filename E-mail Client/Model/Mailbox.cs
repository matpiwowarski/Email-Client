using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace E_mail_Client.Model
{
    [Serializable()]
    public class Mailbox : ISerializable, INotifyPropertyChanged
    {
        // private
        private String _emailAddress;
        private Folder _inbox = new Folder(FolderType.INBOX);
        private Folder _sent = new Folder(FolderType.SENT);
        private Folder _deleted = new Folder(FolderType.DELETED);
        private Folder _starred = new Folder(FolderType.STARRED);
        
        // public
        public String EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EmailAddress"));
            }
        }

        public Folder Inbox
        {
            get => _inbox;
            set
            {
                _inbox = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Inbox"));
            }
        }
        public Folder Sent
        {
            get => _sent;
            set
            {
                _sent = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sent"));
            }
        }
        public Folder Deleted
        {
            get => _deleted;
            set
            {
                _deleted = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Deleted"));
            }
        }
        public Folder Starred
        {
            get => _starred;
            set
            {
                _starred = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Starred"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        public Mailbox()
        {

        }
        public Mailbox(String emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public Mailbox(SerializationInfo info, StreamingContext context)
        {
            _emailAddress = (String)info.GetValue("EmailAddress", typeof(String));
            _inbox = (Folder)info.GetValue("Inbox", typeof(Folder));
            _sent = (Folder)info.GetValue("Sent", typeof(Folder));
            _deleted = (Folder)info.GetValue("Deleted", typeof(Folder));
            _starred = (Folder)info.GetValue("Starred", typeof(Folder));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EmailAddress", _emailAddress);
            info.AddValue("Inbox", _inbox);
            info.AddValue("Sent", _sent);
            info.AddValue("Deleted", _deleted);
            info.AddValue("Starred", _starred);   
        }
    }
}
