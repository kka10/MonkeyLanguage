using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Monkey.Parsing;
using Monkey.Lexing;
using Monkey.Ast;
using Monkey.Ast.Statements;
using Monkey.Ast.Expressions;

namespace Monkey.ParsingTest
{
    public class AstTest1
    {
        [Fact]
        public void TestNodeToCode1()
        {
            var code = "let x = abc;";

            var root = new Root();
            root.Statements = new List<IStatement>();

            root.Statements.Add(
                new LetStatement()
                {
                    Token = new Token(TokenType.LET, "let"),
                    Name = new Identifier(
                        new Token(TokenType.IDENT, "x"),
                        "x"
                    ),
                    Value = new Identifier(
                        new Token(TokenType.IDENT, "abc"),
                        "abc"
                    )
                }
            );

            Assert.Equal(code, root.ToCode());
        }
    }
}