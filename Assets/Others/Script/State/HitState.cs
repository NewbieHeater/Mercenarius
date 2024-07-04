using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class HitState : MonoBehaviour, IState<MonsterController>
{
    private MonsterController _monsterController;

    public void OperateEnter(MonsterController sender)
    {
        _monsterController = sender;

        Debug.Log("����");
        StartCoroutine(startNokBack());
        
        
    }
    float knockbackSpeed = 0.3f;
    IEnumerator startNokBack()
    {
        _monsterController.nav.enabled = false;
        //yield return new WaitForSecondsRealtime(1f);
        _monsterController.health--;
        //_monsterController.AttackPoint.LookAt(_monsterController.target.transform);
        //Vector3 b = transform.TransformDirection(_monsterController.target.position);
        //var disx = _monsterController.target.transform.position.x - _monsterController.enemyRb.transform.position.x;
        //var disy = _monsterController.target.transform.position.y - _monsterController.enemyRb.transform.position.y;
        //float a = Mathf.Sqrt(Mathf.Pow(disx,2) + Mathf.Pow(disy,2));
        //_monsterController.enemyRb.AddForce(disx/a*100,disy/a*100,0);
        Vector3 KnockBackPos = transform.position + (-_monsterController.target.transform.position + transform.position).normalized * knockbackSpeed; // �˹� �� �̵��� ��ġ
        float t = 0;
        while (t < 1f/3f)
        {
            transform.position = Vector3.Lerp(transform.position, KnockBackPos, 3 * t);
            t += Time.deltaTime;
            yield return null;
        }
        

        //_monsterController.enemyRb.transform.position += b;
        //_monsterController.enemyRb.AddForce(b*-1f,ForceMode.Impulse);
        yield return new WaitForSeconds(0.05f);
        _monsterController.isHit = false;
        _monsterController.MoveAble = true;
        //StartCoroutine(isHit());
        yield return null;
    }
    public void OperateUpdate(MonsterController sender)
    {

    }

    public void OperateExit(MonsterController sender)
    {
        _monsterController.nav.enabled = true;
        StopAllCoroutines();
    }
}
