using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour
{
    private Rigidbody2D BallBody;
    private CircleCollider2D BallCollider;
    public PingPongGameController GameController;

    [Header("Ball Movements")]
    public float BallStartSpeed;
    private float BallCurrentSpeed;
    public float IncreaseRate;
    [Header("Score Detection")]
    public Transform PlayerGoal;
    public Transform AIGoal;
    public int LatestGoal; // = 1 if the player scores and = -1 if the AI Scores

    private void Start()
    {
        BallBody = GetComponent<Rigidbody2D>();
        BallCollider = GetComponent<CircleCollider2D>();
        LatestGoal = 1;//Start the ball from the player Side
        BallCurrentSpeed = BallStartSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == AIGoal)
        {
            GameController.PlayerScore++;
            LatestGoal = -1;
            ResetBall(1);
        }
        if (collision.transform == PlayerGoal)
        {
            GameController.AIScore++;
            LatestGoal = 1;
            ResetBall(1);
        }
        if(collision.transform == GameController.Player|| collision.transform == GameController.AI)
        {
            BallCurrentSpeed *= IncreaseRate;
            BallBody.velocity = BallBody.velocity.normalized * BallCurrentSpeed;
        }
    }

    public void ResetBall(float Time)
    {
        this.transform.position = Vector3.zero;
        BallBody.velocity = Vector3.zero;
        BallCurrentSpeed = BallStartSpeed;
        StartCoroutine(IResetBall(Time));
    }

    private IEnumerator IResetBall(float Time)
    {
        yield return new WaitForSeconds(Time);
        //Callculate Direction
        BallBody.velocity = (Vector3.right * LatestGoal + Vector3.up * Random.Range(-0.25f, 0.25f)).normalized * BallCurrentSpeed;
    }
}
