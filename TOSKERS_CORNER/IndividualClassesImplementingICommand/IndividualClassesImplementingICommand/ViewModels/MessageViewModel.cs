using IndividualClassesImplementingICommand.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IndividualClassesImplementingICommand.ViewModels
{
    public class MessageViewModel
    {
        public string MessageText { get; set; }
        public MessageCommand DisplayMessageCommand { get; private set; }
        

        public MessageViewModel ()
        {
            DisplayMessageCommand = new MessageCommand(displayMessage);
        }


        public void displayMessage()
        {
            MessageBox.Show(MessageText);
        }

    }
}
