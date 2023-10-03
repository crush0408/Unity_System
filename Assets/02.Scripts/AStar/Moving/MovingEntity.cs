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
    protected IEnumerator movingCoroutine; // 무빙 코루틴 객체


    public virtual void Init(NodeBlock nodeBlock)
    {
        currentNode = nodeBlock;
        moveSpeed = 2f;
    }
    protected virtual void Reset()
    {
        if(movingCoroutine != null)
        {
            StopCoroutine(movingCoroutine);
            movingCoroutine = null;
        }
    }
    public void Move(Action _arriveAction, NodeBlock _node, Action _movingAction) // 이동
    {
        if(movingCoroutine == null)
        {
            destNode = _node;
            arrveAction = _arriveAction;
            movingAction = _movingAction;

            movingCoroutine = MovingCoroutine();
            StartCoroutine(movingCoroutine);
        }
        else
        {
            Debug.Log("해당 객체가 이미 움직이는 중입니다");
        }
    }
    protected IEnumerator MovingCoroutine()
    {
        myWay = AStarManager.PathFinding(currentNode, destNode);
        currentWayIndex = 0;
        totalWayIndex = myWay.Count;

        // 이동
        while (currentWayIndex < totalWayIndex)
        {
            movingAction?.Invoke();
            if(CheckWayPoint())
            {
                currentWayIndex++;
                currentNode = myWay[Mathf.Clamp(currentWayIndex, 0, totalWayIndex - 1)];

            }

            if(currentWayIndex < totalWayIndex)
            {
                moveDir = (myWay[currentWayIndex].transform.position - transform.position).normalized;
                transform.position += moveDir * (moveSpeed * Time.deltaTime);
            }
            else
            {
                moveDir = Vector3.zero;
                float smoothTime = currentCheckDistance / moveSpeed; // 보간 시간
                float elapsedTime = 0;
                Vector3 startPosition = transform.position;
                while (elapsedTime < smoothTime)
                {
                    transform.position
                        = Vector3.Lerp(startPosition, myWay[totalWayIndex - 1].transform.position, Mathf.Clamp01(elapsedTime / smoothTime));
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                transform.position = myWay[totalWayIndex - 1].transform.position; // 마지막 블록 pos 반환
            }
            yield return null;
        }

        currentNode = myWay[totalWayIndex - 1];
        myWay.Clear();
        movingCoroutine = null;
        arrveAction?.Invoke();
    }

    protected bool CheckWayPoint()
    {
        currentCheckDistance = moveSpeed * Time.deltaTime;
        return SUtil.CheckDistance(transform.position, myWay[currentWayIndex].transform.position, currentCheckDistance);
    }
}
