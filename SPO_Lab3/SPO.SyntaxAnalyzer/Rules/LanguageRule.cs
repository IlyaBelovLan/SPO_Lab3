using System.Collections.Generic;
using SPO.SyntaxAnalyzer.Symbols;

namespace SPO.SyntaxAnalyzer.Rules
{
    /// <summary>
    /// Правило преобразования.
    /// </summary>
    public class LanguageRule
    {
        /// <summary>
        /// Левая часть правила.
        /// </summary>
        public string LeftPart { get; set; }

        /// <summary>
        /// Правая часть правила.
        /// </summary>
        public string RightPart { get; set; }

        /// <summary>
        /// Символы правила.
        /// </summary>
        public IList<Symbol> Symbols { get; set; }

        public LanguageRule(string leftPart, string rightPart, IList<Symbol> symbols)
        {
            LeftPart = leftPart;
            RightPart = rightPart;
            Symbols = symbols;
        }
    }
}
