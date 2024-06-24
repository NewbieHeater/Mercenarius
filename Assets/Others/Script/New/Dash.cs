using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(5,1,5), Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.A))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //���콺 Ŭ�� ��ġ
                Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                //���콺 Ŭ�� ��ġ - ���� ��ġ (�������)
                Vector3 dashDestDir = (dashDestPos - transform.position).normalized;
                //������ǥ ��ġ
                Vector3 dashDest = transform.position + dashDestDir * 4f;
                Vector3 curPosition = transform.position;
                StartCoroutine(dash(dashDest, curPosition));
                
            }
        }

        IEnumerator dash(Vector3 Dest, Vector3 pos)
        {
            float t = 0;
            while (t < 1f)
            {
                
                transform.position = Vector3.Lerp(pos, Dest, t);
                t += Time.deltaTime;
                Debug.Log(t);
                yield return null;
            }
           
        }
    }
}
