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
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : UserControl
    {
        public Classes.Note Items { get; protected set; }
        public Classes.Note SelectedItem { get; protected set; }

        public MainView()
        {
            InitializeComponent();

            this.Items = Classes.Note.GetRootInstance();
            this.Items.Read(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\sample.txt");

            this.nvControler.GoBack += this.GoBackHandler;
            this.nvControler.GoNext += this.GoNextHandler;
            this.nvControler.Upper += this.UpperHandler;
            this.nvControler.AllOpen += this.AllOpenHandler;
            this.nvControler.AllClose += this.AllCloseHandler;

            RefreshTree();
        }

        public void RefreshTree()
        {
            TreeViewItem tvi = new TreeViewItem();

            this.ReadNote(tvi, this.Items);

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
            

            //TreeViewItem tvi = (TreeViewItem)this.treeNotes.SelectedItem;

            //Classes.Note nt = this.Items.Find((int)tvi.Tag);

            //this.ReadNotes(nt);

            this.adrsActiveNote.SetNote(nt);
        }

        private void treeNotes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.Refresh();
        }



        //イベントハンドラ

        private void GoBackHandler(object sender, RoutedEventArgs e)
        {
            Classes.Note tn = ((NoteSheet.NoteSheet)sender).item;
            this.(tn);
            this.adrsActiveNote.SetNote(tn);
        }
        private void GoNextHandler(object sender, RoutedEventArgs e)
        {
            Classes.Note tn = ((NoteSheet.NoteSheet)sender).item;
            this.ReadNotes(tn);
            this.adrsActiveNote.SetNote(tn);
        }
        private void UpperHandler(object sender, RoutedEventArgs e)
        {
            Classes.Note tn = ((NoteSheet.NoteSheet)sender).item;
            this.notelist.se(tn);
            this.adrsActiveNote.SetNote(tn);
        }
        private void AllOpenHandler(object sender, RoutedEventArgs e)
        {
            foreach (NoteSheet.NoteSheet tns in this.dckNoteView.Children)
            {
                tns.OpenBody();
            }
        }
        private void AllCloseHandler(object sender, RoutedEventArgs e)
        {
            foreach (NoteSheet.NoteSheet tns in this.dckNoteView.Children)
            {
                tns.CloseBody();
            }
        }
    }
}
