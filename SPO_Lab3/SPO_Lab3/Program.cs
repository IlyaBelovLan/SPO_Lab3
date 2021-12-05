using System;
using SPO.LexicalAnalyzer;
using SPO.SyntaxAnalyzer;
using SPO.SyntaxAnalyzer.Trees;

namespace SPO_Lab3
{
    class Program
    {
        static void Main()
        {
            string input = "/Test code string/if min > max then max := min else if b > c then min := max else max := min;";

            var states = LexicalAnalyzer.CreateSpoLabGraph();
            LexicalAnalyzer analyzer = new LexicalAnalyzer(states);

            try
            {
                Console.WriteLine("Все готово для анализа.");
                Console.WriteLine("Нажмите enter для лексического анализа...");
                Console.Read();

                var lexList = analyzer.AnalyzeLine(input);
                foreach (var lex in lexList)
                {
                    Console.WriteLine(lex.LexType + " " + lex.Value);
                }
                Console.WriteLine();

                //-------------------

                Console.WriteLine("Нажмите enter для синтаксического анализа...");
                Console.Read();
                Console.Read();

                var syntaxAnalyzer = new SyntaxAnalyzer(
                    SyntaxAnalyzerSettings.Rules, 
                    SyntaxAnalyzerSettings.ColumnTerminals,
                    SyntaxAnalyzerSettings.RowTerminals,
                    SyntaxAnalyzerSettings.Relations,
                    SyntaxAnalyzerSettings.LexSubstitute,
                    SyntaxAnalyzerSettings.StartSymbol,
                    SyntaxAnalyzerSettings.EndSymbol);

                var outputRules = syntaxAnalyzer.Analyze(lexList);

                var outputTree = new OutputTree(outputRules);

                Console.WriteLine("Дерево вывода для представленной кодовой строки:");
                outputTree.PrintTree();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сообщение об ошибке: " + ex.Message);
            }
        }
    }
}
