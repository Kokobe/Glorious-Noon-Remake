using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrabController : MonoBehaviour
{
    public Animator anim;

    // Grip trigger thresholds for picking up objects, with some hysteresis.
    public float grabBegin = 0f;
    public float grabEnd = .9f;
    public float grabRadius = .3f;
    public Rigidbody attach;

    [SerializeField]
    protected OVRInput.Controller m_controller;
    protected float m_prevFlex;

    public Transform rig;
    public GameObject sword_go;
    public FixedJoint joint = null;
    public bool holding = false;
    public TextMesh debugText;
    public Swords_Manager swords_manager;
    private List<GameObject> restricted_swords;
    public GameObject[] go_grabbables;
    private Dictionary<GameObject, float> grabbables = new Dictionary<GameObject, float>();

    public bool isBoomerangEnchanted = false;

    public Rigidbody sword_rigidbody;

    protected void CheckForGrabOrRelease(float prevFlex)
    {
        //debugText.text = "in position";
        if (((m_prevFlex >= grabBegin) && (prevFlex < grabBegin)) || Input.GetKeyDown(KeyCode.G))
        {
            GrabBegin();
            anim.Play("Grab", -1, 0f);

        }
        else if (((m_prevFlex <= grabEnd) && (prevFlex > grabEnd)) || Input.GetKeyDown(KeyCode.T))
        {
            GrabEnd();
            anim.Play("Grab 0", -1, 1f);

        }
    }

    protected void GrabBegin() //pick up
    {
        if (!holding)
        {
            sword_go = getClosesetSword();
          
            if (sword_go != null
                && (!sword_go.GetComponent<FixedJoint>() || sword_go.GetComponent<stuck>())
                && !restricted_swords.Contains(sword_go))
            {
                pickUp();
                restricted_swords.Add(sword_go);
            }
        }
    }

    private GameObject getClosesetSword()
    {
        float closest = -1f;
        GameObject closest_go = null;
        foreach(GameObject go in grabbables.Keys)
        {
            if (closest < 0 || grabbables[go] <= closest)
            {
                closest = grabbables[go];
                closest_go = go;
            }
        }


        if (closest <= grabRadius)
            return closest_go;
        else
            return null;
    }

    private void pickUp()
    {
        holding = true;
        var sword_transform = sword_go.transform;
        var offRot = Vector3.up;

        sword_rigidbody = sword_go.GetComponent<Rigidbody>();

        if (joint == null)
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
          // debugText.text += "" + (bool) sword_go.GetComponent<FixedJoint>();
            sword_go.GetComponent<Rigidbody>().isKinematic = false;
            joint = sword_go.AddComponent<FixedJoint>();
           // debugText.text += "+";
            joint.connectedBody = attach;
        }
        else
        {
          // debugText.text = "joint is there";
        }

    }

    private void applyEnchantments(Offset o)
    {

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
        if (joint != null)
        {
            Destroy(sword_go.GetComponent<FixedJoint>());
            joint = null;
            sword_rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(m_controller) * -1;
            sword_rigidbody.velocity = rig.transform.rotation*  OVRInput.GetLocalControllerVelocity(m_controller) *
                                        (sword_rigidbody.angularVelocity.magnitude/10f);
           // sword_rigidbody.velocity = Quaternion.Euler(0, rig.rotation.eulerAngles.y, 0) * sword_rigidbody.velocity;
            //sword_rigidbody.velocity += (Vector3.forward * 10f);

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
    }

    void Start()
    {
        restricted_swords = swords_manager.restricted_swords;
        foreach (GameObject go in go_grabbables)
        {
            grabbables.Add(go, 0f);
        }
    }
    Dictionary<GameObject, float> cloneDictionary()
    {
        Dictionary<GameObject, float> newDict = new Dictionary<GameObject, float>();
        foreach (GameObject go in grabbables.Keys)
        {
            newDict.Add(go, grabbables[go]);
        }
        return newDict;
    }

    void Update()
    {
        Dictionary<GameObject, float> copy = cloneDictionary();
        foreach (GameObject go in grabbables.Keys)
        {
            copy[go] = Vector3.Distance(attach.transform.position, go.transform.position);

        }
        grabbables = copy;

        float prevFlex = m_prevFlex;
        m_prevFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        CheckForGrabOrRelease(prevFlex);

    }
}
