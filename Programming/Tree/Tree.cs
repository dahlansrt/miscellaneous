using System;
using System.Collections.Generic;

namespace Tree
{
    class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node(int data)
        {
            this.Data = data;
            Left = Right = null;
        }
    }
    class BinaryTree
    {
        public Node Root { get; set; }
        public BinaryTree(Node root)
        {
            this.Root = root;
        }
        public BinaryTree(){}

        #region Lowest Common Ancestor
        public Node RecursiveLCA(Node node, int x, int y)
        {
            if (node == null) return null;

            //If both x & y are smaller, then RecursiveLCA lies in left
            if (node.Data > x && node.Data > y) return this.RecursiveLCA(node.Left, x, y);
            //If both x & y are greater, then RecursiveLCA lies in right
            if (node.Data < x && node.Data < y) return this.RecursiveLCA(node.Right, x, y);


            return node;
        }
        public bool RecursiveContains(Node node, int x)
        {
            if (node == null) return false;
            if (node.Data == x) return true;
            if (node.Data > x) return this.RecursiveContains(node.Left, x);

            return this.RecursiveContains(node.Right, x);
        }
        public Node IterativeLCA(Node node, int x, int y)
        {
            while (node != null)
            {
                if (node.Data > x && node.Data > y) //If both x & y are smaller, then LCA lies in left
                {
                    node = node.Left;
                }
                else if (node.Data < x && node.Data < y) //If both x & y are greater, then LCA lies in right
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }
            return node;
        }
        public bool IterativeContains(Node node, int x)
        {
            while (node != null)
            {
                if (node.Data == x) return true;
                if (node.Data > x) node = node.Left;
                else node = node.Right;
            }
            return false;
        }
        #endregion

        public string Print(int x, int y, int z)
        {
            return ($"Lowest Common Ancestor of {x} & {y} is {z}");
        }
        public void PrintLine()
        {
            Console.WriteLine("\n=================================");
        }

        #region Tree Traversals (Inorder, Preorder & Postorder)
        /// <summary>
        /// 1. Depth First Traversals:
        ///     a. Inorder () => LEFT, ROOT, RIGHT
        ///     b. Preorder () => ROOT, LEFT, RIGHT
        ///     c. Postorder () => LEFT, RIGHT, ROOT
        /// 2. Breadth First or Level Order Traversal
        /// </summary>
        public void PrintInOrder(Node node)
        {
            if (node != null)
            {
                PrintInOrder(node.Left);
                Console.Write(node.Data + " ");
                PrintInOrder(node.Right);
            }
        }
        public void PrintPreOrder(Node node)
        {
            if (node != null)
            {
                Console.Write(node.Data + " ");
                PrintPreOrder(node.Left);
                PrintPreOrder(node.Right);
            }
        }
        public void PrintPostOrder(Node node)
        {
            if (node != null)
            {
                PrintPostOrder(node.Left);
                PrintPostOrder(node.Right);
                Console.Write(node.Data + " ");
            }
        }
        /// <summary>
        /// Get Height of Tree
        /// </summary>
        public int Height(Node node)
        {
            if (node == null) return 0;
            else
            {
                int left = Height(node.Left);
                int right = Height(node.Right);

                return left > right ? left + 1 : right + 1;
            }
        }
        public void PrintByLevel(Node node, int level)
        {
            if (node == null) return;
            if (level == 1) Console.Write(node.Data + " ");
            else if (level > 1)
            {
                PrintByLevel(node.Left, level - 1);
                PrintByLevel(node.Right, level - 1);
            }
        }
        public void PrintLevelOrder(Node node)
        {
            for (int i = 1; i <= Height(node); i++)
                PrintByLevel(node, i);
        }
        /// <summary>
        /// Time O(n) - n is number of nodes in the binary tree
        /// Space O(n) - n is number of nodes in the binary tree
        /// </summary>
        public void PrintLevelOrder(Node node, Queue<Node> queue)
        {
            queue.Enqueue(node);
            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                Console.Write(node.Data + " ");

                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
            PrintLine();
        }
        #endregion

        #region Binary Tree Construction
        /// <summary>
        /// Insert Node in level order
        /// Parent Node = [i]
        /// Left Child = [2 * i + 1]
        /// Right Child = [2 * i + 2]
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="root"></param>
        /// <param name="i"></param>
        /// <returns>root</returns>
        public Node InsertLevelOrder(int[] _data, Node root, int i)
        {
            if (i < _data.Length)
            {
                Node _node = new Node(_data[i]);
                root = _node;

                root.Left = InsertLevelOrder(_data, root.Left, 2 * i + 1);
                root.Left = InsertLevelOrder(_data, root.Left, 2 * i + 2);
            }
            return root;
        }
        /// <summary>
        /// Create a node with i as key
        /// If i is root, then changes root
        /// if parent of i is not created, then creates parent first
        /// </summary>
        public void CreateNode(int[] data, int i, Node[] created)
        {
            // Return if already created
            if (created[i] != null) return;
            // Create a new node and set created[i]
            created[i] = new Node(i);
            // If i is Root, set Root & Return
            if (data[i] == -1)
            {
                Root = created[i];
                return;
            }
            // If parent is null, then creates parent first
            if (created[data[i]] == null) CreateNode(data, data[i], created);

            // Get Parent pointer
            Node P = created[data[i]];
            if (P.Left == null)
                P.Left = created[i];
            else
                P.Right = created[i];
        }
        /// <summary>
        /// Create tree from given parent array representation
        /// Root => data[index] =-1
        /// </summary>
        public void CreateTree(int[] data, int n)
        {
            // To keep track of created nodes, init NULL for all
            Node[] created = new Node[n];
            for (int i = 0; i < n; i++)
            {
                created[i] = null;
            }
            for (int i = 0; i < n; i++)
            {
                CreateNode(data, i, created);
            }
        }
        #endregion
    }
    class Tree
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree(new Node(20));
            tree.Root.Left = new Node(8);
            tree.Root.Right = new Node(22);
            tree.Root.Left.Left = new Node(4);
            tree.Root.Left.Right = new Node(12);
            tree.Root.Left.Right.Left = new Node(10);
            tree.Root.Left.Right.Right = new Node(14);

            #region Lowest Common Ancestor
            Node t;
            int x = 10, y = 14;

            if (tree.RecursiveContains(tree.Root, x) && tree.RecursiveContains(tree.Root, y))
            {
                t = tree.RecursiveLCA(tree.Root, x, y);
                Console.WriteLine($"Recursive: {tree.Print(x, y, t.Data)}");
            }
            if (tree.IterativeContains(tree.Root, x) && tree.IterativeContains(tree.Root, y))
            {
                t = tree.IterativeLCA(tree.Root, x, y);
                Console.WriteLine($"Iterative: {tree.Print(x, y, t.Data)}");
            }

            x = 14;
            y = 8;
            if (tree.RecursiveContains(tree.Root, x) && tree.RecursiveContains(tree.Root, y))
            {
                t = tree.RecursiveLCA(tree.Root, x, y);
                Console.WriteLine($"Recursive: {tree.Print(x, y, t.Data)}");
            }
            if (tree.IterativeContains(tree.Root, x) && tree.IterativeContains(tree.Root, y))
            {
                t = tree.IterativeLCA(tree.Root, x, y);
                Console.WriteLine($"Iterative: {tree.Print(x, y, t.Data)}");
            }

            x = 10;
            y = 22;
            if (tree.RecursiveContains(tree.Root, x) && tree.RecursiveContains(tree.Root, y))
            {
                t = tree.RecursiveLCA(tree.Root, x, y);
                Console.WriteLine($"Recursive: {tree.Print(x, y, t.Data)}");
            }
            if (tree.IterativeContains(tree.Root, x) && tree.IterativeContains(tree.Root, y))
            {
                t = tree.IterativeLCA(tree.Root, x, y);
                Console.WriteLine($"Iterative: {tree.Print(x, y, t.Data)}");
            }
            #endregion

            #region Binary Tree Construction
            /*
             Parent Node = [i]
             Left Child = [2*i+1]
             Right Child = [2*i+2]
             */
            tree = new BinaryTree();
            int[] data = new int[] { 1, 2, 3, 4, 5, 6, 6, 6, 6 };
            tree.Root = tree.InsertLevelOrder(data, tree.Root, 0);
            tree.PrintInOrder(tree.Root);
            tree.PrintLine();

            /*
             ROOT => data[i] = -1
             */
            tree = new BinaryTree();
            data = new int[] { -1, 0, 0, 1, 1, 3, 5 };
            tree.CreateTree(data, data.Length);
            Console.WriteLine("Inorder traversal of constructed tree ");
            tree.PrintInOrder(tree.Root);
            tree.PrintLine();

            data = new int[] { 1, 3, 1, -1, 3 };
            tree.CreateTree(data, data.Length);
            Console.WriteLine("Inorder traversal of constructed tree ");
            tree.PrintInOrder(tree.Root);
            tree.PrintLine();
            #endregion

            tree = new BinaryTree(new Node(1));
            tree.Root.Left = new Node(2);
            tree.Root.Right = new Node(3);
            tree.Root.Left.Left = new Node(4);
            tree.Root.Left.Right = new Node(5);

            // Breadth First or Level Order Traversal
            tree.PrintLevelOrder(tree.Root, new Queue<Node>());
            tree.PrintLevelOrder(tree.Root);
        }
    }
}
