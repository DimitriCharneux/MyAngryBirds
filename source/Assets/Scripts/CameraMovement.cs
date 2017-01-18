using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public Transform left, right, projectile;
	public ParticleSystem particles;
    public static Transform projectileS;

    private Vector3 leftPosition;
	private float xStartParticles;
	void Start () {
		if (particles != null) {
			particles.GetComponent<Renderer> ().sortingLayerName = "Foreground";
			particles.GetComponent<Renderer> ().sortingOrder = 1;
			xStartParticles = particles.transform.position.x;
		}
        projectileS = projectile;
        leftPosition = transform.position;
        StartCoroutine(SawLevel());
    }
	
	void Update () {
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(projectileS.position.x, leftPosition.x, right.position.x);
		transform.position = p;

		if(particles != null){
			particles.transform.position = projectileS.position;
			if (particles.transform.position.x < xStartParticles) {
				particles.Pause ();
			} else {
				playParticles ();
			}
		}
	}

	void playParticles(){
		if(particles.isPaused){
			particles.Clear ();
			particles.Play ();
		}
	}

    IEnumerator SawLevel()
    {
		yield return new WaitForSeconds(1f);
        while (Vector3.Distance(leftPosition, left.position) > 0.05f)
        {
            leftPosition = Vector3.Lerp(leftPosition, left.position, 2 * Time.deltaTime);
            yield return null;
        }
    }
}
