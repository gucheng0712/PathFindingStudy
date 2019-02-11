using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Open = 0,
    Blocked = 1,
    LightTerrain = 2,
    MediumTerrain = 3,
    HeavyTerrain = 4
}

public class Node : IComparable<Node>
{
    public NodeType nodeType = NodeType.Open;

    // the position of the node
    public Vector3 position;
    public int xIndex = -1;
    public int yIndex = -1;

    public float distanceTraveled = Mathf.Infinity;
    public float priority; // the priority for finding when it as a neighbor

    public List<Node> neighbors = new List<Node>();

    public Node previous; // the previous node


    public Node(int xIndex, int yIndex, NodeType nodeType)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.nodeType = nodeType;
    }

    // To Compare the priority of two nodes, inherited from IComparable
    public int CompareTo(Node other)
    {
        if (priority < other.priority)
        {
            return -1;
        }
        else if (priority > other.priority)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // Reset the node
    public void Reset()
    {
        previous = null;
    }
}
