using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField]
    private Button quitButtonStart;
    [SerializeField]
    private Button quitButtonEnd;

    // Start is called before the first frame update
    void Start()
    {
        quitButtonStart.onClick.AddListener(Application.Quit);
        quitButtonEnd.onClick.AddListener(Application.Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
