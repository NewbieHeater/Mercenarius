using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    static CharacterManager instance;

    [SerializeField] List<Character> characterPrefab;
    List<Character> characterList = new List<Character>();
    Character currentCharacter;
    GameObject character;

    public static CharacterManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);

        Initiate();
    }

    void Initiate()
    {
        foreach (var character in characterPrefab)
        {
            characterList.Add(character);
        }
    }

    public void SelectCharacter(int index)
    {
        currentCharacter = characterList[index];
        SceneManager.LoadScene("Main");
    }

    public Character FindCurrentCharacter()
    {
        return currentCharacter;
    }

    public void SetCharacter(GameObject obj)
    {
        character = obj;
    }

    public GameObject GetCharacter()
    {
        return character;
    }
}
