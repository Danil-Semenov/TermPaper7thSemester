using ReactiveUI;
using TENET;
using System.Windows;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : ReactiveWindow<HomeViewModel>
    {
        public Home()
        {
            DataContext = ViewModel = new HomeViewModel();
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
               // this.Bind(this.ViewModel, x => x.nameUser, x => x.TextBlock1.Text);

                this.BindCommand(this.ViewModel, x => x.Chat, x => x.Button1);
                this.Bind(this.ViewModel, x => x.Button1Text, x => x.Button1.Header);
                this.Bind(this.ViewModel, x => x.Button1Vision, x => x.Button1.Visibility, isVisible => isVisible ? Visibility.Visible : Visibility.Collapsed,
visibility => visibility == Visibility.Visible);
                this.BindCommand(this.ViewModel, x => x.EditInformation, x => x.Button2);
                this.Bind(this.ViewModel, x => x.Button2Text, x => x.Button2.Header);
                this.Bind(this.ViewModel, x => x.Vision, x => x.Button2.Visibility, isVisible => isVisible ? Visibility.Visible : Visibility.Collapsed,
visibility => visibility == Visibility.Visible);
                this.BindCommand(this.ViewModel, x => x.OrderNewProject, x => x.Button3);
                this.Bind(this.ViewModel, x => x.Button3Text, x => x.Button3.Header);
                this.Bind(this.ViewModel, x => x.Vision, x => x.Button3.Visibility, isVisible => isVisible ? Visibility.Visible : Visibility.Collapsed,
visibility => visibility == Visibility.Visible);
                this.BindCommand(this.ViewModel, x => x.ViewProgress, x => x.Button4);
                this.Bind(this.ViewModel, x => x.Button4Text, x => x.Button4.Header);
                this.Bind(this.ViewModel, x => x.Vision, x => x.Button4.Visibility, isVisible => isVisible ? Visibility.Visible : Visibility.Collapsed,
visibility => visibility == Visibility.Visible);
                this.BindCommand(this.ViewModel, x => x.Back, x => x.Button6);

                this.BindCommand(this.ViewModel, x => x.Report, x => x.Button5);
                this.Bind(this.ViewModel, x => x.Button5Text, x => x.Button5.Header);
                this.Bind(this.ViewModel, x => x.Button5Vision, x => x.Button5.Visibility, isVisible => isVisible ? Visibility.Visible : Visibility.Collapsed,
visibility => visibility == Visibility.Visible);

            });
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if(GlobalData.level !=4)
                this.Hide();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}