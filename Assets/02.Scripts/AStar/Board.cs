using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private readonly float nodeInterval = 1f;

    private List<Node> allNodeList = new List<Node>();

    private Dictionary<short, Node> nodeDict = new Dictionary<short, Node>();

    public Board() { }
    public Board(in float _nodeInterval, List<Node> _allNodeList, Dictionary<short, Node> _nodeDict)
    {
        this.nodeInterval = _nodeInterval;
        this.allNodeList = _allNodeList;
        this.nodeDict = _nodeDict;
    }


    // Check Node
    public bool Exists(Node node)
    {
        return Exists(node.x, node.y);
    }
    public bool Exists(float x, float y)
    {
        foreach (Node node in allNodeList)
        {
            if(node.x == x && node.y == y)
            {
                return true;
            }
        }
        return false;
    }
    public bool Exists(float x, float y, ref short id)
    {
        foreach (Node node in allNodeList)
        {
            if(node.x == x && node.y == y)
            {
                id = node.id;
                return true;
            }
        }
        id = -1;
        return false;
    }

    // GetNode
    public Node GetNode(float x, float y)
    {
        foreach (Node node in allNodeList)
        {
            if(node.x == x && node.y == y)
            {
                return node;
            }
        }

        Debug.Log("Can't Get Node : " + "x(" + x.ToString() +"), y("+ y.ToString() + ")");
        return null;
    }

    public List<Node> GetAroundNodes(Node target)
    {
        List<Node> aroundNodes = new List<Node>();
        short id = -1;

        //[-1,-1] [ 0,-1] [ 1,-1]
        //[-1, 0]         [ 1, 0]
        //[-1, 1] [ 0, 1] [ 1, 1]


        /*
        // Example 1
        // 성능 관련 이슈 체크 필요 => 성능 상 같으나, 가독성 측면에서 예제2가 더 우수함
        for (float x = -nodeInterval; x <= nodeInterval; x += nodeInterval)
        {
            for (float y = -nodeInterval; y <= nodeInterval; y += nodeInterval)
            {
                if (x == 0 && y == 0) continue; // Target Node Exception

                if(Exists(target.x + x, target.y + y, ref id))
                {
                    aroundNodes.Add(nodeDict[id]);
                }
            }
        }
        */



        // Example 2
        // Straight (직선)
        if(Exists(target.x + nodeInterval, target.y, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        if (Exists(target.x - nodeInterval, target.y, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        if (Exists(target.x, target.y + nodeInterval, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        if (Exists(target.x , target.y - nodeInterval, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        // Diagonal (대각선)
        if(Exists(target.x + nodeInterval, target.y + nodeInterval, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        if (Exists(target.x - nodeInterval, target.y - nodeInterval, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        if (Exists(target.x + nodeInterval, target.y - nodeInterval, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }
        if (Exists(target.x - nodeInterval, target.y + nodeInterval, ref id))
        {
            aroundNodes.Add(nodeDict[id]);
        }

        return aroundNodes;
    }
}
