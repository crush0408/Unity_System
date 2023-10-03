using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class AStar
{
    private static float GetSqrDistance(Node a, Node b) // Node용 거리 가져오기 함수
    {
        return Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2);
    }
    private static void SetCost(Node current, Node dest)
    {
        float g = current.parent.G + GetSqrDistance(current.parent, current); // prev G + parent to current G
        float h = GetSqrDistance(current, dest);
        current.SetPrice(g, h);
    }
    public static List<Node> PathFinding(Board board, Node start, Node dest)
    {
        if(board.Exists(start) && board.Exists(dest))
        {
            Dictionary<short, Node> openList = new Dictionary<short, Node>();
            Dictionary<short, Node> closeList = new Dictionary<short, Node>();

            Node current = start.Clone();
            current.SetPrice(0, 0);
            current.parent = null;

            if (start.id == dest.id) return new List<Node>() { start }; // if Start == End Exception

            while (current != null)
            {
                closeList.Add(current.id, current);

                if (current.id == dest.id) break; // If arrive dest, break;

                List<Node> aroundNodeList = board.GetAroundNodes(current); // GetAroundNodes

                for (int i = 0; i < aroundNodeList.Count; i++) // List 
                {
                    if (aroundNodeList[i].isWall && aroundNodeList[i].id != dest.id) continue; // if can't go node, not dest

                    if (!closeList.ContainsKey(aroundNodeList[i].id)) // Does not exists node at closeList 
                    {
                        aroundNodeList[i] = aroundNodeList[i].Clone();
                        aroundNodeList[i].parent = current;

                        SetCost(aroundNodeList[i], dest);

                        if (openList.ContainsKey(aroundNodeList[i].id)) // Input Open List
                        {
                            if (aroundNodeList[i].G < openList[aroundNodeList[i].id].G)
                            {
                                openList[aroundNodeList[i].id] = aroundNodeList[i];
                            }
                        }
                        else
                        {
                            openList.Add(aroundNodeList[i].id, aroundNodeList[i]);
                        }
                    }
                }

                float minFValue = float.MaxValue;
                short id = -100;
                foreach (Node node in openList.Values)
                {
                    if(node.id == dest.id)
                    {
                        id = node.id;
                        break;
                    }
                    else
                    {
                        if(node.F < minFValue)
                        {
                            minFValue = node.F;
                            id = node.id;
                        }
                    }
                }


                try
                {
                    current = openList[id];
                    openList.Remove(id);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                    Debug.Log($"id : {id}, openList Count : {openList.Count}, CurX : {current.x}, CurY : {current.y}");
                }
            }

            List<Node> pathNodeList = new List<Node>();
            pathNodeList.Add(closeList[dest.id]); // Add dest Node

            Node parent = pathNodeList[0].parent;
            while (parent != null)
            {
                pathNodeList.Add(parent);
                parent = parent.parent;
            }

            pathNodeList.Reverse();
            return pathNodeList;
        }
        Debug.Log("Not Exist Node Start Or Dest : " + "Start(" + board.Exists(start) + "), Dest(" + board.Exists(dest) + ")");
        return null;
    }
}