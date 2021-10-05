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
    public class ProjectEditViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        

        public ProjectEditViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            Back = ReactiveCommand.Create(() =>
            {

                var EditProjectsWindow = new EditProjectsWindow();
                EditProjectsWindow.Show();
            });

            Save = ReactiveCommand.Create(() =>
            {
                

            });

        }
        
    }
}