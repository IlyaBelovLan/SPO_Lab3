using System.Collections.Generic;
using System.Linq;

namespace SPO.LexicalAnalyzer
{
    public class DfaSettings
    {
        public static DfaSettings Settings = new DfaSettings();

        private IList<string> _letters = new List<string>();
        private IList<string> _digits = new List<string>();
        private string _digitDelimiter;
        private IList<string> _commentsStartEnd = new List<string>();
        private IList<string> _compOperators = new List<string>();
        private IList<string> _lexEnds = new List<string>();
        private IList<string> _assignSymbols = new List<string>();
        private IList<string> _keywords = new List<string>();

        /// <summary>
        /// Список буквенных символов.
        /// </summary>
        public IList<string> Letters
        {
            get => _letters.ToList();
        }

        /// <summary>
        /// Список цифр.
        /// </summary>
        public IList<string> Digits
        {
            get => _digits.ToList();
        }

        /// <summary>
        /// Разделитель десятичной и дробной части в числах.
        /// </summary>
        public string DigitDelimiter
        {
            get => _digitDelimiter;
        }

        /// <summary>
        /// Символы начала и конца комментария.
        /// </summary>
        public IList<string> CommentsStartEnd
        {
            get => _commentsStartEnd.ToList();
        }

        /// <summary>
        /// Операторы сравнения.
        /// </summary>
        public IList<string> CompOperators
        {
            get => _compOperators.ToList();
        }

        /// <summary>
        /// Символы конца лексемы.
        /// </summary>
        public IList<string> LexEnds
        {
            get => _lexEnds.ToList();
        }

        /// <summary>
        /// Символы для присваиванияя.
        /// </summary>
        public IList<string> AssignSymbols
        {
            get => _assignSymbols.ToList();
        }

        /// <summary>
        /// Ключевые слова.
        /// </summary>
        public IList<string> Keywords
        {
            get => _keywords.ToList();
        }


        public static IList<string> TransitionSymbols = new List<string>();

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
            _lexEnds.Add(";");

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
