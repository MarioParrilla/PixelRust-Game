using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D playerRigidBody;
    private Vector3 movement;
    private Animator playerAnimator;
    public bool enableMove;
    public Button btnResume;
    public GameObject pausePanel;

    public Joystick joystick;

    public Button btnInv, btnE, btnF;
    public GameObject box = null, tree = null, sign = null, door = null;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        btnResume.onClick.AddListener(delegate(){MainMenu.Resume(pausePanel);});

        if(PlayerPrefs.GetString("control") == "PC"){
            joystick.gameObject.SetActive(false);
            btnInv.gameObject.SetActive(false);
            btnE.gameObject.SetActive(false);
            btnF.gameObject.SetActive(false);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }else{
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector3.zero;
        if(PlayerPrefs.GetString("control") == "PC"){
            joystick.gameObject.SetActive(false);
            btnInv.gameObject.SetActive(false);
            btnE.gameObject.SetActive(false);
            btnF.gameObject.SetActive(false);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }else{
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
        UpdateAnimatioAndMove(); 

        if (pausePanel != null)
        {
            if(!MainMenu.inInventory)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pauseResume();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D usable){
        if (usable.tag == "Box")
        {
            box = usable.gameObject;
        }

        if (usable.tag == "Tree")
        {
            tree = usable.gameObject;
        }

        if (usable.tag == "Sign")
        {
            sign = usable.gameObject;
        }

        if (usable.tag == "Door")
        {
            door = usable.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D usable){
        box = null;
        tree = null;
        sign = null;
        door = null;
    }

    public void useAndroid(){
        if(box!=null) box.GetComponent<Box>().UseInAndroid();
        if(tree!=null) tree.GetComponent<CutTree>().cutAndroid();
        if(sign!=null) sign.GetComponent<Sign>().readSignAndroid();
        if(door!=null) door.GetComponent<RoomTransfer>().androidTransfer();
    }


    public void pauseResume(){
        if(MainMenu.paused) MainMenu.Resume(pausePanel);
        else MainMenu.Pause(pausePanel);
    }
    private void UpdateAnimatioAndMove(){
        if(movement != Vector3.zero){
            
            if (enableMove)
            {
                MovePlayer();
                playerAnimator.SetFloat("moveX", movement.x);
                playerAnimator.SetFloat("moveY", movement.y);
                playerAnimator.SetBool("Walking", true);
            }
        }else playerAnimator.SetBool("Walking", false);
    }

    private void MovePlayer(){
        playerRigidBody.MovePosition(transform.position + movement * speed / 20);
    }
}
