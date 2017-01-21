using UnityEngine;

public class Gaze : MonoBehaviour {

    public float gazeDistace = Mathf.Infinity;
    public LayerMask hitMask;
    protected Camera faceCamera;
    private void Start()
    {
        faceCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;
		Ray lookRay = faceCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        bool hit = Physics.Raycast( lookRay, out hitInfo, gazeDistace, hitMask);
		if (hit)
        {
            Mood somethingMoody = hitInfo.collider.GetComponent<Mood>();
			
			Debug.Log("Hit something");
            if (somethingMoody)
            {
				Debug.DrawLine(lookRay.origin, lookRay.origin + lookRay.direction, Color.green);
                Debug.Log(somethingMoody);
                somethingMoody.MakeMoreAngry();
            }
        }
		else
		{
			Debug.Log("Missing Rays");
			Debug.DrawLine(lookRay.origin, lookRay.origin + lookRay.direction, Color.red);
		}
    }

}
