using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;     
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Buh_uchet
{
    public partial class MainWindow : Window
    {
        List<Model> list = new List<Model>()
            {
                new Model("Ира", "some", 1000, DateTime.Today),
                new Model("Ирина", "some", 10000,  DateTime.Today.AddDays(1))
            };
        public MainWindow()
        {
            InitializeComponent();
            Calendar.SelectedDate = DateTime.Today;
            try
            {
                NoteType.ItemsSource = JsonMoment.MyDeserialize<List<string>>("типы записей.json");
                BuhUchet.ItemsSource = JsonMoment.MyDeserialize<List<Model>>("учет.json");
            }
            catch 
            { 
            
            }
            UpdateSumm();
        }

        private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Model> list2 = new List<Model>();
            foreach (Model model in list)
            {
                if (model.NoteDate == Calendar.SelectedDate)
                {
                    list2.Add(model);
                }
            }
            BuhUchet.ItemsSource = list2;
            UpdateSumm();
        }

        private void ShowType_Click(object sender, RoutedEventArgs e)
        {
            NoteTypeWindow noteTypeWindow = new NoteTypeWindow();
            noteTypeWindow.Show();
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            int Money = Convert.ToInt32(Summ.Text);
            Model newe = new Model(Name.Text, NoteType.SelectedItem.ToString(), Money, DateTime.Today);
            List<Model> ForAdd = BuhUchet.ItemsSource.Cast<Model>().ToList();
            ForAdd.Add(newe);
            BuhUchet.ItemsSource= ForAdd;
            UpdateSumm();
            JsonMoment.MySerialize<List<Model>>(ForAdd, "учет.json");
            Name.Text = "";
            NoteType.SelectedValue = -1;
            Summ.Text = "";
        }
        public static void UpdateSumm()
        {
            int finalsumm = 0;
            var items = (Application.Current.MainWindow as MainWindow).BuhUchet.Items .Cast<Model>().ToList();
            foreach(var x in items)
            {
                if((x as Model).PlusMoney == true)
                {
                    finalsumm += (x as Model).Moneys;
                }
                else
                {
                    finalsumm-= (x as Model).Moneys;
                }
            }
            if (finalsumm < 0)
            {
                (Application.Current.MainWindow as MainWindow).Suuming.Text ="расходы:" + Convert.ToString(finalsumm);
            }
            else
            {
                (Application.Current.MainWindow as MainWindow).Suuming.Text = "доходы:" + Convert.ToString(finalsumm);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            List<Model> list =BuhUchet.ItemsSource.Cast<Model>().ToList();
            list.RemoveAt(BuhUchet.SelectedIndex);
            BuhUchet.ItemsSource = list;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            List<Model> ForAdd = BuhUchet.ItemsSource.Cast<Model>().ToList();
            BuhUchet.ItemsSource = ForAdd;
            UpdateSumm();
            JsonMoment.MySerialize<List<Model>>(ForAdd, "учет.json");
        }
    }
}
