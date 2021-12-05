namespace SPO.SyntaxAnalyzer.Tables
{
    /// <summary>
    /// Интерфейс таблицы предшествования.
    /// </summary>
    public interface IPrecedenceTable
    {
        /// <summary>
        /// Возвращает отношение между двумя символами.
        /// </summary>
        /// <param name="row">Символ строки.</param>
        /// <param name="column">Символ столбца.</param>
        /// <returns>Отношение.</returns>
        public string GetRelation(string row, string column);
    }
}
