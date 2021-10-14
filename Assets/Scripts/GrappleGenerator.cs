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
    Vector3 highestPoint;

    GameObject grapplePointPrefab;
    GrapplePoint[] grapplePoints;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,10f));
        maxX = point.x - 1;
        minX = (point.x * -1) + 1;

        grapplePointPrefab = Resources.Load<GameObject>("Grapple Point");
        grapplePointCount = 10;
        grapplePoints = new GrapplePoint[grapplePointCount];
        for (int i = 0; i < grapplePointCount; i++)
        {
            grapplePoints[i] = Instantiate(grapplePointPrefab, new Vector3(0,-8,0), Quaternion.identity, gameObject.transform).GetComponent<GrapplePoint>();
        }
        highestPoint = Vector3.zero;
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
        for (int i = 0; i < grapplePointCount; i++)
        {
            grapplePoints[i].SetTransform(new Vector3(0,-8,0));
            grapplePoints[i].SetActive(false);
        }

        // first grapple point matches the grappling hook's starting position 
        highestPoint = new Vector3(0, -3.5f, 0);
        grapplePoints[0].SetTransform(highestPoint);
        grapplePoints[0].SetActive(true);

        GeneratePoints();
    }

    void GeneratePoints()
    {
        for (int i = 0; i < grapplePointCount; i++)
        {
            if (!grapplePoints[i].IsActive())
            {
                // weight towards previous point
                float localMaxX = highestPoint.x + 4f;
                if (localMaxX > maxX) localMaxX = maxX;

                float localMinX = highestPoint.x - 4f;
                if (localMinX < minX) localMinX = minX;

                Vector3 pos = Vector3.zero;

                do {
                    pos.x = Random.Range(localMinX, localMaxX);
                    pos.y = Random.Range(highestPoint.y, highestPoint.y + 3);
                } while (Vector3.Distance(pos, highestPoint) < 2);

                grapplePoints[i].SetTransform(pos);

                if (pos.y > highestPoint.y)
                {
                    highestPoint = pos;
                }

                grapplePoints[i].SetActive(true);
            }
        }
    }

    public void MoveDown(Vector3 distance)
    {
        transform.position = Vector3.zero;
        foreach (Transform child in transform)
        {
            child.position -= distance;
        }
        highestPoint -= distance;
    }
}
