using UnityEngine;
using UnityEngine.AI;

public class WalkSomewhere : MonoBehaviour {

    public Transform target;
    protected NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
	}

}
