using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = Screen.fullScreen;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleFullscreen()
    {
        if (toggle.isOn)
        {
            Screen.fullScreen = true;
            Debug.Log("Fullscreen on");
        } else
        {
            Screen.fullScreen = false;
            Debug.Log("Fullscreen off");
        }
    }
}
