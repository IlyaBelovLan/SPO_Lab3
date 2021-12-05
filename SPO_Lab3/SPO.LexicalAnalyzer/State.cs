using System;
using System.Collections.Generic;

namespace SPO.LexicalAnalyzer
{
    /// <summary>
    /// Состояние.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Делегат функции перехода.
        /// </summary>
        /// <param name="ch">Символ перехода.</param>
        public delegate void TransactionAction(string ch);

        /// <summary>
        /// Имя состояния.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список переходов.
        /// </summary>
        private IList<IList<string>> Trans { get; }
        
        /// <summary>
        /// Список состояний.
        /// </summary>
        private IList<State> States { get; }
        
        /// <summary>
        /// Список функций.
        /// </summary>
        private IList<TransactionAction> Actions { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя состояния.</param>
        public State(string name)
        {
            Name = name;

            Trans = new List<IList<string>>();
            States = new List<State>();
            Actions = new List<TransactionAction>();
        }

        /// <summary>
        /// Добавляет переход.
        /// </summary>
        public void AddTransaction(IList<string> trans, State state, TransactionAction action)
        {
            Trans.Add(trans);
            States.Add(state);
            Actions.Add(action);
        }

        /// <summary>
        /// Возвращает следующее состояние.
        /// </summary>
        public State GetNextState(string ch)
        {
            for (int i = 0; i < Trans.Count; i++)
            {
                if (Trans[i].Contains(ch))
                {
                    return States[i];
                }
            }

            throw new Exception($"Неизвестная лексема!");
        }

        /// <summary>
        /// Возвращает следующее состояние.
        /// </summary>
        public TransactionAction GetTransactionAction(string ch)
        {
            for (int i = 0; i < Trans.Count; i++)
            {
                if (Trans[i].Contains(ch))
                {
                    return Actions[i];
                }
            }

            throw new Exception($"Неизвестная лексема!");
        }
    }
}
