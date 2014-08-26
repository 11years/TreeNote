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

namespace TreeNote.Elements
{
    /// <summary>
    /// NoteEditer.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteEditer : UserControl
    {
        public Classes.Note note { get; protected set; }
        public NoteEditer()
        {
            InitializeComponent();
        }

        public void SetNote(Classes.Note note)
        {
            this.note = note;

            this.textTitle.Text = this.note.title;
            this.textBody.Text = this.note.body;
        }

        protected void _setTitle(object sender, RoutedEventArgs e)
        {
            this.note.title = this.textTitle.Text;
        }
        /// <summary>
        /// 本文テキスト編集時にNoteを更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textTitle.TextChanged += _setTitle;
        }
        private void textTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textTitle.TextChanged -= _setTitle;
        }

        protected void _setBody(object sender, RoutedEventArgs e)
        {
            this.note.body = this.textBody.Text;
        }
        /// <summary>
        /// 本文テキスト編集時にNoteを更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBody_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBody.TextChanged += _setBody;
        }
        private void txtBody_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textBody.TextChanged -= _setBody;
        }
    }
}
