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
using System.Windows.Shapes;

namespace Buh_uchet
{
    /// <summary>
    /// Логика взаимодействия для NoteTypeWindow.xaml
    /// </summary>
    public partial class NoteTypeWindow : Window
    {
        public NoteTypeWindow()
        {
            InitializeComponent();
        }

        private void AddType_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = (Application.Current.MainWindow as MainWindow).NoteType.Items.Cast<string>().ToList();
            list.Add(Type.Text);
            JsonMoment.MySerialize<List<string>>(list, "типы записей.json");
            (Application.Current.MainWindow as MainWindow).NoteType.ItemsSource = list;
            Close();
        }
    }
}
