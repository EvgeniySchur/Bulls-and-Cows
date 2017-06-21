using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulls_and_Cows
{
    class ComputerAnswer: Answer
    {
        enum Str
        {
          Enter_Num = 8,
          Attempts,
          YouWin,
          CPUWin,
          ErrorInput,
          Continue,
          Yes,
          No,
          I_Think_This,
          Enter_Sequence
        }
        //Вызов конструктора базового класса
        public ComputerAnswer(string[] text) : base (text){}
        public  void Start()
        {
            Switcher();
            Rules();
            GameInProgress(PrintAttempts);
        }
        private void GameInProgress(int printAttempts)
        {
            output.PrintLine(text[(int)Str.Enter_Num] + text[(int)Str.Attempts] + GameLogic.Attempts);
            string reference = string.Empty;
            for (var i = 0; i < int.MaxValue; i++)
            {
                output.PrintLine(text[(int)Str.Enter_Sequence]);
                reference = input.ReadLine((int)Module.Human);
                if (reference != error) break;
                else output.PrintLine(text[(int)Str.ErrorInput]); continue;
            }
            //Создаем множество
            var set = Logic.RangeOfValues();
           
            for (var i = 0; i< GameLogic.Attempts - 1; i++)
            {
                printAttempts--;
                //Выберем случайный элемент из множества допустимых ответов
                var rnd = new Random();
                var indexOfValue = rnd.Next(set.Count-1);
                var answer = set[indexOfValue];
                //Удалим этот ответ из множества допустимых, т.к. мы его уже использовали
                set.Remove(set[indexOfValue]);

                output.PrintLine(text[(int)Str.I_Think_This] + answer + text[(int)Str.Attempts] + printAttempts);
                var result = Logic.CheckAnswer(reference, answer);
                output.PrintLine(result);
               
                if (result == rightAnswer)
                {
                    output.PrintLine(text[(int)Str.CPUWin] + (GameLogic.Attempts - printAttempts) + text[(int)Str.Attempts]);
                    break;
                }
                //Запускаем сито 
                set = Logic.Sieve(set, answer, result);
            } 

            EndGame(printAttempts, (int)Module.CPU);
        }
    }
}
