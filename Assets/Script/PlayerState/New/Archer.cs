using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        Debug.Log("Archer Attack!");
    }
    public Vector3 CheckGround(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
            return hit.point;
        else
            return transform.position;
    }
}