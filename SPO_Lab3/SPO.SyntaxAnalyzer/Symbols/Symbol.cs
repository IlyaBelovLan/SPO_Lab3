namespace SPO.SyntaxAnalyzer.Symbols
{
    /// <summary>
    /// Символ кодовой цепочки.
    /// </summary>
    public class Symbol
    {
        /// <summary>
        /// Значение символа.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Тип символа.
        /// </summary>
        public SymbolType SymbolType { get; set; }

        public Symbol(){}

        public Symbol(string value, SymbolType type)
        {
            Value = value;
            SymbolType = type;
        }
    }
}
