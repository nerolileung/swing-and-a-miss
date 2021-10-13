using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    GrappleGenerator grappleManager;
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GameObject player;

    GrappleStates state;
    Vector2 mousePos;
    Vector3 attachPos;

    enum GrappleStates
    {
        AIMING = 0,
        FIRING,
        PULLING
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GrappleStates.AIMING;
        mousePos = new Vector2();
        attachPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gamePlaying)
        {
            switch (state)
            {
                case GrappleStates.AIMING:
                    Vector3 targetPos = grappleManager.cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                    targetPos.z = 0;
                    // position grapple along player-to-mouse vector
                    transform.position = Vector3.MoveTowards(attachPos, targetPos, 0.5f);
                break;
                case GrappleStates.FIRING:
                break;
                case GrappleStates.PULLING:
                    if (Vector3.Distance(transform.position,player.transform.position) < 0.1f)
                        state = GrappleStates.AIMING;
                break;
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
                state = GrappleStates.FIRING;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (state == GrappleStates.FIRING && other.gameObject.name == "Grapple Point(Clone)")
        {
            state = GrappleStates.PULLING;
            transform.position = other.transform.position;
            attachPos = transform.position;

            /*Vector3 playerTargetPos = transform.position;
            playerTargetPos.y -= 1;
            transform.parent.position = playerTargetPos;*/
        }
    }
}
