using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using TENET.Model;

namespace TENET
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class WinGisWindow1 : ReactiveWindow<WinGisViewModel>
    {

        public WinGisWindow1()
        {
            InitializeComponent();

           
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            //var map = new AxMapWinGIS.AxMap();

            //host.Child = map;

            //this.grid1.Children.Add(host);
        }
    }
}
