using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navMeshAgent;

    [SerializeField]
    Transform target;


    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position);    
    }
}
