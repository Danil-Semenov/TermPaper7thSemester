using System.Windows;
using ReactiveUI;
using TENET;


namespace TENET
{

    public partial class MainWindow : ReactiveWindow<ViewModel>
    {
        public MainWindow()
        {
            DataContext = ViewModel = new ViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.Bind(this.ViewModel, x => x.login, x => x.Login.Text);
                this.Bind(this.ViewModel, x => x.password, x => x.Password.Text);
                this.BindCommand(ViewModel, x => x.TheCommand, x => x.Button);
                this.Bind(this.ViewModel, x => x.massage, x => x.TextBlock.Text);

            });
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
