using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using TENET.Model;

namespace TENET
{
    public class ProgressViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ProgressViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            GlobalData.id = Convert.ToInt32(PublicDataConnecton.GetUserId(GlobalData.login, GlobalData.password));
            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });


        }

    }
}
