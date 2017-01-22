using UnityEngine;
using UnityEngine.AI;

public class PushOver : MonoBehaviour {

	public GameDirector director;

	protected Rigidbody rb;
	protected NavMeshAgent agent;
	protected Mood mood;

	protected bool physicsMode = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
		mood = GetComponent<Mood>();
	}
	public void TurnToPhysics()
	{
		if (!physicsMode)
		{
			agent.enabled = false;
			rb.useGravity = true;
			director.PlayerTouched();
			physicsMode = true;
			mood.StartCursingRightAway();
		}
	}

}
