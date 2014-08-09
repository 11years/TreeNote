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
    /// NoteTreeViewControlPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteTreeViewControlPanel : UserControl
    {
        public static readonly RoutedEvent AddEvent;
        public static readonly RoutedEvent InsertEvent;
        public static readonly RoutedEvent RemoveEvent;

        /// <summary>
        /// RoutedEventの登録
        /// </summary>
        static NoteTreeViewControlPanel()
        {
            AddEvent =
                EventManager.RegisterRoutedEvent("Add",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));
            InsertEvent =
                EventManager.RegisterRoutedEvent("Insert",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));
            RemoveEvent =
                EventManager.RegisterRoutedEvent("Remove",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));
        }

        public NoteTreeViewControlPanel()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Add
        {
            add { base.AddHandler(AddEvent, value); }
            remove { base.RemoveHandler(AddEvent, value); }
        }
        public event RoutedEventHandler Insert
        {
            add { base.AddHandler(InsertEvent, value); }
            remove { base.RemoveHandler(InsertEvent, value); }
        }
        public event RoutedEventHandler Remove
        {
            add { base.AddHandler(RemoveEvent, value); }
            remove { base.RemoveHandler(RemoveEvent, value); }
        }
        protected virtual void fire_Event(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(AddEvent, this);
            fire_Event(te);
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(InsertEvent, this);
            fire_Event(te);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(RemoveEvent, this);
            fire_Event(te);
        }

    }
}
