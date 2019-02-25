using System.Windows.Media;
using UsingINotifyPropertyChanged;

public class BackgroundViewModel : ObservableObject
{
    private Brush _color;

    public Brush Color
    {
        get
        {
            if (_color == null)
                return Brushes.Red;

            return _color;
        }
        set
        {
            _color = value;
            OnPropertyChanged("Color");
        }
    }
}