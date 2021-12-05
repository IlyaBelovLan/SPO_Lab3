using System;
using System.Collections.Generic;
using System.Linq;
using SPO.LexicalAnalyzer;
using SPO.SyntaxAnalyzer.Extensions;
using SPO.SyntaxAnalyzer.Rules;
using SPO.SyntaxAnalyzer.Symbols;
using SPO.SyntaxAnalyzer.Tables;

namespace SPO.SyntaxAnalyzer.SyntaxAnalyzer
{
    /// <summary>
    /// Синтаксичиеский анализатор.
    /// </summary>
    public class SyntaxAnalyzer
    {
        /// <summary>
        /// Заменитель для констант, переменных и т.п.
        /// </summary>
        private string LexSubstitute { get; set; }

        /// <summary>
        /// Символ начала строки.
        /// </summary>
        private string StartSymbol { get; set; }

        /// <summary>
        /// Символ конца строки.
        /// </summary>
        private string EndSymbol { get; set; }

        /// <summary>
        /// Таблица предшествования.
        /// </summary>
        private IPrecedenceTable PrecedenceTable { get; set; }

        /// <summary>
        /// Остовные правила языка.
        /// </summary>
        private IList<LanguageRule> Rules { get; set; }

        public SyntaxAnalyzer(
            IList<LanguageRule> rules,
            IList<string> columnTerminals, 
            IList<string> rowTerminals,
            IList<IList<char>> relations, 
            string lexSubstitute, 
            string startSymbol, 
            string endSymbol)
        {
            Rules = rules;

            PrecedenceTable = new PrecedenceTable(columnTerminals, rowTerminals, relations);

            LexSubstitute = lexSubstitute;
            StartSymbol = startSymbol;
            EndSymbol = endSymbol;
        }

        public IList<LanguageRule> Analyze(IList<LexInfo> lexList)
        {
            var sequence = ProcessLexInfo(lexList);
            sequence.Add(new Symbol(";", SymbolType.Terminal));
            sequence.Add(new Symbol { SymbolType = SymbolType.Terminal, Value = EndSymbol });


            var outputRules = new List<LanguageRule>();

            //1
            IList<Symbol> pseudoStack = new List<Symbol> {new Symbol {SymbolType = SymbolType.Terminal, Value = StartSymbol}};

            for (int i = 0; i < sequence.Count; i++)
            {
                //2
                var stackSymbol = GetTerminal(pseudoStack);
                var currentSymbol = sequence[i];

                //3
                if (stackSymbol.Value == StartSymbol && currentSymbol.Value == EndSymbol)
                    return outputRules;

                //4
                var relation = PrecedenceTable.GetRelation(stackSymbol.Value, currentSymbol.Value);

                //5
                if (relation == " ")
                    throw new Exception("Введенный код не принимается синтаксическим анализатором!");

                //6
                if (relation == "=" || relation == "<")
                {
                    pseudoStack.Add(currentSymbol);
                }
                else
                {
                    i--;

                    //7
                    var rollingSequence = string.Join(" ", RollUp(pseudoStack).Select(s => s.Value));

                    //8
                    var rule = Rules.FirstOrDefault(f => f.RightPart == rollingSequence);
                    if(rule == null)
                        throw new Exception("Введенный код не принимается синтаксическим анализатором!");

                    pseudoStack.Add(new Symbol { SymbolType = SymbolType.NonTerminal, Value = rule.LeftPart});

                    outputRules.Add(rule);
                }
            }

            throw new Exception("Введенный код не принимается синтаксическим анализатором!");
        }


        private IList<Symbol> RollUp(IList<Symbol> stack)
        {
            var upSymbol = GetTerminal(stack);
            var upSymbolIndex = stack.IndexOf(upSymbol);

            var resultSequence = new List<Symbol> { upSymbol };

            if (stack.ExistsPlaceByRight(upSymbol))
            {
                resultSequence.Insert(1, stack[upSymbolIndex + 1]);
            }


            for (int i = upSymbolIndex - 1; i >= 0; i--)
            {
                if (stack[i].SymbolType == SymbolType.Terminal)
                {
                    var relation = PrecedenceTable.GetRelation(stack[i].Value, upSymbol.Value);
                    if (relation == "=")
                    {
                        resultSequence.Insert(0, stack[i]);
                        upSymbol = stack[i];
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    resultSequence.Insert(0, stack[i]);
                }
            }

            stack.RemoveSince(stack.Count - resultSequence.Count);

            return resultSequence;
        }

        /// <summary>
        /// Возращает терминальный символ с вершины стека.
        /// </summary>
        /// <param name="symbols">Стек символов.</param>
        /// <returns>Терминальный символ.</returns>
        private static Symbol GetTerminal(IList<Symbol> symbols) => symbols.Last(l => l.SymbolType == SymbolType.Terminal);
        

        /// <summary>
        /// Преобразует список информации о лексемах в список символов.
        /// </summary>
        /// <param name="lexList">Список информации о лексемах.</param>
        /// <returns>Список символов.</returns>
        private IList<Symbol> ProcessLexInfo(IList<LexInfo> lexList) => lexList.Select(s => new Symbol
            {
                SymbolType = SymbolType.Terminal,
                Value = s.LexType == LexType.Constant || s.LexType == LexType.Variable ? LexSubstitute : s.Value
            })
            .ToList();
    }
}
