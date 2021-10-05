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
    public class ProjectsViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ProjectsViewModel()
        {
            Back = ReactiveCommand.Create(() =>
            {

                var Home = new Home();
                Home.Show();
            });


        }

    }
}
