using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public Ninja n;
    public Image i;
    int maxHealth;
    public GlowUpAndDown g;
	// Use this for initialization
	void Start () {
        if(n)
        maxHealth = n.health;
	}
	
	// Update is called once per frame
	void Update () {
        if(n)
            i.fillAmount = (float) (maxHealth - n.hits) / maxHealth;
        else if(g)
        {
            i.fillAmount = (g.t + 1f) / (2f);
        }

	}
}
