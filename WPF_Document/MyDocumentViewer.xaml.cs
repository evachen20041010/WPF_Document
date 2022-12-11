using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_Document
{
    /// <summary>
    /// MyDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class MyDocumentViewer : Window
    {
        Color fontColor = Colors.Black;
        Color backgroundColor = Colors.Transparent;
        public MyDocumentViewer()
        {
            InitializeComponent();
            foreach (FontFamily fontfamily in Fonts.SystemFontFamilies)
            {
                cmbFontFamily.Items.Add(fontfamily);
            }
            cmbFontFamily.SelectedIndex = 0;
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 24, 32, 40, 50, 60, 80, 100 };
            cmbFontSize.SelectedIndex = 4;
            fontColorPicker.SelectedColor = fontColor;
            backgroundColorPicker.SelectedColor = backgroundColor;
        }

        private void Editor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object property = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(FontWeights.Bold));

            property = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(FontStyles.Italic));

            property = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(TextDecorations.Underline));

            property = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = property;

            property = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.SelectedItem = property;

            SolidColorBrush? foregroundProperty = rtbEditor.Selection.GetPropertyValue(Inline.ForegroundProperty) as SolidColorBrush;
            if (foregroundProperty != null)
            {
                fontColorPicker.SelectedColor = foregroundProperty.Color;
            }
        }

        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.SelectedItem);
            }
        }

        private void FontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)e.NewValue;
            SolidColorBrush colorBrush = new SolidColorBrush(fontColor);
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, colorBrush);
        }

        private void BackgroundColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            backgroundColor = (Color)e.NewValue;
            SolidColorBrush colorBrush = new SolidColorBrush(backgroundColor);
            rtbEditor.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, colorBrush);
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Rich Text Format file|*.ref|html檔案|*.html|所有檔案|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Rich Text Format file|*.ref|html檔案|*.html|所有檔案|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(fileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
            }
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyDocumentViewer myDocumentViewer = new MyDocumentViewer();
            myDocumentViewer.Show();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            rtbEditor.Document.Blocks.Clear();
        }

    }
}
