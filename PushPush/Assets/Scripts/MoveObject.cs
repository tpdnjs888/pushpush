using UnityEngine;
using System.Collections;

namespace PushPush.MoveObjectBase
{
    public abstract class MoveObject : MonoBehaviour
    {
        public LayerMask BlockingLayer;
        public float Speed = 5f;

        protected bool IsMoving = false;

        private Rigidbody2D rb2d;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        protected virtual void Move(Vector3 dir)
        {
            if (IsMoving)
                return;

            RaycastHit2D hit;

            var start = transform.position;
            var end = start + dir;

            if (CanMove(start, end, out hit))
            {
                IsMoving = true;
                StartCoroutine(MoveCoroutine(end));
            }
        }

        protected bool CanMove(Vector3 start, Vector3 end, out RaycastHit2D hit)
        {
            hit = Physics2D.Linecast(start, end, BlockingLayer);

            return hit.transform == null;
        }

        protected virtual IEnumerator MoveCoroutine(Vector3 end)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position, end, Speed * Time.deltaTime);
                rb2d.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;

                yield return null;
            }
            IsMoving = false;
        }
    }
}