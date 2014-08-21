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
        protected List<Classes.Note> history;
        protected int historyInd;

        public MainView()
        {
            InitializeComponent();

            history = new List<Classes.Note>();
            historyInd = 0;

            this.nvControler.GoBack += this.GoBackHandler;
            this.nvControler.GoNext += this.GoNextHandler;
            this.nvControler.Upper += this.UpperHandler;
            this.nvControler.AllOpen += this.AllOpenHandler;
            this.nvControler.AllClose += this.AllCloseHandler;

        }

        public void SetNote(Classes.Note note)
        {
            this.Items = note;
            this.mainTree.SetNote(note);
        }

        public void SelectNote(Classes.Note note, bool updateHistory = true)
        {
            this.SelectedItem = note;

            if (note != null)
            {
                if (updateHistory)
                {
                    for(int i = historyInd  - 1; i >= 0; i--)
                    {
                        history.RemoveAt(i);
                    }

                    
                    if (history.Count == 0)
                    {
                        history.Insert(0, note);
                    }
                    else if (note != history[0])
                    {
                        //移動していた場合のみ記録
                        history.Insert(0, note);
                    }
                    historyInd = 0;
                }

                this.notelist.SetNote(note, this.txtFilter.Text);
                this.adrsActiveNote.SetNote(note);
            }else{
                this.notelist.Clear();
                //アドレスバー初期化未実装
            }
        }

        protected void History(int moveCnt)
        {
            int thist = historyInd + moveCnt;
            if (thist >= 0 && thist < history.Count)
            {
                historyInd = thist;
                SelectNote(history[historyInd],false);
            }
        }

        //イベントハンドラ

        private void GoBackHandler(object sender, RoutedEventArgs e)
        {
            History(1);
        }
        private void GoNextHandler(object sender, RoutedEventArgs e)
        {
            History(-1);
        }
        private void UpperHandler(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItem != null)
            {
                if (this.SelectedItem.parent != null)
                {
                    this.SelectNote(this.SelectedItem.parent);
                }
            }
        }
        private void AllOpenHandler(object sender, RoutedEventArgs e)
        {
            this.notelist.AllOpen();
        }
        private void AllCloseHandler(object sender, RoutedEventArgs e)
        {
            this.notelist.AllClose();
        }

        private void mainTree_SelectedNoteChange(object sender, RoutedEventArgs e)
        {
            SelectNote(((Elements.NoteTreeView)sender).SelectedNote());
        }

        private void ntvControl_Add(object sender, RoutedEventArgs e)
        {
            this.mainTree.AddNote();
        }

        private void ntvControl_Insert(object sender, RoutedEventArgs e)
        {
            this.mainTree.InsertNote();
        }

        private void ntvControl_Remove(object sender, RoutedEventArgs e)
        {
            this.mainTree.RemoveNote();
        }

        private void adrsActiveNote_SelectNote(object sender, RoutedEventArgs e)
        {
            this.SelectNote((Classes.Note)e.OriginalSource);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.notelist.SetNote(this.SelectedItem, this.txtFilter.Text);
        }

    }
}
