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
        maxX = point.x - 1;
        minX = (point.x * -1) + 1;

        grapplePointPrefab = Resources.Load<GameObject>("Grapple Point");
        grapplePoints = new List<GameObject>();

        GrapplePoint.generator = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gamePlaying)
        {
            // keep 10 points on screen at all times
            GeneratePoints();
        }
    }

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // first grapple point matches the grappling hook's starting position 
        grapplePoints.Add(Instantiate(grapplePointPrefab, new Vector3(0, -3.5f, 0), Quaternion.identity, gameObject.transform));

        GeneratePoints();
    }

    void GeneratePoints()
    {
        while (grapplePoints.Count < 10)
        {
            Vector3 previousPointPos = grapplePoints[grapplePoints.Count-1].transform.position;

            // weight towards previous point
            float localMaxX = previousPointPos.x + 4f;
            if (localMaxX > maxX) localMaxX = maxX;

            float localMinX = previousPointPos.x - 4f;
            if (localMinX < minX) localMinX = minX;

            float xPos = Random.Range(localMinX, localMaxX);
            float yPos = Random.Range(previousPointPos.y + 1, previousPointPos.y + 4);

            grapplePoints.Add(Instantiate(grapplePointPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, gameObject.transform));
        }
    }

    public void MoveDown(Vector3 distance)
    {
        transform.position = Vector3.zero;
        foreach (Transform child in transform)
        {
            child.position -= distance;
        }
    }
}
