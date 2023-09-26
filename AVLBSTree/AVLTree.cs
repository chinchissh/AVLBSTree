using System;

namespace AVLBSTree
{
    public class AVLTree
    {
        private Node root;

        public AVLTree()
        {
            root = null;
        }

        // метод для вставки нового элемента
        public void Insert(int key)
        {
            root = Insert(root, key);
        }

        // рекурсивный метод для вставки нового элемента
        private Node Insert(Node node, int key)
        {
            if (node == null)
            {
                return new Node(key);
            }

            if (key < node.Key)
            {
                node.Left = Insert(node.Left, key);
            }
            else if (key > node.Key)
            {
                node.Right = Insert(node.Right, key);
            }
            else
            {
                // дубликаты не допускаются
                return node;
            }

            // обновление высоты вершины
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // балансировка дерева
            int balance = GetBalance(node);

            // вращения
            if (balance > 1 && key < node.Left.Key)
            {
                return RotateRight(node);
            }

            if (balance < -1 && key > node.Right.Key)
            {
                return RotateLeft(node);
            }

            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // метод для удаления элемента
        public void Delete(int key)
        {
            root = Delete(root, key);
        }

        // рекурсивный метод для удаления элемента
        private Node Delete(Node node, int key)
        {
            if (node == null)
            {
                return node;
            }

            if (key < node.Key)
            {
                node.Left = Delete(node.Left, key);
            }
            else if (key > node.Key)
            {
                node.Right = Delete(node.Right, key);
            }
            else
            {
                // найден элемент для удаления

                // если у вершины есть только один потомок или нет потомков
                if ((node.Left == null) || (node.Right == null))
                {
                    Node temp = null;
                    if (temp == node.Left)
                    {
                        temp = node.Right;
                    }
                    else
                    {
                        temp = node.Left;
                    }

                    // случай, когда нет потомков
                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else // случай, когда есть один потомок
                    {
                        node = temp;
                    }
                }
                else
                {
                    // случай, когда есть два потомка, найдем наименьший элемент в правом поддереве
                    Node temp = MinValueNode(node.Right);

                    // копируем значение наименьшего элемента в текущую вершину
                    node.Key = temp.Key;

                    // удаляем наименьший элемент в правом поддереве
                    node.Right = Delete(node.Right, temp.Key);
                }
            }

            // если дерево имеет только одну вершину
            if (node == null)
            {
                return node;
            }

            // обновляем высоту текущей вершины
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // балансировка дерева
            int balance = GetBalance(node);

            // вращения
            if (balance > 1 && GetBalance(node.Left) >= 0)
            {
                return RotateRight(node);
            }

            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && GetBalance(node.Right) <= 0)
            {
                return RotateLeft(node);
            }

            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // метод для поиска элемента
        public bool Find(int key)
        {
            return Find(root, key);
        }

        // рекурсивный метод для поиска элемента
        private bool Find(Node node, int key)
        {
            if (node == null)
            {
                return false;
            }

            if (key < node.Key)
            {
                return Find(node.Left, key);
            }
            else if (key > node.Key)
            {
                return Find(node.Right, key);
            }
            else
            {
                return true;
            }
        }

        // метод для обхода и печати содержимого дерева (ин-ордер обход)
        public void InOrderTraversal()
        {
            InOrderTraversal(root);
        }

        private void InOrderTraversal(Node node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Key + " ");
                InOrderTraversal(node.Right);
            }
        }

        // вспомогательные методы для высоты и баланса вершины
        private int GetHeight(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }

        private int GetBalance(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        // методы для вращений
        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            return x;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return y;
        }

        // метод для поиска вершины с наименьшим значением
        private Node MinValueNode(Node node)
        {
            Node current = node;

            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }
    }
}
