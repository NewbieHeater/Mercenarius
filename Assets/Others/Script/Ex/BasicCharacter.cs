using System;
using UnityEngine;

namespace Common.Scripts
{
  public class BasicCharacter : MonoBehaviour
  {

    //private static readonly int WALK_PROPERTY = Animator.StringToHash("Walk");


    [SerializeField]
    private float speed = 2f;

    [Header("Relations")]
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private Rigidbody rg = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;


    private Vector3 _movement;

    private void Update ()
    {
      // Vertical
      float inputY = 0;
      if ( Input.GetKey(KeyCode.UpArrow) )
        inputY = 1;
      else if ( Input.GetKey(KeyCode.DownArrow) )
        inputY = -1;

      // Horizontal
      float inputX = 0;
      if ( Input.GetKey(KeyCode.RightArrow) )
      {
        inputX = 1;
        spriteRenderer.flipX = false;
      }
      else if ( Input.GetKey(KeyCode.LeftArrow) )
      {
        inputX = -1;
        spriteRenderer.flipX = true;
      }

      // Normalize
      _movement = new Vector3(inputX, 0, inputY).normalized;

      //animator.SetBool(WALK_PROPERTY,
                       //Math.Abs(_movement.sqrMagnitude) > Mathf.Epsilon);
    }

    private void FixedUpdate ()
    {
            rg.velocity = _movement * speed;
    }
  }
}