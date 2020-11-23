using System;
using Xunit;
using Monkey.Lexing;
using System.Collections.Generic;

namespace Monkey.LexingTest
{
    public class LexerTest
    {
        [Fact]
        public void TestNextToken1()
        {
            var input = "=+(){},;";

            var testTokens = new List<Token>();
            testTokens.Add(new Token(TokenType.ASSIGN, "="));
            testTokens.Add(new Token(TokenType.PLUS, "+"));
            testTokens.Add(new Token(TokenType.LPAREN, "("));
            testTokens.Add(new Token(TokenType.RPAREN, ")"));
            testTokens.Add(new Token(TokenType.LBRACE, "{"));
            testTokens.Add(new Token(TokenType.RBRACE, "}"));
            testTokens.Add(new Token(TokenType.COMMA, ","));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.EOF, ""));

            var lexer = new Lexer(input);

            foreach (var testToken in testTokens)
            {
                var token = lexer.NextToken();
                Assert.Equal(testToken.Type, token.Type);
                Assert.Equal(testToken.Literal, token.Literal);
            }
        }

        [Fact]
        public void TestNextToken2()
        {
            var input =
            @"
            let five = 5;
            let ten = 10;

            let add = fn(x, y) {
                x + y;
            };

            let result = add(five, ten);
            ";

            var testTokens = new List<Token>();

            testTokens.Add(new Token(TokenType.LET, "let"));
            testTokens.Add(new Token(TokenType.IDENT, "five"));
            testTokens.Add(new Token(TokenType.ASSIGN, "="));
            testTokens.Add(new Token(TokenType.INT, "5"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));

            testTokens.Add(new Token(TokenType.LET, "let"));
            testTokens.Add(new Token(TokenType.IDENT, "ten"));
            testTokens.Add(new Token(TokenType.ASSIGN, "="));
            testTokens.Add(new Token(TokenType.INT, "10"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));

            testTokens.Add(new Token(TokenType.LET, "let"));
            testTokens.Add(new Token(TokenType.IDENT, "add"));
            testTokens.Add(new Token(TokenType.ASSIGN, "="));
            testTokens.Add(new Token(TokenType.FUNCTION, "fn"));
            testTokens.Add(new Token(TokenType.LPAREN, "("));
            testTokens.Add(new Token(TokenType.IDENT, "x"));
            testTokens.Add(new Token(TokenType.COMMA, ","));
            testTokens.Add(new Token(TokenType.IDENT, "y"));
            testTokens.Add(new Token(TokenType.RPAREN, ")"));
            testTokens.Add(new Token(TokenType.LBRACE, "{"));
            testTokens.Add(new Token(TokenType.IDENT, "x"));
            testTokens.Add(new Token(TokenType.PLUS, "+"));
            testTokens.Add(new Token(TokenType.IDENT, "y"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.RBRACE, "}"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));

            testTokens.Add(new Token(TokenType.LET, "let"));
            testTokens.Add(new Token(TokenType.IDENT, "result"));
            testTokens.Add(new Token(TokenType.ASSIGN, "="));
            testTokens.Add(new Token(TokenType.IDENT, "add"));
            testTokens.Add(new Token(TokenType.LPAREN, "("));
            testTokens.Add(new Token(TokenType.IDENT, "five"));
            testTokens.Add(new Token(TokenType.COMMA, ","));
            testTokens.Add(new Token(TokenType.IDENT, "ten"));
            testTokens.Add(new Token(TokenType.RPAREN, ")"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.EOF, ""));

            var lexer = new Lexer(input);

            foreach (var testToken in testTokens)
            {
                var token = lexer.NextToken();
                Assert.Equal(testToken.Type, token.Type);
                Assert.Equal(testToken.Literal, token.Literal);
            }
        }

        [Fact]
        public void TestNextToken3()
        {
            var input = "1 == 1; 1 != 0; ><*/-=";

            var testTokens = new List<Token>();

            testTokens.Add(new Token(TokenType.INT, "1"));
            testTokens.Add(new Token(TokenType.EQ, "=="));
            testTokens.Add(new Token(TokenType.INT, "1"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.INT, "1"));
            testTokens.Add(new Token(TokenType.NOT_EQ, "!="));
            testTokens.Add(new Token(TokenType.INT, "0"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.GT, ">"));
            testTokens.Add(new Token(TokenType.LT, "<"));
            testTokens.Add(new Token(TokenType.ASTERISK, "*"));
            testTokens.Add(new Token(TokenType.SLASH, "/"));
            testTokens.Add(new Token(TokenType.MINUS, "-"));
            testTokens.Add(new Token(TokenType.ASSIGN, "="));
            testTokens.Add(new Token(TokenType.EOF, ""));

            var lexer = new Lexer(input);

            foreach (var testToken in testTokens)
            {
                var token = lexer.NextToken();
                Assert.Equal(testToken.Type, token.Type);
                Assert.Equal(testToken.Literal, token.Literal);
            }
        }

        [Fact]
        public void TestNextToken4()
        {
            var input =
            @"
            if (5 < 10) {
                return true;
            } else {
                return false;
            }
            ";

            var testTokens = new List<Token>();

            testTokens.Add(new Token(TokenType.IF, "if"));
            testTokens.Add(new Token(TokenType.LPAREN, "("));
            testTokens.Add(new Token(TokenType.INT, "5"));
            testTokens.Add(new Token(TokenType.LT, "<"));
            testTokens.Add(new Token(TokenType.INT, "10"));
            testTokens.Add(new Token(TokenType.RPAREN, ")"));
            testTokens.Add(new Token(TokenType.LBRACE, "{"));
            testTokens.Add(new Token(TokenType.RETURN, "return"));
            testTokens.Add(new Token(TokenType.TRUE, "true"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.RBRACE, "}"));
            testTokens.Add(new Token(TokenType.ELSE, "else"));
            testTokens.Add(new Token(TokenType.LBRACE, "{"));
            testTokens.Add(new Token(TokenType.RETURN, "return"));
            testTokens.Add(new Token(TokenType.FALSE, "false"));
            testTokens.Add(new Token(TokenType.SEMICOLON, ";"));
            testTokens.Add(new Token(TokenType.RBRACE, "}"));
            testTokens.Add(new Token(TokenType.EOF, ""));

            var lexer = new Lexer(input);

            foreach (var testToken in testTokens)
            {
                var token = lexer.NextToken();
                Assert.Equal(testToken.Type, token.Type);
                Assert.Equal(testToken.Literal, token.Literal);
            }
        }
    }
}
