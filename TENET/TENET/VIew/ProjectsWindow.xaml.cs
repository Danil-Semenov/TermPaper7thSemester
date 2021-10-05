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
    public partial class ProjectsWindow : ReactiveWindow<ProjectsViewModel>
    {
        public ProjectsWindow()
        {
            DataContext = ViewModel = new ProjectsViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
            });

            var proektTable = new DataTable();
            string sql = "SELECT [Название] as 'Проект', [Дата_Начала], [Дата_сдачи], [Предмет_договора] as 'Предмет договора' FROM dbo.Проект WHERE [Название] IS NOT NULL AND [Предмет_договора] IS NOT NULL AND [Дата_Начала] IS NOT NULL AND [Дата_сдачи] IS NOT NULL";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ProjectsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            //adapter.Dispose();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
