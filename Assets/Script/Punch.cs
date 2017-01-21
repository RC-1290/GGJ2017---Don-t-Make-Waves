using UnityEngine;

public class Punch : MonoBehaviour
{
	protected Vector3 velocity;
	protected Vector3 lastPos;

	public float strength = 1.0f;

	private void Start()
	{
		lastPos = transform.position;
	}

	private void FixedUpdate()
	{
		Vector3 currentPos = transform.position;
		velocity = currentPos - lastPos;
		lastPos = currentPos;
	}

	private void OnCollisionEnter(Collision collision)
	{
		PushOver target = collision.gameObject.GetComponent<PushOver>();
		if (target)
		{
			target.TurnToPhysics();
			Rigidbody targetRB = collision.gameObject.GetComponent<Rigidbody>();
			targetRB.AddForceAtPosition(velocity * strength, collision.contacts[0].point);
		}
	}
}
