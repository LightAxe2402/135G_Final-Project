using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrolAI : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private GameObject patrolPathParent = null;
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float acceptanceRange = 0.01f;

    private NavMeshAgent agent = null;
    private GameObject currentPatrolPoint = null;
    private int currentPatrolIndex = 1;

    /// <summary>
    /// Getting Private References
    /// </summary>
    private void Awake()
    {
        // Getting NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Setting the First Target Point
        SetNewPoint(patrolPathParent.transform.GetChild(currentPatrolIndex).gameObject);
    }

    /// <summary>
    /// Moving towards the next patrol point
    /// </summary>
    private void Update()
    {
        agent.destination = currentPatrolPoint.transform.position;

        // Checking the Range to the Next Point
        if (CheckPointDistance() <= acceptanceRange)
        {
            // Set the next path to the next
            currentPatrolIndex++;
            if (currentPatrolIndex > patrolPathParent.transform.childCount - 1)
                currentPatrolIndex = 0;

            SetNewPoint(patrolPathParent.transform.GetChild(currentPatrolIndex).gameObject);
        }
    }

    /// <summary>
    /// Takes in a new patrol point and sets the NavAgent
    /// </summary>
    /// <param name="gameObject"></param>
    private void SetNewPoint(GameObject point)
    {
        // Setting the Destination
        agent.destination = point.transform.position;
        agent.speed = patrolSpeed;
        currentPatrolPoint = point;

        return;
    }

    /// <summary>
    /// Takes in two points and returns the difference
    /// </summary>
    /// <returns></returns>
    private float CheckPointDistance()
    {
        // Getting the difference in space
        return Vector3.Distance(transform.position, currentPatrolPoint.transform.position);
    }
}
