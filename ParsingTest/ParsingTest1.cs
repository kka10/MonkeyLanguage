using System;
using Xunit;
using Xunit.Abstractions;
using Monkey.Parsing;
using Monkey.Lexing;
using Monkey.Ast;
using Monkey.Ast.Statements;
using Monkey.Ast.Expressions;

namespace Monkey.ParsingTest
{
    public class ParsingTest1
    {
        private readonly ITestOutputHelper output;

        public ParsingTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

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
            this._CheckParserErrors(parser);

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

        [Fact]
        public void TestReturnStatement1()
        {
            var input =
            @"
            return 5;
            return 10;
            return 993322;
            ";

            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var root = parser.ParseProgram();
            this._CheckParserErrors(parser);

            Assert.Equal(root.Statements.Count, 3);

            foreach (var statement in root.Statements)
            {
                var returnStatement = statement as ReturnStatement;
                Assert.NotNull(returnStatement);

                Assert.Equal(returnStatement.TokenLiteral(), "return");
            }
        }

        [Fact]
        public void TestIdentifierExpression1()
        {
            var input = @"foobar;";

            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var root = parser.ParseProgram();
            this._CheckParserErrors(parser);

            Assert.Equal(root.Statements.Count, 1);

            var statement = root.Statements[0] as ExpressionStatement;
            Assert.NotNull(statement);

            var ident = statement.Expression as Identifier;
            Assert.NotNull(ident);
            Assert.Equal(ident.Value, "foobar");
            Assert.Equal(ident.TokenLiteral(), "foobar");
        }

        [Fact]
        public void TestIntegerLiteralExpression1()
        {
            var input = @"123";

            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            var root = parser.ParseProgram();
            this._CheckParserErrors(parser);

            Assert.Equal(root.Statements.Count, 1);

            var statement = root.Statements[0] as ExpressionStatement;
            Assert.NotNull(statement);

            var integerLiteral = statement.Expression as IntegerLiteral;
            Assert.NotNull(integerLiteral);
            Assert.Equal(integerLiteral.Value, 123);
            Assert.Equal(integerLiteral.TokenLiteral(), "123");
        }

        [Fact]
        public void TestPrefixExpressions1()
        {
            var tests = new[] {
                ("!5", "!", 5),
                ("-15", "-", 15),
            };

            foreach (var (input, op, value) in tests)
            {
                var lexer = new Lexer(input);
                var parser = new Parser(lexer);
                var root = parser.ParseProgram();
                this._CheckParserErrors(parser);

                Assert.Equal(root.Statements.Count, 1);

                var statement = root.Statements[0] as ExpressionStatement;
                Assert.NotNull(statement);
                
                var expression = statement.Expression as PrefixExpression;
                Assert.NotNull(expression);
                Assert.Equal(expression.Operator, op);
                
                this._TestIntegerLiteral(expression.Right, value);
            }
        }

        [Fact]
        public void TestInfixExpressions1()
        {
            var tests = new [] {
                ("1 + 1", 1, "+", 1),
                ("1 - 1", 1, "-", 1),
                ("1 * 1", 1, "*", 1),
                ("1 / 1", 1, "/", 1),
                ("1 < 1", 1, "<", 1),
                ("1 > 1", 1, ">", 1),
                ("1 == 1", 1, "==", 1),
                ("1 != 1", 1, "!=", 1),
            };

            foreach (var (input, leftValue, op, RightValue) in tests)
            {
                var lexer = new Lexer(input);
                var parser = new Parser(lexer);
                var root = parser.ParseProgram();
                this._CheckParserErrors(parser);

                Assert.Equal(root.Statements.Count, 1);

                var statement = root.Statements[0] as ExpressionStatement;
                Assert.NotNull(statement);

                var expression = statement.Expression as InfixExpression;
                Assert.NotNull(expression);

                this._TestIntegerLiteral(expression.Left, leftValue);

                Assert.Equal(expression.Operator, op);

                this._TestIntegerLiteral(expression.Right, RightValue);
            }
        }

        [Fact]
        public void TestOperatorPrecedenceParsing()
        {
            var tests = new []
            {
                ("a + b", "(a + b)"),
                ("!-a", "(!(-a))"),
                ("a + b - c", "((a + b) - c)"),
                ("a * b / c", "((a * b) / c)"),
                ("a + b * c", "(a + (b * c))"),
                ("a + b * c + d / e - f", "(((a + (b * c)) + (d / e)) - f)"),
                ("1 + 2; -3 * 4", $"(1 + 2){Environment.NewLine}((-3) * 4)"),
                ("5 > 4 == 3 < 4", "((5 > 4) == (3 < 4))"),
                ("3 + 4 * 5 == 3 * 1 + 4 * 5", "((3 + (4 * 5)) == ((3 * 1) + (4 * 5)))"),
            };

            foreach (var (input, code) in tests)
            {
                var lexer = new Lexer(input);
                var parser = new Parser(lexer);
                var root = parser.ParseProgram();
                this._CheckParserErrors(parser);

                var actual = root.ToCode();
                Assert.Equal(code, actual);
            }
        }

        private void _CheckParserErrors(Parser parser)
        {
            if (parser.Errors.Count == 0) return;
            var message = "\n" + string.Join("\n", parser.Errors);
            output.WriteLine(message);
        }

        private void _TestIntegerLiteral(IExpression expression, int value)
        {
            var integerLiteral = expression as IntegerLiteral;

            Assert.NotNull(integerLiteral);
            Assert.Equal(integerLiteral.Value, value);
            Assert.Equal(integerLiteral.TokenLiteral(), $"{value}");
        }
    }
}
