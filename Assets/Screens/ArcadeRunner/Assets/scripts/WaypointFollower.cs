using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    public bool isEnemy;
    [SerializeField] private float speed = 2f;

    private void Update()
    {   
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        
        if(isEnemy){
            if(currentWaypointIndex== 0){
                transform.localScale = new Vector2(-1, 1);
            }
            else{
                transform.localScale = new Vector2(1, 1);
            }

        }
    }
}
