using UnityEngine;

public class Mood : MonoBehaviour {

	public float angriness = 0.0f;
	public float angerSpeed = 0.01f;
	public float relativeAngerReductionSpeed = 0.5f;
	public float headDistMultiplier = 0.01f;
	public float maxHeadDist = 0.012f;

	public Transform head;
	public AudioSource headPopSound;

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

			if (angriness > 1.0f)
			{// Game Over
				director.PlayerStared();

				head.transform.localScale = neutralHeadscale * angriness;
			}
			if (angriness > 2.0f)
			{//POP!
				head.gameObject.AddComponent<Rigidbody>();
				head.gameObject.AddComponent<BoxCollider>();
				head.SetParent(null);
				headPopSound.Play();
				head = null;
			}

		}

    }

    public void MakeMoreAngry()
    {
		angriness += angerSpeed;
    }

}
