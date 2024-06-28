using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinningWeapon : MonoBehaviour
{
    public GameObject prefab; // ������ �������� �����ϴ� ����
    public SOSkill SOSkill; // ��ų�� ���õ� ������ ������ �ִ� ScriptableObject
    public Image imgIcon;

    // Cooldown �̹���
    public Image imgCool;
    public float movementDuration = 0.2f; // �������� ��ǥ ��ġ���� �̵��ϴ� �� �ɸ��� �ð��� �����ϴ� ����
    public float maxScaleXZ = 2f; // �������� x�� z �������� �ִ밪�� �����ϴ� ����
    public float farDistance = 0.1f; // �� ������ ������ �Ÿ�

    private bool isCoolingDown = false; // ��ٿ� ������ ���θ� ��Ÿ���� ����
    private List<GameObject> instantiatedPrefabs = new List<GameObject>(); // ������ �������� �����ϴ� ����Ʈ

    private void Update()
    {
        // "Standard" Ű�� ������ �� �����ϰ� ��ٿ� ���� �ƴ� ��쿡�� ����
        if (Input.GetKeyDown(KeyCode.Z) && !isCoolingDown)
        {
            // ��ٿ� ����
            StartCoroutine(CoolDown());
            // ���̸� ��� ��ǥ ������ ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // �÷��̾� ������Ʈ�� ȸ�� ���� ���
                Quaternion lookTarget = Quaternion.LookRotation(hit.point - transform.position);
                // �÷��̾ ��ǥ �������� ��� ȸ��
                //transform.rotation = lookTarget;

                // �÷��̾� ��ġ�� ���������� farDistance �Ÿ���ŭ ������ ��ġ ���
                Vector3 sidePos1 = transform.position;
                // �������� �������� �����ϰ� �̵���Ű�� �ڷ�ƾ ����
                StartCoroutine(MovePrefab(prefab, sidePos1, sidePos1 + (hit.point - transform.position).normalized * SOSkill.SkillDistance));

                // movementDuration�� �Ŀ� ����Ǵ� �Լ� ȣ��
                //StartCoroutine(DelayedPrefabCreation(hit, transform.position));
            }
        }
    }
    /*
    // 1�� �Ŀ� ����Ǹ� ������ �������� �����ϰ� �̵���Ű�� �Լ�
    IEnumerator DelayedPrefabCreation(RaycastHit hit, Vector3 playerPosition)
    {
        // movementDuration�� ���
        yield return new WaitForSeconds(movementDuration);

        // �÷��̾� ��ġ�� �������� farDistance �Ÿ���ŭ ������ ��ġ ���
        Vector3 sidePos2 = playerPosition - transform.right * farDistance;
        // ������ �������� �����ϰ� �̵���Ű�� �ڷ�ƾ ����
        StartCoroutine(MovePrefab(prefab, sidePos2, sidePos2 + (hit.point - playerPosition).normalized * SOSkill.SkillDistance));

        // ��ų ���� �ð� �Ŀ� ��� �������� �����ϴ� �ڷ�ƾ ����
        StartCoroutine(DestroyAllPrefabsAfterDuration(SOSkill.SkillDuration));
    }
    */
    // �������� �̵���Ű�� �Լ�
    IEnumerator MovePrefab(GameObject prefab, Vector3 startPos, Vector3 endPos)
    {
        // �������� ���� ��ġ�� ����
        GameObject instance = Instantiate(prefab, startPos, Quaternion.identity);
        instantiatedPrefabs.Add(instance); // ����Ʈ�� �߰�

        // ��� �ð� �ʱ�ȭ
        float elapsedTime = 0f;

        // �̵��� �Ϸ�� ������ �ݺ�
        while (elapsedTime < movementDuration)
        {
            // ��� �ð��� ���� �̵� ���� ���
            float t = elapsedTime / movementDuration;
            // �������� �������� �̵�
            instance.transform.position = Vector3.Lerp(startPos, endPos, t * 3f);

            // x�� z ������ ���� ������ Ű��
            //float scaleXZ = Mathf.Lerp(1f, maxScaleXZ, t);
            //instance.transform.localScale = new Vector3(scaleXZ, instance.transform.localScale.y, scaleXZ);

            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(DestroyAllPrefabsAfterDuration());
    }

    // ��ų ���� �ð� �Ŀ� ��� �������� �����ϴ� �Լ�
    IEnumerator DestroyAllPrefabsAfterDuration()
    {
        Debug.Log("����");
        // ��ų ���� �ð� ���
        //yield return new WaitForSeconds(duration);

        // ��� ������ ����
        foreach (GameObject instance in instantiatedPrefabs)
        {
            Destroy(instance);
        }

        // ����Ʈ �ʱ�ȭ
        instantiatedPrefabs.Clear();

        // ��ٿ� ����
        StartCoroutine(CoolDown());
        yield return null;
    }

    // ��ٿ��� ó���ϴ� �ڷ�ƾ �Լ�
    IEnumerator CoolDown()
    {
        // ��ٿ� �� �÷��� ����
        isCoolingDown = true;

        // ��ٿ� �Ⱓ��ŭ ���
        yield return new WaitForSeconds(SOSkill.Cooltime);

        // ��ٿ� ���� �� �÷��� ����
        isCoolingDown = false;
    }
}
