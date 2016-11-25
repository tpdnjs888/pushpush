using UnityEngine;
using System.Collections;
using PushPush.MoveObjectBase;

namespace PushPush
{
    public class Player : MoveObject
    {

        private void Update()
        {
            if (IsMoving)
                return;

            var horizontal = (int)Input.GetAxisRaw("Horizontal");
            var vertical = (int)Input.GetAxisRaw("Vertical");

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                var dragPosition = Input.GetTouch(0).deltaPosition.normalized;
                horizontal = (int)dragPosition.x;
                vertical = (int)dragPosition.y;
            }

            if (horizontal == 0 && vertical == 0)
                return;

            if (horizontal != 0)
                vertical = 0;

            Move(new Vector3(horizontal, vertical));
        }

        protected override void Move(Vector3 dir)
        {
            if (IsMoving)
                return;

            RaycastHit2D hit;

            var start = transform.position;
            var end = start + dir;

            if (CanMove(start, end, out hit) == false)
            {
                var ball = hit.transform.GetComponent<Ball>();

                if (ball == null)
                    return;

                if (ball.Push(transform.position) == false)
                    return;
            }

            IsMoving = true;
            StartCoroutine(MoveCoroutine(end));
        }

        protected override IEnumerator MoveCoroutine(Vector3 end)
        {
            yield return StartCoroutine(base.MoveCoroutine(end));

            PushPush.Manager.GameManager.Instance.UpdateSituation();
        }
    }
}