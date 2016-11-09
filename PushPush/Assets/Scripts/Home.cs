using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour {

    public bool haveBall = false;

    private SpriteRenderer sprite;
    private Sprite inBall;
    private Sprite outBall;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        outBall = Resources.Load<Sprite>("Home1");
        inBall = Resources.Load<Sprite>("Home2");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (LayerMask.NameToLayer("Ball") == col.gameObject.layer)
        {
            haveBall = true;
            sprite.sprite = inBall;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (LayerMask.NameToLayer("Ball") == col.gameObject.layer)
        {
            haveBall = false;
            sprite.sprite = outBall;
        }
    }
}
