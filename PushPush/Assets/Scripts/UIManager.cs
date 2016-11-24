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

    public Animator StageAni;
    public Text StepTextAni;

    public float PlayerLastMoveTime;

    public readonly string[] AfkText = { "아~ 함~ 졸려.. =.=", "빨리 생각하세요~" };
    public readonly string Working = "영 차~!    \n   영 차~!";

    public void Exit()
    {
        GameManager.Instance.Exit();
    }

    public void GameStart()
    {
        GameManager.Instance.StartGame();

        StartScene.SetActive(false);

        StartCoroutine(StageAniCoroutine());
    }

    public IEnumerator StageAniCoroutine()
    {
        while (true)
        {
            StageAni.Play("CharacterMovingAni");
            StopCoroutine("StepTextChanger");
            StepTextAni.text = Working;

            while (Time.time - PlayerLastMoveTime <= 3f)
                yield return null;

            StageAni.Play("CharacterBored");
            StartCoroutine("StepTextChanger");

            while (Time.time - PlayerLastMoveTime > 3f)
                yield return null;
        }
    }

    public IEnumerator StepTextChanger()
    {
        while (true)
        {
            StepTextAni.text = AfkText[0];
            yield return new WaitForSeconds(3f);
            StepTextAni.text = AfkText[1];
            yield return new WaitForSeconds(2f);
        }
    }
}
