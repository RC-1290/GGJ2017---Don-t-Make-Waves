using UnityEngine;

public class Punch : MonoBehaviour
{
	protected Vector3 velocity;
	protected Vector3 lastPos;

    public float falconPunchModifier = 1000.0f;
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
		Rigidbody punchTarget = collision.rigidbody;

		if (punchTarget)
		{
			float multiplier = 1.0f;
			if (falconPunchAxis.Length > 0)
			{
				multiplier = falconPunchModifier * Input.GetAxis(falconPunchAxis);
				
				Rigidbody targetRB = collision.rigidbody;
				
				Vector3 averagePosition = new Vector3();
				for(int i = 0; i < collision.contacts.Length; ++i)
				{
					averagePosition += collision.contacts[i].point;
				}
				averagePosition /= collision.contacts.Length;

				Vector3 forceApplied = (velocity - collision.rigidbody.velocity) * multiplier * collision.rigidbody.mass;
				targetRB.AddForceAtPosition(forceApplied, averagePosition);

			}
		}
	}
}
