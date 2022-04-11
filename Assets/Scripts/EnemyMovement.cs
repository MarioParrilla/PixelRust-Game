using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Coroutine c, c2;
    private bool run = false, inContact = false;
    private Animator playerAnimator;
    private GameObject player;

    public int speed, vision;
    // Start is called before the first frame update
    void Start()
    {
        run = true;
        playerAnimator = GetComponent<Animator>(); 
        player = GameObject.Find("Player");
        c = StartCoroutine(move());
    }

    void Update(){

    }

        void OnTriggerEnter2D(Collider2D obj){
        if(obj.CompareTag("Player") && !inContact){
            run = false;
            inContact = true;
            StopCoroutine(move());
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        if(obj.CompareTag("Player") && inContact){
            StartCoroutine(moveColddown());
        }
    }

    IEnumerator moveColddown() {
        yield return new WaitForSeconds(1f);
        run = true;
        StartCoroutine(move());
        inContact = false;
    }

    IEnumerator move() {
        int count = 0;
        while (run)
        {   
            if(Vector2.Distance(transform.position, player.transform.position) < vision){
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                yield return new WaitForSeconds(0);
            }else{
                if(count < 5){
                transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
                playerAnimator.SetFloat("moveX", 0);
                playerAnimator.SetFloat("moveY", 1);
                count++;
                }
                else if(count < 10){
                    transform.position = new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
                    playerAnimator.SetFloat("moveX", 1);
                    playerAnimator.SetFloat("moveY", 0);
                    count++;
                }
                else if(count < 15){
                    transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
                    playerAnimator.SetFloat("moveX", 0);
                    playerAnimator.SetFloat("moveY", -1);
                    count++;
                }            
                else if(count < 20){
                    transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
                    playerAnimator.SetFloat("moveX", -1);
                    playerAnimator.SetFloat("moveY", 0);
                    count++;
                }
                else count = 0;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
