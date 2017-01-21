using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PushOver : MonoBehaviour {

	protected Rigidbody rb;
	protected NavMeshAgent agent;

	public GameDirector director;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (director.isPlayerLayer( collision.gameObject.layer ))
		{
			agent.enabled = false;
			rb.useGravity = true;
			director.DudeGotAngry();
		}
	}

}
