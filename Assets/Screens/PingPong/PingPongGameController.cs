using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PingPongGameController : MonoBehaviour
{
    [Header("Score")]
    public int PlayerScore;
    public int AIScore;
    public TextMeshPro PlayerScoreText;
    public TextMeshPro AIScoreText;
    public TextMeshPro StartText;
    [Header("Game")]
    public Transform Player;
    public Transform AI;
    public PingPongBall Ball;
    [Header("Player / AI Movements")]
    public float MaxY;
    public float MinY;
    public float PlayerSpeed;
    public float AISpeed;
    public float AISleepThreshold;
    [Header("EndGame")]
    public int EndScore = 10;
    public TextMeshPro EndMessageText;
    public Transform[] DisabledObjects;
    public Transform[] EnabledObjects;

    private bool Started;

    IEnumerator IRestartGame;
    private void Start()
    {
        for (int i = 0; i < DisabledObjects.Length; i++)
            DisabledObjects[i].gameObject.SetActive(true);
        for (int i = 0; i < EnabledObjects.Length; i++)
            EnabledObjects[i].gameObject.SetActive(false);
        EndMessageText.gameObject.SetActive(false);

        IRestartGame = null;
        Started = false;
    }
    private void Update()
    {



        if(Started == false)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Started = true;
                Ball.ResetBall(1);
            }
        }
        else
        {
            StartText.gameObject.SetActive(false);
            PlayerMovements();
            AIMovements();

            PlayerScoreText.text = PlayerScore.ToString();
            AIScoreText.text = AIScore.ToString();


            if (AIScore == EndScore)
                EndGame(false);
            if (PlayerScore == EndScore)
                EndGame(true);
        }

    }

    private void PlayerMovements()
    {
        if (Input.GetAxis("Vertical") > 0 && IRestartGame == null)
            Player.transform.position = new Vector3(Player.transform.position.x, Mathf.Clamp(Player.transform.position.y + Time.deltaTime * Input.GetAxis("Vertical") * PlayerSpeed, MinY, MaxY), Player.transform.position.z);
        if (Input.GetAxis("Vertical") < 0 && IRestartGame == null)
            Player.transform.position = new Vector3(Player.transform.position.x, Mathf.Clamp(Player.transform.position.y + Time.deltaTime * Input.GetAxis("Vertical") * PlayerSpeed, MinY, MaxY), Player.transform.position.z);
    }

    private void AIMovements()
    {
        if(Ball.transform.position.y - AI.position.y > 0 && Mathf.Abs(Ball.transform.position.y - AI.position.y)> AISleepThreshold)
        {
            AI.transform.position = Vector3.Lerp(AI.transform.position, new Vector3(AI.transform.position.x, Mathf.Clamp(AI.transform.position.y + Time.deltaTime * PlayerSpeed, MinY, MaxY), AI.transform.position.z), 100 * Time.deltaTime);
        }
        if (Ball.transform.position.y - AI.position.y < 0 && Mathf.Abs(Ball.transform.position.y - AI.position.y) > AISleepThreshold)
        {
            AI.transform.position = Vector3.Lerp(AI.transform.position, new Vector3(AI.transform.position.x, Mathf.Clamp(AI.transform.position.y - Time.deltaTime * PlayerSpeed, MinY, MaxY), AI.transform.position.z),100*Time.deltaTime);
        }
        
    }


    private void EndGame(bool Win)
    {
        for (int i = 0; i < DisabledObjects.Length; i++)
            DisabledObjects[i].gameObject.SetActive(false);
        for (int i = 0; i < EnabledObjects.Length; i++)
            EnabledObjects[i].gameObject.SetActive(true);
        EndMessageText.gameObject.SetActive(true);
        if (Win)
            EndMessageText.text = "You Won";
        if (!Win)
            EndMessageText.text = "You Lost";
        if(IRestartGame == null)
        {
            IRestartGame = RestartGame();
            StartCoroutine(IRestartGame);
        }
    }

    public IEnumerator RestartGame()
    {
        while (!Input.GetKeyDown(KeyCode.X))
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync("PingPong");
        SceneManager.LoadSceneAsync("PingPong",LoadSceneMode.Additive);
    }
}
