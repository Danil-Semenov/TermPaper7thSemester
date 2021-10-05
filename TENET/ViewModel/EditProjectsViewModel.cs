using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TENET.Model;

namespace TENET
{
    public class EditProjectsViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        //public ReactiveCommand<Unit, Unit> Save { get; }
        public ReactiveCommand<Unit, Unit> send1 { get; }
        public EditProjectsViewModel()
        {
            
            var PublicDataConnecton = new DataConnecton();
            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });

            send1 = ReactiveCommand.Create(() =>
            {
                //GlobalData.id = numberProject;

            });

        }
        
        [Reactive]
        public int numberProject { get; set; }

    }
}
