using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_mail_Client.Model
{
    public class Folder: TreeViewItem
    {
        public string FolderName { get; set; }
        public Mailbox Email { get; set; }
        public List<Mail> Mails { get; set; }

        public void AddMails(params Mail[] mails)
        {
            foreach (Mail mail in mails)
            {
                Mails.Add(mail);
            }
        }
        public Folder(string folderName, Mailbox email)
        {
            Email = email;
            Mails = new List<Mail>();
            FolderName = folderName;

            // TreeViewItem properties
            Header = folderName;
            Foreground = Brushes.White;

            MouseLeftButtonUp += Folder_MouseLeftButtonUp;
        }

        private void Folder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Controller controller = new Controller();
            controller.DisableDeleteButton();
            controller.LoadMails(this);
        }
    }
}
