using System.Windows;
using ReactiveUI;
using TENET;
using TENET.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : ReactiveWindow<MessageViewModel>
    {
        string sValue = "";
        public MessageWindow()
        {

            DataContext = ViewModel = new MessageViewModel();
            InitializeComponent();


            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                
            });

            id2 = GlobalData.id;

            var proektTable = new DataTable();
            string sql = "SELECT ФИО  FROM dbo.Сотрудник ";
            var cn = new SqlConnection(Connection.String);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ClientsGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            ClientsGrid.MouseLeftButtonDown += GotFocus;

            otpravit1();

            otprav.MouseLeftButtonDown += GotFocus1;
        }

        private void GotFocus1(System.Object sender, System.EventArgs e)
        {
            var drv = otprav.SelectedItem as DataRowView;
            sValue = drv != null ? drv.Row["текст"] as string : string.Empty;
        }


        private void Delete(object sender, RoutedEventArgs e)
        {
            var cn = new SqlConnection(Connection.String);
            cn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"Delete From [dbo].[Сообщение] Where [текст] = '{sValue}'; ";
            cmd.ExecuteNonQuery();
            cn.Close();
            otpravit();
            MessageBox.Show($"Ваше сообщение удалено");
        }


        private void GotFocus(System.Object sender, System.EventArgs e)
        {
            otpravit();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            var proektTable2 = new DataTable();
            var text = TextBox.Text;
            string sql2 = $"Select m.Дата_отправки as 'Дата отправки', m.текст, p.ФИО AS [ФИО отправителя], q.ФИО AS [ФИО получателя] From[Сообщение] m Inner join Сотрудник p ON m.fk_id_сотрудника_отправителя = p.id_сотрудника Inner join Сотрудник q ON m.fk_id_сотрудника_получателя = q.id_сотрудника Where((m.fk_id_сотрудника_отправителя ={id2}) or(m.fk_id_сотрудника_получателя = {id2}))and (m.текст LIKE '%{text}%')";
            var cn2 = new SqlConnection(Connection.String);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable2);
            otprav.ItemsSource = proektTable2.DefaultView;
            cn2.Close();
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
                var connection = new SqlConnection(Connection.String);
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO [oil].[dbo].[Сообщение] VALUES('{message}', {poluhatel}, {id2}, '{date}'); ";
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Сообщение отправлено");
                Message.Text = "";
                otpravit();
            }
        }
        public void otpravit()
        {
            var proektTable2 = new DataTable();
            poluhatel = ClientsGrid.SelectedIndex == 0 ? ClientsGrid.SelectedIndex + 1 : ClientsGrid.SelectedIndex + 2;
            string sql2 = $"Select m.Дата_отправки as 'Дата отправки', m.текст, p.ФИО AS [ФИО отправителя], q.ФИО AS [ФИО получателя] From[Сообщение] m Inner join Сотрудник p ON m.fk_id_сотрудника_отправителя = p.id_сотрудника Inner join Сотрудник q ON m.fk_id_сотрудника_получателя = q.id_сотрудника Where((m.fk_id_сотрудника_отправителя ={id2}) and(m.fk_id_сотрудника_получателя = {poluhatel}))or((m.fk_id_сотрудника_отправителя ={poluhatel}) and(m.fk_id_сотрудника_получателя = {id2}) )";
            var cn2 = new SqlConnection(Connection.String);
            SqlCommand command2 = new SqlCommand(sql2, cn2);
            var adapter2 = new SqlDataAdapter(command2);
            cn2.Open();
            adapter2.Fill(proektTable2);
            otprav.ItemsSource = proektTable2.DefaultView;
            cn2.Close();
        }
        public void otpravit1()
        {
            var proektTable2 = new DataTable();
            string sql2 = $"Select m.Дата_отправки as 'Дата отправки', m.текст, p.ФИО AS [ФИО отправителя], q.ФИО AS [ФИО получателя] From[Сообщение] m Inner join Сотрудник p ON m.fk_id_сотрудника_отправителя = p.id_сотрудника      Inner join Сотрудник q ON m.fk_id_сотрудника_получателя = q.id_сотрудника     Where(m.fk_id_сотрудника_отправителя ={id2}) or(m.fk_id_сотрудника_получателя = {id2})";
            var cn2 = new SqlConnection(Connection.String);
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
