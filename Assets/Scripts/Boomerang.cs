using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {
    public Transform returnPosition;
    public float throwSpeed;
    public float angularSpeed;
    public float throwDistance;

    Rigidbody r;


    void Awake()
    {
        r = GetComponent<Rigidbody>();

    }


    void OnCollisionExit(Collision c)
    {
        if (r.velocity.magnitude < 3 && !c.gameObject.tag.Equals("Sword"))
        {
            throwDistance = 0;
            r.angularVelocity *= angularSpeed;
        }
        else if (r.velocity.magnitude < 15)
        {
            r.angularVelocity *= angularSpeed;
            throwDistance = r.velocity.magnitude;

        }
        else
        {
            r.angularVelocity *= angularSpeed;
            throwDistance = 15f;

        }

    }

    void OnEnable()
    {
        throwDistance = r.velocity.magnitude;
        throwSpeed = 1f;
        angularSpeed = 1f;
    }

    void FixedUpdate()
    {
        float distance = (transform.position - returnPosition.position).magnitude;



        if (distance < throwDistance)
        {

           


        }
        else
        {


            if (distance != 0)
            {
                if ((transform.position - returnPosition.position).magnitude < 15f)
                    r.velocity = -1 * (transform.position - returnPosition.position) * throwSpeed;
                else
                    r.velocity = -1 * (transform.position - returnPosition.position).normalized * 17f * throwSpeed;

            }
            else
            {
                r.velocity = Vector3.zero;

            }

          

        }



    }
}
