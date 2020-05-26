using E_mail_Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace E_mail_Client
{
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
            if (MailInfo.RecipientTextBox.Text.Length > 0 && MailInfo.SubjectTextBox.Text.Length > 0)
            {
                BusyIndicator.IsBusy = true;

                String author = String.Empty;
                String receiverString = String.Empty;
                String topic = String.Empty;
                String content = String.Empty;

                MailInfo.GetMailInformation(ref author, ref receiverString, ref topic);
                // formatted content 
                TextRange tr = new TextRange(TextEditor.ContentBox.Document.ContentStart,
                    TextEditor.ContentBox.Document.ContentEnd);

                using (MemoryStream ms = new MemoryStream())
                {
                    tr.Save(ms, DataFormats.Xaml);
                    content = Encoding.ASCII.GetString(ms.ToArray());
                }

                ObservableCollection<String> receivers = new ObservableCollection<String>();
                // add all receivers
                AddAllReceivers(receiverString, receivers);

                if (_mainWindow != null)
                {
                    // adding into inbox folder
                    foreach (Mailbox m in _mainWindow.Mailboxes)
                    {
                        if (receivers.Contains(m.EmailAddress))
                        {
                            Mail mail = new Mail(topic, content, author, DateTime.Now, false, receivers);

                            if (AttachmentListBox.Items.Count > 0)
                            {
                                foreach (String item in AttachmentListBox.Items)
                                {
                                    mail.AddAttachment(item);
                                }
                            }
                            m.Inbox.Add(mail);
                            _mainWindow.Story.Begin(this);
                        }
                    }

                    // adding into sent folder
                    foreach (Mailbox m in _mainWindow.Mailboxes)
                    {
                        if (m.EmailAddress == author)
                        {
                            Mail mail = new Mail(topic, content, author, DateTime.Now, true, receivers);

                            if (AttachmentListBox.Items.Count > 0)
                            {
                                foreach (String item in AttachmentListBox.Items)
                                {
                                    mail.AddAttachment(item);
                                }
                            }
                            m.Sent.Add(mail);
                            break; // there's only 1 author
                        }
                    }
                }
                Wait3SecondsAndCloseWindow();
            }
            else
            {
                MessageBox.Show("The recipient and the subject should be filled.");
            }
        }

        public async Task Wait3SecondsAndCloseWindow()
        {
            await Task.Delay(3000);
            BusyIndicator.IsBusy = false;
            this.Close();
        }

        private void AddAllReceivers(string receiverString, ObservableCollection<string> receiverSet)
        {
            if (receiverString.Length > 0)
            {
                // filling ';' instead of every ' '
                string convertedString1 = receiverString.Replace(" ", ";");
                // filling ';' instead of every ','
                string convertedString2 = convertedString1.Replace(",", ";");

                // adding every receiver to the set

                string[] receivers = convertedString2.Split(';');

                foreach (string receiver in receivers)
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

            if (dlg.ShowDialog() == true)
            {
                foreach (String file in dlg.FileNames)
                {
                    string fileName = System.IO.Path.GetFileName(file);
                    if(fileName.Contains(".jpg") || fileName.Contains(".png"))
                    {
                        StackPanel item = new StackPanel();
                        item.Orientation = Orientation.Horizontal;
                        TextBlock text = new TextBlock();
                        text.Text = fileName;

                        Border border = new Border();
                        border.BorderThickness = new Thickness(1, 1, 1, 1);
                        border.BorderBrush = Brushes.Black;

                        Thickness margin = border.Margin;
                        margin.Left = 20;
                        border.Margin = margin;

                        Image img = new Image();
                        img.Width = 20;
                        img.Height = 20;
                        img.Source = new BitmapImage(new Uri(file));

                        border.Child = img;

                        item.Children.Add(text);
                        item.Children.Add(border);
                        AttachmentListBox.Items.Add(item);
                    }
                    else
                    {
                        AttachmentListBox.Items.Add(fileName);
                    }
                }
            }
            // make attachment list visible
            AttachmentListBox.Visibility = Visibility.Visible;
        }
    }
}
