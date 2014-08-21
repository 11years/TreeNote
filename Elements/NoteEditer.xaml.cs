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
    }
}
