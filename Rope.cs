using System.Xml.Linq;
using System;
using System.Collections;
using System.Reflection.Emit;

namespace Assn2 {
    public class Rope {


        private class Node
        {
            // Read/write properties

            public string s { get; set; } //only for leaf nodes
            public int length { get; set; } //augmented data
            public Node left { get; set; }
            public Node right { get; set; }

            public Node(int len, string str = null)
            {
                length = len;
                s = str;
            }
        }

        private Node root { get; set; }

        // Rope(string S)
        // Purpose: Rope is the contructor for data types Rope. Creates a
        // balanced Rope from a given string.
        public Rope(string S)
        {
            root = new Node(S.Length);
            double floored = root.length / 2;

            //split the string in half (ask if this is the right way to do it)
            string firstHalf = S.Substring(0, Convert.ToInt32(Math.Floor(floored)));
            string secondHalf = S.Substring(Convert.ToInt32(Math.Floor(floored)));

            root.left = Build(firstHalf, 0, Convert.ToInt32(Math.Floor(floored)));

            root.right = Build(secondHalf, Convert.ToInt32(Math.Floor(floored)), root.length);
        }

        // Build(string s, int i, int j)
        // Purpose: Build recursively constructs a balanced rope for S[i, j]
        // and returns the root of the tree.
        private Node Build(string s, int i, int j)
        {
            if (j - i <= 10)
            {
                return new Node(j - i, s);
            }

            Node n = new Node(j - i);

            double floored = n.length / 2;

            string firstHalf = s.Substring(0, Convert.ToInt32(Math.Floor(floored)));
            string secondHalf = s.Substring(Convert.ToInt32(Math.Floor(floored)));

            n.left = Build(firstHalf, 0, Convert.ToInt32(Math.Floor(floored)));
            n.right = Build(secondHalf, Convert.ToInt32(Math.Floor(floored)), n.length);

            return n;
        }

        // Insert(string sequence, int i)
        // Purpose: Inserts a string (sequence) at index i of the Rope by
        // splitting the tree and concatenating the pieces in proper order.
        public void Insert(string sequence, int i)
        {
            if (i < 0 || i > root.length)
            {
                Console.WriteLine("Invalid index for insertion.");
                return;
            }

            // Split at index i
            var splitResult = Split(root, i);
            Rebalance();

            // Concatenate the left part with the inserted sequence and then concatenate with the right part
            Node newNode = Concatenate(new Node(sequence.Length, sequence), splitResult);
            this.root = Concatenate(this.root, newNode);
        }

        // Delete(int i, int j)
        // Purpose: Deletes the substring from index i to index j by splitting
        // the tree twice and concatenating the first third and last third together.
        public void Delete(int i, int j)
        {
            Node secondThird = Split(this.root, i - 1);
            Rebalance();
            Node thirdThird = Split(secondThird, j);
            Rebalance();
            this.root = Concatenate(this.root, thirdThird);
        }

        // Substring(int i, int j)
        // Purpose: Returns the substring of the rope from index i to index j
        // by splitting the tree twice and taking the second third of the original rope.
        public string Substring(int i, int j)
        {
            Node secondThird = Split(this.root, i);
            Rebalance();
            Node thirdThird = Split(secondThird, j);
            Rebalance();

            return thirdThird.ToString();
        }

        // Find(string s)
        // Purpose: Returns the index of the first occurence of a substring in
        // the Rope otherwise it returns -1.
        public int Find(string S)
        {
            char[] toFind = S.ToCharArray();
            List<int> indices = new List<int>();
            Stack<Node> stacky = new Stack<Node>();
            int currIndex = 0;

            stacky.Push(root);

            while (stacky.Count > 0)
            {
                Node curr = stacky.Pop();

                if (curr.s == null)
                {
                    int leftLength = curr.left != null ? curr.left.length : 0;

                    if (currIndex < leftLength)
                    {
                        stacky.Push(curr.right);
                        stacky.Push(curr.left);
                    }
                    else
                    {
                        currIndex -= leftLength;
                        stacky.Push(curr.right);
                    }
                }
                else
                {
                    int i;
                    for (i = 0; i < curr.length; i++)
                    {
                        if (curr.s[i] == toFind[0])
                        {
                            indices.Add(currIndex + i);
                        }
                    }
                    currIndex += i;
                }
            }

            for (int i = 0; i < indices.Count; i++)
            {
                int foundCount = 1;
                for (int j = 1; j < toFind.Length; j++)
                {
                    if (CharAt(indices[i] + j) == toFind[j])
                    {
                        foundCount++;
                        if (foundCount == toFind.Length)
                        {
                            return indices[i];
                        }
                    }
                }
            }
            return -1;
        }

        // CharAt(int i)
        // Purpose: Returns the char at index i of the Rope otherwise returns a
        // null terminator if the index is not in the rope.
        public char CharAt(int i)
        {
            Node current = root;
            int currIndex = i;

            while (current.s == null)
            {
                int leftLength = current.left != null ? current.left.length : 0;

                if (currIndex < leftLength)
                {
                    current = current.left;
                }
                else
                {
                    currIndex -= leftLength;
                    current = current.right;
                }
            }
            if (currIndex < 0 || currIndex >= current.length)
            {
                Console.WriteLine("This index is not within the bounds of the string.");
                return '\0';
            }
            return current.s[currIndex];
        }

        // IndexOf(char c)
        // Purpose: Returns the index of the first occurence of a character c,
        // otherwise returns -1 if the char is not in the Rope.
        public int IndexOf(char c)
        {
            Stack<Node> stacky = new Stack<Node>();
            int currIndex = 0;

            stacky.Push(root);
            Node curr = stacky.Pop();

            while (curr  != null )
            {
                if (curr.s == null)
                {
                    stacky.Push(curr.right);
                    stacky.Push(curr.left);
                }
                else
                {
                    int i;
                    for (i=0; i < curr.length; i++)
                    {
                        if (curr.s[i] == c)
                        {
                            return currIndex + i;
                        }
                    }
                    currIndex += i;
                }
                if (stacky.Count == 0)
                {
                    Console.WriteLine("This character was not found in the array.");
                    return -1;
                }
                else
                {
                    curr = stacky.Pop();
                }
            }
            return -1;
        }

        // Reverse()
        // Purpose: Takes a Rope as input and augments the Rope to represent
        // the reverse of its string.
        public void Reverse()
        {
            // Create a stack and push the root onto the top
            Stack<Node> stacky = new Stack<Node>();
            Node temp;
            Node curr;

            stacky.Push(root);

            // Traverse the entire tree from right to left and swaps the leaf nodes.
            while (stacky.Count > 0)
            {
                curr = stacky.Pop();

                if (curr.s == null)
                {
                    temp = curr.left;
                    curr.left = curr.right;
                    curr.right = temp;

                    stacky.Push(curr.left);
                    stacky.Push(curr.right);
                }
                else
                {
                    string newStr = "";
                    for (int i = curr.s.Length - 1; i > -1; i--)
                    {
                        newStr += curr.s[i];
                    }
                    curr.s = newStr;
                }
            }
            return;
        }

        // Length()
        // Purpose: Returns the total length of the string by accessing the
        // length of the root node.
        public int Length()
        {
            return root.length;
        }

        // ToString()
        // Purpose: Returns the string representation of the Rope by traversing
        // the tree right to left and addign every leaf node to a stack.
        public string ToString()
        {
            Stack<Node> stacky = new Stack<Node>();
            string toReturn = "";

            stacky.Push(root);

            Node curr;

            while (stacky.Count > 0 )
            {
                curr = stacky.Pop();
                if (curr.s == null)
                {
                    stacky.Push(curr.right);
                    stacky.Push(curr.left);
                }
                else
                {
                    toReturn += curr.s;
                }
            }
            return toReturn;
        }

        // PrintRope()
        // Purpose: Wrapper function of PrintRope(Node n, int index) which
        // prints the augmented binary tree representation of the current Rope.
        public void PrintRope()
        {
            PrintRope(root, 0);
        }
        
        // PrintRope(Node n, int index)
        // Purpose: Prints the augmented binary tree representation of the
        // current Rope.
        private void PrintRope(Node n, int index)
        {
            if (n != null)
            {
                PrintRope(n.right, index + 8);
                if (n.s == null)
                {
                    Console.WriteLine(new String(' ', index) + n.length.ToString() + " ");
                }
                else
                {
                    Console.WriteLine(new String(' ', index) + n.s + " ");
                }
                PrintRope(n.left, index + 8);
            }
        }
        
        // Concatenate(Node p, Node q)
        // Purpose: Returns a new Node which has a left property equivalent
        // to p, and a right property equivalent to q.
        private Node Concatenate(Node p, Node q)
        {
            Node n = new Node(p.length + q.length);
            n.right = q;
            n.left = p;

            return n;

        }

        // Split(Node p, int i)
        // Purpose: Split the rope with root p at index i and return the root
        // of the right subtree.
        private Node Split(Node p, int i)
        {
            // Condition of finding the node at index i;
            if (p.s != null)
            {
                // Splits the substring at index i and sets the children to be the substrings.
                string firstHalf = p.s.Substring(0, i);
                string secondHalf = p.s.Substring(i, p.length - i);
                Node left = new Node(i, firstHalf);
                Node right = new Node(p.length - i, secondHalf);
                // Creates and returns a parent node for the new subtrees.
                Node newNode = Concatenate(left, right);
                return newNode;
            }
            // Condition of searching the right child to find the node at index i.
            if (i > p.left.length)
            {
                // Recurses the right subtree to find node p.
                Node next = Split(p.right, i - p.left.length);
                // Split the current node by augmenting its data and creating a newNode.
                Node newNode = new Node(next.right.length);
                newNode.right = next.right;
                newNode.left = p.right.left;
                next.right = null;
                p.right = next.left;
                // Returns the newly created Node that is split off.
                return newNode;
            }
            // Condition of searching the left child to find the node at index i.
            else if (i < p.length)
            {
                // Recurses the left subtree to find Node p.
                Node next = Split(p.left, i);
                // Split the current node by augmenting its data and creating a newNode.
                Node newNode = new Node(next.right.length);
                newNode.left = next.left;
                newNode.right = p.right;
                newNode.length += newNode.right.length;

                p.right = null;
                p.length = p.left.length;
                // Returns the augmented current node.
                return next;
            }
            // Returns the final root of the second tree.
            // Sidenote: the original tree is now also edited to reflect the
            // split, so there are now two unique trees.
            return p;
        }

        private Node Rebalance()
        {
            // algorithm is add nodes to a rabalance array, where a node at index n in the 
            // rebalance array has Fibonacci(n+1) to Fibonacci(n+2) items, and the entire list has the nodes
            // from largest to smallest and therefore left to right concatenated 

            // create the aarray of nodes with length of the fibonacci numbers
            int slots = 0;
            List<Node> fibonacci = new List<Node>();
            fibonacci.Add(new Node(1));
            fibonacci.Add(new Node(2));
            while (true)
            {
                if (root.length < fibonacci[slots].length)
                {
                    break;
                }
                slots++;
                if(slots < 2)
                {
                    /*already added first two fibonacci numbers, 
                     * since algorithm uses fibonacci sequence that starts at 
                     * the second fibonacci number and therefore isn't calculated the
                     * same way ie. 1, 2, 3, 5, ... instead of 1, 1, 2, 3, 5, ...
                     */
                    continue;
                }
                fibonacci[slots] = new Node(fibonacci[slots - 1].length + fibonacci[slots - 2].length);

            }

            Node[] rebalanceArray = new Node[slots];
            Node curr;
            int i = 0;
            Stack<Node> stacky = new Stack<Node>();
            stacky.Push(root);

            // Add all the nodes to the rebalance array
            while (stacky.Count > 0)
            {
                curr = stacky.Pop();
                if (curr.s == null)
                {
                    stacky.Push(curr.right);
                    stacky.Push(curr.left);
                }
                else
                {
                    rebalanceArray[i] = curr;
                }

            }

            Node result = null;
            // Concatinate all the node in the rebalance array.
            for (int slot = 0; slot < slots; ++slot)
            {
                Node n = rebalanceArray[slot];
                if (n != null)
                {
                    if(result == null) result = n; //track the leftmost root node

                    for (int fib = 0; fib < slots; ++fib) //loop through to find the balanced spot for current node
                    {
                        if(n.length >= fibonacci[fib].length && n.length < fibonacci[fib+1].length) // if the length of result is in the interval of the current fibonacci number and the next one
                        {
                            if(fibonacci[fib].left==null) // if no other node is currently at that length
                            {
                                fibonacci[fib].left = n;
                                break;
                            }
                            else //otherwise compress the nodes up to the next free fibonacci length
                            {
                                while(fib<slots) //continue looping through the fibonacci numbers, compressing until there's a free spot
                                {
                                    if (fibonacci[fib].left!=null)
                                    {
                                        n = Concatenate(fibonacci[fib].left, n); //compress, maintaining left to right order
                                        fibonacci[fib].left = null; //reset reference
                                        fib++; //iterate
                                    }
                                    else //otherwise node has found its place
                                    {
                                        fibonacci[fib].left=n;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }    
                }
            }

            for (int c = slots; c > 0; --c) // loop one last time (left to right)to concatenate all the smaller nodes in the array
            {
                    if (fibonacci[c].left != null)
                    {
                        result = Concatenate(result, fibonacci[c].left); //compress
                    }
             }



            root = result;

            return null;

        }//: Rebalance the rope  using the algorithm found on pages 1319-1320 of Boehm et al.(9 marks).
    }
}
