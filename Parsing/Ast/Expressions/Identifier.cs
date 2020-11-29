using Monkey.Lexing;

namespace Monkey.Ast.Expressions
{
    public class Identifier : IExpression
    {
        public Token Token { get; set; }
        public string Value { get; set; }

        public Identifier(Token token, string value)
        {
            this.Token = token;
            this.Value = value;
        }

        public string TokenLiteral() => this.Token?.Literal ?? "";

        public string ToCode() => this.Value ?? "";
    }
}