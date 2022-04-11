using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransfer : MonoBehaviour
{
    public Vector3 movePlayer;
    public Text textToUse;
    private CameraMovement camera;
    private bool playerStay;
    private Collider2D player;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStay) Transfer();
    }

    void OnTriggerStay2D(Collider2D obj){
        if(obj.CompareTag("Player")){
            textToUse.text = "Usar F";
            textToUse.enabled = true;
            player = obj;
            playerStay = true;
        }
    }

        void OnTriggerEnter2D(Collider2D obj){
            if(obj.CompareTag("Player")){
                textToUse.text = "Usar F";
                textToUse.enabled = true;
                player = obj;
                playerStay = true;
            }
        }

    void OnTriggerExit2D(Collider2D obj){
        textToUse.enabled = false;
        playerStay = false;
        player = null;
    }

    private void Transfer(){
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.transform.position = movePlayer;
            camera.transform.position = new Vector3(movePlayer.x, movePlayer.y, -1);    
            playerStay = false;
        }
    }

    public void androidTransfer(){
        player.transform.position = movePlayer;
        camera.transform.position = new Vector3(movePlayer.x, movePlayer.y, -1);    
        playerStay = false;
    }
}
