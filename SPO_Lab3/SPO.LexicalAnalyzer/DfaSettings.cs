using System.Collections.Generic;
using System.Linq;

namespace SPO.LexicalAnalyzer
{
    public class DfaSettings
    {
        /// <summary>
        /// Объект настроек.
        /// </summary>
        public static DfaSettings Settings = new DfaSettings();

        private readonly IList<string> _letters = new List<string>();
        private readonly IList<string> _digits = new List<string>();
        private readonly string _digitDelimiter;
        private readonly IList<string> _commentsStartEnd = new List<string>();
        private readonly IList<string> _compOperators = new List<string>();
        private readonly IList<string> _lexEnds = new List<string>();
        private readonly IList<string> _assignSymbols = new List<string>();
        private readonly IList<string> _keywords = new List<string>();
        private readonly string _statementEnd;

        /// <summary>
        /// Список буквенных символов.
        /// </summary>
        public IList<string> Letters => _letters.ToList();

        /// <summary>
        /// Список цифр.
        /// </summary>
        public IList<string> Digits => _digits.ToList();

        /// <summary>
        /// Разделитель десятичной и дробной части в числах.
        /// </summary>
        public string DigitDelimiter => _digitDelimiter;

        /// <summary>
        /// Символы начала и конца комментария.
        /// </summary>
        public IList<string> CommentsStartEnd => _commentsStartEnd.ToList();

        /// <summary>
        /// Операторы сравнения.
        /// </summary>
        public IList<string> CompOperators => _compOperators.ToList();

        /// <summary>
        /// Символы конца лексемы.
        /// </summary>
        public IList<string> LexEnds => _lexEnds.ToList();

        /// <summary>
        /// Символы для присваиванияя.
        /// </summary>
        public IList<string> AssignSymbols => _assignSymbols.ToList();

        /// <summary>
        /// Ключевые слова.
        /// </summary>
        public IList<string> Keywords => _keywords.ToList();

        /// <summary>
        /// Символ конца выражения.
        /// </summary>
        public string StatementEnd => _statementEnd;

        /// <summary>
        /// Конструктор настроек.
        /// </summary>
        public DfaSettings()
        {
            //Инициализация букв.
            for (char ch = 'A'; ch <= 'Z'; ch++) { _letters.Add(ch.ToString()); }
            for (char ch = 'a'; ch <= 'z'; ch++) { _letters.Add(ch.ToString()); }
            _letters.Add("_");


            //Инициализация цифр.
            for(char ch = '0'; ch <= '9'; ch++) { _digits.Add(ch.ToString()); }

            //Инициализация числового разделителя.
            _digitDelimiter = ".";

            //Символы начала и конца комментария.
            _commentsStartEnd.Add("/");
            _commentsStartEnd.Add("/");

            //Операторы сравнения.
            _compOperators.Add("<");
            _compOperators.Add(">");
            _compOperators.Add("=");

            //Символы конца лексемы.
            _lexEnds.Add(" ");
            _lexEnds.Add("\n");

            //Символ конца выражения.
            _statementEnd = ";";

            //Символы присваивания.
            _assignSymbols.Add(":");
            _assignSymbols.Add("=");

            //Ключевые слова.
            _keywords.Add("if");
            _keywords.Add("then");
            _keywords.Add("else");
        }
    }
}
