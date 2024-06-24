using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinningWeapon : MonoBehaviour
{
    public GameObject prefab; // 생성할 프리팹을 설정하는 변수
    public SOSkill SOSkill; // 스킬에 관련된 정보를 가지고 있는 ScriptableObject
    public Image imgIcon;

    // Cooldown 이미지
    public Image imgCool;
    public float movementDuration = 0.2f; // 프리팹이 목표 위치까지 이동하는 데 걸리는 시간을 설정하는 변수
    public float maxScaleXZ = 2f; // 프리팹의 x와 z 스케일의 최대값을 설정하는 변수
    public float farDistance = 0.1f; // 두 프리팹 사이의 거리

    private bool isCoolingDown = false; // 쿨다운 중인지 여부를 나타내는 변수
    private List<GameObject> instantiatedPrefabs = new List<GameObject>(); // 생성된 프리팹을 저장하는 리스트

    private void Update()
    {
        // "Standard" 키가 눌렸을 때 실행하고 쿨다운 중이 아닌 경우에만 실행
        if (Input.GetKeyDown(KeyCode.Z) && !isCoolingDown)
        {
            // 쿨다운 시작
            StartCoroutine(CoolDown());
            // 레이를 쏘아 목표 방향을 설정
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // 플레이어 오브젝트의 회전 각도 계산
                Quaternion lookTarget = Quaternion.LookRotation(hit.point - transform.position);
                // 플레이어를 목표 방향으로 즉시 회전
                //transform.rotation = lookTarget;

                // 플레이어 위치의 오른쪽으로 farDistance 거리만큼 떨어진 위치 계산
                Vector3 sidePos1 = transform.position;
                // 오른쪽의 프리팹을 생성하고 이동시키는 코루틴 실행
                StartCoroutine(MovePrefab(prefab, sidePos1, sidePos1 + (hit.point - transform.position).normalized * SOSkill.SkillDistance));

                // movementDuration초 후에 실행되는 함수 호출
                //StartCoroutine(DelayedPrefabCreation(hit, transform.position));
            }
        }
    }
    /*
    // 1초 후에 실행되며 왼쪽의 프리팹을 생성하고 이동시키는 함수
    IEnumerator DelayedPrefabCreation(RaycastHit hit, Vector3 playerPosition)
    {
        // movementDuration초 대기
        yield return new WaitForSeconds(movementDuration);

        // 플레이어 위치의 왼쪽으로 farDistance 거리만큼 떨어진 위치 계산
        Vector3 sidePos2 = playerPosition - transform.right * farDistance;
        // 왼쪽의 프리팹을 생성하고 이동시키는 코루틴 실행
        StartCoroutine(MovePrefab(prefab, sidePos2, sidePos2 + (hit.point - playerPosition).normalized * SOSkill.SkillDistance));

        // 스킬 지속 시간 후에 모든 프리팹을 삭제하는 코루틴 실행
        StartCoroutine(DestroyAllPrefabsAfterDuration(SOSkill.SkillDuration));
    }
    */
    // 프리팹을 이동시키는 함수
    IEnumerator MovePrefab(GameObject prefab, Vector3 startPos, Vector3 endPos)
    {
        // 프리팹을 시작 위치에 생성
        GameObject instance = Instantiate(prefab, startPos, Quaternion.identity);
        instantiatedPrefabs.Add(instance); // 리스트에 추가

        // 경과 시간 초기화
        float elapsedTime = 0f;

        // 이동이 완료될 때까지 반복
        while (elapsedTime < movementDuration)
        {
            // 경과 시간에 따른 이동 비율 계산
            float t = elapsedTime / movementDuration;
            // 프리팹을 목적지로 이동
            instance.transform.position = Vector3.Lerp(startPos, endPos, t * 3f);

            // x와 z 스케일 값을 서서히 키움
            //float scaleXZ = Mathf.Lerp(1f, maxScaleXZ, t);
            //instance.transform.localScale = new Vector3(scaleXZ, instance.transform.localScale.y, scaleXZ);

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(DestroyAllPrefabsAfterDuration());
    }

    // 스킬 지속 시간 후에 모든 프리팹을 삭제하는 함수
    IEnumerator DestroyAllPrefabsAfterDuration()
    {
        Debug.Log("삭제");
        // 스킬 지속 시간 대기
        //yield return new WaitForSeconds(duration);

        // 모든 프리팹 삭제
        foreach (GameObject instance in instantiatedPrefabs)
        {
            Destroy(instance);
        }

        // 리스트 초기화
        instantiatedPrefabs.Clear();

        // 쿨다운 시작
        StartCoroutine(CoolDown());
        yield return null;
    }

    // 쿨다운을 처리하는 코루틴 함수
    IEnumerator CoolDown()
    {
        // 쿨다운 중 플래그 설정
        isCoolingDown = true;

        // 쿨다운 기간만큼 대기
        yield return new WaitForSeconds(SOSkill.Cooltime);

        // 쿨다운 종료 후 플래그 해제
        isCoolingDown = false;
    }
}
