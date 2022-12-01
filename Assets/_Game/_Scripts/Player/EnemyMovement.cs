using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour //change later
{

    public enum enemyState
    {
        WANDERING,
        CHASING,
        INVESTIGATING,
    }
    public NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRadius = 5;
    private Vector3 lastPlayerPosition;
    enemyState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = enemyState.WANDERING;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] inRangePlayers = Physics.OverlapSphere(this.transform.position, sightRadius, whatIsPlayer);
        if (inRangePlayers.Length > 0)
        {
            player = inRangePlayers[0].transform;
            currentState = enemyState.CHASING;
            lastPlayerPosition = player.position;
            MoveTowardsPlayer(lastPlayerPosition);
        } else if (currentState == enemyState.CHASING)
        {
            currentState=enemyState.INVESTIGATING;
            lastPlayerPosition = player.position;
            MoveTowardsPlayer(lastPlayerPosition);
        }
    }

    void MoveTowardsPlayer(Vector3 TargetPosition)
    {
        agent.SetDestination(TargetPosition);

    }
}
