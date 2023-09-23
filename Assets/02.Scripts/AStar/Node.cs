using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public short id;

    public float x, y; // position

    public float F => G + H;

    public float G { get; private set; } = 0;
    public float H { get; private set; } = 0;

    public bool isWall = false;

    public Node parent = null;

    public Node() { }
    public Node(float x, float y, short id, bool isWall)
    {
        this.x = x;
        this.y = y;
        this.id = id;
        this.isWall = isWall;
    }
    public Node Clone()
    {
        Node clone = new Node(this.x, this.y, this.id, this.isWall);
        clone.G = G;
        clone.H = H;
        clone.parent = parent;
        return clone;
    }
    public void SetPrice(float g, float h)
    {
        this.G = g;
        this.H = h;
    }
    public void Clear()
    {
        isWall = false;
        G = 0;
        H = 0;
        id = 0;
        parent = null;
    }
}