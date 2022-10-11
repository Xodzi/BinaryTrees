using System;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class BinTree<T> where T : IComparable<T>
    {
        public BinNode<T> Root; // корень дерева
        public BinTree() { }
        /// <summary>
        /// Добавление нового узла
        /// </summary>
        /// <param name="root"></param>
        public BinTree(T root) { Add(root); }
        public BinTree(T[] items) { foreach (T item in items) Add(item); }

        public void Add(T value) { Add(ref Root, value); }

        public void Add(ref BinNode<T> node, T value)
        {
            if (node == null) node = new BinNode<T>(value);
            else
            {
                if (node.Value.CompareTo(value) > 0)
                    Add(ref node.Left, value); // node.Value > value
                else Add(ref node.Right, value); // node.Value <= value
                                                 //if (node.Value.CompareTo(value) < 0) // уник. дерево
                                                 // Add(ref node.Right, value); // node.Value < value
            }
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
        private bool Contains(ref BinNode<T> node, T value)
        {
            if (node == null) return false;
            if (node.Value.CompareTo(value) == 0) return true;
            if (node.Value.CompareTo(value) > 0) return Contains(ref
            node.Left, value);
            else return Contains(ref node.Right, value);
        }

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
                BinNode<T> item = CutMax(ref Root.Left, ref Root);
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


        private BinNode<T> CutMax(ref BinNode<T> node, ref BinNode<T> parent) // ищем самую правую вершину
        {
            BinNode<T> curParent = parent;
            BinNode<T> curNode = node;
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

        private void RemoveLeft(ref BinNode<T> node, T value, ref BinNode<T> parent) // удаление значения в левой ветке
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
                BinNode<T> item = CutMax(ref node.Left, ref node);
                node.Value = item.Value; // замена только поля данных
                                         //item.Left = node.Left; // замена вершины целиком
                                         //item.Right = node.Right; // замена вершины целиком
                                         //parent.Left = item; // замена вершины целиком
                return;
            }
            if (node.Value.CompareTo(value) > 0)
                RemoveLeft(ref node.Left, value, ref node); // влево
            else RemoveRight(ref node.Right, value, ref node);
        }
        private void RemoveRight(ref BinNode<T> node, T value,
        ref BinNode<T> parent) // Удаление значения в правой ветке
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
                BinNode<T> item = CutMax(ref node.Left, ref node);
                node.Value = item.Value; // замена только поля данных
                                         //item.Left = node.Left; // замена вершины целиком
                                         //item.Right = node.Right; // замена вершины целиком
                                         //parent.Right = item; // замена вершины целиком
                return;
            }
            if (node.Value.CompareTo(value) > 0)
                RemoveLeft(ref node.Left, value, ref node); // влево
            else RemoveRight(ref node.Right, value, ref node);
        }
        public void TraversePreorder() // Обход в прямом порядке
        {
            TraversePreorder(Root);
        }
        private void TraversePreorder(BinNode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");
                TraversePreorder(node.Left);
                TraversePreorder(node.Right);
            }
        }
        public void TraverseInorder() // Симметричный обход
        {
            TraverseInorder(Root);
        }
        private void TraverseInorder(BinNode<T> node)
        {
            if (node != null)
            {
                TraverseInorder(node.Left);
                Console.Write(node.Value + " ");
                TraverseInorder(node.Right);
            }
        }
        public void TraversePostorder() // Обход в обратном порядке
        {
            TraversePostorder(Root);
        }
        private void TraversePostorder(BinNode<T> node)
        {
            if (node != null)
            {
                TraversePostorder(node.Left);
                TraversePostorder(node.Right);
                Console.Write(node.Value + " ");
            }
        }
        public void TraverseBreadthFirst() // Обход в ширину
        {
            var node = new Queue<BinNode<T>>();
            node.Enqueue(Root);
            TraverseBreadthFirst(node);
        }
        private void TraverseBreadthFirst(Queue<BinNode<T>> que)
        {
            if (que.Count == 0) return;
            var children = new Queue<BinNode<T>>();
            foreach (BinNode<T> node in que)
            {
                Console.Write(node.Value + " "); // Вывод по уровням
                if (node.Left != null) children.Enqueue(node.Left);
                if (node.Right != null) children.Enqueue(node.Right);
            }
            que.Clear();
            Console.WriteLine();
            TraverseBreadthFirst(children);
        }
        public bool MirrorSymmetry()
        {
            return MirrorSymmetry(Root.Left, Root.Right);
        }
        public bool MirrorSymmetry(BinNode<T> left, BinNode<T> right)
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
        public static int High(BinNode<T> root)
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
        public bool Full(BinNode<int> root)
        {
            if (root == null) return true;
            if (root.Left == null && root.Right == null)
            {
                return true;
            }
            else if (root.Left != null && root.Right != null)
            {
                return (Full(root.Left) & Full(root.Right));
            }
            else
            {
                return false;
            }
        }
        public bool Perfect(BinNode<int> root)
        {
            if (root == null) return true;
            if (root.Left != null && root.Right == null) return false;
            if (root.Right != null && root.Left == null) return false;
            if ((root.Left.Left != null || root.Left.Right != null) && (root.Right.Left != null || root.Right.Right != null))
            {
                return Perfect(root.Left) & Perfect(root.Right);
            }
            else if ((root.Left.Left == null || root.Left.Right == null) && (root.Right.Left == null || root.Right.Right == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
