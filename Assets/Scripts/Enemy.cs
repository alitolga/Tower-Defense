using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Attributes")]
    public float speed = 10f;
    public string enemyName;
    public float health = 100f;
    public int value;

    [Header("Waypoint variables (Set by default)")]
    public Transform target;
    public int waypointIndex = 0;
    public float waypointReachRadius = 0.25f;
    

    private void Start()
    {
        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= waypointReachRadius)
        {
            GetNextWaypoint();
        }

    }

    void GetNextWaypoint()
    {
        if(waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += value;
        Destroy(gameObject);
    }



}
