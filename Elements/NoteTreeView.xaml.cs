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

        public NoteTreeView()
        {
            InitializeComponent();
        }

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
            //未実装
        }
    }
}
