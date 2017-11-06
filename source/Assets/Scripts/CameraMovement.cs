using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public Transform left, right;
	public ParticleSystem particles;
    public static Transform projectile;

    private Vector3 leftPosition;
	void Start () {
        leftPosition = transform.position;
        StartCoroutine(SawLevel());
        particles.Pause();
    }
	
	void Update () {
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(projectile.position.x, leftPosition.x, right.position.x);
		transform.position = p;

		if(particles != null){
			particles.transform.position = projectile.position;
		}
	}

    public void stopParticles()
    {
        particles.Pause();
    }

    public void playParticles(){
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
