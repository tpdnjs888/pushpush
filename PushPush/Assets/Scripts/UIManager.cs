using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PushPush.Manager;

public class UIManager : MonoBehaviour
{

    #region singleton
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<UIManager>();

            return instance;
        }
    }
    #endregion

    public GameObject StartScene;

    public Text StepText;
    public Text StageText;

    public readonly string[] AfkText = { "빨리 생각하세요~", "아~ 함~ 졸려.. =.=" };
    public readonly string Working = "영 차~! \n영 차~!";

    public void Exit()
    {
        GameManager.Instance.Exit();
    }

    public void GameStart()
    {
        GameManager.Instance.StartGame();

        StartScene.SetActive(false);
    }
}
