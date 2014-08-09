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
    /// NoteTreeView.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteTreeView : UserControl
    {
        public static readonly RoutedEvent SelectedNoteChangeEvent;
        List<Classes.Note> treeitem;

        public NoteTreeView()
        {
            InitializeComponent();
        }

        //public Classes.Note item { get; protected set; }

        /// <summary>
        /// RoutedEventの登録
        /// </summary>
        static NoteTreeView()
        {
            SelectedNoteChangeEvent =
                EventManager.RegisterRoutedEvent("SelectedNoteChange",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));
        }
        public event RoutedEventHandler SelectedNoteChange
        {
            add { base.AddHandler(SelectedNoteChangeEvent, value); }
            remove { base.RemoveHandler(SelectedNoteChangeEvent, value); }
        }
        protected virtual void fire_Event(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }
        private void treeNotes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            RoutedEventArgs te = new RoutedEventArgs(SelectedNoteChangeEvent, this);
            fire_Event(te);
        }

        public Classes.Note SelectedNote()
        {
            return (Classes.Note)this.treeNotes.SelectedItem;
        }

        public void SetNote(Classes.Note note)
        {
            //this.item = note;

            treeitem = new List<Classes.Note>();
            treeitem.Add(note);

            this.treeNotes.ItemsSource = treeitem;
            
        }

        public void SelectNote(Classes.Note note)
        {
            foreach (TreeViewItem t in this.treeNotes.Items)
            {
                //未実装
            }
        }

        public void AddNote(Classes.Note note = null)
        {
            if (note == null)
            {
                //新規追加
                Classes.Note newNote = new Classes.Note(DateTime.Now.ToString(),"");
                Classes.Note selectedNote = (Classes.Note)this.treeNotes.SelectedItem;

                selectedNote.Add(newNote);

                //ツリービューの表示を更新
                //this.treeNotes.Items.Refresh();
            }
            else
            {
                //既存のものを追加
            }
        }

        //public void RefreshTree()
        //{
        //    TreeViewItem tvi = new TreeViewItem();

        //    this.ReadNote(tvi, this.item);

        //    this.treeNotes.Items.Add(tvi);
        //}

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
    }
}
