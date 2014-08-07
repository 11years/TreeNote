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

namespace TreeNote
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        TreeNote.Classes.Note _note;

        public MainWindow()
        {
            InitializeComponent();

            this._note = Classes.Note.GetRootInstance();
            this._note.Read(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\sample.txt");

            RefreshTree();
        }

        public void RefreshTree()
        {
            TreeViewItem tvi = new TreeViewItem();

            this.ReadNote(tvi, this._note);

            this.treeNotes.Items.Add(tvi);
        }

        protected TreeViewItem ReadNote(TreeViewItem tvi, Classes.Note nt)
        {
            tvi.Header = nt.title;
            tvi.Tag = nt.id;

            if (nt.HasChild())
            {
                foreach (Classes.Note cnt in nt.children)
                {
                    TreeViewItem ttvi = new TreeViewItem();
                    this.ReadNote(ttvi, cnt);
                    tvi.Items.Add(ttvi);
                }
            }
            return tvi;
        }

        public void Refresh()
        {
            this.dckNoteView.Children.Clear();

            TreeViewItem tvi = (TreeViewItem)this.treeNotes.SelectedItem;

            Classes.Note nt = this._note.Find((int)tvi.Tag);

            this.ReadNotes(nt);

            this.adrsActiveNote.SetNote(nt);
        }

        private void treeNotes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.Refresh();
        }

        protected void ReadNotes(Classes.Note note)
        {
            this.dckSelectedNoteView.Children.Clear();
            this.dckNoteView.Children.Clear();

            NoteSheet.NoteSheet ns = new NoteSheet.NoteSheet(ref note);
            ns.btnSelect.Visibility = System.Windows.Visibility.Collapsed;

            this.dckSelectedNoteView.Children.Add(ns);
            DockPanel.SetDock(ns, Dock.Top);
            ns = null;

            if (note.HasChild())
            {
                for (int i = 0; i < note.children.Count;i++)
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

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblSaveBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this._note.Save(@"C:\Users\yusuke.gotou\Documents\Visual Studio 2012\Projects\TreeNote\TreeNote\bin\Debug\sample.txt");
        }

        private void SelectNoteHandler(object sender, RoutedEventArgs e)
        {
            Classes.Note tn = ((NoteSheet.NoteSheet)sender)._note;
            this.ReadNotes(tn);
            this.adrsActiveNote.SetNote(tn);
        }
    }
}
