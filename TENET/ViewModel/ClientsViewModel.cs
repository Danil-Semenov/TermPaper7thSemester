using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using TENET.Model;
using System.Data;
using System.Data.SqlClient;


namespace TENET
{
    public class ClientsViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ClientsViewModel()
        {
            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
              
            });

            



        }

    }
}