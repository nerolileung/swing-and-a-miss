using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private GrappleGenerator generator;

    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private Text scoreEnd;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private Text scoreHUD;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject grapple;
    [SerializeField]
    GameObject deathWall;

    public bool gamePlaying;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        gamePlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying)
        {
            if (scoreHUD.text != score.ToString())
                scoreHUD.text = score.ToString();
        }
    }

    public void StartGame()
    {
        gamePlaying = true;
        score = 0;
        scoreHUD.text = "0";
        HUD.SetActive(true);
        
        // reset everything to starting positions
        player.transform.position = new Vector3(0, -4, 0);
        player.GetComponent<TargetJoint2D>().target = player.transform.position;

        grapple.GetComponent<Grapple>().Reset();

        deathWall.transform.localScale = new Vector3(200, 5, 1);
        deathWall.GetComponent<DeathWall>().advanceSpeed = 1f;

        generator.Reset();
    }

    public void EndGame()
    {
        gamePlaying = false;
        HUD.SetActive(false);

        scoreEnd.text = "Final Score: " + scoreHUD.text;
        endScreen.SetActive(true);
        player.GetComponent<TargetJoint2D>().target = player.transform.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
