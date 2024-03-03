using System.Xml.Linq;
using System;

public class Rope {

    private class Node<T> where T : IComparable
    {
        // Read/write properties

        public string s { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }

        public Node(T item)
        {
            Item = item;
            Left = Right = null;
        }
    }

    private Node root { get; set; }

    public Rope(string S)
    {

    }//: Create a balanced rope from a given string S (5 marks).
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

    }
    public int IndexOf(char c)
    {

    }//: Return the index of the first occurrence of character c (4 marks).
    public void Reverse()
    {

    }//: Reverse the string represented by the current rope (5 marks).
    public int Length()
    {

    }//: Return the length of the string (1 mark).
    public string ToString()
    {

    }//: Return the string represented by the current rope (4 marks).
    public void PrintRope()
    {

    }//: Print the augmented binary tree of the current rope (4 marks).
    //The public methods are strongly supported by the following (and indispensible) private methods.
    private Node Build(string s, int i, int j)
    {

    } //: Recursively build a balanced rope for S[i, j] and return its root
    //(part of the constructor).
    private Node Concatenate(Node p, Node q)  //: Return the root of the rope constructed by concatenating tworopes with roots p and q (3 marks).
    {
        
    }
    private Node Split(Node p, int i)
    {

    } //: Split the rope with root p at index i and return the root of the right subtree(9 marks).
    private Node Rebalance()
    {

    }//: Rebalance the rope  using the algorithm found on pages 1319-1320 of Boehm et al.(9 marks).
}