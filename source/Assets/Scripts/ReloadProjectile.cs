using UnityEngine;
using System.Collections;

public class ReloadProjectile : MonoBehaviour {
    public GameObject projectilereference;

    private GameObject projectile;

    // Use this for initialization
    void Start()
    {
        reload();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == projectile.GetComponent<Collider2D>())
        {
            reload();
        }
        Destroy(other.gameObject);
 
    }

    public void reload()
    {
        GameObject.Find("Main Camera").GetComponent<CameraMovement>().stopParticles();
        projectile = (GameObject)Instantiate(projectilereference, projectilereference.transform.parent);
        projectile.SetActive(true);

        projectile.GetComponent<Rigidbody2D>().isKinematic = true;


        CameraMovement.projectile = projectile.transform;
    }

}
