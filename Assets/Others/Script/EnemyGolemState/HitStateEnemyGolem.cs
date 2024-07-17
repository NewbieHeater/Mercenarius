using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class HitStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _monsterController;

    public void OperateEnter(EnemyGolemController sender)
    {
        _monsterController = sender;
        //_monsterController.curHealth -= 1;
        StartCoroutine(startNokBack());
        _monsterController.anim.SetBool("Hit", true);
        _monsterController.Hpbar.fillAmount = _monsterController.curHealth / _monsterController.maxHealth;
        Debug.Log(_monsterController.curHealth / _monsterController.maxHealth);
    }
    float knockbackSpeed = 0.3f;
    IEnumerator startNokBack()
    {
        _monsterController.nav.enabled = false;
        //yield return new WaitForSecondsRealtime(1f);
        _monsterController.curHealth -= 10;
        Debug.Log(_monsterController.curHealth);
        //_monsterController.AttackPoint.LookAt(_monsterController.target.transform);
        //Vector3 b = transform.TransformDirection(_monsterController.target.position);
        //var disx = _monsterController.target.transform.position.x - _monsterController.enemyRb.transform.position.x;
        //var disy = _monsterController.target.transform.position.y - _monsterController.enemyRb.transform.position.y;
        //float a = Mathf.Sqrt(Mathf.Pow(disx,2) + Mathf.Pow(disy,2));
        //_monsterController.enemyRb.AddForce(disx/a*100,disy/a*100,0);
        Vector3 KnockBackPos = transform.position + (-_monsterController.target.transform.position + transform.position).normalized * knockbackSpeed; // 넉백 시 이동할 위치
        float t = 0;
        while (t < 1f / 3f)
        {
            transform.position = Vector3.Lerp(transform.position, KnockBackPos, 3 * t);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    public void OperateUpdate(EnemyGolemController sender)
    {

    }

    public void OperateExit(EnemyGolemController sender)
    {
        _monsterController.MoveAble = true;
        _monsterController.nav.enabled = true;
        StopAllCoroutines();
    }
}
