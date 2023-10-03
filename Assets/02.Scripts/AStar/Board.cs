using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable] // 하이라키에서 보고 싶다면 주석 제거
public class Board
{
    private readonly float nodeInterval = 1f;
    
    private List<Node> allNodeList = new List<Node>();

    private Dictionary<short, Node> nodeDict = new Dictionary<short, Node>(); // 해시맵 구성

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
    public bool Exists(float x, float y, ref short id) // 해당 위치 노드 체크
    {
        foreach (Node node in allNodeList)
        {
            if(node.x == x && node.y == y)
            {
                // 있다면 해당 노드의 id 반환
                id = node.id;
                return true;
            }
        }
        // 없다면 id = -1 반환
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
        // 주위 8방향 가져오기

        // Straight (직선)
        if(Exists(target.x + nodeInterval, target.y, ref id)) // 참조 형식 활용
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
