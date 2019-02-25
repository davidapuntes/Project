using System.Windows.Media;
using UsingINotifyPropertyChanged;

public class MainViewModel : ObservableObject
{
    public PersonViewModel Person { get; private set; }
    public BackgroundViewModel Background { get; private set; }

    public MainViewModel()
    {
        Person = new PersonViewModel();
        Background = new BackgroundViewModel();
    }

    public void SetBackground(Brush brushColor)
    {
        Background.Color = brushColor;
    }
}