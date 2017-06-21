using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulls_and_Cows
{
    class GameManager
    {
        enum Str
        {
            Welcome,
            Select,
            ErrorInput,
            HumanWin,
            CPUWin,
            Draw,
        }

        
        static Text texts = new Text();
        static HumanAnswer humAnswer = new HumanAnswer(texts.Human);
        static ComputerAnswer cpuAnswer = new ComputerAnswer(texts.CPU);
        static string[] text = texts.GameManager;
        static IReadLine input = new InputOutput();
        static IPrintLine output = new InputOutput();

        public static int AttempsToWinHuman { get; set; }
        public static int AttemptsToWinCPU { get; set; }
        public static int SwitchSide { get; set; }
        public static void GameCycle()
        {
            Dialogue();
        }
        private static void Dialogue()
        {
            SwitchSide = default(int);
            output.PrintLine(text[(int)Str.Welcome] + "\n\n" + text[(int)Str.Select]);
            for (var i = 0; i < int.MaxValue; i++)
            {
                var answer = input.ReadLine((int)Module.GameManager);
                if (answer == "1")
                {
                    HumanStart();
                    break;
                }
                if (answer == "2")
                {
                    CPUStart();
                    break;
                }
                else output.PrintLine(text[(int)Str.ErrorInput]); continue;
            }          
        }
        public static void SetWinner()
        {
            Console.Clear();
            output.PrintLine(text[CompareAttempts()]);
            input.ReadLine((int)Module.GameManager);
            Console.Clear();
            Dialogue();
        }

       static int CompareAttempts()
        {
            if (AttempsToWinHuman < AttemptsToWinCPU) return (int)Str.HumanWin;
            if (AttempsToWinHuman > AttemptsToWinCPU) return (int)Str.CPUWin;
            if (AttempsToWinHuman == AttemptsToWinCPU) return (int)Str.Draw;

            return (int)Str.ErrorInput;
        }

        public static void HumanStart() { Console.Clear(); humAnswer.Start(); }
        public static void CPUStart()  { Console.Clear(); cpuAnswer.Start(); }
       
    }
}
