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
    public partial class TextEditorUserControl : UserControl
    {
        public bool IsReadOnly
        {
            set
            {
                TextBox.IsReadOnly = value;
            }
        }
        public TextEditorUserControl()
        {
            InitializeComponent();
        }

        public void SetText(string text)
        {
            TextBox.Document.Blocks.Clear();
            TextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
        }
        public string GetText()
        {
            return new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd).Text;
        }
    }
}
