using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    float detectRadius = 10f;
    float detectAttackRadius = 1.5f;
    float moveSpeed = 2f;
    public Transform player;
    NavMeshAgent navMesh;
    bool isDetective = false;
    public bool isAttack {get; set;} = false;
    public float attackCooltime {get; private set;} = 2f;
    public float timer {get; private set;} = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        if(player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    public void DetectPlayer()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if(distToPlayer <= detectRadius) isDetective = true;
        else isDetective = false;
    }

    public void DetectAttackPlayer()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= detectAttackRadius) {
            isAttack = true;
            isDetective = false;
        }
    }

    public void MoveToPlayer()
    {
        if(isDetective)
        {
            navMesh.SetDestination(player.position);
            navMesh.speed = moveSpeed;
        }
        else 
        {
            navMesh.ResetPath();
        }
    }

    public void ResetMove()
    {
        navMesh.ResetPath();
    }

    public bool IsDetective => isDetective;
}
