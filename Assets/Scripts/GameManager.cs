using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private GrappleGenerator generator;

    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject grapple;
    [SerializeField]
    GameObject deathWall;

    public bool gamePlaying;

    // Start is called before the first frame update
    void Start()
    {
        gamePlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gamePlaying = true;
        
        // reset everything to starting positions
        player.transform.position = new Vector3(0, -4, 0);
        grapple.GetComponent<Grapple>().Reset();
        deathWall.transform.localScale = new Vector3(200, 5, 1);
        deathWall.GetComponent<DeathWall>().advanceSpeed = 1f;

        generator.Reset();
    }

    public void EndGame()
    {
        gamePlaying = false;
        endScreen.SetActive(true);
    }
}
