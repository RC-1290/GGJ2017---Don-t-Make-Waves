using System.Collections.Generic;
using UnityEngine;

public class Mood : MonoBehaviour {

	public float angriness = 0.0f;
	public float angerSpeed = 0.01f;
	public float relativeAngerReductionSpeed = 0.5f;
	public float headDistMultiplier = 0.01f;
	public float maxHeadDist = 0.012f;

	public Transform head;
	public AudioSource headPopSound;
	public AudioSource complaintSound;

	public List<AudioClip> complaints;

    public GameDirector director;

	protected Vector3 neutralHeadPos;
	protected Vector3 neutralHeadscale;

    protected bool hittingPlayer = false;

    private void Start()
    {
        angriness = 0.0f;
		neutralHeadPos = head.localPosition;
		neutralHeadscale = head.localScale;
        hittingPlayer = false;
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

		if (head)
		{
			// Mood based head jitter: 
			float distanceMultiplier = headDistMultiplier * angriness;
			Vector3 randomDir = new Vector3(
				Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist),
				Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist),
				Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist));
			head.localPosition = neutralHeadPos + randomDir;

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

				head.transform.localScale = neutralHeadscale * angriness;
			}
			if (angriness > 2.0f)
			{//POP!
				Rigidbody rb = head.gameObject.AddComponent<Rigidbody>();
				rb.AddExplosionForce(300.0f, transform.position, 20.0f);
				head.gameObject.AddComponent<BoxCollider>();
				head.transform.localScale = neutralHeadscale;
				head.SetParent(null);
				headPopSound.Play();
				head = null;
			}

		}

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
