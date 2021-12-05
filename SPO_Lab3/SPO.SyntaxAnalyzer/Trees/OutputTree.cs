using System;
using System.Collections.Generic;
using System.Linq;
using SPO.SyntaxAnalyzer.Rules;
using SPO.SyntaxAnalyzer.Symbols;

namespace SPO.SyntaxAnalyzer.Trees
{
    /// <summary>
    /// Дерево вывода.
    /// </summary>
    public class OutputTree
    {
        /// <summary>
        /// Корневой символ.
        /// </summary>
        public Symbol RootSymbol { get; set; }

        /// <summary>
        /// Дочерние узлы.
        /// </summary>
        public IList<OutputTree> Childs { get; set; } = new List<OutputTree>();

        public OutputTree(Symbol rootSymbol)
        {
            RootSymbol = rootSymbol;
        }

        public OutputTree(IList<LanguageRule> rules)
        {
            BuildTree(rules);
        }

        /// <summary>
        /// Выводит дерево.
        /// </summary>
        public void PrintTree()
        {
            Console.WriteLine(RootSymbol.Value);
            PrintChilds();
        }

        /// <summary>
        /// Выводит все дочерние узлы.
        /// </summary>
        /// <param name="deep"></param>
        private void PrintChilds(int deep = 1)
        {
            string tabulate = "";
            string link = "------";

            for (int i = 1; i <= deep - 1; i++) tabulate += "\t";

            foreach (var child in Childs)
            {
                Console.WriteLine(tabulate + "|" + link + child.RootSymbol.Value + "\n");
                child.PrintChilds(deep + 1);
            }
        }

        /// <summary>
        /// Строит дерево.
        /// </summary>
        /// <param name="rules">Правила.</param>
        private void BuildTree(IList<LanguageRule> rules)
        {
            DecomposeRule(rules.Last());
            rules.RemoveAt(rules.Count - 1);

            for (int i = Childs.Count - 1; i >= 0; i--)
            {
                if (Childs[i].RootSymbol.SymbolType == SymbolType.NonTerminal)
                {
                    Childs[i].BuildTree(rules);
                }
            }

        }

        /// <summary>
        /// Раскалдывает правила по дочерним узлам.
        /// </summary>
        /// <param name="rule">Правило.</param>
        private void DecomposeRule(LanguageRule rule)
        {
            RootSymbol = rule.Symbols[0];

            for (int i = 1; i < rule.Symbols.Count; i++)
            {
                Childs.Add(new OutputTree(rule.Symbols[i]));
            }
        }
    }
}
