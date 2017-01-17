using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public Transform left, right, projectile;
    public static Transform projectileS;

    private Vector3 leftPosition;
	void Start () {
        projectileS = projectile;
        leftPosition = transform.position;
        StartCoroutine(SawLevel());
    }
	
	void Update () {
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(projectileS.position.x, leftPosition.x, right.position.x);
        transform.position = p;
	}

    IEnumerator SawLevel()
    {
        while (Vector3.Distance(leftPosition, left.position) > 0.05f)
        {
            leftPosition = Vector3.Lerp(leftPosition, left.position, Time.deltaTime);
            yield return null;
        }
    }
}
