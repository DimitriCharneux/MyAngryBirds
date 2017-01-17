using UnityEngine;
using System.Collections;

public class ReloadProjectile : MonoBehaviour {
    public GameObject projectile, projectilereference;
    public float resetSpeed = 0.025f;

    private float sqrResetSpeed;
    private Rigidbody2D projectileBody;
    private SpringJoint2D spring;
    private Collider2D projectileCollider;

    // Use this for initialization
    void Start()
    {
        projectileCollider = projectile.GetComponent<Collider2D>();
        sqrResetSpeed = resetSpeed * resetSpeed;
        spring = projectile.GetComponent<SpringJoint2D>();
        projectileBody = projectile.GetComponent<Rigidbody2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == projectileCollider)
        {
            Reset();
        } else {
            Destroy(other.gameObject);
        }
    }

    public void Reset()
    {
        Destroy(projectile);
        projectile = (GameObject)Instantiate(projectilereference, projectilereference.transform.parent);
        projectile.SetActive(true);
        projectileCollider = projectile.GetComponent<Collider2D>();
        spring = projectile.GetComponent<SpringJoint2D>();
        projectileBody = projectile.GetComponent<Rigidbody2D>();
        projectileBody.isKinematic = true;

        CameraMovement.projectileS = projectile.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Le spring sert a détecter quand le projectile est lancé.
        if (spring == null && projectileBody.velocity.sqrMagnitude < sqrResetSpeed)
        {
            Reset();
        }
    }
}
