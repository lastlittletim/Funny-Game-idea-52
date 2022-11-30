using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI manaDisplay;
    public PlayerController2 playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController == null) return; //ignore code if no player controller is assigned
        manaDisplay.text = playerController.mana.ToString(); //set text
    }
}
