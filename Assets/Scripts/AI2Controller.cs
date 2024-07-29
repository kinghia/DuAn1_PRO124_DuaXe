using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI2Controller : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints };
    [Header("AI settings")]
    public AIMode aiMode;

    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    WayPoints2 currentWaypoint = null;
    WayPoints2[] allWayPoints;

    TopDownCarController topDownCarController;

    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allWayPoints = FindObjectsOfType<WayPoints2>();
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
                FollowWaypoints2();
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

    void FollowWaypoints2()
    {
        if (currentWaypoint == null)
            currentWaypoint = FindClosestWayPoint();

        if (currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;

            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWayPoint)
            {
                currentWaypoint = currentWaypoint.nextwaypoint[Random.Range(0, currentWaypoint.nextwaypoint.Length)];
            }
        }
    }
    WayPoints2 FindClosestWayPoint()
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
