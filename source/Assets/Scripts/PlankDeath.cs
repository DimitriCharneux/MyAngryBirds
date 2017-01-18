using UnityEngine;
using System.Collections;

public class PlankDeath : MonoBehaviour {
    public float hp = 50;

    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Damager")
            return;

        hp-= collision.relativeVelocity.sqrMagnitude;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
    }
}
