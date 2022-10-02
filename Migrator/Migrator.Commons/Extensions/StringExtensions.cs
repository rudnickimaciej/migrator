using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Commons.Extensions
{
    internal static class StringExtensions
    { 
        public static string Clean(this string s)
        {
            return s.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' ');
        }
    }
}
