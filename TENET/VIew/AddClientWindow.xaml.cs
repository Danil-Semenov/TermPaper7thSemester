using System.Windows;
using ReactiveUI;
using TENET;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class AddClientWindow : ReactiveWindow<AddClientViewModel>
    {
        public AddClientWindow()
        {
            DataContext = ViewModel = new AddClientViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.BindCommand(this.ViewModel, x => x.Back, x => x.ButtonBack);
                this.Bind(this.ViewModel, x => x.FIO, x => x.EditFIO.Text);
                this.Bind(this.ViewModel, x => x.Company, x => x.EditCompany.Text);
                this.Bind(this.ViewModel, x => x.Login, x => x.EditLogin.Text);
                this.Bind(this.ViewModel, x => x.Password, x => x.EditPassword.Text);
                this.BindCommand(this.ViewModel, x => x.Add, x => x.ButtonAdd);
                
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }

        private void button_Click3(object sender, RoutedEventArgs e)
        {
            this.UpdateLayout();
        }
    }
}
