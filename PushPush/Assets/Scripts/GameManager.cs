using UnityEngine;
using System.Collections;

namespace PushPush.Manager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = "GameManager";
                        instance = go.AddComponent<GameManager>();
                    }
                }

                return instance;
            }
        }

        public Stage Stage;
        public bool GameClear = false;

        public void UpdateSituation()
        {
            if (Stage == null)
                Stage = GameObject.FindObjectOfType<Stage>();

            foreach (var home in Stage.Homes)
            {
                if (home.haveBall == false)
                    return;
            }

            GameClear = true;

#if UNITY_EDITOR
            Debug.Log("Clear!");
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}