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

        public MainWindow()
        {
            InitializeComponent();

            Classes.Note note = Classes.NoteReader.Read(
                System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\sample.txt");

            this.main.SetNote(note);
        }

        private void lblSaveBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this._note.Save(@"C:\Users\yusuke.gotou\Documents\Visual Studio 2012\Projects\TreeNote\TreeNote\bin\Debug\sample.txt");
        }

    }
}
