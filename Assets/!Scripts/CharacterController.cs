using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Character character;

    void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        AttackInput();
        MoveInput();
    }

    void AttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            character.Attack();
        }
    }

    void MoveInput()
    {
        character.Move();
    }
}
