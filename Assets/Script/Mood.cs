using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mood : MonoBehaviour {

	public float angriness = 0.0f;
	public float angerSpeed = 0.01f;
	public float relativeAngerReductionSpeed = 0.5f;
	public float headDistMultiplier = 0.01f;
	public float maxHeadDist = 0.012f;

	public GameObject headPhysics;
	public GameObject headGraphic;
	public AudioSource headPopSound;
	public AudioSource complaintSound;

	public List<AudioClip> complaints;

    public GameDirector director;

	protected NavMeshAgent agent;

	protected Vector3 neutralHeadPos;
	protected Vector3 neutralHeadscale;

    protected bool hittingPlayer = false;

    private void Start()
    {
        angriness = 0.0f;
		neutralHeadPos = headGraphic.transform.localPosition;
		neutralHeadscale = headGraphic.transform.localScale;
        hittingPlayer = false;

		agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (hittingPlayer)
        {
            MakeMoreAngry();
        }
		else
		{
			angriness -= angerSpeed * relativeAngerReductionSpeed;
			angriness = angriness < 0.0f ? 0.0f : angriness;
		}

		if (headPhysics)
		{
			// Mood based head jitter: 
			float distanceMultiplier = headDistMultiplier * angriness;
			Vector3 randomDir = new Vector3(
				Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist),
				Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist),
				Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist));
			headGraphic.transform.localPosition = neutralHeadPos + randomDir;

			if (angriness > 0.4f)
			{
				if (!complaintSound.isPlaying)
				{
					int randomIndex = Mathf.RoundToInt(Random.Range(0, complaints.Count));
					complaintSound.clip = complaints[randomIndex];
					complaintSound.Play();
				}
			}

			if (angriness > 1.0f)
			{// Game Over
				director.PlayerStared();

				headGraphic.transform.localScale = neutralHeadscale * angriness;

				agent.enabled = false;
			}
			if (angriness > 2.0f)
			{//POP!
				Rigidbody rb = headPhysics.GetComponent<Rigidbody>();
				//Joint headJoint = head.GetComponent<Joint>();
				rb.AddExplosionForce(600.0f, transform.position, 20.0f);
				headGraphic.transform.localScale = neutralHeadscale;
				headPopSound.Play();
				headPhysics = null;
			}

		}

    }

	public void MakeFullAngry()
	{
		angriness = angriness < 1.0f ? 1.0f : angriness;
	}

	public void StartCursingRightAway()
	{
		angriness = angriness < 0.4f ? 0.4f : angriness;
	}

    public void MakeMoreAngry()
    {
		angriness += angerSpeed;
    }

}
