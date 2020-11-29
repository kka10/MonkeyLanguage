using Monkey.Lexing;

namespace Monkey.Ast.Statements
{
    public class ExpressionStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Expression { get; set; }
        
        public string ToCode() {
            return this.Expression?.ToCode() ?? "";
        }

        public string TokenLiteral()
        {
            return this.Token.Literal;
        }
    }
}