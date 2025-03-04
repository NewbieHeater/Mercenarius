using Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public float gameTIme;
    public float maxGameTIme = 2 * 10f;
    public ObjectPooler pool;
    public ItemDescription mItemDescription;
    public Image hpImage;
    //public EnemyVariables variables;
    public bool isUIOpen = false;
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        gameTIme += Time.deltaTime;
        //Ÿ�̸� ������ ������ �ð� �ֱ�
        if (gameTIme > maxGameTIme) 
        {
            gameTIme = maxGameTIme; 
        }
    }
    //���� �ٸ� ��ũ��Ʈ�� �����ϱ� ���Ϸ��� ���� ���ӸŴ���
}
