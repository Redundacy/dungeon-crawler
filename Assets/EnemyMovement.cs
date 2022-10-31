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
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        enemyState currentState = enemyState.WANDERING;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        //find closest player
        //move towards them
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
            return;
    }
}
