using UnityEngine;

public class Mood : MonoBehaviour {
    public float angriness = 0.0f;
    public float angerSpeed = 0.01f;

	public float headDistMultiplier = 0.05f;
	public float maxHeadDist = 0.012f;

	public Transform head;

    public GameDirector director;

	protected Vector3 neutralHeadPos;

    protected bool hittingPlayer = false;

    private void Start()
    {
        angriness = 0.0f;
		neutralHeadPos = head.localPosition;
        hittingPlayer = false;
    }

    private void FixedUpdate()
    {
        if (hittingPlayer)
        {
            MakeMoreAngry();
        }

		// Mood based head jitter: 
		float distanceMultiplier = headDistMultiplier * angriness;
		Vector3 randomDir = new Vector3(
			Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist),
			Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist),
			Mathf.Clamp( Random.Range(-distanceMultiplier, distanceMultiplier), -maxHeadDist, maxHeadDist));
		head.localPosition = neutralHeadPos + randomDir;

        if (angriness > 1.0f)
        {
            director.DudeGotAngry();
        }
    }

    public void MakeMoreAngry()
    {
        angriness += angerSpeed;
    }

}
