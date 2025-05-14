using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    float rotSpeed = 200f;
    public Transform player;
    NavMeshAgent navMesh;
    public bool isDetective {get; set;} = false;
    public bool isAttack {get; set;} = false;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        if(player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        Debug.Log("detective = " + isDetective);
        Debug.Log("attack = " + isAttack);
    }

    public void DetectPlayer(float radius)
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if(distToPlayer <= radius) isDetective = true;
        else isDetective = false;
    }

    public void DetectAttackPlayer(float attackRadius)
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= attackRadius) {
            isAttack = true;
            isDetective = false;
        }
        else 
        {
            isAttack = false;
        }
    }

    public void MoveToPlayer(float moveSpeed, float rotateSpeed)
    {
        if(isDetective)
        {
            navMesh.isStopped = false;
            navMesh.updateRotation = true;
            navMesh.SetDestination(player.position);
            navMesh.speed = moveSpeed;
            navMesh.angularSpeed = rotateSpeed;
        }
        else 
        {
            ResetMove();
        }
    }

    public void RotateToPlayer()
    {
        navMesh.isStopped = true;
        navMesh.updateRotation = false;

        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                rotSpeed * Time.deltaTime
            );
        }
    }

    public void ResetMove()
    {
        navMesh.ResetPath();
    }

    public bool IsDetective => isDetective;
}
