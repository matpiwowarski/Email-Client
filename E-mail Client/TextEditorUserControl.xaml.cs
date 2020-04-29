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
                if(value == true)
                {
                    ExpanderGrid.Visibility = Visibility.Collapsed;
                    EditorStackPanel.Visibility = Visibility.Collapsed;
                }
            }
        }
        public TextEditorUserControl()
        {
            InitializeComponent();
            LoadFontFamilyComboBox();
            LoadFontSizeComboBox();
        }

        private void LoadFontSizeComboBox()
        {
            FontSize.Items.Add(8);
            FontSize.Items.Add(9);
            FontSize.Items.Add(10);
            FontSize.Items.Add(11);
            FontSize.Items.Add(12);
            FontSize.Items.Add(14);
            FontSize.Items.Add(16);
            FontSize.Items.Add(18);
            FontSize.Items.Add(20);
            FontSize.Items.Add(22);
            FontSize.Items.Add(24);
            FontSize.Items.Add(26);
            FontSize.Items.Add(28);
            FontSize.Items.Add(36);
            FontSize.Items.Add(48);
            FontSize.Items.Add(72);

            FontSize.SelectedIndex = 0;
        }

        private void LoadFontFamilyComboBox()
        {
            foreach(FontFamily font in Fonts.SystemFontFamilies)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = font.ToString();
                item.FontFamily = font;
                // adding
                FontFamily.Items.Add(item);
                FontFamily.SelectedIndex = 0;
            }
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
