using UnityEngine;

public class Punch : MonoBehaviour
{
	protected Vector3 velocity;
	protected Vector3 lastPos;

	public float strength = 1.0f;

    public float falconPunchModifier = 10.0f;
    public string falconPunchAxis;

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
        float multiplier = falconPunchModifier * Input.GetAxis(falconPunchAxis);
		PushOver target = collision.gameObject.GetComponent<PushOver>();
		if (target)
		{
			target.TurnToPhysics();
			Rigidbody targetRB = collision.gameObject.GetComponent<Rigidbody>();
			targetRB.AddForceAtPosition(velocity * strength * multiplier, collision.contacts[0].point);
		}
	}
}
