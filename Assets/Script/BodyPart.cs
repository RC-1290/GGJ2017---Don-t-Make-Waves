using UnityEngine;
public class BodyPart : MonoBehaviour
{
	public Mood owner;

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Punch>())
		{
			owner.MakeMoreAngry();
		}
	}

	public void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.GetComponent<Punch>())
		{
			owner.MakeMoreAngry();
		}
	}

	public void OnJointBreak(float breakForce)
	{
		owner.MakeFullAngry();
	}
}