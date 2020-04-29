using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace E_mail_Client.Model
{
    public class Folder : ObservableCollection<Mail>, INotifyPropertyChanged
    {
        // private
        private FolderType _type;
        // public
        public FolderType Type
        {
            get => _type;
            set {
                _type = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Type"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        public Folder()
        {

        }
        public Folder(FolderType type)
        {
            _type = type;
        }
    }
}
