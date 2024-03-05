using System.Xml.Linq;
using System;

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

        //split the string in half (ask if this is the right way to do it)
        string firstHalf = S.Substring(0, Math.Floor(root.length / 2) - 1);
        string secondHalf = S.Substring(Math.Floor(root.length / 2) - 1, root.length - 1);

        root.left = Build(firstHalf, 0, Math.Floor(root.length / 2));

        root.right = Build(secondHalf, Math.Floor(root.length / 2), root.length);


    }//: Create a balanced rope from a given string S (5 marks).


    private Node Build(string s, int i, int j)
    {
        if (j - i <= 10) 
        {
            return new Node(j - i, s);
        }

        Node n = new Node(j - i);

        string firstHalf = s.Substring(0, Math.Floor(n.length / 2) - 1);
        string secondHalf = s.Substring(Math.Floor(n.length / 2) - 1, n.length - 1);

        n.left = Build(firstHalf, i, Math.Floor(j/2));
        n.right = Build(secondHalf, Math.Floor(i/2), j);

        n.length = n.left.length + n.right.length;

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

    } //: Return the substring S[i, j] (6 marks).
    public int Find(string S)
    {

    }//: Return the index of the first occurrence of S; -1 otherwise(9 marks).
    
    public char CharAt(int i)  //: Return the character at index i (3 marks).
    {
        Node current = root;
        int currIndex = i

        while (current.length > 10)
        {
            if (i >current.left.length)
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

    }//: Return the index of the first occurrence of character c (4 marks).

    public void Reverse()
    {

    }//: Reverse the string represented by the current rope (5 marks).

    public int Length()
    {
        return root.length;

    }//: Return the length of the string (1 mark).

    public string ToString()
    {

    }//: Return the string represented by the current rope (4 marks).
    
    public void PrintRope()
    {

    }//: Print the augmented binary tree of the current rope (4 marks).



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

    } //: Split the rope with root p at index i and return the root of the right subtree(9 marks).
    
    private Node Rebalance()
    {

    }//: Rebalance the rope  using the algorithm found on pages 1319-1320 of Boehm et al.(9 marks).
}