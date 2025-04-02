using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Green_5
    {
        public struct Student
        {
            private string _name;
            private string _surname;
            private int[] _marks;

            public Student(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

            public string Name => _name ?? default;
            public string Surname => _surname ?? default;
            public int[] Marks => _marks?.ToArray();
            public double AvgMark
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return default;
                    double s = 0;
                    foreach (var m in _marks)
                    {
                        s += m;
                    }
                    return s / _marks.Length;
                }
            }
            public void Exam(int mark)
            {
                if(mark < 2 || mark > 5 || _marks == null) return;
                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] == 0)
                    {
                        _marks[i] = mark;
                        return;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Средний балл: {AvgMark}");
            }
        }
        public class Group
        {
            private string _name;
            protected Student[] _students;
            private int _count;

            public Group(string name, int size = 10)
            {
                _name = name;
                _students = new Student[size];
                _count = 0;
            }
            public string Name => _name ?? default;
            public Student[] Students => _students ?? default;

            public virtual double AvgMark
            {
                get
                {
                    if (_count == 0) return 0;
                    double s = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        s += _students[i].AvgMark;
                    }
                    return s / _count;
                }
            }
            public void Add(Student student)
            {
                if (_count < _students.Length)
                {
                    _students[_count++] = student;
                }
            }
            public void Add(Student[] students)
            {
                foreach (var student in students)
                {
                    Add(student);
                }
            }

            public static void SortByAvgMark(Group[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 1; i < array.Length; i++)
                {
                    Group key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].AvgMark < key.AvgMark)
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {Name}, Средний балл: {AvgMark:F2}");
                for (int i = 0; i < _count; i++)
                {
                    _students[i].Print();
                }
            }
        }
        public class EliteGroup : Group
        {
            public EliteGroup(string name, int size) : base(name, size) { }
            public override double AvgMark
            {
                get
                {
                    if(_students.Length == 0) return 0;
                    double sum = 0;
                    double count = 0;
                    foreach (var student in _students)
                    {
                        if (student.Marks.Any(m => m != 0))
                        {
                            double weightedSum = 0;
                            foreach (var mark in student.Marks)
                            {
                                if (mark != 0)
                                {
                                    switch (mark)
                                    {
                                        case 5:
                                            weightedSum += mark * 1;
                                            break;
                                        case 4:
                                            weightedSum += mark * 1.5;
                                            break;
                                        case 3:
                                            weightedSum += mark * 2;
                                            break;
                                        case 2:
                                            weightedSum += mark * 2.5;
                                            break;
                                    }
                                }
                            }
                            sum += weightedSum / student.Marks.Count(m => m != 0);
                            count++;
                        }
                    }
                    return count == 0 ? 0 : sum / count;
                }
            }

        }
        public class SpecialGroup : Group
        {
            public SpecialGroup(string name, int size) : base(name, size) { }
            public override double AvgMark
            {
                get
                {
                    if (Students.Length == 0) return 0;
                    double sum = 0;
                    int count = 0;
                    foreach (var student in Students)
                    {
                        if (student.Marks.Any(m => m != 0))
                        {
                            double weightedSum = 0;
                            foreach (var mark in student.Marks)
                            {
                                if (mark != 0)
                                {
                                    switch (mark)
                                    {
                                        case 5:
                                            weightedSum += mark * 1;
                                            break;
                                        case 4:
                                            weightedSum += mark * 0.75;
                                            break;
                                        case 3:
                                            weightedSum += mark * 0.5;
                                            break;
                                        case 2:
                                            weightedSum += mark * 0.25;
                                            break;
                                    }
                                }
                            }
                            sum += weightedSum / student.Marks.Count(m => m != 0);
                            count++;
                        }
                    }
                    return count == 0 ? 0 : sum / count;
                }
            }
        }
    }
}
