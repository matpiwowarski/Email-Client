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
            if(RecipientTextBox.Text.Length > 0 && SubjectTextBox.Text.Length > 0)
            {
                String author = MailboxComboBox.Text;
                String receiver = RecipientTextBox.Text;
                String topic = SubjectTextBox.Text;
                String content = ContentTextBox.Text;

                Mail mail = new Mail(author, receiver, topic, content);

                if(_mainWindow != null)
                {
                    // adding to inbox
                    if (_mainWindow.Mailbox1.EmailAdress == receiver)
                    {
                        _mainWindow.Mailbox1.Inbox.Add(mail);
                    }
                    else if (_mainWindow.Mailbox2.EmailAdress == receiver)
                    {
                        _mainWindow.Mailbox2.Inbox.Add(mail);
                    }
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
