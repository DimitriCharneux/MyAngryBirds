using UnityEngine;
using System.Collections;

public class TargetDeath : MonoBehaviour {
    public float damagedImpactSpeed;
    public int hitPoints = 2;

    private int currentHitPoints ;
    private float damagedImpactSpeedSqr;

    // Use this for initialization
    void Start () {
        damagedImpactSpeedSqr = damagedImpactSpeed * damagedImpactSpeed;
        currentHitPoints = hitPoints;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Damager")
            return;
        if (collision.relativeVelocity.sqrMagnitude < damagedImpactSpeedSqr)
            return;
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }


	// Update is called once per frame
	void Update () {

	}
}
