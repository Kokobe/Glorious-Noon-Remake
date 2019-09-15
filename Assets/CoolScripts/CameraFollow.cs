using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject actor;
    public Vector3 offset;
    public float smoothTime = 0.3F;
    public Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPosition = actor.transform.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        var targetRotation = Quaternion.LookRotation(actor.GetComponent<Rigidbody>().velocity);
        // rotation *= Quaternion.Euler(0, -90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
    }
}
