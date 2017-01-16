using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {
    public GameObject[] targets;
    public GameObject win, lose, menu;
    public UnityEngine.UI.Text nbTargets;

    private bool isTerminated;
	// Use this for initialization
	void Start () {
	    targets = GameObject.FindGameObjectsWithTag("Target");
        isTerminated = false;
    }
	
	// Update is called once per frame
	void Update () {
        targets = GameObject.FindGameObjectsWithTag("Target");
        nbTargets.text = "Il reste " +targets.Length + " cible(s).";
        if (targets.Length <= 0 && !isTerminated)
        {
            win.SetActive(true);
            menu.SetActive(true);
            Time.timeScale = 0;
            isTerminated = true;
        }
	}

    public void PauseButton()
    {
        if (menu.activeSelf)
        {
            startTime();
        } else
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        } 
    }

    public void startTime()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void ReloadButton()
    {
        startTime();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Level1Button()
    {
        startTime();
        Application.LoadLevel("level1");
    }

    public void Level2Button()
    {
        startTime();
        Application.LoadLevel("level2");
    }
}
