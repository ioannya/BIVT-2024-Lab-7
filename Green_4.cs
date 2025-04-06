using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Green_4
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _res;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _res = new double[3];
            }


            public string Name => _name;
            public string Surname => _surname;
            public double[] Jumps => _res;
            public double BestJump
            {
                get
                {
                    if (_res == null || _res.Length == 0)
                    {
                        return default;
                    }
                    return _res.Max();
                }
            }
            public void Jump(double result)
            {
                if (_res == null) return;
                for (int i = 0; i < _res.Length; i++)
                {
                    if (_res[i] == 0)
                    {
                        _res[i] = result;
                        return;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                int n = array.Length;
                for (int i = 1; i < n; ++i)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].BestJump < key.BestJump)
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Лучший результат: {BestJump}");
            }
        }
        public abstract class Discipline
        {
            private string _name;
            private Participant[] _participants;

            public string Name => _name;
            public Participant[] Participants => _participants;

            public Discipline(string name)
            {
                _name = name;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                Participant[] temp = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    temp[i] = _participants[i];
                }
                temp[temp.Length - 1] = participant;
                _participants = temp;
            }
            public void Add(Participant[] newParticipants)
            {
                Array.Resize(ref _participants, _participants.Length + newParticipants.Length);
                for (int i = 0; i < newParticipants.Length; i++)
                {
                    _participants[_participants.Length - newParticipants.Length + i] = newParticipants[i];
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
            public abstract void Retry(int index);
            public void Print()
            {
                Console.WriteLine($"Дисциплина: {Name}");
                Console.WriteLine("Участники:");
                foreach (var participant in Participants)
                {
                    participant.Print();
                }
            }
        }
        public class LongJump : Discipline
        {
            public LongJump() : base("Long jump") { }
            public override void Retry(int index)
            {
                if (index >= 0 && index < Participants.Length)
                {
                    Participant participant = Participants[index];
                    double bestJump = participant.BestJump;
                    Participant updated = new Participant(participant.Name, participant.Surname);
                    updated.Jump(bestJump);
                    updated.Jump(0);
                    updated.Jump(0);
                    Participants[index] = updated;
                }
            }
           
        }
        public class HighJump : Discipline
        {
            public HighJump() : base("High jump") { }
            public override void Retry(int index)
            {
                if (index >= 0 && index < Participants.Length)
                {
                    Participant participant = Participants[index];
                    double[] jumps = participant.Jumps;
                    if (jumps != null && jumps.Length > 0)
                    {
                        for (int i = jumps.Length - 1; i >= 0; i--)
                        {
                            if(jumps[i] != 0)
                            {
                                jumps[i] = 0;
                                break;
                            }
                        }
                    }
                }
            }
           
        }
    }
}
