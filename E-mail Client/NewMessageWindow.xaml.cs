using E_mail_Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace E_mail_Client
{
    /// <summary>
    /// Interaction logic for NewMessageWindow.xaml
    /// </summary>
    public partial class NewMessageWindow : Window
    {
        private MainWindow _mainWindow;

        public NewMessageWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            if(RecipientTextBox.Text.Length > 0 && SubjectTextBox.Text.Length > 0)
            {
                String author = MailboxComboBox.Text;
                String receiverString = RecipientTextBox.Text;
                String topic = SubjectTextBox.Text;
                String content = ContentTextBox.Text;

                HashSet<String> receivers = new HashSet<String>();
                // add all receivers
                AddAllReceivers(receiverString, receivers);

                Mail mail = new Mail(author, receivers, topic, content);

                if(AttachmentListBox.Items.Count > 0)
                {
                    foreach (String item in AttachmentListBox.Items)
                    {
                        mail.AddAttachment(item);
                    }
                }

                if(_mainWindow != null)
                {
                    // adding to inbox
                    if (receivers.Contains(_mainWindow.Mailbox1.EmailAdress))
                        _mainWindow.Mailbox1.Inbox.Add(mail);
                    if (receivers.Contains(_mainWindow.Mailbox2.EmailAdress))
                        _mainWindow.Mailbox2.Inbox.Add(mail);
                    // adding to sent
                    if (_mainWindow.Mailbox1.EmailAdress == author)
                    {
                        _mainWindow.Mailbox1.Sent.Add(mail);
                    }
                    else if (_mainWindow.Mailbox2.EmailAdress == author)
                    {
                        _mainWindow.Mailbox2.Sent.Add(mail);
                    }
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("The recipient and the subject should be filled.");
            }
            */
        }

        private void AddAllReceivers(string receiverString, HashSet<string> receiverSet)
        {
            if (receiverString.Length > 0)
            {
                // filling ';' instead of every ' '
                string convertedString1 = receiverString.Replace(" ", ";");
                // filling ';' instead of every ','
                string convertedString2 = convertedString1.Replace(",", ";");

                // adding every receiver to the set

                string[] receivers = convertedString2.Split(';');

                foreach(string receiver in receivers)
                {
                    receiverSet.Add(receiver);
                }
            }
        }

        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Title = "Select Attachments";

            // .jpg, .png, .gif, .bmp, .wmv, .mp3, .mpg, .mpeg, and all files
            var imageFilter = "Image(*.JPG; *.PNG; *.GIF; *.BMP)| *.JPG; *.PNG; *.GIF; *.BMP |";
            var videoFilter = "Video(*.WMV;*.MPG;*.MPEG)| *.WMV;*.MPG;*.MPEG |";
            var audioFilter = "Audio(*.MP3)| *.MP3 |";
            dlg.Filter = imageFilter + videoFilter + audioFilter + "All files (*.*)|*.*";

            if(dlg.ShowDialog() == true)
            {
                foreach(String file in dlg.FileNames)
                {
                    string fileName = System.IO.Path.GetFileName(file);
                    AttachmentListBox.Items.Add(fileName);
                }
            }
            // make attachment list visible
            AttachmentListBox.Visibility = Visibility.Visible;
        }
    }
}
