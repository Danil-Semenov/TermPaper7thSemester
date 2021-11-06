using System.Windows;
using ReactiveUI;
using TENET;
using System.Data;
using TENET.Model;
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

            fill();
            ClientsGrid.MouseLeftButtonDown += GotFocus;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            var proektTable = new DataTable();
            var text = TextBox.Text;
            string sql2 = $"SELECT ФИО, dbo.Компания.Название as 'Компания', логин, пароль, dbo.Проект.Название as 'Заказанный проект', dbo.Команда.Название as 'Команда проекта' FROM dbo.Клиент, dbo.Проект, dbo.Команда, dbo.Компания where (dbo.Проект.id_проект = dbo.Клиент.fk_id_проект) and (dbo.Команда.fk_id_проект = dbo.Проект.id_проект) and (dbo.Компания.id_компании = dbo.Клиент.fk_id_компания) and (dbo.Клиент.ФИО LIKE '%{text}%')";
            var cn2 = new SqlConnection(Connection.String);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable);
            ClientsGrid.ItemsSource = proektTable.DefaultView;
            cn2.Close();
            TextBox.Text = " ";
            TextBox1.Text = " ";
        }

        private void GotFocus(System.Object sender, System.EventArgs e)
        {
            var drv = ClientsGrid.SelectedItem as DataRowView;
            var sValue = drv != null ? drv.Row["ФИО"] as string : string.Empty;
            TextBox1.Text=sValue;
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            delete();
            fill();
        }

        public void delete()
        {
            var text1 = TextBox1.Text;
            var cn = new SqlConnection(Connection.String);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Delete from dbo.Клиент where ФИО = '{text1}' ";
            cmd.ExecuteNonQuery();
            cn.Close();
            TextBox.Text = " ";
            TextBox1.Text = " ";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            fill();

        }

        public void fill()
        {
            var proektTable = new DataTable();
            string sql = "SELECT ФИО, dbo.Компания.Название as 'Компания', логин, пароль, dbo.Проект.Название as 'Заказанный проект', dbo.Команда.Название as 'Команда проекта' FROM dbo.Клиент, dbo.Проект, dbo.Команда, dbo.Компания where (dbo.Проект.id_проект = dbo.Клиент.fk_id_проект) and (dbo.Команда.fk_id_проект = dbo.Проект.id_проект) and (dbo.Компания.id_компании = dbo.Клиент.fk_id_компания)";
            var cn = new SqlConnection(Connection.String);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ClientsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();

        }

    }
}

