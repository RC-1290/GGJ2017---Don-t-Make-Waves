using UnityEngine;

public class Gaze : MonoBehaviour {

    public float gazeDistance = 16;
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
        bool hit = Physics.Raycast( lookRay, out hitInfo, gazeDistance, hitMask);
		if (hit)
        {
            Mood somethingMoody = hitInfo.collider.GetComponentInParent<Mood>();
			
            if (somethingMoody)
            {
				Debug.DrawLine(lookRay.origin, hitInfo.point, Color.green);
                somethingMoody.MakeMoreAngry();
            }
        }
		else
		{
			Debug.DrawLine(lookRay.origin, lookRay.origin + lookRay.direction  * gazeDistance, Color.red);
		}
    }

}
