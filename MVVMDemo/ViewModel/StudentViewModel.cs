using MVVMDemo.Model;
using MVVMDemo.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo.ViewModel
{
    public class StudentViewModel : ViewModelBase
    {
        private Student _student;
        private ObservableCollection<Student> _students; // Observable !! Irá linkada a la vista !!

        public ICommand SubmitCommand { get; set; }


        public Student Student
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                NotifyPropertyChanged("Student");
            }
        }
        public ObservableCollection<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                NotifyPropertyChanged("Students");
            }
        }

        public StudentViewModel()
        {
            Student = new Student();
            Students = new ObservableCollection<Student>();
            SubmitCommand = new RelayCommand(param => this.Submit(),
                     esMayorDeEdad);
        }



        private void Submit()
        {
            Student.JoiningDate = DateTime.Today.Date;
            Students.Add(Student);
            Student = new Student();
        }

        //Estos métodos son los que usaremos como predicate en el constructor del RelayCommand
        public bool esMayorDeEdad(object message)
        {
            if(message == null)
            {
                return false;
            }

            if ((int)message < 18)
                return false;

            return true;
        }


    }
}
