using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Green_1
    {
        public abstract class Participant
        {
            private string _surname;
            private string _group;
            private string _trainer;
            private double _result;

            private protected double standart;
            private static int passedcount;

            static Participant()
            {
                passedcount = 0;
            }

            public Participant(string surname, string group, string trainer)
            {
                _surname = surname;
                _group = group;
                _trainer = trainer;
                _result = 0;
            }

            public string Surname
            {
                get 
                { 
                    //if(_surname == null) return default;
                    return _surname; 
                }
            }

            public string Group => _group;

            public string Trainer => _trainer;

            public double Result => _result;

            public static int PassedTheStandard => passedcount;

            public bool HasPassed => _result <= standart && _result > 0;

            public void Run(double result)
            {
                if (_result == 0)
                {
                    _result = result;
                    if (HasPassed)
                    {
                        passedcount++;
                    }
                }
            }
            public static Participant[] GetTrainerParticipants(Participant[] participants, Type participantType, string trainer)
            {
                return participants.Where(participant => participant.GetType() == participantType && participant.Trainer == trainer).ToArray();

            }
            public void Print()
            {
                Console.WriteLine($"Фамиия: {Surname}");
                Console.WriteLine($"Группа: {Group}");
                Console.WriteLine($"Учитель: {Trainer}");
                Console.WriteLine($"Результат: {Result}");
                Console.WriteLine($"Прошли: {HasPassed}");
            }
        }
        public class Participant100M : Participant
        {
            public Participant100M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                standart = 12;
            }
        }
        public class Participant500M : Participant
        {
            public Participant500M(string surname, string group, string trainer) : base(surname, group, trainer)
            {
                standart = 90;
            }
        }
    }
}
