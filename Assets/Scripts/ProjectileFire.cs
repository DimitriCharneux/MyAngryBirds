using UnityEngine;
using System.Collections;

public class ProjectileFire : MonoBehaviour {
    public LineRenderer catapultBandBack, catapultBandFront;
    public float maxStretch = 3f;

    private SpringJoint2D spring;
    private Transform catapult;
    private bool clickedOn;
    private Ray mouseRay, leftCatapultToProjectile;
    private Rigidbody2D rigidbody;
    private Vector2 prevVelocity;
    private float projectileRadius;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        catapult = spring.connectedBody.transform;
    }

	void Start () {
        setupLine();
        mouseRay = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catapultBandFront.transform.position, Vector3.zero);
        CircleCollider2D projectileCollider = GetComponent<CircleCollider2D>();
        projectileRadius = projectileCollider.radius;
    }

    void setupLine()
    {
        catapultBandFront.SetPosition(0, catapultBandFront.transform.position);
        catapultBandBack.SetPosition(0, catapultBandBack.transform.position);

        catapultBandFront.sortingLayerName = "Foreground";
        catapultBandBack.sortingLayerName = "Foreground";

        catapultBandFront.sortingOrder = 3;
        catapultBandBack.sortingOrder = 1;

        catapultBandFront.enabled = true;
        catapultBandBack.enabled = true;

    }

	void Update () {
        if (clickedOn) {
            //TODO modif si tactile
            Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 catapultToMouse = mouseInWorld - catapult.position;

            if (catapultToMouse.sqrMagnitude > maxStretch * maxStretch) {
                mouseRay.direction = catapultToMouse;
                mouseInWorld = mouseRay.GetPoint(maxStretch);
            }

            mouseInWorld.z = 0;
            transform.position = mouseInWorld;
        }


        if (spring != null)
        {
            if (!clickedOn)
            {
                if (!rigidbody.isKinematic && prevVelocity.sqrMagnitude > rigidbody.velocity.sqrMagnitude){
                    Destroy(spring);
                    rigidbody.velocity = prevVelocity;
                }
                prevVelocity = rigidbody.velocity;
            }
            updateLine();
        } else {
            catapultBandBack.enabled = false;
            catapultBandFront.enabled = false;
        }
	}

    void updateLine()
    {
        Vector2 catapultToProjectile = transform.position - catapultBandFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + projectileRadius);
        catapultBandBack.SetPosition(1, holdPoint);
        catapultBandFront.SetPosition(1, holdPoint);
    }

    void OnMouseDown()
    {
        if (spring != null)
        {
            spring.enabled = false;
            clickedOn = true;
        }
    }

    void OnMouseUp()
    {
        if (spring != null)
        {
            spring.enabled = true;
        }
        clickedOn = false;
        rigidbody.isKinematic = false;
    }
}
