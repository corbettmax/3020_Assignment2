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

        public Rope(string S)
        {
            root = new Node(S.Length);
            double floored = root.length / 2;

            //split the string in half (ask if this is the right way to do it)
            string firstHalf = S.Substring(0, Convert.ToInt32(Math.Floor(floored)));
            string secondHalf = S.Substring(Convert.ToInt32(Math.Floor(floored)));

            root.left = Build(firstHalf, 0, Convert.ToInt32(Math.Floor(floored)));

            root.right = Build(secondHalf, Convert.ToInt32(Math.Floor(floored)), root.length);


        }//: Create a balanced rope from a given string S (5 marks).


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

        } //: Recursively build a balanced rope for S[i, j] and return its root
          //(part of the constructor).


        public void Insert(string S, int i)
        {

        }//: Insert string S at index i (5 marks).
        public void Delete(int i, int j)
        {

        } //: Delete the substring S[i, j] (5 marks).

        public string Substring(int i, int j)
        {
            return "";
        } //: Return the substring S[i, j] (6 marks).

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
        }//: Return the index of the first occurrence of S; -1 otherwise(9 marks).

        public char CharAt(int i)  //: Return the character at index i (3 marks).
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

            return current.s[currIndex];
        }
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
                curr = stacky.Pop();
            }

            return -1;


        }//: Return the index of the first occurrence of character c (4 marks).

        public void Reverse()
        {
            Stack<Node> stacky = new Stack<Node>();
            Node temp;
            Node curr;

            stacky.Push(root);

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

        }//: Reverse the string represented by the current rope (5 marks).

        public int Length()
        {
            return root.length;

        }//: Return the length of the string (1 mark).

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

        }//: Return the string represented by the current rope (4 marks).

        public void PrintRope()
        {
            PrintRope(root, 0);

        }//: Print the augmented binary tree of the current rope (4 marks).

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



        //The public methods are strongly supported by the following (and indispensible) private methods.
        private Node Concatenate(Node p, Node q)  //: Return the root of the rope constructed by concatenating tworopes with roots p and q (3 marks).
        {
            Node n = new Node(p.length + q.length);
            n.right = q;
            n.left = p;

            return n;

        }

        private Node Split(Node p, int i)
        {
            return null;
        } //: Split the rope with root p at index i and return the root of the right subtree(9 marks).

        private Node Rebalance()
        {
            // The basic rebalancing algorithm is add nodes to a rabalance array, where a node at index K in the 
            // rebalance array has Fibonacci(K+1) to Fibonacci(K+2) items, and the entire list has the nodes
            // from largest to smallest concatenated.

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

            // Add all the nodes to the rebalance array.
            //AddNodeToRebalanceArray(rebalanceArray, root, false);

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