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
    public partial class ClientsWindow : ReactiveWindow<ClientsViewModel>
    {
        public ClientsWindow()
        {
            DataContext = ViewModel = new ClientsViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                
            });

            var proektTable = new DataTable();
            string sql = "SELECT ФИО, dbo.Компания.Название as 'Компания', логин, пароль, dbo.Проект.Название as 'Заказанный проект', dbo.Команда.Название as 'Команда проекта' FROM dbo.Клиент, dbo.Проект, dbo.Команда, dbo.Компания where (dbo.Проект.id_проект = dbo.Клиент.fk_id_проект) and (dbo.Команда.fk_id_проект = dbo.Проект.id_проект) and (dbo.Компания.id_компании = dbo.Клиент.fk_id_компания)";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ClientsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            //adapter.Dispose();

            


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}

