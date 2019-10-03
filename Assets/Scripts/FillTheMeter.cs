using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillTheMeter : MonoBehaviour {
    public Image progressBar;
    public float amt;
	// Use this for initialization
	void OnDestroy()
    {
        if(progressBar)
            progressBar.fillAmount += amt;
    }
}
