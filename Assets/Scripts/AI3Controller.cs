using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI3Controller : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints };
    [Header("AI settings")]
    public AIMode aiMode;

    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    
    WayPoints3 currentWaypoint = null;
    WayPoints3[] allWayPoints;

    TopDownCarController topDownCarController;

    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WayPoints3>();
    }
    void Start()
    {

    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;

            case AIMode.followWaypoints:
                FollowWaypoints();
                break;
        }

        inputVector.x = TurnTowardTarger();
        inputVector.y = 1.0f;

        topDownCarController.SetInputVector(inputVector);

    }
    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

    void FollowWaypoints()
    {
        if (currentWaypoint == null)
            currentWaypoint = FindClosestWayPoint();

        if (currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWayPoint)
            {
                currentWaypoint = currentWaypoint.nextwaypoint3[Random.Range(0, currentWaypoint.nextwaypoint3.Length)];
            }
        }
    }
    WayPoints3 FindClosestWayPoint()
    {
        return allWayPoints.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }
    float TurnTowardTarger()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }
}

