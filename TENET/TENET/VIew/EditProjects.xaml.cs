using System.Windows;
using ReactiveUI;
using TENET;
using System.Data;
using System.Data.SqlClient;
using System;
using TENET.Model;

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
                this.BindCommand(this.ViewModel, x => x.Edit, x => x.ButtonEdit);
                this.BindCommand(this.ViewModel, x => x.Add, x => x.ButtonAdd);
            });

            var proektTable = new DataTable();
            string sql = "SELECT [Название] as 'Проект' FROM [dbo].[Проект]";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ProjectsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            ProjectsGrid.MouseLeftButtonDown += GotFocus;
        }
        private void GotFocus(System.Object sender, System.EventArgs e)
        {
            var drv = ProjectsGrid.SelectedItem as DataRowView;
            var sValue = drv != null ? drv.Row["Проект"] as string : string.Empty;
            Zapolnit(sValue);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            var proektTable = new DataTable();
            var text = TextBox.Text;
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            string sql2 = $"SELECT distinct [Проект].[Название] as 'Проект' ,[Дата_начала] as 'Дата начала',[Дата_сдачи] as 'Дата сдачи',[Предмет_договора] as 'Предмет договора', Команда.Название as 'Команда' , [Вид_работы].Название as 'Работа', dbo.Клиент.ФИО FROM[dbo].[Проект], [dbo].[Техническое_задание],[dbo].[Команда],[dbo].[Вид_работы], [dbo].[Проект_работа], dbo.Клиент Where(Команда.fk_id_проект = Проект.id_проект) and([Проект_работа].fk_id_проект = Проект.id_проект and[Проект_работа].id_работа = [Вид_работы].fk_id_работа) and dbo.Проект.Название LIKE '%{text}%' and (id_проект = dbo.Клиент.fk_id_проект)";
            var cn2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable);
            otprav.ItemsSource = proektTable.DefaultView;
            cn2.Close();

            TextBox.Text = "";

        }
        public void Zapolnit(string name)
        {
            var PublicDataConnecton = new DataConnecton();
            var proektTable = new DataTable();
            string sql = $"SELECT distinct [Проект].[Название] as 'Проект' ,[Дата_начала] as 'Дата начала',[Дата_сдачи] as 'Дата сдачи',[Предмет_договора] as 'Предмет договора', Команда.Название as 'Команда' , [Вид_работы].Название as 'Работа', dbo.Клиент.ФИО FROM[dbo].[Проект], [dbo].[Техническое_задание],[dbo].[Команда],[dbo].[Вид_работы], [dbo].[Проект_работа], dbo.Клиент Where(Команда.fk_id_проект = Проект.id_проект) and([Проект_работа].fk_id_проект = Проект.id_проект and[Проект_работа].id_работа = [Вид_работы].fk_id_работа) and dbo.Проект.Название = '{name}' and (id_проект = dbo.Клиент.fk_id_проект)";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            otprav.ItemsSource = proektTable.DefaultView;
            cn.Close();

            GlobalData.proekt = name;
            GlobalData.team = PublicDataConnecton.GetTeam(name);
            //GlobalData.work = " ";
        }
        int id1 { get; set; }

        private void button_Click1(object sender, RoutedEventArgs e)
        {

            this.Hide();
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void FindClient (string name)
        {
            var proektTable = new DataTable();
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            string sql2 = $"SELECT distinct [Проект].[Название] as 'Проект' ,[Дата_начала] as 'Дата начала',[Дата_сдачи] as 'Дата сдачи',[Предмет_договора] as 'Предмет договора', Команда.Название as 'Команда' , [Вид_работы].Название as 'Работа', dbo.Клиент.ФИО FROM[dbo].[Проект], [dbo].[Техническое_задание],[dbo].[Команда],[dbo].[Вид_работы], [dbo].[Проект_работа], dbo.Клиент Where(Команда.fk_id_проект = Проект.id_проект) and([Проект_работа].fk_id_проект = Проект.id_проект and[Проект_работа].id_работа = [Вид_работы].fk_id_работа) and dbo.Клиент.ФИО LIKE '%{name}%' and (id_проект = dbo.Клиент.fk_id_проект)";
            var cn2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable);
            otprav.ItemsSource = proektTable.DefaultView;
            cn2.Close();


        }

        private void Find_Click1(object sender, RoutedEventArgs e)
        {
            var text1 = TextBox1.Text;
            FindClient(text1);
            TextBox1.Text = "";
        }

        public void FindData(DateTime date1, DateTime date2)
        {
            var proektTable = new DataTable();

            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            string sql2 = $" SELECT distinct [Проект].[Название] as 'Проект' ,[Дата_начала] as 'Дата начала',[Дата_сдачи] as 'Дата сдачи',[Предмет_договора] as 'Предмет договора', Команда.Название as 'Команда' , [Вид_работы].Название as 'Работа', dbo.Клиент.ФИО FROM[dbo].[Проект], [dbo].[Техническое_задание],[dbo].[Команда],[dbo].[Вид_работы], [dbo].[Проект_работа], dbo.Клиент Where(Команда.fk_id_проект = Проект.id_проект) and([Проект_работа].fk_id_проект = Проект.id_проект and[Проект_работа].id_работа = [Вид_работы].fk_id_работа) and (id_проект = dbo.Клиент.fk_id_проект) and (dbo.Проект.Дата_начала > '{date1}' and dbo.Проект.Дата_начала < '{date2}')";
            var cn2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable);
            otprav.ItemsSource = proektTable.DefaultView;
            cn2.Close();


        }

        private void Find_Click2(object sender, RoutedEventArgs e)
        {
            var date1 = Convert.ToDateTime(datePicker1.SelectedDate);
            var date2 = Convert.ToDateTime(datePicker2.SelectedDate);
            FindData(date1, date2);
        }
    }
}
