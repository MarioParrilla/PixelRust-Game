using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn : MonoBehaviour
{
    public Button btnPlayAgain;
    // Start is called before the first frame update
    void Start()
    {   
        btnPlayAgain.onClick.AddListener(delegate(){MainMenu.gameScene();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
