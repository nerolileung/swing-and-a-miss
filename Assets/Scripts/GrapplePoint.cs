using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -6)
        {
            active = false;
        }
    }

    public void SetTransform(Vector3 position)
    {
        transform.position = position;
    }
    public bool IsActive()
    {
        return active;
    }
    public void SetActive(bool toggle)
    {
        active = toggle;
    }
}
