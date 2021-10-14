using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    GrappleGenerator grappleManager;
    [SerializeField]
    GameManager manager;
    GameObject player;

    GrappleStates state;
    Vector2 mousePos;
    public Vector2 prevPos;

    Rigidbody2D rb;
    TargetJoint2D joint;

    enum GrappleStates
    {
        AIMING = 0,
        LAUNCH,
        FIRING,
        RETURNING,
        PULLING
    }

    // Start is called before the first frame update
    void Start()
    {
        player = manager.GetPlayer();

        state = GrappleStates.AIMING;
        mousePos = Vector2.zero;
        prevPos = new Vector2(0, -3.5f);

        rb = gameObject.GetComponent<Rigidbody2D>();

        joint = gameObject.GetComponent<TargetJoint2D>();
        joint.enabled = false;
    }

    void FixedUpdate()
    {
        if (manager.gamePlaying)
        {
            if (state == GrappleStates.AIMING || state == GrappleStates.LAUNCH)
            {
                // need to know where the mouse is for both of these states
                Vector3 targetPos = grappleManager.cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                targetPos.z = 0;
                Vector2 direction = new Vector2(targetPos.x, targetPos.y) - prevPos;


                if (state == GrappleStates.AIMING)
                {
                    // position grapple according to mouse
                    rb.MovePosition(prevPos + Vector2.ClampMagnitude(direction, 0.5f));
                }
                else
                {
                    // add force to grapple in mouse direction
                    rb.AddForce(direction, ForceMode2D.Impulse);
                    state = GrappleStates.FIRING;
                }
            }
            // pull grapple back if it goes too far or slows down
            else if (state == GrappleStates.FIRING && (Vector2.Distance(rb.position, prevPos) > 5 || rb.velocity.magnitude < 1) && !joint.enabled)
            {
                joint.target = prevPos;
                joint.enabled = true;
                state = GrappleStates.RETURNING;
                rb.velocity = Vector2.zero;
            }
            // return to aiming if grapple has returned
            else if (state == GrappleStates.RETURNING && Vector2.Distance(rb.position, joint.target) < 0.1f)
            {
                joint.enabled = false;
                state = GrappleStates.AIMING;
            }
            // also return to aiming if player is close enough
            else if (state == GrappleStates.PULLING && Vector2.Distance(rb.position, player.transform.position) < 1)
            {
                state = GrappleStates.AIMING;
            }
        }
    }

    void OnGUI()
    {
        if (state == GrappleStates.AIMING && manager.gamePlaying)
        {
            Event currentEvent = Event.current;

            // get mouse location
            mousePos = new Vector2(currentEvent.mousePosition.x, grappleManager.cam.pixelHeight - currentEvent.mousePosition.y);

            // did left mouse button get clicked?
            if (currentEvent.button == 0 && currentEvent.isMouse && state == GrappleStates.AIMING)
            {
                state = GrappleStates.LAUNCH;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (state == GrappleStates.FIRING && other.gameObject.name == "Grapple Point(Clone)")
        {
            state = GrappleStates.PULLING;

            // score based on distance between previous and current point
            manager.score += Mathf.FloorToInt(Vector2.Distance(rb.position, prevPos));

            // set new grapple positions
            transform.position = other.transform.position;
            prevPos = new Vector2(transform.position.x, transform.position.y);
            rb.velocity = Vector2.zero;

            // set new player positions
            player.GetComponent<TargetJoint2D>().target = rb.position;
        }
    }

    public void Reset()
    {
        state = GrappleStates.AIMING;
        transform.position = new Vector3(0, -3.5f, 0);
        prevPos = transform.position;
    }

    public bool IsAttached()
    {
        return state == GrappleStates.AIMING || state == GrappleStates.PULLING;
    }

    public void MoveDown(Vector2 distance)
    {
        prevPos -= distance;
        joint.target -= distance;
    }
}
