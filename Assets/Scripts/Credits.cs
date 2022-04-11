using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject panel;

    public void onCredits(){
        panel.SetActive(true);
    }

        public void exitCredits(){
        panel.SetActive(false);
    }

}
