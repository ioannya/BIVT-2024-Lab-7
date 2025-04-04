using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Green_3
    {
        public class Student
        {
            private static int _ID = 0;
            private string _name;
            private string _surname;
            private int[] _marks;
            private bool _isExpelled;
            private int _examsTaken;
            private int _id;

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[3]; 
                _isExpelled = false;
                _examsTaken = 0;
                _id = _ID++;
            }

            static Student()
            {
                _ID = 1;
            }
            public string Name => _name;
            public string Surname => _surname;
            public int ID => _id;
            public int[] Marks
            {
                get
                {
                    if (_marks == null)
                    {
                        return default;
                    }
                    var a = new int[_marks.Length];
                    Array.Copy(_marks, a, _marks.Length);
                    return a;
                }
                
            }

            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return default;
                    double sum = 0;
                    for (int i = 0; i < _examsTaken; i++) 
                    {
                        sum += _marks[i];
                    }
                    return _examsTaken == 0 ? 0 : sum / _examsTaken;
                }
            }

            public bool IsExpelled => _isExpelled;

            public void Exam(int mark)
            {
                if(IsExpelled || _marks == null || _marks.Length == 0) return;
                if(mark < 2 || mark > 5) return;
                if(_examsTaken >= _marks.Length) return;
                
                _marks[_examsTaken] = mark;
                _examsTaken++;
                if (mark == 2)
                {
                    _isExpelled = true;
                }

            }

            public static void SortByAvgMark(Student[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 1; i < array.Length; i++)
                {
                    Student key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].AvgMark < key.AvgMark)
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Restore()
            {
                if (_isExpelled) _isExpelled = false;
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Средний балл: {AvgMark:F2}, Исключен: {IsExpelled}");
            }
        }
        public class Commission
        {
            public static void Sort(Student[] students)
            {
                if (students == null || students.Length == 0) return;

                for (int i = 1; i < students.Length; i++)
                {
                    Student key = students[i];
                    int j = i - 1;

                    while (j >= 0 && students[j].ID < key.ID)
                    {
                        students[j + 1] = students[j];
                        j = j - 1;
                    }
                    students[j + 1] = key;
                }
            }
            public static Student[] Expel(ref Student[] students)
            {
                var expelledStudents = students.Where(student => student.IsExpelled).ToArray();
                students = students.Where(student => !student.IsExpelled).ToArray();
                return expelledStudents;
            }
            public static void Restore(ref Student[] students, Student restored)
            {
                if(!students.Any(student => student.ID == restored.ID)) return;
                restored.Restore();
                students = students.Where(student => student.ID != restored.ID).Append(restored).OrderBy(student => student.ID).ToArray();
            }
        }
    }
}
