using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {
	int cpt = 0;
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyUp( KeyCode.F3 ) )
		{
			Debug.Log(this.gameObject.scene.name + "_" + cpt.ToString() + ".png" + " captured.");
			ScreenCapture.CaptureScreenshot(this.gameObject.scene.name + "_" + cpt.ToString() + ".png"); 
			cpt++;
		}
	}
}
