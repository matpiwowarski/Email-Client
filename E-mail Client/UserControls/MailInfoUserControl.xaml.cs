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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_mail_Client
{
    public partial class MailInfoUserControl : UserControl
    {
        public MailInfoUserControl()
        {
            InitializeComponent();
        }
        public void GetMailInformation(ref String author, ref String receiverString, ref String topic)
        {
            author = MailboxComboBox.Text;
            receiverString = RecipientTextBox.Text;
            topic = SubjectTextBox.Text;
        }
    }
}
