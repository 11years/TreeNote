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

        public Classes.Note _note;

        public static readonly RoutedEvent EnterNoteEvent;
        static NoteSheet()
        {
            EnterNoteEvent =
                EventManager.RegisterRoutedEvent("EnterNote",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(NoteSheet));
        }
        public event RoutedEventHandler EnterNote
        {
            add { base.AddHandler(EnterNoteEvent, value); }
            remove { base.RemoveHandler(EnterNoteEvent, value); }
        }
        protected virtual void fire_EnterNoteEvent(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(EnterNoteEvent, this);
            fire_EnterNoteEvent(te);
        }

        protected NoteSheet()
        {
            InitializeComponent();
        }

        public NoteSheet(ref Classes.Note note)
        {
            InitializeComponent();

            //this.btnSelect.AddHandler(Button.ClickEvent, new RoutedEventHandler(fire_EnterNoteEvent)); 

            this._note = note;

            this.SetNote();

            if (note.HasChild() == false) { this.btnSelect.Visibility = System.Windows.Visibility.Collapsed; }
        }

        protected void SetNote()
        {
            this.btnTitle.Content = this._note.title;
            this.txtBody.Text = this._note.body;
        }

        private void btnTitle_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtBody.IsVisible)
            {
                this.txtBody.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.txtBody.Visibility = System.Windows.Visibility.Visible;
            }
        }


        protected void EditTitle()
        {
            TextBox tBox = new TextBox();
            tBox.Text = this.btnTitle.Content.ToString();
            tBox.LostFocus += txtTitle_LostFocus;
            tBox.LostKeyboardFocus += txtTitle_LostFocus;
            tBox.Style = (Style)(FindResource("TitleEdit"));

            dckHeader.Children.Add(tBox);

            tBox.SelectAll();
            tBox.Focus(); tBox.Focus();

            this.btnTitle.Visibility = System.Windows.Visibility.Collapsed;
            //this.txtTitle.Visibility = System.Windows.Visibility.Visible;
        }

        private void txtTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tBox = (TextBox)sender;

            this._note.SetTitle(tBox.Text);
            this.btnTitle.Content = tBox.Text;

            dckHeader.Children.Remove(tBox);

            this.btnTitle.Visibility = System.Windows.Visibility.Visible;
        }

        private void txtBody_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        protected void _setBody(object sender, RoutedEventArgs e)
        {
            this._note.SetBody(this.txtBody.Text);
        }

        private void txtBody_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtBody.TextChanged += _setBody;
        }

        private void txtBody_LostFocus(object sender, RoutedEventArgs e)
        {
            this.txtBody.TextChanged -= _setBody;
        }



        //private void lblDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    this.EditTitle();
        //}
    }
}
