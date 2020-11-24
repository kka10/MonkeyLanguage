using Monkey.Lexing;

namespace Monkey.Ast.Statements
{
    public class ReturnStatement: IStatement
    {
        public Token Token { get; set; }
        public IExpression ReturnValue { get; set; }

        public string TokenLiteral() => this.Token.Literal;
    }
}