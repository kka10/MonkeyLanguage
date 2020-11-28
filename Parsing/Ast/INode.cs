namespace Monkey.Ast
{
    public interface INode
    {
        string TokenLiteral();
        string ToCode();
    }
}