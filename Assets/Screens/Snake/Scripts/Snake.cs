using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    private Vector2 direction = Vector2.right;
    private Vector2 input;
    public AudioSource upgrade,downgrade;
    public int initialSize = 4;
    public TextMeshPro scoreText;
    int score;
    public TextMeshPro timeText;
    float time;
    public int Graws;
    public TextMeshPro starttext;
    private void Start()
    {
        StartCoroutine("Timesdsd");
        // ResetState();
        StartGame();
        Timer = NextPosTime;
        Graws = initialSize;
    }

    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.X))
        {
           
            starttext.gameObject.SetActive(false);
        }*/



        scoreText.text = "Score: " + score;
        timeText.text = "Time Elapsed: " + time;
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2.left;
            }
        }


        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            // Set the new direction based on the input
            if (input != Vector2.zero)
            {
                direction = input;
            }

            // Set each segment's position to be the same as the one it follows. We
            // must do this in reverse order so the position is set to the previous
            // position, otherwise they will all be stacked on top of each other.
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }
            Vector3 Pos = this.transform.position;
            // Move the snake in the direction it is facing
            // Round the values to ensure it aligns to the grid
            float x = Mathf.Round(transform.position.x) + direction.x;
            float y = Mathf.Round(transform.position.y) + direction.y;

            transform.position = new Vector2(x, y);
            Timer = NextPosTime;
            if(Graws > 0)
            {
                Graws--;
                StartCoroutine(IGraw(Pos));
            }
        }
    }
    IEnumerator IGraw(Vector3 pos)
    {
        yield return null;
        Grow(pos);
    }

    public float NextPosTime;
    private float Timer;
    private void FixedUpdate()
    {

    }

    public void Grow(Vector3 pos)
    {
        score+=10;
        Transform segment = Instantiate(segmentPrefab);
        Deloader.Instance.AddToTrash(segment.gameObject);
        segment.position = pos;
        segments.Add(segment);
    }

    public void UnGrow()
    {
        score-=10;
        Destroy(segments[segments.Count - 1].gameObject);
        segments.RemoveAt(segments.Count - 1);
        Destroy(segments[segments.Count - 1].gameObject);
        segments.RemoveAt(segments.Count - 1);
    }

    public void ResetState()
    {
        time = 0;
        score = 0;
        direction = Vector2.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) {
            //Grow();
        }
    }
    public void StartGame()
    {
        time = 0;
        score = 0;
        direction = Vector2.right;
        transform.position = Vector3.zero;

       
        segments.Add(transform);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food")) {
            upgrade.Play();
            Graws++;
        } else if (other.gameObject.CompareTag("Obstacle")) {
            SceneManager.UnloadSceneAsync("Snake");
            SceneManager.LoadSceneAsync("Snake",LoadSceneMode.Additive);
   
        } else if (other.gameObject.CompareTag("Enemy")) {
            if(segments.Count < 1)
            {
                SceneManager.UnloadSceneAsync("Snake");
                SceneManager.LoadSceneAsync("Snake", LoadSceneMode.Additive);
            } else 
            {
                downgrade.Play();
                UnGrow();
            }
        }
    }

    IEnumerator Timesdsd()
    {
        while(true)
        {
            time += 1;
            yield return new WaitForSecondsRealtime(1);
        }
    }

}
