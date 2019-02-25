using UsingINotifyPropertyChanged;

public class PersonViewModel : ObservableObject
{
    private string _name;

    public string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_name))
                return "Unknown";

            return _name;
        }
        set
        {
            _name = value;
            //Implementa la clase ObservableObject que a su vez implementa INotifyPropertyChanged
            OnPropertyChanged("Name");
        }
    }
}