using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight_Spin : MonoBehaviour
{
    public Vector3 rotAngle1;
    public Vector3 rotAngle2;
    Quaternion currentRot;
    Quaternion firstRot;
    Quaternion secondRot;
    bool goingForward = true;
    // Start is called before the first frame update
    void Start()
    {
        currentRot = transform.rotation;
        firstRot = Quaternion.Euler(rotAngle1);
        secondRot = Quaternion.Euler(rotAngle2);
    }

    IEnumerator Rotate()
    {
        var t = 0f;
        Quaternion from = transform.rotation;
        while (t <= 1)
        {
            transform.rotation = (goingForward)? Quaternion.Lerp(from, secondRot, t) : Quaternion.Lerp(from, firstRot, t);
            t += Time.fixedDeltaTime;
            yield return null;
        }
        goingForward = !goingForward;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Rotate());
        }


    }
}
