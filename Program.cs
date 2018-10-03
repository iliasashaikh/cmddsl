using System;
using ServiceStack.Text;
using Sprache;
using Xunit;

namespace cmddsl
{
    class Program
    {
        static void Main(string[] args)
        {
            var parseA = Parse.Char('A').AtLeastOnce();
            System.Console.WriteLine(parseA.Parse("Abcd").Dump());

            var command = @"rec --for cash --from ""1-jan-2016"" --to ""31-dec-2018"" --clients a,b,c,d";

            TestWord();
            
        }

        private static void TestWord()
        {
            var word = @"--for cash";
            var r = PosixCommandGrammar.LongOptionParser.Parse(word);
            r.PrintDump();

            r = PosixCommandGrammar.OptionallyQuotedWordParser.Parse(@"a");
            Assert.Equal(r,"a");

            r = PosixCommandGrammar.OptionallyQuotedWordParser.Parse("\"a\"");
            Assert.Equal(r, "a");
        }
    }

    class PosixCommandGrammar
    {
        public static readonly Parser<string> WordParser = Parse.LetterOrDigit.AtLeastOnce().Text().Token();

        public static readonly Parser<string> OptionallyQuotedWordParser =
        (
            from open in Parse.Char('"')
            from word in WordParser
            from close in Parse.Char('"')
            select word
        ).Token();

        public static readonly Parser<string> LongOptionParser = 
            (
                from twodashes in Parse.String("--")
                from optionName in WordParser
                select optionName
            ).Token();
    }
}
