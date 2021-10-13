using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMove : MonoBehaviour
{
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GrappleGenerator generator;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Grapple grapple;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // scroll up, but only when player is at rest
        if (manager.gamePlaying && player.transform.position.y > 0f && grapple.IsAiming())
        {
            foreach (Transform child in transform)
            {
                child.position -= Vector3.up * Time.deltaTime;
            }
            player.GetComponent<TargetJoint2D>().target -= Vector2.up * Time.deltaTime;
            grapple.MoveDown(Vector2.up * Time.deltaTime);
            generator.MoveDown(Vector3.up * Time.deltaTime);
        }
    }
}
