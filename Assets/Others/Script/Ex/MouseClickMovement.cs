using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**/
public class MouseClickMovement : MonoBehaviour
{
    Vector3 destPos; //목적지의 좌표
    Vector3 dir; //목적지의 방향
    Quaternion lookTarget; //목표까지 돌아야하는 값
    bool move = false; // 방향 맞추기 전까지 움직이지 않게 만들기

    public float moveSpeed; //캐릭터 이동속도
    RaycastHit hit;
    Ray ray;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) //우클릭시 실행
        {
            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); //레이 좌표 저장

            if (Physics.Raycast(ray, out hit))//클릭한 곳에 물체가 있는지 검사&레이 초기화
            {
                print(hit.transform.name);//클릭한 대상 출력

                destPos = new Vector3(hit.point.x, transform.position.y, hit.point.z); //목표 위치 재설정

                dir = destPos - transform.position; //현재 위치에서 목표위치로의 벡터

                lookTarget = Quaternion.LookRotation(dir);//목표까지 돌아야하는 각도 설정
                move = true;
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            move = false;
        }

        Move();//Move 실행
    }

    private void Move()
    {
        if (move)
        {
            dir = destPos - transform.position; //현재 위치에서 목표위치로의 벡터
            lookTarget = Quaternion.LookRotation(dir);//목표까지 돌아야하는 각도 설정
            transform.rotation = lookTarget; //클릭위치로 회전
            transform.position += dir.normalized * Time.deltaTime * moveSpeed; //이동방향으로 moveSpeed만큼 이동


            if((transform.position - destPos).magnitude < Time.deltaTime * moveSpeed)
            {
                transform.position = destPos;
                
                move = false; 
            }
        }
    }
}
