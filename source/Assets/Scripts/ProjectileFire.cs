using UnityEngine;
using System.Collections;

public class ProjectileFire : MonoBehaviour {
    public LineRenderer catapultBandBack, catapultBandFront;
    public float maxStretch = 3f;
    public bool isBoostable = true;
    public float powerBoost = 2f;
    public float resetSpeed = 0.025f;
    public ReloadProjectile reload;

    private CameraMovement camera;
    private bool boostActivated;
    private bool bulletLaunched = false;
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
        boostActivated = false;
    }

	void Start () {
        camera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
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
        //si la balle n'est pas partie
        if (spring != null)
        {
            //deplacement du boulet à la position de la souris si on clique
            if (Input.GetMouseButton(0))
            {
                spring.enabled = false;
                Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 catapultToMouse = mouseInWorld - catapult.position;

                if (catapultToMouse.sqrMagnitude > maxStretch * maxStretch)
                {
                    mouseRay.direction = catapultToMouse;
                    mouseInWorld = mouseRay.GetPoint(maxStretch);
                }

                mouseInWorld.z = 0;
                transform.position = mouseInWorld;   
            }
            else
            {
                //on détruit les cordes quand la balle commence à ralentir
                if (!rigidbody.isKinematic && prevVelocity.sqrMagnitude > rigidbody.velocity.sqrMagnitude)
                {
                    Destroy(spring);
                    rigidbody.velocity = prevVelocity;
                }
                prevVelocity = rigidbody.velocity;
            }
            updateLine();
        }
        else
        {
            catapultBandBack.enabled = false;
            catapultBandFront.enabled = false;

            //Boost de la balle
            if (Input.GetButtonDown("Fire1") && isBoostable && !boostActivated)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x * powerBoost, rigidbody.velocity.y);
                boostActivated = true;
            }

            //destruction de la balle
            if (rigidbody.velocity.sqrMagnitude < resetSpeed)
            {
                reload.reload();
                Destroy(this.gameObject);
            }
        }
	}

    //mise a jour des corde de la catapulte
    void updateLine()
    {
        Vector2 catapultToProjectile = transform.position - catapultBandFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + projectileRadius);
        catapultBandBack.SetPosition(1, holdPoint);
        catapultBandFront.SetPosition(1, holdPoint);
    }

    //feu !!
    void OnMouseUp()
    {
        if (spring != null)
        {
            spring.enabled = true;
            camera.playParticles();
        }
        rigidbody.isKinematic = false;
    }
}
