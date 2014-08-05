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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeNote.NoteSheet
{
    /// <summary>
    /// NoteSheet.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteSheet : UserControl
    {

        public NoteSheet()
        {
            InitializeComponent();
        }

        private void txtSubject_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.IsReadOnly = true;
        }

        private void txtSubject_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.IsReadOnly = false;
        }

        private void txtTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.IsReadOnly = true;
        }

        private void txtTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.txtTitle.IsReadOnly = false;
            this.txtTitle.SelectAll();
            this.txtTitle.Focus();
        }

        private void lblDropDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.txtSubject.IsVisible)
            {
                this.txtSubject.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.txtSubject.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
