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

        public List<Stage> Stages;
        public bool GameClear = false;
        public bool GameStart = false;
        public int CurrentStageNumber = -1;

        public Stage CurrentStage = null;
        public Player Player = null;
        public int Step = 0;
        public bool AllStageClear = false;

        private GameObject field = null;

        public void UpdateSituation()
        {
            Step++;

            foreach (var home in CurrentStage.Homes)
            {
                if (home.haveBall == false)
                    return;
            }

            StageClear();
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
            Stages = Resources.LoadAll<Stage>("Prefabs/Stage/").ToList();

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
                Destroy(CurrentStage.gameObject);
                CurrentStage = null;
            }

            if (Player != null)
            {
                Destroy(Player.gameObject);
                Player = null;
            }

            var playerTemp = Resources.Load<Player>("Prefabs/Player");
            Player = Instantiate<Player>(playerTemp);

            Step = 0;
            AllStageClear = false;

            CurrentStage = Instantiate<Stage>(Stages[CurrentStageNumber]);
            CurrentStage.transform.SetParent(field.transform);
            CurrentStage.transform.localPosition = Vector3.zero;

            Player.transform.position = CurrentStage.PlayerPosition.position;
        }

        public void StageClear()
        {
            if (AllStageClear)
                return;

            if (CurrentStageNumber == Stages.Count - 1)
                AllStageClear = true;

            ++CurrentStageNumber;
            LoadStage();
        }

        public void EndGame()
        {
            PlayerPrefs.SetInt("CurrentStage", CurrentStageNumber);
        }
    }
}