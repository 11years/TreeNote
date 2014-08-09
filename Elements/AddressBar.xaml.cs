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
                    tn = tn.Search(tn.parent.id);
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

                this.stkBase.Children.Add(bt);
            }
        }
    }
}
