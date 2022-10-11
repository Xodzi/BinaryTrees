

namespace BinaryTrees
{
    /// <summary>
    /// Узел АВЛ-дерева
    /// </summary>
    public class AVLNode<T>
    {
        public T Value; // Поле Value
        public AVLNode<T> Left; // Ссылка на левую ветвь или null
        public AVLNode<T> Right; // Ссылка на правую ветвь или null
        public int Balance { get; set; } = 0;
        public AVLNode(T value, AVLNode<T> left = null, AVLNode<T> right = null)
        {
            Value = value;
            Left = left;
            Right = right;
            if (left == null && right == null)
            {
                Balance = 0;
            }
            else if (left == null && right != null)
            {
                Balance = right.Balance;
            }
            else if (left != null && right == null)
            {
                Balance = left.Balance;
            }
            else
            {
                Balance = left.Balance - right.Balance;
            }
        }
        public AVLNode(AVLNode<T> node)
        {
            Value = node.Value;
            Left = node.Left;
            Right = node.Right;
            Balance = node.Balance;
        }
    }
}
