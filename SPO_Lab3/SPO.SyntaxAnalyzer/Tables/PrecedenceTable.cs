using System.Collections.Generic;

namespace SPO.SyntaxAnalyzer.Tables
{
    /// <summary>
    /// Таблица предшествования.
    /// </summary>
    public class PrecedenceTable : IPrecedenceTable
    {
        /// <summary>
        /// Список терминальных символов.
        /// </summary>
        private IList<string> ColumnTerminals { get; set; }

        /// <summary>
        /// Список терминальных символов.
        /// </summary>
        private IList<string> RowTerminals { get; set; }

        /// <summary>
        /// Отношения терминальных символов.
        /// </summary>
        private IList<IList<char>> Relations { get; set; }

        public PrecedenceTable(IList<string> columnTerminals, IList<string> rowTerminals, IList<IList<char>> relations)
        {
            ColumnTerminals = columnTerminals;
            RowTerminals = rowTerminals;
            Relations = relations;
        }

        /// <inheritdoc />
        public string GetRelation(string row, string column)
        {
            var rowIndex = RowTerminals.IndexOf(row);
            var columnIndex = ColumnTerminals.IndexOf(column);
            return Relations[rowIndex][columnIndex].ToString();
        }
    }
}
