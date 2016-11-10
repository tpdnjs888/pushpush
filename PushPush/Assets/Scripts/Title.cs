using UnityEngine;
using System.Collections;
using PushPush.Manager;

public class Title : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        
        if (GameManager.Instance.Touch() && GameManager.Instance.GameStart == false)
        {
            GameManager.Instance.StartGame();

            gameObject.SetActive(false);
        }
	}
}
