using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button mainPlayButton;
    [SerializeField]
    private Button controlsPlayButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
