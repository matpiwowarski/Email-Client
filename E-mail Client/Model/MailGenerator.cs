using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace E_mail_Client.Model
{
    public class MailGenerator
    {
        public Mail GenerateMail()
        {
            ObservableCollection<String> receivers = new ObservableCollection<String>();

            String formattedText = "<Section xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xml:space=\"preserve\" TextAlignment=\"Left\" LineHeight=\"Auto\" IsHyphenationEnabled=\"False\" xml:lang=\"en-us\" FlowDirection=\"LeftToRight\" NumberSubstitution.CultureSource=\"User\" NumberSubstitution.Substitution=\"AsCulture\" FontFamily=\"Segoe UI\" FontStyle=\"Normal\" FontWeight=\"Normal\" FontStretch=\"Normal\" FontSize=\"12\" Foreground=\"#FF000000\" Typography.StandardLigatures=\"True\" Typography.ContextualLigatures=\"True\" Typography.DiscretionaryLigatures=\"False\" Typography.HistoricalLigatures=\"False\" Typography.AnnotationAlternates=\"0\" Typography.ContextualAlternates=\"True\" Typography.HistoricalForms=\"False\" Typography.Kerning=\"True\" Typography.CapitalSpacing=\"False\" Typography.CaseSensitiveForms=\"False\" Typography.StylisticSet1=\"False\" Typography.StylisticSet2=\"False\" Typography.StylisticSet3=\"False\" Typography.StylisticSet4=\"False\" Typography.StylisticSet5=\"False\" Typography.StylisticSet6=\"False\" Typography.StylisticSet7=\"False\" Typography.StylisticSet8=\"False\" Typography.StylisticSet9=\"False\" Typography.StylisticSet10=\"False\" Typography.StylisticSet11=\"False\" Typography.StylisticSet12=\"False\" Typography.StylisticSet13=\"False\" Typography.StylisticSet14=\"False\" Typography.StylisticSet15=\"False\" Typography.StylisticSet16=\"False\" Typography.StylisticSet17=\"False\" Typography.StylisticSet18=\"False\" Typography.StylisticSet19=\"False\" Typography.StylisticSet20=\"False\" Typography.Fraction=\"Normal\" Typography.SlashedZero=\"False\" Typography.MathematicalGreek=\"False\" Typography.EastAsianExpertForms=\"False\" Typography.Variants=\"Normal\" Typography.Capitals=\"Normal\" Typography.NumeralStyle=\"Normal\" Typography.NumeralAlignment=\"Normal\" Typography.EastAsianWidths=\"Normal\" Typography.EastAsianLanguage=\"Normal\" Typography.StandardSwashes=\"0\" Typography.ContextualSwashes=\"0\" Typography.StylisticAlternates=\"0\"><Paragraph><Run FontFamily=\"Georgia\" FontWeight=\"Bold\" FontSize=\"18\" Foreground=\"#FF696969\">generated text</Run></Paragraph><Paragraph><Run></Run></Paragraph><Paragraph><Run FontStyle=\"Italic\" TextDecorations=\"Underline\">every 10 seconds </Run></Paragraph></Section>";
            String receiver = "test@test.pl";
            receivers.Add(receiver);
            
             Mail mail = new Mail("Generated Mail", formattedText, "mail bot", DateTime.Now, false, receivers);
            
            return mail;
        }
    }
}
