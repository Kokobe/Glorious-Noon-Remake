using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrabController : MonoBehaviour
{

    // Grip trigger thresholds for picking up objects, with some hysteresis.
    public float grabBegin = 0f;
    public float grabEnd = .9f;
    public Rigidbody attach;

    [SerializeField]
    protected OVRInput.Controller m_controller;
    protected float m_prevFlex;

    public GameObject sword_go;
    public FixedJoint joint = null;
    public bool holding = false;
    public TextMesh debugText;
    public List<Collider> colliders = new List<Collider>();
    public List<GameObject> restricted_swords = new List<GameObject>();

    public bool isBoomerangEnchanted = false;

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag.Equals("Sword") || c.gameObject.tag.Equals("EnchanterStone"))
        {
            if ((colliders.IndexOf(c) != -1))
            {
                colliders.Remove(c);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float prevFlex = m_prevFlex;
        // Update values from inputs
        m_prevFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        CheckForGrabOrRelease(prevFlex, other);
    }

    protected void CheckForGrabOrRelease(float prevFlex, Collider c = null)
    {
        //debugText.text = "in position";
        if (((m_prevFlex >= grabBegin) && (prevFlex < grabBegin)) || Input.GetKeyDown(KeyCode.G))
        {
            GrabBegin(c);
        }
        else if (((m_prevFlex <= grabEnd) && (prevFlex > grabEnd)) || Input.GetKeyDown(KeyCode.T))
        {
            GrabEnd();
            Debug.Log("T");
        }
    }

    protected void GrabBegin(Collider c) //pick up
    {
        if (!holding && (c.gameObject.tag.Equals("Sword") || c.gameObject.tag.Equals("EnchanterStone")))
        {
            addCollider(c);
            sword_go = getSword(getClosestCollider());
            if (colliders.Count > 0 && !holding 
                && (!sword_go.GetComponent<FixedJoint>() || sword_go.GetComponent<stuck>())
                && !restricted_swords.Contains(sword_go))
            {
               //debugText.text = "pick up successful";
                pickUp();
                restricted_swords.Add(sword_go);
            }
        }
    }

    private void pickUp()
    {
        holding = true;
        var sword_transform = sword_go.transform;
        var offRot = Vector3.up;

       if(joint == null)
        {
            Destroy(sword_go.GetComponent<FixedJoint>());
            Destroy(sword_go.GetComponent<stuck>());

            sword_transform.rotation = attach.rotation;
            Offset o = sword_go.GetComponent<Offset>();
            sword_transform.position = attach.position + sword_transform.position - o.offset.position;
            //assign enchantments
            isBoomerangEnchanted = o.isBoomerangEnchanted;

            //do enchantments
            applyEnchantments(o);
            debugText.text += "" + (bool) sword_go.GetComponent<FixedJoint>();
            sword_go.GetComponent<Rigidbody>().isKinematic = false;
            joint = sword_go.AddComponent<FixedJoint>();
            debugText.text += "+";
            joint.connectedBody = attach;
        }
        else
        {
            debugText.text = "joint is there";
        }

    }

    private void applyEnchantments(Offset o)
    {

    }

    private void addCollider(Collider c)
    {
        if ((!c.gameObject.GetComponent<FixedJoint>() || c.gameObject.GetComponent<stuck>())
            && colliders.IndexOf(c) == -1)
        {
            colliders.Add(c);
        }
    }

    private Collider getClosestCollider()
    {
        var closestDist = (transform.position - colliders[0].ClosestPointOnBounds(transform.position)).sqrMagnitude;
        Collider closestCollider = colliders[0];

        foreach (Collider s in colliders)
        {
            var dist = (transform.position - s.ClosestPointOnBounds(transform.position)).sqrMagnitude;
            if (dist <= closestDist)
            {
                closestDist = dist;
                closestCollider = s;
            }
        }

        return closestCollider;
    }

    private GameObject getSword(Collider c)
    {
        GameObject sword;
        if (c.gameObject.GetComponent<Rigidbody>())
        {
            sword = c.gameObject;
        }
        else
        {
            sword = c.transform.parent.gameObject;
        }
        return sword;
    }

    protected void GrabEnd() //throw
    {
        Rigidbody sword_rigidbody = sword_go.GetComponent<Rigidbody>();
        //Destroy(joint);
        sword_rigidbody.velocity = OVRInput.GetLocalControllerVelocity(m_controller) * 1.8f;
        sword_rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(m_controller)* -1;

        //sword_rigidbody.velocity += Vector3.forward * 2f;

        Destroy(sword_go.GetComponent<FixedJoint>());


       if (isBoomerangEnchanted)
        {
            if (sword_go.GetComponent<Boomerang>())
                Destroy(sword_go.GetComponent<Boomerang>());
            var b = sword_go.AddComponent<Boomerang>();

            b.returnPosition = attach.transform;
            sword_rigidbody.useGravity = false;
        }
          
        restricted_swords.Remove(sword_go);
        holding = false;

    }

    void Start()
    {

    }


    void Update()
    { 
        if (holding)
        {
            float prevFlex = m_prevFlex;
            m_prevFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
            CheckForGrabOrRelease(prevFlex);
        }
    }
}
