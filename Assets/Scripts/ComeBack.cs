using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeBack : MonoBehaviour
{

    List<Vector3[]> snapShots = new List<Vector3[]>();
    List<Quaternion> rotationShots = new List<Quaternion>();
    public bool done = false;
  
    public bool recording = false;
   
    // Use this for initialization
    void Start()
    {

        StartCoroutine(SnapShot());
        //   StartCoroutine(Move(transform.position + Vector3.right * 5f, Quaternion.identity, 5f));
    }

    IEnumerator SnapShot()
    {
        while (true)
        {




            if (recording)
            {


                Vector3[] values = { transform.position, Vector3.up * .2f }; //{position, angular velocity, time it took)
                rotationShots.Add(transform.rotation);

                snapShots.Add(values);

                yield return new WaitForSecondsRealtime(.2f);
                yield return null;

            }
            else
            {

                yield return null;

            }




        }

    }

    IEnumerator Move(Vector3 to, Quaternion toRotation, float time)
    {
        float t = 0f;
        Vector3 fromposition = transform.position;
        Quaternion fromrotation = transform.rotation;

        while (t < time)
        {

            transform.rotation = Quaternion.Lerp(fromrotation, toRotation, t / time);

            transform.position = Vector3.Lerp(fromposition, to, t / time);


            t += Time.fixedDeltaTime;
            yield return null;


        }
        done = true;



    }

    Vector3 ConvertTo360(Vector3 xyz)
    {
        Vector3 finished;

        finished.x = xyz.x % 360f;
        finished.y = xyz.y % 360f;
        finished.z = xyz.z % 360f;

        finished.x += (finished.x < 0) ? 360 : 0;
        finished.y += (finished.y < 0) ? 360 : 0;
        finished.z += (finished.z < 0) ? 360 : 0;

        return finished;


    }

    IEnumerator ReplayReverse()
    {

        print(snapShots.Count);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        for (int i = snapShots.Count - 1; i >= 0; i--)
        {

            Vector3 position = snapShots[i][0];

            Quaternion rotat = rotationShots[i];

            float time = snapShots[i][1].magnitude;


            StartCoroutine(Move(position, rotat, time));
            yield return new WaitUntil(() => done);
            done = false;
        }

        print("done");
        snapShots.Clear();
        rotationShots.Clear();
        yield return null;

    }


    public IEnumerator Replay(List<Vector3[]> positions, List<Quaternion> rotations)
    {

        print("starting replay");
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        transform.position = positions[0][0];
        transform.rotation = rotations[0];

        for (int i = 1; i < snapShots.Count; i++)
        {

            Vector3 position = positions[i][0];

            Quaternion rotat = rotations[i];

            float time = positions[i][1].magnitude;


            StartCoroutine(Move(position, rotat, time));
            yield return new WaitUntil(() => done);
            done = false;
        }

        print("done");
        snapShots.Clear();
        rotationShots.Clear();
        yield return null;

    }

    void OnCollisionEnter(Collision c)
    {
        if (!recording){
           
            recording = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //		print (transform.rotation.eulerAngles);

        if (snapShots.Count > 10 && recording)
        {

            recording = false;
         
            //	StartCoroutine (Replay(snapShots,rotationShots));
            StartCoroutine(ReplayReverse());
        }
      


    }
}
