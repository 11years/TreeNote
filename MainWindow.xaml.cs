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

using Microsoft.Win32;

namespace TreeNote
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        protected string filepath;
        protected Classes.Note note;

        public MainWindow()
        {
            InitializeComponent();

            CreateNewFile();
        }

        protected void CreateNewFile()
        {
            filepath = "";

            this.note = new Classes.Note();
            this.note.title = "新しいノート";

            this.mainView.SetNote(this.note);
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            CreateNewFile();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            this.OpenFile();
        }

        protected void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = "*.*";
            if (ofd.ShowDialog() == true)
            {
                this.note = Classes.NoteReader.Read(ofd.FileName);
                this.mainView.SetNote(this.note);
            }
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            this.SaveAs();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.Save();
        }

        protected void SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.DefaultExt = "*.*";
            if (sfd.ShowDialog() == true)
            {
                this.filepath = sfd.FileName;
                this.Save();
            }
        }

        protected void Save()
        {
            if (this.filepath == "")
            {
                SaveAs();
            }
            else
            {
                if (Classes.NoteWriter.Save(this.filepath, this.note) == false)
                {
                    //エラー表示未実装
                    int i = 1;
                }
            }
        }

    }
}
