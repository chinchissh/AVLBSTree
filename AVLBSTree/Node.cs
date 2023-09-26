using System;

namespace AVLBSTree
{
    public class Node
    {
        public int Key { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Height { get; set; }

        public Node(int key)
        {
            Key = key;
            Left = null;
            Right = null;
            Height = 1;
        }
    }
}
