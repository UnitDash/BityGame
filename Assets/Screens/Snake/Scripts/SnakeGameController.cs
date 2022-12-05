using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeGameController : MonoBehaviour
{
    [SerializeField] Collider2D gridArea;
    [SerializeField] Transform enemyPrefab;
    [SerializeField] float delayMin,delayMax;
    public GameObject player;

    private void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    private void Update()
    {
        //Debug.Log(player);
        /*
        if(player == null)
        {
            SceneManager.LoadScene("Snake");
        }
        */
    }

    private void RandomizePosition(Transform enemyPos)
    {
        Bounds bounds = gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        enemyPos.position = new Vector2(x, y);
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            Transform enemy = Instantiate(enemyPrefab);
            Deloader.Instance.AddToTrash(enemy.gameObject);
            RandomizePosition(enemy);
            yield return new WaitForSeconds(Random.Range(delayMin,delayMax));
        }
    }
}
