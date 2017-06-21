using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulls_and_Cows
{
    class HumanAnswer: Answer
    {
        enum Str
        {
           Start = 8,
           Attempts,
           YouWin,
           CPUWin,
           ErrorInput,
           Continue,
           Yes,
           No
        }
        //Вызов конструктора базового класса
        public HumanAnswer(string[] text): base (text){}
        public void Start()
        {
            Switcher();
            Rules();
            //Устанавливаем последовательность для отгадывания
            Sequence = Logic.SetSequence();
            GameInProgress(Sequence, PrintAttempts);
        }
        private void GameInProgress(string sequence, int printAttempts)
        {
            output.PrintLine(text[(int)Str.Start] + text[(int)Str.Attempts] + printAttempts);

            # if DEBUG
           // Показываем последоавтельность для проверки
            output.PrintLine(sequence);
            #endif

            for (var i = 0; i < GameLogic.Attempts; i++)
            {
                var answer = input.ReadLine((int)Module.Human);
                if (answer != error)
                {
                    printAttempts--;
                    if (Logic.CheckAnswer(sequence, answer) != rightAnswer)
                    {
                        if (printAttempts == 0 && answer != sequence)
                            output.PrintLine(text[(int)Str.CPUWin]);
                        else
                            output.PrintLine(Logic.CheckAnswer(sequence, answer) + text[(int)Str.Attempts] + printAttempts);
                    }
                    else
                    {
                        output.PrintLine(text[(int)Str.YouWin]+ text[(int)Str.Attempts] + (GameLogic.Attempts-printAttempts));
                        break;
                    }
                }
                else
                {
                    output.PrintLine(text[(int)Str.ErrorInput]);
                    i--;
                }
            }
            EndGame(printAttempts, (int)Module.Human);
        }
    }
}
