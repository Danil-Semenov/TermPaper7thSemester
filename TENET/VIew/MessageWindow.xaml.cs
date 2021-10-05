using System.Windows;
using ReactiveUI;
using TENET;
using TENET.Model;
using System.Data;
using System.Data.SqlClient;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : ReactiveWindow<MessageViewModel>
    {
        public MessageWindow()
        {
            //ComboBox.DisplayMemberPath = Users.Name;
            DataContext = ViewModel = new MessageViewModel();
            InitializeComponent();


            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                
            });

            id2 = GlobalData.id;

            var proektTable = new DataTable();
            string sql = "SELECT id_сотрудника as 'Номер сотрудника', ФИО  FROM dbo.Сотрудник ";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ClientsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();

            var proektTable1 = new DataTable();
            string sql1 = $"SELECT текст, Дата_отправки as 'Дата отправки', fk_id_сотрудника_отправителя as 'Номер отправителя' FROM dbo.Сообщение Where fk_id_сотрудника_получателя = {id2} ";
            var cn1 = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand(sql1, cn1);
            var adapter1 = new SqlDataAdapter(command1);
            cn1.Open();
            adapter1.Fill(proektTable1);
            polyh.ItemsSource = proektTable1.DefaultView;
            cn1.Close();

            otpravit();



        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void Send_Click(object sender, RoutedEventArgs e)
        {
            poluhatel = ClientsGrid.SelectedIndex == 0 ? ClientsGrid.SelectedIndex + 1 : ClientsGrid.SelectedIndex + 2;
            if (poluhatel == GlobalData.id) { MessageBox.Show("Нельзя отправить самому себе"); }
            else
            {
                message = Message.Text;
                var date = new System.DateTime();
                date = System.DateTime.Now;
                string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO [oil].[dbo].[Сообщение] VALUES('{message}', {poluhatel}, {id2}, '{date}'); ";
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Сообщение отправлено");
                otpravit();
            }
        }
        public void otpravit()
        {
            var proektTable2 = new DataTable();
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            string sql2 = $"SELECT текст, Дата_отправки as 'Дата отправки' , fk_id_сотрудника_получателя as 'Номер получателя'  FROM dbo.Сообщение Where fk_id_сотрудника_отправителя = {id2} ";
            var cn2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable2);
            otprav.ItemsSource = proektTable2.DefaultView;
            cn2.Close();
        }
        int poluhatel { get; set; }
        string message { get; set; }
        int id2 { get; set; }
    }
}
