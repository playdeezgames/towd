using Common;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FontEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CyFont _font = null;
        private Dictionary<char, string> _table = new Dictionary<char, string>();
        public MainWindow()
        {
            for(int index=32;index<128;++index)
            {
                _table[(char)index] = $"Character{index}";
            }
            InitializeComponent();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog()==true)
            {
                _font = Utility.Load<CyFont>(openFileDialog.FileName);



            }
        }

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _font = null;
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(_font!=null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if(saveFileDialog.ShowDialog()==true)
                {
                    Utility.Save<CyFont>(_font,saveFileDialog.FileName);
                }
            }
        }
    }
}
