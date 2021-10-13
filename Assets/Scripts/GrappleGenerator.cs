using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGenerator : MonoBehaviour
{
    [SerializeField]
    public Camera cam;
    [SerializeField]
    GameManager manager;

    private float minX;
    private float maxX;
    int grapplePointCount;

    GameObject grapplePointPrefab;
    public List<GameObject> grapplePoints;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,10f));
        maxX = point.x;
        minX = point.x * -1;

        grapplePointCount = 0;
        grapplePointPrefab = Resources.Load<GameObject>("Grapple Point");
        grapplePoints = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gamePlaying)
        {
            // keep 10 points on screen at all times
            while (grapplePoints.Count < 10)
            {
                // todo weight towards player position
                float xPos = Random.Range(minX, maxX);
                float yPos = Random.Range(4f, 5f);

                grapplePoints.Add(Instantiate(grapplePointPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, gameObject.transform));

                grapplePointCount++;
            }
        }
    }

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // first grapple point matches the grappling hook's starting position 
        grapplePointCount = 1;
        grapplePoints.Add(Instantiate(grapplePointPrefab, new Vector3(0, -3.5f, 0), Quaternion.identity, gameObject.transform));

        for (int i = 1; i < 10; i++)
        {
            float xPos = Random.Range(minX, maxX);
            float yPos = Random.Range(i - 5f, i - 4f);

            grapplePoints.Add(Instantiate(grapplePointPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, gameObject.transform));

            grapplePointCount++;
        }
    }
}
