using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText, toUse;
    public string messageText;
    private bool playerStay;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStay) ReadSign();
    }

    void OnTriggerStay2D(Collider2D obj){
        if(obj.CompareTag("Player")){
            toUse.text = "Usar F";
            toUse.enabled = true;
            playerStay = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        dialogBox.SetActive(false);
        toUse.enabled = false;
        playerStay = false;
    }

    private void ReadSign(){
        if (Input.GetKey(KeyCode.F))
        {
            dialogText.text = messageText;
            dialogBox.SetActive(true);
        }
    }

    public void readSignAndroid(){
        dialogText.text = messageText;
        dialogBox.SetActive(true);
    }
}
