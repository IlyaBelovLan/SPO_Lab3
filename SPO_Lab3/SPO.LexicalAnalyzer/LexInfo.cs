namespace SPO.LexicalAnalyzer
{
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


        private static LexType DefineLexType(string lex)
        {
            if (DfaSettings.Settings.Keywords.Contains(lex)) return LexType.Keyword;

            if (DfaSettings.Settings.Letters.Contains(lex[0].ToString())) return LexType.Variable;

            if (DfaSettings.Settings.Digits.Contains(lex[0].ToString())) return LexType.Constant;

            if (DfaSettings.Settings.CompOperators.Contains(lex[0].ToString()) || lex.Contains(":=")) return LexType.Operator;

            return LexType.Unknown;
        }
    }
}
