using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    private float cooldownTimerMax;
    private float cooldownTimerCurrent;
    private bool advancing;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimerMax = 5f;
        cooldownTimerCurrent = 0f;
        advancing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gamePlaying)
        {
            cooldownTimerCurrent -= Time.deltaTime;

            if (cooldownTimerCurrent > 0 && advancing)
            {
                // wall advances upwards
                Vector3 scaleDifference;
                scaleDifference.x = 0;
                scaleDifference.z = 0;
                scaleDifference.y = Time.deltaTime;
                transform.localScale += scaleDifference;
            }
            else if (cooldownTimerCurrent < 0)
            {
                cooldownTimerCurrent += cooldownTimerMax;
                advancing = !advancing;
            }
        }
    }

    // end the game when player touches the wall
    void OnTriggerEnter2D(Collider2D other)
    {
        manager.EndGame();
    }
}
