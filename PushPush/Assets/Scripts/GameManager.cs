using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace PushPush.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region singleton
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
        #endregion

        public Dictionary<int, Stage> Stages = new Dictionary<int, Stage>();
        public bool GameClear = false;
        public bool GameStart = false;
        public int CurrentStageNumber = -1;

        public Stage CurrentStage = null;


        private GameObject field = null;

        public void UpdateSituation()
        {
            /*if (Stage == null)
                Stage = GameObject.FindObjectOfType<Stage>();

            foreach (var home in Stage.Homes)
            {
                if (home.haveBall == false)
                    return;
            }*/

            GameClear = true;

#if UNITY_EDITOR
            Debug.Log("Clear!");
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public bool Touch()
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
#else
            return Input.touchCount > 0;
#endif
        }

        public void StartGame()
        {
            CurrentStageNumber = PlayerPrefs.GetInt("CurrentStage", 0);
            var stageTemp = Resources.LoadAll<Stage>("Prefabs/Stage/").ToList();

            for(int i = 0; i < stageTemp.Count; ++i)
                Stages.Add(i + 1, stageTemp[i]);

            field = GameObject.Find("Field");

            LoadStage();
        }

        public void LoadStage()
        {
            if (CurrentStageNumber == -1 || field == null || Stages == null || Stages.Count == 0)
            {
                EndGame();
                return;
            }

            if (CurrentStage != null)
            {
                Destroy(CurrentStage);
                CurrentStage = null;
            }

            CurrentStage = Instantiate<Stage>(Stages[CurrentStageNumber]);
            CurrentStage.transform.SetParent(field.transform);
            CurrentStage.transform.position = Vector3.zero;
        }

        public void EndGame()
        {
            PlayerPrefs.SetInt("CurrentStage", CurrentStageNumber);
        }
    }
}