using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    GrappleGenerator grappleManager;
    [SerializeField]
    GameManager manager;

    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gamePlaying)
        {
            Vector3 targetPos = grappleManager.cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            targetPos.z = 0;
            // todo spin grapple around player according to mouse
            transform.position = targetPos;
        }
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        mousePos = new Vector2(currentEvent.mousePosition.x, grappleManager.cam.pixelHeight - currentEvent.mousePosition.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Grapple Point(Clone)")
        {
            Vector3 playerTargetPos = transform.position;
            playerTargetPos.y -= 1;
            transform.parent.position = playerTargetPos;
        }
    }
}
