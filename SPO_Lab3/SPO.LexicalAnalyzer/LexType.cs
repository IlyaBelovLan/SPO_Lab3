namespace SPO.LexicalAnalyzer
{
    /// <summary>
    /// Тип лексемы.
    /// </summary>
    public enum LexType
    {
        /// <summary>
        /// Неизвестная лексема.
        /// </summary>
        Unknown,

        /// <summary>
        /// Ключевое слово.
        /// </summary>
        Keyword,

        /// <summary>
        /// Константа.
        /// </summary>
        Constant,

        /// <summary>
        /// Переменная.
        /// </summary>
        Variable,

        /// <summary>
        /// Оператор.
        /// </summary>
        Operator
    }
}
