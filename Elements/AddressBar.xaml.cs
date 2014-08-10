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
    /// AddressBar.xaml の相互作用ロジック
    /// </summary>
    public partial class AddressBar : UserControl
    {
        public static readonly RoutedEvent SelectNoteEvent;

        /// <summary>
        /// RoutedEventの登録
        /// </summary>
        static AddressBar()
        {
            SelectNoteEvent =
                EventManager.RegisterRoutedEvent("SelectNote",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));
        }

        public event RoutedEventHandler SelectNote
        {
            add { base.AddHandler(SelectNoteEvent, value); }
            remove { base.RemoveHandler(SelectNoteEvent, value); }
        }
        protected virtual void fire_Event(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }
        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            //こんな実装してしまっていいのか
            RoutedEventArgs te = new RoutedEventArgs(SelectNoteEvent, ((Button)e.Source).Tag);
            //RoutedEventArgs te = new RoutedEventArgs(SelectNoteEvent, this);
            fire_Event(te);
        }

        public AddressBar()
        {
            InitializeComponent();
        }

        public void SetNote(Classes.Note note)
        {
            this.stkBase.Children.Clear();

            List<Classes.Note> list = new List<Classes.Note>();
            Classes.Note tn = note;
            for (int i = 0; i < 3; i++)
            {
                list.Add(tn);

                if (tn.HasParent())
                {
                    tn = tn.parent;
                }
                else
                { 
                    break; 
                }

            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                Button bt = new Button();
                bt.Content = " > " + list[i].title;
                bt.Style = (Style)(FindResource("AddressButton")); ;
                DockPanel.SetDock(bt, Dock.Left);
                bt.Click += btnNote_Click;
                bt.Tag = list[i];

                this.stkBase.Children.Add(bt);
            }
        }
    }
}
