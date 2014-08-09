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

        public Classes.Note item;

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

            this.item = note;

            this.SetNote();

            if (note.HasChild() == false) { this.btnSelect.Visibility = System.Windows.Visibility.Collapsed; }
        }

        protected void SetNote()
        {
            this.btnTitle.Content = this.item.title;
            this.txtBody.Text = this.item.body;
        }

        private void btnTitle_Click(object sender, RoutedEventArgs e)
        {
            Switch();
        }

        /// <summary>
        /// 本文の開閉切り替え
        /// </summary>
        public void Switch()
        {
            if (this.txtBody.IsVisible)
            {
                CloseBody();
            }
            else
            {
                OpenBody();
            }
        }
        /// <summary>
        /// 本文開く
        /// </summary>
        public void OpenBody()
        {
            this.txtBody.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// 本文閉じる
        /// </summary>
        public void CloseBody()
        {
            this.txtBody.Visibility = System.Windows.Visibility.Collapsed;
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

            this.item.title = tBox.Text;
            this.btnTitle.Content = tBox.Text;

            dckHeader.Children.Remove(tBox);

            this.btnTitle.Visibility = System.Windows.Visibility.Visible;
        }

        private void txtBody_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        protected void _setBody(object sender, RoutedEventArgs e)
        {
            this.item.body = this.txtBody.Text;
        }

        /// <summary>
        /// 本文テキスト編集時にNoteを更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBody_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtBody.TextChanged += _setBody;
        }
        private void txtBody_LostFocus(object sender, RoutedEventArgs e)
        {
            this.txtBody.TextChanged -= _setBody;
        }
    }
}
