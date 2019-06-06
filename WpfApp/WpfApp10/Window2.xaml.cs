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
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel.DataAnnotations;
using ClientDemo;

namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : MetroWindow
    {
        string f2token;
        string f2email;
        List<ProfessorTable> Professors { get; set; }
        public Window2(string email, string token)
        {
            InitializeComponent();
            f2token = token;
            f2email = email;

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3(f2email, f2token);
            window3.ShowDialog();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ClientDemo.AuthStuff auth = new ClientDemo.AuthStuff();

            using (var client = auth.CreateClient(f2token))
            {
                //"http://localhost:65458/api/DBManage/GetTables?prepodname=JJoyce@gmail.com"

                var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetTables/" + $"?prepodname={f2email}").Result;
                //textBox1.Text = response.Content.ReadAsStringAsync().Result;

                Professors = JsonConvert.DeserializeObject<List<ProfessorTable>>(response.Content.ReadAsStringAsync().Result);
                //comboBox1.Items.Add(myNewObject.TableName);
                
                DataGrid.ItemsSource = Professors;

            }
        }
        class ProfessorTable
        {
            public string TableName { get; set; }
            public string StudentsCourse { get; set; }
            public string SubjectName { get; set; }
            public string StudentsGroups { get; set; }
            public string UniqueCode { get; set; }
        }


        

        

       

        private void DataGridView1_CellContentClick(object sender, DataGrid e)
        {

            Window4 window4 = new Window4(Professors[e.SelectedIndex].TableName, f2token, f2email);

            Window5 window5 = new Window5(new Window5.Lector { TableName = Professors[e.SelectedIndex].TableName, Name = f2email, Subject = Professors[e.SelectedIndex].SubjectName, UniqueCode = Professors[e.SelectedIndex].UniqueCode });
            window4.Show();
            window5.Show();


        }

        

        

        private void Window2_load(object sender, RoutedEventArgs e)
        {
            AuthStuff auth = new AuthStuff();

            using (var client = auth.CreateClient(f2token))
            {
                //"http://localhost:65458/api/DBManage/GetTables?prepodname=JJoyce@gmail.com"

                var response = client.GetAsync("http://localhost:65458/" + "api/DBManage/GetTables/" + $"?prepodname={f2email}").Result;
                //textBox1.Text = response.Content.ReadAsStringAsync().Result;

                Professors = JsonConvert.DeserializeObject<List<ProfessorTable>>(response.Content.ReadAsStringAsync().Result);
                //comboBox1.Items.Add(myNewObject.TableName);
                DataGrid.ItemsSource = Professors;

            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Window4 window4 = new Window4(Professors[this.DataGrid.SelectedIndex].TableName, f2token, f2email);

                Window5 window5 = new Window5(new Window5.Lector { TableName = Professors[DataGrid.SelectedIndex].TableName, Name = f2email, Subject = Professors[DataGrid.SelectedIndex].SubjectName, UniqueCode = Professors[DataGrid.SelectedIndex].UniqueCode });
                window4.Show();
                window5.Show();
            }
            catch
            {
                label2.Content = "Выберете группу";
            }
        }
    }
}
