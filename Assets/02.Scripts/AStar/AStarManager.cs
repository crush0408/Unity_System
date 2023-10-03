using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarManager : Singleton<AStarManager>
{
    private List<NodeBlock> nodeBlockList = new List<NodeBlock>();
    private Board board;

    [Header("맵 설정")]
    public GameObject wayRoot;
    public NodeBlock nodeBlockPrefab;
    public int Width;
    public int Height;
    public const float NodeInterval = 1f;
    public MovingEntity movingEntity;
    [SerializeField]
    private LayerMask nodeLayer;

    [SerializeField]
    private NodeBlock currentNode;



    private List<Node> allNodeList = new List<Node>(); // Node 리스트
    private Dictionary<short, Node> nodeDict = new Dictionary<short, Node>();

    private void Start() // Play
    {
        // 임시 맵 생성
        for (int x = -Width/2; x <= Width/2; x++)
        {
            for (int y = -Height/2; y <= Height/2; y++)
            {
                NodeBlock nodeBlock = Instantiate(nodeBlockPrefab, wayRoot.transform);
                nodeBlock.transform.position = new Vector3(x, y, 0);

                if (x == 0 && y == 0) // 정 가운데 블럭 시작 블럭 지정
                {
                    currentNode = nodeBlock;
                    // 움직임 객체 초기화
                    movingEntity.Init(currentNode);
                }
            }
        }
        // 레이어 세팅
        nodeLayer.value = ( 1 << nodeBlockPrefab.gameObject.layer); // Convert layer to LayerMask

        // WayRoot에서 길 가져오기
        nodeBlockList = wayRoot.GetComponentsInChildren<NodeBlock>().ToList();
        

        // 길 블록으로 노드 리스트, 노드 딕셔너리 구성
        foreach (var nodeBlock in nodeBlockList)
        {
            Node node = new Node
                (nodeBlock.transform.position.x, nodeBlock.transform.position.y, (short)(allNodeList.Count + 1), nodeBlock.isWall);
            nodeBlock.node = node;
            allNodeList.Add(node);
            nodeDict.Add(node.id, node);
        }

        board = new Board(NodeInterval, allNodeList, nodeDict);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            NodeBlock nodeBlock = null;
            if(CheckNodeBlock(Input.mousePosition, ref nodeBlock))
            {
                movingEntity.Move(() => Debug.Log("도착"), nodeBlock, null);
            }
        }
    }

    private bool CheckNodeBlock(Vector3 inputPos, ref NodeBlock nodeBlock) // Check Node Block
    {
        inputPos = Camera.main.ScreenToWorldPoint(inputPos);
        Collider2D col = Physics2D.OverlapBox(inputPos, Vector2.one * 0.1f, 0f, nodeLayer.value);
        if(col != null)
        {
            NodeBlock colBlock = col.gameObject.GetComponent<NodeBlock>();
            if(colBlock != null)
            {
                nodeBlock = colBlock;
                return true;
            }
        }
        nodeBlock = null;
        return false;
    }

    public static List<NodeBlock> PathFinding(NodeBlock start, NodeBlock dest)
    {
        List<Node> wayNodeList = AStar.PathFinding(Instance.board, start.node, dest.node);

        List<NodeBlock> wayList = new List<NodeBlock>();
        for (int i = 0; i < wayNodeList.Count; i++)
        {
            wayList.Add(GetNodeBlock(wayNodeList[i]));
        }


        return wayList;
    }
    public static NodeBlock GetNodeBlock(Node node)
    {
        return Instance.nodeBlockList.ToList().Find(x => x.node.id == node.id);
    }
}
