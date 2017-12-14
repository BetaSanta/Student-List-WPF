using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace TestTask
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Student selectedStudent;

        [XmlArray("Students")]
        [XmlArrayItem("Student", typeof(Student))]
        public ObservableCollection<Student> Students { get; set; }

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand((o) =>
                    {
                        studentWindow studentWindow = new studentWindow(new Student());
                        if (studentWindow.ShowDialog() == true)
                        {
                            Student student = studentWindow.student;

                            Students.Insert(0, student);
                            SelectedStudent = student;
                        }
                    }));
            }
        }

        // команда редактирования оъекта
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedStudent) =>
                  {
                      if (selectedStudent == null)
                          return;

                      // получаем выделенный объект
                      Student student = selectedStudent as Student;

                      //делаем копию
                      Student copyStud = new Student()
                      {
                          FirstName = student.FirstName,
                          Last = student.Last,
                          Age = student.Age,
                          Gender = student.Gender
                      };

                      studentWindow studentWindow = new studentWindow(copyStud);

                      // возвращаем результат
                      if (studentWindow.ShowDialog() == true)
                      {
                          copyStud = studentWindow.student;

                          student.FirstName = copyStud.FirstName;
                          student.Last = copyStud.Last;
                          student.FullName = copyStud.FirstName + " " + copyStud.Last;
                          student.Age = copyStud.Age;
                          student.Gender = copyStud.Gender;
                      }
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      if (selectedStudent != null)
                      {
                          if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                          {
                              Student student = obj as Student;
                              if (student != null)
                              {
                                  Students.Remove(student);
                              }
                          }
                      }
                  },
                 (obj) => Students.Count > 0));
            }
        }

        public ApplicationViewModel()
        {
            Students = new ObservableCollection<Student>
            {
                new Student{ FirstName = "Robert", Last = "Jarman", Age = 21, Gender = "Мужской"},
                new Student{ FirstName = "Leona", Last = "Menders", Age = 20, Gender = "Женский"},
                new Student{ FirstName = "Helen", Last = "Wilson", Age = 21, Gender = "Женский"},
                new Student{ FirstName = "John", Last = "Smith", Age = 22, Gender = "Мужской"},
                new Student{ FirstName = "Алексей", Last = "Дроздов", Age = 19, Gender = "Мужской"},
                new Student{ FirstName = "Вадим", Last = "Халтурин", Age = 21, Gender = "Мужской"},
                new Student{ FirstName = "Анна", Last = "Говорухина", Age = 20, Gender = "Женский"},
                new Student{ FirstName = "Александр", Last = "Иванов", Age = 20, Gender = "Мужской"}
            };

            // SERIALIZATION
            ObservableCollection<ObservableCollection<Student>> Document = new ObservableCollection<ObservableCollection<Student>>();

            Document.Add(Students);
            Util.SerializeObjectToXML<ObservableCollection<ObservableCollection<Student>>>(Document, @"d:\tmp\POSISIPISOS.txt");
        }

        class Util
        {
            public static void SerializeObjectToXML<T>(T item, string FilePath)
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamWriter wr = new StreamWriter(FilePath))
                {
                    xs.Serialize(wr, item);
                }
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
