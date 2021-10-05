using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using TENET.Model;

namespace TENET
{
    public class TasksViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public TasksViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });

            Save = ReactiveCommand.Create(() =>
            {
                //GlobalData.name = Performance;
                //PublicDataConnecton.UpdatePerfromance(GlobalData.name, GlobalData.id);

            });

        }

        [Reactive]
        public string Performance { get; set; }

    }
}