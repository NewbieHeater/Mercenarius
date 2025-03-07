using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    KeyManager _keyManager = new KeyManager();
    SceneManagerEx _scene = new SceneManagerEx();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static KeyManager KeyInput { get { return Instance._keyManager; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    private void Awake()
    {
        KeyInput.Init();
        //Input.EInit();
    }
    void Start()
    {
        Init();
        UI.Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
        
    }

    public static void Clear()
    {
        UI.Clear();
    }

}
