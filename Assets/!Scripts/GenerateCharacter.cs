using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCharacter : MonoBehaviour
{
    Character character;
    GameObject player;

    void Awake()
    {
        character = CharacterManager.Instance.FindCurrentCharacter();
        player = Instantiate(character.gameObject);
        CharacterManager.Instance.SetCharacter(player);
    }
}
