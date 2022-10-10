using System;

namespace Migrator.Commons.Logger
{
    public interface ILogger
    {
      void Log(string query);
        void Log(string query, ConsoleColor background, ConsoleColor foreground );
    }
}
