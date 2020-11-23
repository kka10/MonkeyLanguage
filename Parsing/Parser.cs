using System;
using System.Collections.Generic;
using Monkey.Lexing;
using Monkey.Ast;
using Monkey.Ast.Statements;
using Monkey.Ast.Expressions;

namespace Monkey.Parsing
{
    public class Parser
    {
        public Token CurrentToken { get; set; }
        public Token NextToken { get; set; }
        public Lexer Lexer { get; }
        public List<string> Errors { get; set; } = new List<string>();

        public Parser(Lexer lexer)
        {
            this.Lexer = lexer;

            this.CurrentToken = this.Lexer.NextToken();
            this.NextToken = this.Lexer.NextToken();
        }

        private void ReadToken()
        {
            this.CurrentToken = this.NextToken;
            this.NextToken = this.Lexer.NextToken();
        }

        public Root ParseProgram()
        {
            var root = new Root();
            root.Statements = new List<IStatement>();

            while (this.CurrentToken.Type != TokenType.EOF)
            {
                var statement = this.ParseStatement();
                if (statement != null)
                {
                    root.Statements.Add(statement);
                }

                this.ReadToken();
            }

            return root;
        }

        public IStatement ParseStatement()
        {
            switch (this.CurrentToken.Type)
            {
                case TokenType.LET:
                    return this.ParseLetStatement();
                default:
                    return null;
            }
        }

        public LetStatement ParseLetStatement()
        {
            var statement = new LetStatement();
            statement.Token = this.CurrentToken;

            if (!this.ExpectPeek(TokenType.IDENT)) return null;

            statement.Name = new Identifier(this.CurrentToken, this.CurrentToken.Literal);

            if (!this.ExpectPeek(TokenType.ASSIGN)) return null;

            while (this.CurrentToken.Type != TokenType.SEMICOLON)
            {
                this.ReadToken();
            }

            return statement;
        }

        private bool ExpectPeek(TokenType type)
        {
            if (this.NextToken.Type == type)
            {
                this.ReadToken();
                return true;
            }

            this.AddNextTokenError(type, this.NextToken.Type);

            return false;
        }

        private void AddNextTokenError(TokenType expected, TokenType actual)
        {
            this.Errors.Add($"{actual.ToString()} ではなく {expected.ToString()} が来なければなりません。");
        }
    }
}
