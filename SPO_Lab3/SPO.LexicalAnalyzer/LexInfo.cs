namespace SPO.LexicalAnalyzer
{
    /// <summary>
    /// Информация о лексеме.
    /// </summary>
    public class LexInfo
    {
        /// <summary>
        /// Тип лексемы.
        /// </summary>
        public LexType LexType { get; set; }

        /// <summary>
        /// Значение лексемы из текста.
        /// </summary>
        public string Value { get; set; }

        public LexInfo(string value)
        {
            Value = value;
            LexType = DefineLexType(value);
        }

        /// <summary>
        /// Определяет тип лексемы.
        /// </summary>
        /// <param name="lex">Строковое значение лексемы.</param>
        /// <returns>Тип лексемы.</returns>
        private static LexType DefineLexType(string lex)
        {
            if (DfaSettings.Settings.Keywords.Contains(lex) || lex == DfaSettings.Settings.StatementEnd) return LexType.Keyword;

            if (DfaSettings.Settings.Letters.Contains(lex[0].ToString())) return LexType.Variable;

            if (DfaSettings.Settings.Digits.Contains(lex[0].ToString())) return LexType.Constant;

            if (DfaSettings.Settings.CompOperators.Contains(lex[0].ToString()) || lex.Contains(":=")) return LexType.Operator;

            return LexType.Unknown;
        }
    }
}
