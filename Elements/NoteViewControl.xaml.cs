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
    /// NoteViewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class NoteViewControl : UserControl
    {
        public static readonly RoutedEvent GoBackEvent;
        public static readonly RoutedEvent GoNextEvent;
        public static readonly RoutedEvent UpperEvent;
        public static readonly RoutedEvent AllCloseEvent;
        public static readonly RoutedEvent AllOpenEvent;
        static NoteViewControl()
        {
            GoBackEvent =
                EventManager.RegisterRoutedEvent("GoBack",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));

            GoNextEvent =
                EventManager.RegisterRoutedEvent("GoNext",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));

            UpperEvent =
                EventManager.RegisterRoutedEvent("GoUpper",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));

            AllCloseEvent =
                EventManager.RegisterRoutedEvent("AllClose",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));

            AllOpenEvent =
                EventManager.RegisterRoutedEvent("AllOpen",
                RoutingStrategy.Bubble,
                typeof(RoutedEventArgs), typeof(RoutedEvent));
        }

        public event RoutedEventHandler GoBack
        {
            add { base.AddHandler(GoBackEvent, value); }
            remove { base.RemoveHandler(GoBackEvent, value); }
        }
        public event RoutedEventHandler GoNext
        {
            add { base.AddHandler(GoNextEvent, value); }
            remove { base.RemoveHandler(GoNextEvent, value); }
        }
        public event RoutedEventHandler Upper
        {
            add { base.AddHandler(UpperEvent, value); }
            remove { base.RemoveHandler(UpperEvent, value); }
        }
        public event RoutedEventHandler AllClose
        {
            add { base.AddHandler(AllCloseEvent, value); }
            remove { base.RemoveHandler(AllCloseEvent, value); }
        }
        public event RoutedEventHandler AllOpen
        {
            add { base.AddHandler(AllOpenEvent, value); }
            remove { base.RemoveHandler(AllOpenEvent, value); }
        }

        protected virtual void fire_Event(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        public NoteViewControl()
        {
            InitializeComponent();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(GoBackEvent, this);
            fire_Event(te);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(GoNextEvent, this);
            fire_Event(te);
        }

        private void btnUpper_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(UpperEvent, this);
            fire_Event(te);
        }

        private void btnAllOpen_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(AllOpenEvent, this);
            fire_Event(te);
        }

        private void btnAllClose_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs te = new RoutedEventArgs(AllCloseEvent, this);
            fire_Event(te);
        }
    }
}
