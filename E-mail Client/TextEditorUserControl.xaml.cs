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
                ContentBox.IsReadOnly = value;
                if(value == true)
                {
                    ExpanderControl.Visibility = Visibility.Collapsed;
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

            FontSize.SelectedIndex = 3; // 11 is deafault value
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
            ContentBox.Document.Blocks.Clear();
            ContentBox.Document.Blocks.Add(new Paragraph(new Run(text)));
        }
        public string GetText()
        {
            return new TextRange(ContentBox.Document.ContentStart, ContentBox.Document.ContentEnd).Text;
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            var currentProperty = ContentBox.Selection.GetPropertyValue(RichTextBox.FontWeightProperty);
            if (currentProperty.Equals(FontWeights.Bold)) // we have already bold text
            {
                ContentBox.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Normal);
            }
            else // bold text
            {
                ContentBox.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            var currentProperty = ContentBox.Selection.GetPropertyValue(RichTextBox.FontStyleProperty);

            if(currentProperty.Equals(FontStyles.Italic)) // we have already italic text
            {
                ContentBox.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Normal);
            }
            else // italic text
            {
                ContentBox.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            var currentProperty = ContentBox.Selection.GetPropertyValue(Run.TextDecorationsProperty);

            if(currentProperty.Equals(TextDecorations.Underline)) // we have already underline text
            {
                ContentBox.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            }
            else //underline text
            {
                ContentBox.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

        }

        private void ClearAllFormatting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
