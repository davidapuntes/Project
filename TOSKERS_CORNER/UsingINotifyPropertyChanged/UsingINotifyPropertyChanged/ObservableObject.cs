using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingINotifyPropertyChanged
{
    
    /*
     * Al implementar la INotifyPropertyChanged, podremos hacer un binding en la vista ,
     * de clases (que hereden de esta, que es la que tiene implementada en la interfaz)
     * 
     * */
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
