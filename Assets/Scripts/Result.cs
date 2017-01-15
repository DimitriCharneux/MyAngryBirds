using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
    public GameObject[] targets;
    public GameObject win, lose;
    public UnityEngine.UI.Text nbTargets;

	// Use this for initialization
	void Start () {
	    targets = GameObject.FindGameObjectsWithTag("Target");
    }
	
	// Update is called once per frame
	void Update () {
        targets = GameObject.FindGameObjectsWithTag("Target");
        nbTargets.text = "Il reste " +targets.Length + " cible(s).";
        if (targets.Length <= 0)
        {
            win.SetActive(true);
            Time.timeScale = 0;
        }
	}
}
