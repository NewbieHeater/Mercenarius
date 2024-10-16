using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpearThrowState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;

    Queue<object> queue = new Queue<object>();
    float spearExistTime = 3f;
    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        Vector3 mousePosition = _playerController.CheckGround(Input.mousePosition);
        Vector3 SpearStartPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Vector3 SpearEndPosition = new Vector3(SpearStartPosition.x + (mousePosition-SpearStartPosition).normalized.x * 5, 0.5f, SpearStartPosition.z + (mousePosition-SpearStartPosition).normalized.z * 5);

        StartCoroutine(MovePrefab(SpearStartPosition, SpearEndPosition));
    }

    public void OperateUpdate(PlayerController sender)
    {
        
    }

    public void OperateExit(PlayerController sender)
    {

    }

    IEnumerator MovePrefab(Vector3 startPos, Vector3 endPos)
    {
        GameObject spear =
                    ObjectPooler.SpawnFromPool("SpearThrow", startPos);
        queue.Enqueue(spear);
        // 경과 시간 초기화
        float elapsedTime = 0f;

        // 이동이 완료될 때까지 반복
        while (elapsedTime < 0.2f)
        {
            // 경과 시간에 따른 이동 비율 계산
            float t = elapsedTime / 0.2f;
            // 프리팹을 목적지로 이동
            spear.transform.position = Vector3.Lerp(startPos, endPos, t);
            
            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(spearExistTime);
        queue.Dequeue();
        spear.SetActive(false);
    }
}
