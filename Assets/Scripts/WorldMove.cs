using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMove : MonoBehaviour
{
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GrappleGenerator generator;
    GameObject player;
    Grapple grapple;

    // Start is called before the first frame update
    void Start()
    {
        player = manager.GetPlayer();
        grapple = manager.GetGrapple().GetComponent<Grapple>();
    }

    // Update is called once per frame
    void Update()
    {
        // scroll up when grapple has advanced
        if (manager.gamePlaying && grapple.prevPos.y > -1 && grapple.IsAttached())
        {
            float modifier = Mathf.Lerp(1, 10, (grapple.prevPos.y+1)/7);
            foreach (Transform child in transform)
            {
                child.position -= Vector3.up * Time.deltaTime * modifier;
                if (child.gameObject.name == "Death Wall")
                {
                    child.position += Vector3.up * Time.deltaTime * modifier * 0.5f;
                }
            }
            player.GetComponent<TargetJoint2D>().target -= Vector2.up * Time.deltaTime * modifier;
            grapple.MoveDown(Vector2.up * Time.deltaTime * modifier);
            generator.MoveDown(Vector3.up * Time.deltaTime * modifier);
        }
    }
}