using System;
using Newtonsoft.Json;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp10.Models;
using ClientDemo;

namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : MetroWindow
    {
        string f5token;
        string f5email;
        string tblname;

        public Window4(string tablename, string token, string email)
        {
            InitializeComponent();

            f5token = token;
            f5email = email;
            tblname = tablename;


        }

        
        private void Window4_load(object sender, EventArgs e)
        {

            
        }


       

        private void Window4_load(object sender, RoutedEventArgs e)
        {
            AuthStuff auth = new AuthStuff();
            var client = auth.CreateClient(f5token);


            try
            {
                using (client)
                {

                    var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetStudents/" + $"?tableName=\"{tblname}\"").Result;

                    dataGridView1.ItemsSource = JsonConvert.DeserializeObject<List<Table>>(response.Content.ReadAsStringAsync().Result);


                }
            }
            catch
            {
                label1.Content = "Произошла ошибка";
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthStuff auth = new AuthStuff();
            var client = auth.CreateClient(f5token);


            try
            {
                using (client)
                {
                    if (dateTimePicker1.SelectedDate.Equals(DateTime.Now))
                    {

                        var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetStudents/" + $"?tableName=\"{tblname}\"").Result;

                        dataGridView1.ItemsSource = JsonConvert.DeserializeObject<List<Table>>(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetStudents/" + $"?tableName=\"{tblname}\"&visitatdate={dateTimePicker1.SelectedDate.Value.ToString("yyyyMMdd")}").Result;

                        dataGridView1.ItemsSource = JsonConvert.DeserializeObject<List<Table>>(response.Content.ReadAsStringAsync().Result);

                    }

                }
            }
            catch
            {
                label1.Content = "Произошла ошибка.";
            }
        }
    }
    class Table
    {
        public string Name { get; set; }
        public string Group { get; set; }
    }
}

