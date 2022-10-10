using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.IO;

namespace Migrator.Commons.Logger
{
    public class TSqlLogger: ILogger
    {
        public void Log(string query)
        {
            var parser = new TSql120Parser(false);
            IList<ParseError> errors;
            var parsedQuery = parser.Parse(new StringReader(query), out errors);
            if (errors.Count > 0)
            {
                foreach (var err in errors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(err.Message);
                    Console.ResetColor();
                }
            }
            var generator = new Sql120ScriptGenerator(new SqlScriptGeneratorOptions()
            {
                KeywordCasing = KeywordCasing.Uppercase,
                IncludeSemicolons = true,
                NewLineBeforeFromClause = true,
                NewLineBeforeOrderByClause = true,
                NewLineBeforeWhereClause = true,
                AlignClauseBodies = false
            });
            string formattedQuery;
            generator.GenerateScript(parsedQuery, out formattedQuery);
            Console.WriteLine(formattedQuery);
        }

        public void Log(string query, ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Log(query);
            Console.ResetColor();
        }
    }
}
