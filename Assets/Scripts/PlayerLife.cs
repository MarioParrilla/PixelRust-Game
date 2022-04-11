using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    public float lifeAmount;
    public static float lAmount;
    public Image lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        lifeBar.fillAmount = lifeAmount;
        lAmount = lifeAmount;
    }

    // Update is called once per frame
    void Update()
    {
        checkLife();
    }

    public void damage(float amount){
        lifeAmount -= amount;
        lAmount = (float) Math.Round(lifeAmount, 2);
        lifeBar.fillAmount = lifeAmount;
    }

    public void heal(float amount){
        if(lifeAmount<1){
            lifeAmount += amount;
            lAmount = (float) Math.Round(lifeAmount, 2);
            lifeBar.fillAmount = lifeAmount;
        }
    }

    public void checkLife(){
        if(lAmount<=0.0f) MainMenu.End();
    }
}
