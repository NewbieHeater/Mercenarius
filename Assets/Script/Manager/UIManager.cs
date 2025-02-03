using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;
    private Dictionary<string, GameObject> panelCache = new Dictionary<string, GameObject>(); // �г� ĳ��

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }
    
    
    public void Init()
    {
        
        //LoadAndCacheAllPanels();
    }

    // Canvas ����
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    // UI ���� ������ ����
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    // Scene UI ǥ��
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    // Popup UI ǥ��
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = ObjectPooler.SpawnFromPool(name, Root.transform.position);
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    // Popup UI �ݱ�
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    // ���� �ֱٿ� ���� Popup UI �ݱ�
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Disable(popup.gameObject);
        popup = null;
        _order--;
    }

    // ��� Popup UI �ݱ�
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    // UI ��� �ʱ�ȭ
    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }

    // �г� �ε� �� ĳ��
    //public void LoadAndCacheAllPanels()
    //{
    //    // UI/Panel ��� ���� ��� �г��� �ε�
    //    GameObject[] panelPrefabs = Managers.Resource.LoadPrefab("UI/Scene");

    //    if (panelPrefabs.Length > 0)
    //    {
    //        foreach (GameObject panelPrefab in panelPrefabs)
    //        {
    //            // �̹� ĳ�õ� �г��� �ǳʶ�
    //            if (!panelCache.ContainsKey(panelPrefab.name))
    //            {
    //                GameObject panelInstance = Object.Instantiate(panelPrefab, Root.transform);
    //                panelInstance.SetActive(false); // �⺻������ ��Ȱ��ȭ
    //                panelCache[panelPrefab.name] = panelInstance;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No panels found in the 'UI/Scene' path.");
    //    }
    //}


    //// �г� ��ȯ
    //public void SwitchPanel(string panelName)
    //{
    //    // ���� Ȱ��ȭ�� �г��� �ִٸ� ��Ȱ��ȭ
    //    foreach (var cachedpanel in panelCache.Values)
    //    {
    //        cachedpanel.SetActive(false);
    //    }

    //    // �ش� �г��� ĳ�õǾ� �ִٸ� Ȱ��ȭ
    //    if (panelCache.TryGetValue(panelName, out GameObject panel))
    //    {
    //        panel.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"Panel '{panelName}' not loaded.");
    //    }
    //}
}
