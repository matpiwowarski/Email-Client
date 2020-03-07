using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_mail_Client.Model
{
    public class Folder: TreeViewItem
    {
        private string _folderName;
        public List<Mail> Mails { get; set; }

        public void AddMails(params Mail[] mails)
        {
            foreach (Mail mail in mails)
            {
                Mails.Add(mail);
            }
        }
        public Folder(string folderName)
        {
            this.Mails = new List<Mail>();
            _folderName = folderName;

            // TreeViewItem properties
            this.Header = folderName;
            this.Foreground = Brushes.White;
        }

    }
}
