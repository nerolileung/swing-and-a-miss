using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    private float cooldownTimerMax;
    private float cooldownTimerCurrent;
    public float advanceSpeed;
    private bool advancing;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimerMax = 5f;
        cooldownTimerCurrent = 0f;
        advanceSpeed = 1f;
        advancing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gamePlaying)
        {
            if (GetComponent<Renderer>().isVisible)
            {
                cooldownTimerCurrent -= Time.deltaTime;

                if (cooldownTimerCurrent > 0 && advancing)
                {
                    // wall advances upwards
                    Vector3 scaleDifference;
                    scaleDifference.x = 0;
                    scaleDifference.z = 0;
                    scaleDifference.y = Time.deltaTime * advanceSpeed;
                    transform.localScale += scaleDifference;
                }
                else if (cooldownTimerCurrent < 0)
                {
                    cooldownTimerCurrent += cooldownTimerMax;
                    advancing = !advancing;
                    advanceSpeed += Time.deltaTime;
                }
            }
            else // compensate for camera and player movement
            {
                transform.localScale = new Vector3(200, 3.2f, 1);
                transform.position = new Vector3(0, -6, 0);

                // reset advancement cooldown
                cooldownTimerCurrent = 5f;
                advancing = true;

                advanceSpeed += Time.deltaTime;
            }
        }
    }

    // end the game when player touches the wall
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            manager.EndGame();
    }
}
