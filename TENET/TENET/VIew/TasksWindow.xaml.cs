using System.Windows;
using ReactiveUI;
using TENET;
using System.Data;
using System.Data.SqlClient;
using TENET.Model;

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
            string sql = $"SELECT distinct dbo.Проект.[Название] as 'Проект' FROM [dbo].[Проект],dbo.Команда, dbo.Сотрудник where (fk_id_команды=id_команда) and (fk_id_проект=id_проект) and (fk_id_программист={GlobalData.id})";
            var cn = new SqlConnection(Connection.String);
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
            GlobalData.project = sValue;
            Fill(GlobalData.project, GlobalData.id);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click_Save(object sender, RoutedEventArgs e)
        {
            var PublicDataConnecton = new DataConnecton();
            GlobalData.name = performance.Text;
            PublicDataConnecton.UpdatePerfromance(GlobalData.name, GlobalData.id, GlobalData.project);
            Fill(GlobalData.project,GlobalData.id);
            MessageBox.Show($"Значение успешно обновлено");
        }

        public void Fill(string name, int id)
        {
            var proektTable = new DataTable();
            //string sql = "select dbo.Проект.Название as 'Проект', [Дата_Начала] as 'Дата Начала', [Дата_сдачи] as 'Дата сдачи', [Текст] as 'Техническое задание', Оценка_эксперта as 'Успеваемость' From dbo.Проект, dbo.Техническое_задание, dbo.Ход_работы WHERE (id_проект=" + idProject + ") AND (fk_id_исполнитель =(select fk_id_программист from dbo.Команда where fk_id_проект =" + idProject + ")) AND (id_ход_раб =(select fk_id_ход_раб from dbo.Команда where fk_id_программист=" + GlobalData.id + ")) and (fk_id_составитель =(select fk_id_менеджер from dbo.Команда where fk_id_проект =" + idProject + "))";
            string sql = $"select distinct dbo.Проект.Название as 'Проект', [Дата_Начала] as 'Дата Начала', [Дата_сдачи] as 'Дата сдачи', [Текст] as 'Техническое задание', Оценка_эксперта as 'Готовность' From dbo.Проект, dbo.Техническое_задание, dbo.Ход_работы, dbo.Команда where (dbo.Проект.Название = '{name}') and (dbo.Техническое_задание.id_тех_зад = (select fk_id_тех_зад from dbo.Сотрудник where id_сотрудника='{id}')) and (dbo.Ход_работы.id_ход_раб = (select fk_id_ход_раб from dbo.Команда where dbo.Команда.fk_id_проект = (select id_проект from dbo.Проект where dbo.Проект.Название='{name}')))";
            var cn = new SqlConnection(Connection.String);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            TasksGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();

        }
    }
}
