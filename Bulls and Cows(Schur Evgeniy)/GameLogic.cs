using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulls_and_Cows
{
    class GameLogic
    {
        public const int Attempts = 10;
        public const int SeqenceLength = 4;
        int _firstNumSeq = 0;

        public string SetSequence() => RangeOfValues()[new Random().Next(RangeOfValues().Count-1)];

        public List<string> RangeOfValues()
        {
            var set = new List<string>(Enumerable.Range(_firstNumSeq, OrederOFMagnitude()).Select(c => $"{c:D4}".ToString()));
            for (var i = 0; i < set.Count; i++)
            {
                var tmp = new HashSet<char>(set[i]);
                if (tmp.Count < SeqenceLength)
                {
                    set.Remove(set[i]);
                    i--;
                }
            }
            return set;
        }
        public string CheckAnswer(string sequence, string answer)
        {
            var bullCounter = default(int);
            var cowCounter = default(int);
            for (var i = 0; i < sequence.Length; i++)
            {
                if (sequence.Contains(answer[i]))
                {
                    if (sequence[i] == answer[i]) bullCounter++; else cowCounter++;
                }
             }
                return $"{bullCounter}b{cowCounter}c";
        }
        /// <summary>
        /// Проходимся по множеству допустимых значений и удаляем все те, которые дают с нашим ответом не такие оценки
        /// </summary>
        /// <param name="set">Множество значений</param>
        /// <param name="answer">Ответ, выбранный из множества значений</param>
        /// <param name="result">Оценка, полученная сопоставлением референсного значения с ответом</param>
        public List<string> Sieve(List<string> set, string answer, string result)
        {
            for (var i = 0; i < set.Count; i++)
            {
                var checkResult = CheckAnswer(set[i], answer);
                if (checkResult != result)
                {
                    set.Remove(set[i]);
                    i--;
                }
            }
            return new List<string>(set);
        }

        public bool Errors(string answer, int module)
        {
            if (module == 2)
                return false;

            if (answer.Length == SeqenceLength)
            {
                Func<string, bool> sequence = (a) =>
                 {
                     var unicChars = new HashSet<char>(a);
                     var counter = default(int);
                     foreach (var item in a)
                     {
                         if (Char.IsDigit(item))
                             counter++;
                     }
                     if (counter == SeqenceLength && unicChars.Count == SeqenceLength) return false;
                     else return true;
                 };

                Func<string, bool> evaluation = (a) =>
                {
                    int[] chekValue = new int[2];
                    if (!(Char.IsDigit(a[0]))) return true;
                    else if (int.Parse(answer[0].ToString()) > SeqenceLength || int.Parse(answer[0].ToString())< default(int)) return true;
                    if (a[1] != 'b') return true;
                    if (!(Char.IsDigit(a[2]))) return true;
                    else if (int.Parse(a[2].ToString()) > SeqenceLength || int.Parse(a[2].ToString()) < default(int)) return true;
                    if (a[3] != 'c') return true;
                   
                    return false;
                };
                if (module == 0)
                    return sequence(answer);
                else if (module == 1)
                    return evaluation(answer);
                else return true;
            }
            else return true;
        }

        int OrederOFMagnitude()
        {
            string maxValue = "1";
            for (var i = 0; i < SeqenceLength; i++)
            {
                 maxValue += "0";
            }
            return Int32.Parse(maxValue);
        }
    }
}
