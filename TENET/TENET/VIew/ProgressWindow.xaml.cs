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
    public partial class ProgressWindow : ReactiveWindow<ProgressViewModel>
    {
        public ProgressWindow()
        {
            DataContext = ViewModel = new ProgressViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
            });

            

            int id = GlobalData.id;
            var proektTable = new DataTable(); 
            string sql = "select dbo.Проект.Название as 'Проект',dbo.Команда.Название as 'Команда, работающая над проектом',dbo.Ход_работы.Оценка_эксперта as 'Процент готовности',dbo.Сотрудник.ФИО as 'Ответственный за проект',dbo.Вид_работы.Название as 'Вид работы' from dbo.Проект,dbo.Команда,dbo.Ход_работы,dbo.Сотрудник,dbo.Вид_работы where (id_проект = (select fk_id_проект from dbo.Клиент where id_клиент=" + id + ")) and (fk_id_проект=(select fk_id_проект from dbo.Клиент where id_клиент=" + id + ")) and (id_ход_раб=(select fk_id_ход_раб from dbo.Команда where dbo.Команда.fk_id_проект=(select fk_id_проект from dbo.Клиент where id_клиент=" + id + "))) and (dbo.Ход_работы.fk_id_ответственный=id_сотрудника) and (dbo.Вид_работы.fk_id_работа = (select id_работа from dbo.Проект_работа where fk_id_проект=(select fk_id_проект from dbo.Клиент where id_клиент=" + id + ")))";
            const string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=oil;Data Source=DESKTOP-0473UDT\\SQLEXPRESS";
            var cn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, cn);
            var adapter = new SqlDataAdapter(command);
            cn.Open();
            adapter.Fill(proektTable);
            ProgressGrid.ItemsSource = proektTable.DefaultView;
            cn.Close();
            if (ProgressGrid.Items.Count > 0)
            Loaded += MyWindow_Loaded;
            
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var drv = ProgressGrid.Items[0] as DataRowView;
            var sValue = drv != null ? drv.Row["Процент готовности"] as string : string.Empty;
            if (sValue == "100%")
                MessageBox.Show("Поздравляем ваш проект завершен!!!");
        }
        
    }
}
