using System.Windows;
using ReactiveUI;
using TENET;
using System.Data;
using System.Data.SqlClient;
using System;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class EditProjectsWindow : ReactiveWindow<EditProjectsViewModel>
    {
        public EditProjectsWindow()
        {
            DataContext = ViewModel = new EditProjectsViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                this.Bind(this.ViewModel, x => x.numberProject, x => x.project.Text);
                
                this.BindCommand(this.ViewModel, x => x.send1, x => x.Button1);

            });

            var proektTable = new DataTable();
            string sql = "select id_проект as 'Номер-проекта', Название as 'Проект',Дата_начала as 'Дата начала', Дата_сдачи as 'Дата сдачи', Предмет_договора as 'Предмет договора' from dbo.Проект";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            AllProjectsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            //adapter.Dispose();
            //fill();

            


        }

        public void fill ()

        {
            int id = Convert.ToInt16(project.Text);
            var proektTable1 = new DataTable();
            
            string sql1 = "Select dbo.Проект.[Название], [часы работы] as 'Часы-работы', dbo.[Вид_работы].Название AS [Виды работ] From dbo.Проект, dbo.[Проект_работа], dbo.[Вид_работы] Where (id_проект =fk_id_проект) and (id_работа=fk_id_работа) and (id_проект="+id+") ;";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn1 = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand(sql1, cn1);
            var adapter1 = new SqlDataAdapter(command1);
            cn1.Open();
            adapter1.Fill(proektTable1);
            EditProjectsGrid.ItemsSource = proektTable1.DefaultView;
            cn1.Close();
            //adapter.Dispose();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            fill();
            this.UpdateLayout();
        }

        private void button_Click3(object sender, RoutedEventArgs e)
        {

            int id = Convert.ToInt16(project.Text);
            int time1 = Convert.ToInt16(time.Text);
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Update dbo.Проект_работа set [часы работы] = '{time1}' where fk_id_проект = '{id}' ";
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
