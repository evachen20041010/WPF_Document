using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPF_Document
{
    /// <summary>
    /// MyDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class MyDocumentViewer : Window
    {
        public MyDocumentViewer()
        {
            InitializeComponent();
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object property = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(FontWeights.Bold));

            property = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(FontStyles.Italic));

            property = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(TextDecorations.Underline));
        }
    }
}
