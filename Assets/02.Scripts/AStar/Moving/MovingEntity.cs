using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 움직임 관련 함수 모음
// AStar 활용하여 이동
public abstract class MovingEntity : MonoBehaviour
{
    // NodeBlock List ( 이 무빙 엔티티는 AStar를 활용해서 이동한다.)
    protected List<NodeBlock> myWay = new List<NodeBlock>(); // 이동 노드 리스트
    protected int currentWayIndex = 0; // 현재 이동 인덱스
    protected int totalWayIndex; // 이동 노드 전체 인덱스

    protected NodeBlock currentNode; // 현재 노드
    protected NodeBlock destNode; // 도착지 노드

    protected float moveSpeed; // 이동 속도
    protected Vector3 moveDir; // 이동 방향

    protected Action arrveAction; // 도착 액션
    protected Action movingAction; // 이동 액션

    protected float currentCheckDistance; // 현재 간격체크 거리

    protected virtual void MovingInit(NodeBlock nodeBlock)
    {

    }
}
