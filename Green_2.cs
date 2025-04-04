using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Green_2
    {
        public class Human
        {
            private string _name;
            private string _surname;

            public Human(string name, string surname)
            {
                _name = name;
                _surname = surname;
            }
            public string Name => _name;
            public string Surname => _surname;
        }

        public class Student : Human
        {
            private int[] _marks;
            private int _count;
            private static int _excellentCount;

            public Student(string name, string surname) : base(name, surname)
            {
                _marks = new int[4];
                _count = 0;
                _excellentCount = 0;
            }

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
                    if(_count < 4) return 0;
                    double s = 0;
                    foreach (var m in _marks)
                    {
                        s += m;
                    }
                    return s / _marks.Length;
                }
            }

            public bool IsExcellent
            {
                get
                {
                    if(_count == 0) return false;
                    foreach (var m in _marks)
                    {
                        if (m < 4) return false;
                    }
                    return true;
                }
            }
            public static int ExcellentAmount => _excellentCount;
            public void Exam(int mark)
            {
                if (_count < _marks.Length && mark <= 5 && mark >= 2)
                {
                    _marks[_count] = mark;
                    _count++;
                    if(_count == 4 && IsExcellent)
                        _excellentCount++;
                }
            }

            public static void SortByAvgMark(Student[] array)
            {
                if(array == null || array.Length == 0) return;
                int n = array.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (array[j].AvgMark < array[j + 1].AvgMark)
                        {
                            Student temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Средний балл: {AvgMark}, Отличники: {IsExcellent}");
            }
        }
    }
}
