using System;
using Xunit;
using Monkey.Parsing;
using Monkey.Lexing;
using Monkey.Ast;
using Monkey.Ast.Statements;

namespace Monkey.ParsingTest
{
    public class ParsingTest1
    {
        [Fact]
        public void TestLetStatement1()
        {
            var input =
            @"
            let x = 5;
            let y = 10;
            let xyz = 838383;
            ";

            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var root = parser.ParseProgram();

            Assert.Equal(root.Statements.Count, 3);

            var tests = new string[] { "x", "y", "xyz" };

            for (int i = 0; i < tests.Length; i++)
            {
                var name = tests[i];
                var statement = root.Statements[i];
                this._TestLetStatement(statement, name);
            }
        }

        private void _TestLetStatement(IStatement statement, string name)
        {
            Assert.Equal(statement.TokenLiteral(), "let");

            var letStatement = statement as LetStatement;

            Assert.NotNull(letStatement);

            Assert.Equal(letStatement.Name.Value, name);

            Assert.Equal(letStatement.Name.TokenLiteral(), name);
        }
    }
}
