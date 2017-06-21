using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulls_and_Cows
{
    class Answer
    {
        enum Str
        {
            Continue = 13,
            Yes,
            No
        }
        protected GameLogic Logic = new GameLogic();
        protected string[] text;
        protected IReadLine input = new InputOutput();
        protected IPrintLine output = new InputOutput();
        protected const string error = "Error";
        protected string rightAnswer;
        /// <summary>
        /// Счетчик попыток, показывается игроку
        /// </summary>
        protected int PrintAttempts { get; set; }
        /// <summary>
        /// Устанавливаем последовательность для отгадывания
        /// </summary>
        protected string Sequence { get; set; }
       
        public Answer(string[] textForModule)
        {
            PrintAttempts = GameLogic.Attempts;
            text = textForModule;
            rightAnswer = GameLogic.SeqenceLength.ToString() + "b0c";
        }
        /// <summary>
        /// Процедура, выводящая правила игры
        /// </summary>
        protected void Rules()
        {
            for (var i = 0; i < 8; i++)
            {
                output.PrintLine(text[i]);
            }
        }
        /// <summary>
        /// Процедура, отвечающая за смену стороны
        /// </summary>
        protected void Switcher()
        {
            GameManager.SwitchSide++;
            if (GameManager.SwitchSide == 3) GameManager.SetWinner();
        }
        /// <summary>
        /// Процедура, запускающая финальный диалог
        /// </summary>
        /// <param name="printAttempts"></param>
        /// <param name="module"></param>
        protected void EndGame(int printAttempts,int module)
        {
            output.PrintLine(text[(int)Str.Continue] + text[(int)Str.Yes] + text[(int)Str.No]);
            if(module == (int)Module.Human)
            {
                GameManager.AttempsToWinHuman = GameLogic.Attempts - printAttempts;
                if (input.ReadLine((int)Module.GameManager) == "1")
                    GameManager.CPUStart();
            }
            if (module == (int)Module.CPU)
            {
                GameManager.AttemptsToWinCPU = GameLogic.Attempts - printAttempts;
                if (input.ReadLine((int)Module.GameManager) == "1")
                    GameManager.HumanStart();
            }

        }

    }
}
