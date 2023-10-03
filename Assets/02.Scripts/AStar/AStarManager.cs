using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AStarManager : Singleton<AStarManager>
{
    private NodeBlock[] nodeBlockArray; // NodeBlock 오브젝트 배nodeBlockArray
    private Board board;

    [Header("맵 설정")]
    public GameObject wayRoot;
    public NodeBlock nodeBlockPrefab;
    public int Width;
    public int Height;
    public const float NodeInterval = 1f;
    [SerializeField]
    private LayerMask nodeLayer;

    [SerializeField]
    private NodeBlock startNode;



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
                    startNode = nodeBlock;
            }
        }
        // 레이어 세팅
        nodeLayer.value = ( 1 << nodeBlockPrefab.gameObject.layer); // Convert layer to LayerMask

        // WayRoot에서 길 가져오기
        nodeBlockArray = wayRoot.GetComponentsInChildren<NodeBlock>();
        

        // 길 블록으로 노드 리스트, 노드 딕셔너리 구성
        foreach (var nodeBlock in nodeBlockArray)
        {
            Node node = new Node
                (nodeBlock.transform.position.x, nodeBlock.transform.position.y, (short)(allNodeList.Count + 1), nodeBlock.isWall);
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
                //PathFinding(startNode, nodeBlock);
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

    public static List<Node> PathFinding(Node start, Node dest)
    {
        return AStar.PathFinding(Instance.board, start, dest);
    }
}
