using System.Windows;
using ReactiveUI;
using TENET;
using System.Data;
using System.Data.SqlClient;


namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class TasksWindow : ReactiveWindow<TasksViewModel>
    {
        public TasksWindow()
        {
            DataContext = ViewModel = new TasksViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                this.BindCommand(this.ViewModel, x => x.Save, x => x.ButtonSave);
                this.Bind(this.ViewModel, x => x.Performance, x => x.performance.Text);
            });

            var proektTable = new DataTable();
            int idProject = GlobalData.ProjectId;

            string sql = "select dbo.Проект.Название as 'Проект', [Дата_Начала] as 'Дата Начала', [Дата_сдачи] as 'Дата сдачи', [Текст] as 'Техническое задание', Оценка_эксперта as 'Успеваемость' From dbo.Проект, dbo.Техническое_задание, dbo.Ход_работы WHERE (id_проект=" + idProject + ") AND (fk_id_исполнитель =(select fk_id_программист from dbo.Команда where fk_id_проект =" + idProject + ")) AND (id_ход_раб =(select fk_id_ход_раб from dbo.Команда where fk_id_программист=" + GlobalData.id + "))";
            //string sql = "select dbo.Проект.Название as 'Проект', [Дата_Начала], [Дата_сдачи], [Текст] as 'Техническое задание' From dbo.Проект, dbo.Техническое_задание WHERE (id_проект=" + idProject + ") AND (fk_id_исполнитель =(select fk_id_программист from dbo.Команда where fk_id_проект =" + idProject + "))";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            TasksGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            //adapter.Dispose();
            
        }

        /*public void GetTasks (int id)

        {

            var proektTable = new DataTable();
            GlobalData.id = 1;
            string sql = "select dbo.Проект.Название, [Текст] as 'Техническое задание' From dbo.Проект, dbo.Техническое_задание WHERE (id_проект={id}) AND (fk_id_исполнитель =(select fk_id_программист from dbo.Команда where fk_id_проект = id))";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            TasksGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            //adapter.Dispose();

        } */

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }
    }
}
