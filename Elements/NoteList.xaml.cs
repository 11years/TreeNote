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
    /// NoteList.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteList : UserControl
    {

        public NoteList()
        {
            InitializeComponent();
        }

        public void SetNote(Classes.Note note)
        {
            this.dckNoteView.Children.Clear();

            NoteSheet.NoteSheet ns = new NoteSheet.NoteSheet(ref note);
            ns.btnSelect.Visibility = System.Windows.Visibility.Collapsed;

            this.dckNoteView.Children.Add(ns);
            DockPanel.SetDock(ns, Dock.Top);
            ns = null;

            if (note.HasChild())
            {
                for (int i = 0; i < note.children.Count; i++)
                {
                    Classes.Note tn = note.children[i];
                    NoteSheet.NoteSheet tns = new NoteSheet.NoteSheet(ref tn);
                    tns.EnterNote += this.SelectNoteHandler;

                    this.dckNoteView.Children.Add(tns);
                    DockPanel.SetDock(tns, Dock.Top);
                    tns = null;
                }
            }
        }

        private void SelectNoteHandler(object sender, RoutedEventArgs e)
        {
            Classes.Note tn = ((NoteSheet.NoteSheet)sender).item;
            this.SetNote(tn);
            //this.adrsActiveNote.SetNote(tn);
            //TODO:イベント起こす
        }

        public void Clear()
        {
            this.dckNoteView.Children.Clear();
        }
    }
}
