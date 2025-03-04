using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCharacter : MonoBehaviour
{
    Character character;
    public GameObject player;

    void Awake()
    {
        character = CharacterManager.Instance.FindCurrentCharacter();
        player = Instantiate(character.gameObject);
        CharacterManager.Instance.SetCharacter(player);
    }
}
