using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;

namespace Contoso.Contexts
{
    public static class BaseDbContextSqlFunctions
    {
        public static string FormatDateTime(DateTime value, string format, string culture) => value.ToString(format);
        public static string FormatDecimal(decimal value, string format, string culture) => value.ToString(format);

        public static void Register(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction
            (
                typeof(BaseDbContextSqlFunctions).GetMethod(nameof(FormatDateTime), new Type[] { typeof(DateTime), typeof(string), typeof(string) })
            )
            .HasTranslation(Transalate);

            modelBuilder.HasDbFunction
            (
                typeof(BaseDbContextSqlFunctions).GetMethod(nameof(FormatDecimal), new Type[] { typeof(decimal), typeof(string), typeof(string) })
            )
            .HasTranslation(Transalate);
        }

        private static SqlExpression Transalate(IReadOnlyCollection<SqlExpression> args)
            => new SqlFunctionExpression
            (
                "FORMAT",
                args,
                false,
                new bool[] { true, true, true },
                typeof(string),
                null
            );
    }
}
