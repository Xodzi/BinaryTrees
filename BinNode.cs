

namespace BinaryTrees
{
    public class BinNode<T>
    {
        public T Value; // Поле Value
        public BinNode<T> Left; // Ссылка на левую ветвь или null
        public BinNode<T> Right; // Ссылка на правую ветвь или null
                                 // Конструктор нового элемента со значениями Value и Left, Right:
        public BinNode(T value, BinNode<T> left = null, BinNode<T> right
        = null)
        { Value = value; Left = left; Right = right; }

    }
}
