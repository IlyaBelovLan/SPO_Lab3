using System.Collections.Generic;
using SPO.SyntaxAnalyzer.Rules;
using SPO.SyntaxAnalyzer.Symbols;

namespace SPO.SyntaxAnalyzer.SyntaxAnalyzer
{
    /// <summary>
    /// Настройки для синтаксического анализатора.
    /// </summary>
    public static class SyntaxAnalyzerSettings
    {
        /// <summary>
        /// Символ начала строки.
        /// </summary>
        public static string StartSymbol => "Н";

        /// <summary>
        /// Символ конца строки.
        /// </summary>
        public static string EndSymbol => "К";

        /// <summary>
        /// Символ замены лексем.
        /// </summary>
        public static string LexSubstitute => "a";

        /// <summary>
        /// Правила языка.
        /// </summary>
        public static IList<LanguageRule> Rules = new List<LanguageRule>
        {
            new LanguageRule("S", "S ;", new List<Symbol> { new Symbol("S", SymbolType.NonTerminal), new Symbol("S", SymbolType.NonTerminal), new Symbol(";", SymbolType.Terminal)}),
            new LanguageRule("S", "if S then S else S", new List<Symbol>
            {
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("if", SymbolType.Terminal),
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("then", SymbolType.Terminal),
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("else", SymbolType.Terminal),
                new Symbol("S", SymbolType.NonTerminal)
            }),
            new LanguageRule("S", "if S then S", new List<Symbol>
            {
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("if", SymbolType.Terminal),
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("then", SymbolType.Terminal),
                new Symbol("S", SymbolType.NonTerminal)
            }),
            new LanguageRule("S", "a := a", new List<Symbol>
            {
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("a", SymbolType.Terminal),
                new Symbol(":=", SymbolType.Terminal),
                new Symbol("a", SymbolType.Terminal)
            }),
            new LanguageRule("S", "a < a", new List<Symbol>
            {
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("a", SymbolType.Terminal),
                new Symbol("<", SymbolType.Terminal),
                new Symbol("a", SymbolType.Terminal)
            }),
            new LanguageRule("S", "a > a", new List<Symbol>
            {
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("a", SymbolType.Terminal),
                new Symbol(">", SymbolType.Terminal),
                new Symbol("a", SymbolType.Terminal)
            }),
            new LanguageRule("S", "a = a", new List<Symbol>
            {
                new Symbol("S", SymbolType.NonTerminal),
                new Symbol("a", SymbolType.Terminal),
                new Symbol("=", SymbolType.Terminal),
                new Symbol("a", SymbolType.Terminal)
            })
        };

        /// <summary>
        /// Символы для таблицы предшествования.
        /// </summary>
        public static IList<string> ColumnTerminals = new List<string>
        {
            ";", "if", "then", "else", "a", ":=", "<", ">", "=", EndSymbol
        };

        /// <summary>
        /// Символы для таблицы предшествования.
        /// </summary>
        public static IList<string> RowTerminals = new List<string>
        {
            ";", "if", "then", "else", "a", ":=", "<", ">", "=", StartSymbol
        };

        /// <summary>
        /// Таблица предшествования.
        /// </summary>
        public static IList<IList<char>> Relations = new List<IList<char>>
        {
            new List<char> { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '>' },
            new List<char> { ' ', ' ', '=', ' ', '<', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { '>', '<' /*'>'*/, ' ', '=', '<', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { '>', '<', ' ', '>', '<', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { '>', ' ', '>', '>', ' ', '=', '=', '=', '=', ' ' },
            new List<char> { ' ', ' ', ' ', ' ', '=', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { ' ', ' ', ' ', ' ', '=', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { ' ', ' ', ' ', ' ', '=', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { ' ', ' ', ' ', ' ', '=', ' ', ' ', ' ', ' ', ' ' },
            new List<char> { '<', '<', ' ', ' ', '<', ' ', ' ', ' ', ' ', ' ' }
        };
    }
}
