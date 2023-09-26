using System;

namespace AVLBSTree
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree avlTree = new AVLTree();

            avlTree.Insert(20);
            avlTree.Insert(30);
            avlTree.Insert(40);
            avlTree.Insert(50);
            avlTree.Insert(10);
            avlTree.Insert(80);

            Console.WriteLine("In-Order обход:");
            avlTree.InOrderTraversal();

            Console.WriteLine("\n\nУдаление элемента 30:");
            avlTree.Delete(30);
            avlTree.InOrderTraversal();

            Console.WriteLine("\n\nПоиск элемента 25: " + avlTree.Find(25));

            Console.ReadKey();
        }
    }
}