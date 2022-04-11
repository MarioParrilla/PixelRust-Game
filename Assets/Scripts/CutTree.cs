using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutTree : MonoBehaviour
{

    public Text toCut;
    public Sprite[] sprites;
    public GameObject wood;
    public int state;
    private SpriteRenderer spriteRenderer;
    private bool playerStay;
    private SpawnScript spawnScript;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = 0;
        spawnScript =  GameObject.Find("SpawnManager").GetComponent<SpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Cut();
    }

    void OnTriggerStay2D(Collider2D obj){
        if(obj.CompareTag("Player")){
            playerStay = true;
        }
    }

    void OnTriggerEnter2D(Collider2D obj){
        if(obj.CompareTag("Player")){
            playerStay = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        toCut.enabled = false;
        playerStay = false;
    }

    private void Cut(){
        if (playerStay)
        {
            toCut.text = "Usar F";
            toCut.enabled = true;
            if (Input.GetKeyDown("f"))
            {
                state++;
                GameObject.Instantiate(wood, new Vector3(transform.position.x+Random.Range(-2.0f, 2.0f), transform.position.y+Random.Range(-2.0f, 2.0f), transform.position.z), transform.rotation);                                                                            
                if (state==5){
                    Destroy(this.gameObject);
                    spawnScript.spawnTree();
                }
                else{
                    spriteRenderer.sprite = sprites[state];
                }
            }
        }
    }

    public void cutAndroid(){
        if (playerStay)
        {
            toCut.text = "Usar F";
            toCut.enabled = true;
            state++;
            GameObject.Instantiate(wood, new Vector3(transform.position.x+Random.Range(-2.0f, 2.0f), transform.position.y+Random.Range(-2.0f, 2.0f), transform.position.z), transform.rotation);                                                                            
            if (state==5){
                Destroy(this.gameObject);
                spawnScript.spawnTree();
            }
            else{
                spriteRenderer.sprite = sprites[state];
            }
        }
    }

}
