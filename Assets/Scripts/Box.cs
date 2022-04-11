using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public List<GameObject> loot = new List<GameObject>();
    public Text toUse;
    public Sprite usedSprite;
    public bool used;
    private bool playerStay;

    private bool pressedFAndroid = false;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!used) Open();
    }

    void OnTriggerStay2D(Collider2D obj){
        if (!used)
        {
            if(obj.CompareTag("Player")){
                toUse.text = "Usar F";
                toUse.enabled = true;
                playerStay = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        toUse.enabled = false;
        playerStay = false;
    }

    private void Open(){
        if (playerStay)
        {
            if (Input.GetKey(KeyCode.F))
            {
                GameObject.Instantiate(loot[Random.Range(0, loot.Count)], new Vector3(transform.position.x+Random.Range(-2.0f, 2.0f), transform.position.y+Random.Range(-2.0f, 2.0f), transform.position.z), transform.rotation);
                GetComponent<SpriteRenderer>().sprite = usedSprite;
                used = true;
                toUse.enabled = false;  
            }
        }
    }

    public void UseInAndroid(){
        if (!used)
        {
            if (playerStay)
            {
                Debug.Log("wat");                
                GameObject.Instantiate(loot[Random.Range(0, loot.Count)], new Vector3(transform.position.x+Random.Range(-2.0f, 2.0f), transform.position.y+Random.Range(-2.0f, 2.0f), transform.position.z), transform.rotation);
                GetComponent<SpriteRenderer>().sprite = usedSprite;
                used = true;
                toUse.enabled = false;  
            }
        }
    }
}
