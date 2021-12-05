using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPO.LexicalAnalyzer
{
    /// <summary>
    /// Лексический анализатор.
    /// </summary>
    public class LexicalAnalyzer
    {
        /// <summary>
        /// Список лексем.
        /// </summary>
        public static IList<LexInfo> LexInfos { get; set; } = new List<LexInfo>();

        /// <summary>
        /// Текущая прочитанная последовательность.
        /// </summary>
        public static StringBuilder LexBuilder { get; set; } = new StringBuilder("");

        /// <summary>
        /// Текущий индекс чтения.
        /// </summary>
        public static int GlobalIndex { get; set; }

        /// <summary>
        /// Состояния КА.
        /// </summary>
        public IList<State> States { get; set; }

        /// <summary>
        /// Текущее состояние.
        /// </summary>
        public State CurrentState { get; set; }

        /// <summary>
        /// Начальное состояние.
        /// </summary>
        public State StartState { get; set; }

        /// <summary>
        /// Конечное состояние.
        /// </summary>
        public State FinishState { get; set; }

        public LexicalAnalyzer(IList<State> states)
        {
            States = states;
            StartState = CurrentState = states.First(f => f.Name == "S");
            FinishState = states.First(f => f.Name == "F");
        }

        /// <summary>
        /// Переход с записью символа перехода.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public static void Write(string ch)
        {
            LexBuilder.Append(ch);
        }

        /// <summary>
        /// Переход с сохранием, очищением и записью символа перехода.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public static void SaveResetWrite(string ch)
        {
            LexInfos.Add(new LexInfo(LexBuilder.ToString()));
            LexBuilder.Clear();
            LexBuilder.Append(ch);
        }

        /// <summary>
        /// Переход с записью, сохранием и очищением буффера.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public static void WriteSaveReset(string ch)
        {
            LexBuilder.Append(ch);
            LexInfos.Add(new LexInfo(LexBuilder.ToString()));
            LexBuilder.Clear();
        }

        /// <summary>
        /// Переход с сохранением лексемы и очищением буффера сохранения.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public static void SaveReset(string ch)
        {
            LexInfos.Add(new LexInfo(LexBuilder.ToString()));
            LexBuilder.Clear();
        }

        /// <summary>
        /// Переход с шагом назад.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public static void SaveResetStepBack(string ch)
        {
            LexInfos.Add(new LexInfo(LexBuilder.ToString()));
            LexBuilder.Clear();
            GlobalIndex--;
        }

        /// <summary>
        /// Переход без действий.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public static void None(string ch)
        {
        }

        /// <summary>
        /// Выделяет из строки лексемы языка.
        /// </summary>
        /// <param name="line">Входная строка.</param>
        /// <returns>Список лексем.</returns>
        public IList<LexInfo> AnalyzeLine(string line)
        {
            GlobalIndex = 0;

            for (GlobalIndex = 0; GlobalIndex < line.Length; GlobalIndex++)
            {
                var nextState = CurrentState.GetNextState(line[GlobalIndex].ToString());
                var action = CurrentState.GetTransactionAction(line[GlobalIndex].ToString());

                action(line[GlobalIndex].ToString());

                CurrentState = nextState;

                if (CurrentState == FinishState)
                {
                    CurrentState = StartState;
                }
            }

            return LexInfos;
        }

        /// <summary>
        /// Строит граф из состояний.
        /// </summary>
        /// <returns>Список состояний.</returns>
        public static IList<State> CreateSpoLabGraph()
        {
            State s = new State("S");

            State d1 = new State("D1");
            State d2 = new State("D2");

            State v = new State("V");

            State g = new State("G");

            State c = new State("C");

            State f = new State("F");

            State i1 = new State("I1");
            State i2 = new State("I2");

            State e1 = new State("E1");
            State e2 = new State("E2");
            State e3 = new State("E3");
            State e4 = new State("E4");

            State t1 = new State("T1");
            State t2 = new State("T2");
            State t3 = new State("T3");
            State t4 = new State("T4");

            //S
            s.AddTransaction(DfaSettings.Settings.Digits, d1, Write);
            s.AddTransaction(DfaSettings.Settings.Letters.Where(w => w != "i" && w != "e" && w != "t").ToList(), v, Write);
            s.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, Write);
            s.AddTransaction(DfaSettings.Settings.CommentsStartEnd.Where(w => w == "/").ToList(), c, None);

            s.AddTransaction(DfaSettings.Settings.CompOperators.ToList(), f, WriteSaveReset);

            s.AddTransaction(new List<string>{ DfaSettings.Settings.StatementEnd }, f, WriteSaveReset);

            s.AddTransaction(DfaSettings.Settings.LexEnds, f, None);

            s.AddTransaction(new List<string> {"e"}, e1, Write);
            s.AddTransaction(new List<string> {"i"}, i1, Write);
            s.AddTransaction(new List<string> {"t"}, t1, Write);


            //D1
            d1.AddTransaction(DfaSettings.Settings.Digits, d1, Write);
            d1.AddTransaction(DfaSettings.Settings.CommentsStartEnd.Where(w => w == "/").ToList(), c, SaveReset);
            d1.AddTransaction(DfaSettings.Settings.LexEnds.Where(w => w != ";").ToList(), f, SaveReset);

            //d1.AddTransaction(DfaSettings.Settings.LexEnds.Where(w => w == ";").ToList(), f, SaveReset);
            d1.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            d1.AddTransaction(new List<string> {DfaSettings.Settings.DigitDelimiter}, d2, Write);
            d1.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);

            //D2
            d2.AddTransaction(DfaSettings.Settings.Digits, d2, Write);
            d2.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            d2.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            d2.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            //V
            v.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters).ToList(), v, Write);
            v.AddTransaction(DfaSettings.Settings.CommentsStartEnd.Where(w => w == "/").ToList(), c, SaveReset);
            v.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            v.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            v.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            v.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            //G
            g.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == "=").ToList(), f, WriteSaveReset);

            //C
            c.AddTransaction(DfaSettings.Settings.Letters.Concat(new List<string>{" ", "\n", ";"}).ToList(), c, None);
            c.AddTransaction(DfaSettings.Settings.CommentsStartEnd.Where(w => w == "/").ToList(), f, None);

            //E1
            e1.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            e1.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "l")).ToList(), v, Write);
            e1.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            e1.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            e1.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            e1.AddTransaction(new List<string> {"l"}, e2, Write);

            //E2
            e2.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            e2.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "s")).ToList(), v, Write);
            e2.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            e2.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            e2.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            e2.AddTransaction(new List<string> {"s"}, e3, Write);

            //E3
            e3.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            e3.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "e")).ToList(), v, Write);
            e3.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            e3.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            e3.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            e3.AddTransaction(new List<string> {"e"}, e4, Write);

            //E4
            e4.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            e4.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters).ToList(), v, Write);
            e4.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            e4.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            e4.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);


            //I1
            i1.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            i1.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "f")).ToList(), v, Write);
            i1.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            i1.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            i1.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            i1.AddTransaction(new List<string> {"f"}, i2, Write);

            //I2
            i2.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            i2.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters).ToList(), v, Write);
            i2.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            i2.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            i2.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);

            //T1
            t1.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            t1.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "h")).ToList(), v, Write);
            t1.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            t1.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            t1.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            t1.AddTransaction(new List<string> {"h"}, t2, Write);


            //T2
            t2.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            t2.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "e")).ToList(), v, Write);
            t2.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            t2.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);
            t2.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);
            t2.AddTransaction(new List<string> {"e"}, t3, Write);


            //T3
            t3.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            t3.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters.Where(w => w != "n")).ToList(), v, Write);
            t3.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);
            t3.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            t3.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            t3.AddTransaction(new List<string> {"n"}, t4, Write);

            //T4
            t4.AddTransaction(DfaSettings.Settings.AssignSymbols.Where(w => w == ":").ToList(), g, SaveResetWrite);
            t4.AddTransaction(DfaSettings.Settings.Digits.Concat(DfaSettings.Settings.Letters).ToList(), v, Write);
            t4.AddTransaction(DfaSettings.Settings.LexEnds, f, SaveReset);

            t4.AddTransaction(new List<string> { DfaSettings.Settings.StatementEnd }, f, SaveResetStepBack);

            t4.AddTransaction(DfaSettings.Settings.CompOperators, f, SaveResetStepBack);

            IList<State> states = new List<State> { s, d1, d2, v, g, c, f, i1, i2, e1, e2, e3, e4, t1, t2, t3, t4 };

            return states;
        }
    }
}
