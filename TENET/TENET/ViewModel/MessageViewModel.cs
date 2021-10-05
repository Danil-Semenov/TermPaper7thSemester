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
    public class MessageViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> Back { get; }
        public ReactiveCommand<Unit, Unit> Send { get; }
        public MessageViewModel()
        {
            var PublicDataConnecton = new DataConnecton();
            
            Send = ReactiveCommand.Create(() =>
            {
                User = PublicDataConnecton.GetFioList();
                Recipientid = PublicDataConnecton.GetRecipientid(Recipient);
                PublicDataConnecton.SendMassege( Recipientid, GlobalData.id, Massage);
            });
            Back = ReactiveCommand.Create(() =>
            {
                var Home = new Home();
                Home.Show();
            });
        }

        [Reactive]
        public string Massage { get; set; }
        [Reactive]
        public string Recipient { get; set; }
        public int Recipientid { get; set; }
        [Reactive]
        public List<string> History { get; set; }
        public List<string> User { get; set; }
    }
}
