using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTreeLeftNodes : MonoBehaviour
{
    public class Node
    {
        public int val;
        public Node left;
        public Node right;
    }
    
    void DisplayTreeAllLeftNodes(Node node)
    {
        if (node == null)
            return;
        
        var result = new List<int>();
        var queue = new Queue<Node>();
        queue.Enqueue(node);
        bool first = true;
        while (queue.Count > 0)
        {
            first = true;
            int lastLayerCount = queue.Count;
            while (lastLayerCount > 0)
            {
                var otherNode = queue.Dequeue();
                lastLayerCount--;

                if (first)
                {
                    first = false;
                    result.Add(otherNode.val);
                }

                if (otherNode.left != null)
                {
                    queue.Enqueue(otherNode.left);
                }

                if (otherNode.right != null)
                {
                    queue.Enqueue(otherNode.right);
                }
            }

        }
        

        for (int i = 0; i < result.Count; i++)
        {
            Debug.Log(result[i]);
        }
    }

    #region Test
    private void Start()
    {
        var root = new Node();
        root.val = 2;
        root.left = new Node();
        root.left.val = 11;
        root.right = new Node();
        root.right.val = 23;

        var left = root.left;
        left.left = new Node();
        left.left.val = 10;
        left.right = new Node();
        left.right.val = 15;
        
        var right = root.right;
        right.left = new Node();
        right.left.val = 7;
        right.right = new Node();
        right.right.val = 14;
        
        left = right.left;
        left.right = new Node();
        left.right.val = 12;
        right = left.right;
        right.left = new Node();
        right.left.val = 13;

        DisplayTreeAllLeftNodes(root);
    }
    #endregion
    
}