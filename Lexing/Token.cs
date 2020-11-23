using System;
using System.Collections.Generic;

namespace Monkey.Lexing
{
    public class Token
    {
        public Token(TokenType type, string literal)
        {
            this.Type = type;
            this.Literal = literal;
        }

        public TokenType Type { get; set; }
        public string Literal { get; set; }

        public static TokenType LookupIdentifier(string identifier)
        {
            if (Keywords.ContainsKey(identifier))
            {
                return Keywords[identifier];
            }

            return TokenType.IDENT;
        }

        public static Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>() {
            { "let", TokenType.LET },
            { "fn", TokenType.FUNCTION },
            { "return", TokenType.RETURN },
            { "if", TokenType.IF },
            { "else", TokenType.ELSE },
            { "true", TokenType.TRUE },
            { "false", TokenType.FALSE },
        };
    }

    public enum TokenType
    {
        ILLEGAL,
        EOF,
        IDENT,
        INT,
        ASSIGN,
        PLUS,
        COMMA,
        SEMICOLON,
        LPAREN,
        RPAREN,
        LBRACE,
        RBRACE,
        FUNCTION,
        LET,
        EQ,
        NOT_EQ,
        MINUS,
        ASTERISK,
        SLASH,
        GT,
        LT,
        BANG,
        TRUE,
        FALSE,
        IF,
        ELSE,
        RETURN,
    }
}