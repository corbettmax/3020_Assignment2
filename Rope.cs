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
            int len = S.Length;

            if (len > root.length) return -1;

            return -1;

        }//: Return the index of the first occurrence of S; -1 otherwise(9 marks).

        public char CharAt(int i)  //: Return the character at index i (3 marks).
        {
            Node current = root;
            int currIndex = i;
    
        while (current.length > 10)
            {
                if (i > current.left.length)
                {
                    currIndex = currIndex - current.left.length;
                    current = current.right;
                }
                else
                {
                    current = current.left;
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
            /*Stack<Node> stacky = new Stack<Node>();
            int depth = 1;
            int ab = 0;

            stacky.Push(root);

            Node curr;

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
                    
                    
                }

            }*/

            return null;
        }//: Rebalance the rope  using the algorithm found on pages 1319-1320 of Boehm et al.(9 marks).
    }
}