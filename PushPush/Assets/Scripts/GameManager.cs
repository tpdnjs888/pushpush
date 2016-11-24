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
        public bool IsStart = false;

        public Stage CurrentStage = null;
        public Player Player = null;
        public bool AllStageClear = false;

        private int step = 0;
        public int Step
        {
            get { return step; }
            set
            {
                step = value;
                UIManager.Instance.StepText.text = string.Format("Step {0}", step);
                UIManager.Instance.PlayerLastMoveTime = Time.time;
            }
        }

        private int currentStageNumber = 0;
        public int CurrentStageNumber
        {
            get { return currentStageNumber; }
            set
            {
                currentStageNumber = value;
                UIManager.Instance.StageText.text = string.Format("Stage {0}", currentStageNumber + 1);
            }
        }

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
            if (IsStart == false)
            {
                CurrentStageNumber = PlayerPrefs.GetInt("CurrentStage", 0);

                Stages = Resources.LoadAll<Stage>("Prefabs/Stage/").ToList();

                field = GameObject.Find("Field");

                IsStart = true;
            }
            LoadStage();
        }

        public void LoadStage()
        {
            if (CurrentStageNumber == -1 || field == null || Stages == null || Stages.Count == 0)
            {
                Exit();
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
                CurrentStageNumber = 0;

            PlayerPrefs.SetInt("CurrentStage", CurrentStageNumber);

            ++CurrentStageNumber;
            LoadStage();
        }

        public void EndStage()
        {
            PlayerPrefs.SetInt("CurrentStage", CurrentStageNumber);
        }

        public void Exit()
        {
            EndStage();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}