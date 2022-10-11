using System;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class AVLTree<T> where T : IComparable<T>
    {
        new public AVLNode<T> Root; // корень дерева
        public AVLTree() { }
        /// <summary>
        /// Добавление нового узла
        /// </summary>
        /// <param name="root"></param>
        public AVLTree(T root) { Add(root); }
        public AVLTree(T[] items) { foreach (T item in items) Add(item); }
        public void Add(T value) { Add(ref Root, value); }
        public void Add(ref AVLNode<T> node, T value)
        {
            if (node == null) node = new AVLNode<T>(value);
            else
            {
                if (node.Value.CompareTo(value) > 0)
                {
                    //node.Balance--;
                    Root.Balance++;
                    Add(ref node.Left, value); // node.Value > value
                }
                else
                {
                    Root.Balance--;
                    //node.Balance++;
                    Add(ref node.Right, value); // node.Value <= value
                }
            }
        }
        /// <summary>
        /// Возвращает высоту дерева
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int High(AVLNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            int left = High(root.Left) + 1;
            int right = High(root.Right) + 1;
            if (left > right)
            {
                return left;
            }
            else
            {
                return right;
            }
        }
        public void RotateLeftSmall(ref AVLNode<T> root)
        {
            var leftchild = root.Left;
            root.Left = leftchild.Right;
            leftchild.Right = root;
            Root = leftchild;
            SetBalance(Root);
            SetBalance(leftchild);
        }
        public void RotateRightSmall(ref AVLNode<T> root)
        {
            var rightchild = root.Right;
            root.Right = rightchild.Left;
            rightchild.Left = root;
            Root = rightchild;
            SetBalance(Root);
            SetBalance(rightchild);
        }
        /// <summary>
        /// Считает баланс для конкретного узла дерева
        /// </summary>
        /// <param name="root"></param>
        /// <exception cref="Exception"></exception>
        public void SetBalance(AVLNode<T> root)
        {
            if (root == null) throw new Exception("null node");
            if (root.Left == null && root.Right == null) { root.Balance = 0; return; }
            if (root.Left != null && root.Right == null) { root.Balance = High(root.Left); return; }
            if (root.Left == null && root.Right != null) { root.Balance = -High(root.Right); return; }
            root.Balance = (High(root.Left) - High(root.Right));
            return;
        }
        /// <summary>
        /// Корректировка баланса дерева
        /// </summary>
        /// <param name="root"></param>
        public void Correct(AVLNode<T> root)
        {
            SetBalance(root);
            int balance = root.Balance;
            if (balance == 2)
            {
                RotateLeftSmall(ref root);
            }
            if (balance == -2)
            {
                RotateRightSmall(ref root);
            }
            SetBalance(root);
            return;
        }
        private bool Contains(ref AVLNode<T> node, T value)
        {
            if (node == null) return false;
            if (node.Value.CompareTo(value) == 0) return true;
            if (node.Value.CompareTo(value) > 0) return Contains(ref
            node.Left, value);
            else return Contains(ref node.Right, value);
        }
        /// <summary>
        /// Проверяет содержится ли значение в дереве
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            return Contains(ref Root, value);
        }
        /// <summary>
        /// Удаляет значение из дерева
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value) // удаление значения
        {
            if (Root == null) return;
            if (Root.Value.CompareTo(value) == 0) // удаление корня
            {
                if (Root.Left == null && Root.Right == null)
                { Root = null; return; } // корень терминальный
                if (Root.Left != null && Root.Right == null)
                { Root = Root.Left; return; } // только левое
                if (Root.Left == null && Root.Right != null)
                { Root = Root.Right; return; } // только правое
                AVLNode<T> item = CutMax(ref Root.Left, ref Root);
                Root.Value = item.Value; // замена только поля данных
                                         //item.Left = Root.Left; // замена вершины целиком
                                         //item.Right = Root.Right; // замена вершины целиком
                                         //Root = item; // замена вершины целиком
                return;
            }
            if (Root.Value.CompareTo(value) > 0)
                RemoveLeft(ref Root.Left, value, ref Root); // влево
            else RemoveRight(ref Root.Right, value, ref Root);
        }
        /// <summary>
        /// Ищет самую правую вершину(максимальное значение)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private AVLNode<T> CutMax(ref AVLNode<T> node, ref AVLNode<T> parent) // ищем самую правую вершину
        {
            AVLNode<T> curParent = parent;
            AVLNode<T> curNode = node;
            bool flag = true;
            while (curNode.Right != null)
            {
                curParent = curNode;
                curNode = curNode.Right;
                flag = false;
            }
            if (curNode.Left == null)
            {
                if (flag) curParent.Left = null;
                else curParent.Right = null;
            } // вершина - лист
            else
            {
                if (flag) curParent.Left = curNode.Left;
                else curParent.Right = curNode.Left;
            } // есть левое
            return curNode;
        }
        private void RemoveLeft(ref AVLNode<T> node, T value, ref AVLNode<T> parent) // удаление значения в левой ветке
        {
            if (node == null) return;
            if (node.Value.CompareTo(value) == 0) // узел для удаления
            {
                if (node.Left == null && node.Right == null)
                { parent.Left = null; return; } // вершина - лист
                if (node.Left != null && node.Right == null)
                { parent.Left = node.Left; return; } // левое
                if (node.Left == null && node.Right != null)
                { parent.Left = node.Right; return; } // правое
                AVLNode<T> item = CutMax(ref node.Left, ref node);
                node.Value = item.Value; // замена только поля данных
                                         //item.Left = node.Left; // замена вершины целиком
                                         //item.Right = node.Right; // замена вершины целиком
                                         //parent.Left = item; // замена вершины целиком
                parent.Balance--;
                return;
            }
            if (node.Value.CompareTo(value) > 0)
                RemoveLeft(ref node.Left, value, ref node); // влево
            else RemoveRight(ref node.Right, value, ref node);
        }
        private void RemoveRight(ref AVLNode<T> node, T value, ref AVLNode<T> parent) // Удаление значения в правой ветке
        {
            if (node == null) return;
            if (node.Value.CompareTo(value) == 0) // узел для удаления
            {
                if (node.Left == null && node.Right == null)
                { parent.Right = null; return; } // вершина - лист
                if (node.Left != null && node.Right == null)
                { parent.Right = node.Left; return; } // левое
                if (node.Left == null && node.Right != null)
                { parent.Right = node.Right; return; } // правое
                AVLNode<T> item = CutMax(ref node.Left, ref node);
                node.Value = item.Value; // замена только поля данных
                                         //item.Left = node.Left; // замена вершины целиком
                                         //item.Right = node.Right; // замена вершины целиком
                                         //parent.Right = item; // замена вершины целиком
                parent.Balance++;
                return;
            }
            if (node.Value.CompareTo(value) > 0)
                RemoveLeft(ref node.Left, value, ref node); // влево
            else RemoveRight(ref node.Right, value, ref node);
        }
        /// <summary>
        /// Обход в прямом порядке
        /// </summary>
        public void TraversePreorder() 
        {
            TraversePreorder(Root);
        }
        private void TraversePreorder(AVLNode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");
                TraversePreorder(node.Left);
                TraversePreorder(node.Right);
            }
        }
        /// <summary>
        /// Симметричный обход
        /// </summary>
        public void TraverseInorder()
        {
            TraverseInorder(Root);
        }
        private void TraverseInorder(AVLNode<T> node)
        {
            if (node != null)
            {
                TraverseInorder(node.Left);
                Console.Write(node.Value + " ");
                TraverseInorder(node.Right);
            }
        }
        /// <summary>
        /// Обход в обратном порядке
        /// </summary>
        public void TraversePostorder()
        {
            TraversePostorder(Root);
        }
        private void TraversePostorder(AVLNode<T> node)
        {
            if (node != null)
            {
                TraversePostorder(node.Left);
                TraversePostorder(node.Right);
                Console.Write(node.Value + " ");
            }
        }
        /// <summary>
        /// Обход в ширину
        /// </summary>
        public void TraverseBreadthFirst()
        {
            var node = new Queue<AVLNode<T>>();
            node.Enqueue(Root);
            TraverseBreadthFirst(node);
        }
        private void TraverseBreadthFirst(Queue<AVLNode<T>> que)
        {
            if (que.Count == 0) return;
            var children = new Queue<AVLNode<T>>();
            foreach (AVLNode<T> node in que)
            {
                Console.Write(node.Value + " "); // Вывод по уровням
                if (node.Left != null) children.Enqueue(node.Left);
                if (node.Right != null) children.Enqueue(node.Right);
            }
            que.Clear();
            Console.WriteLine();
            TraverseBreadthFirst(children);
        }
        /// <summary>
        /// Проверка симметричности дерева
        /// </summary>
        /// <returns></returns>
        public bool MirrorSymmetry()
        {
            return MirrorSymmetry(Root.Left, Root.Right);
        }
        public bool MirrorSymmetry(AVLNode<T> left, AVLNode<T> right)
        {
            if (right == null && left == null) return true; //оба null
            if (right == null && left != null) return false;
            if (left == null && right != null) return false;
            bool leftside = true;
            bool rightside = true;
            leftside = MirrorSymmetry(left.Left, right.Right);
            rightside = MirrorSymmetry(left.Right, right.Left);
            return (leftside & rightside);
        }
    }
}
