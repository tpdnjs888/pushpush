using UnityEngine;
using System.Collections;
using PushPush.MoveObjectBase;

public class Ball : MoveObject {

    public bool Push(Vector3 pushPosition)
    {
        if (IsMoving)
            return false;

        var dir = transform.position - pushPosition;
        var end = transform.position + dir;

        RaycastHit2D hit;

        var canMove = CanMove(transform.position, end, out hit);
        Move(dir);

        return canMove;
    }
}
