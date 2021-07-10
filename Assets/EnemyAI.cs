using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navMeshAgent;

    [SerializeField]
    Transform target;

    Transform root;

    private void Awake()
    {
        root = GetComponentInParent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position);    
    }
}
