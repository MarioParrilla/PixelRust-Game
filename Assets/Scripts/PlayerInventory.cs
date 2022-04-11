using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> bag = new List<GameObject>();
    public List<GameObject> equip = new List<GameObject>();
    public GameObject inventory;
    public GameObject selector, opcions;
    public Text toTake, diaryText;
    private PlayerMovement playerM;
    private PlayerLife playerLife;
    private int ID, ID_Equipo, idMenu;
    public bool active;
    private bool takingItem = false, pressedEAndroid = false, pressedUpAndroid = false, pressedDownAndroid = false, pressedRigthAndroid = false, pressedLeftAndroid = false;
    private Collider2D item;

    public Image[] selection;
    public Sprite[] selectionSprite;
    public int ID_Selected;

    public float itemDamage, itemShield;
    public Button btnInv, btnE, btnF, btnInvRigth, btnInvLeft, btnInvUp, btnInvDown;

    // Start is called before the first frame update
    void Start()
    {
        itemDamage = 0;
        itemShield = 0;
        playerM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerLife = GameObject.Find("Player").GetComponent<PlayerLife>();
        StartCoroutine(ClearDiary());
    }

    // Update is called once per frame
    void Update()
    {
        if (!MainMenu.paused)
        {
            if(takingItem) TakeItem();

            NavegarItems();

            if (Input.GetKeyDown(KeyCode.I)){
                inventory.SetActive(!active);
                MainMenu.inInventory = false;
                idMenu = 1;
                btnInvRigth.gameObject.SetActive(!btnInvRigth.gameObject.active);
                btnInvLeft.gameObject.SetActive(!btnInvLeft.gameObject.active);
                btnInvUp.gameObject.SetActive(!btnInvUp.gameObject.active);
                btnInvDown.gameObject.SetActive(!btnInvDown.gameObject.active);
                active = !active;
            }
            if (!active){
                MainMenu.inInventory = false;
                playerM.enableMove = true;
                idMenu = 0;
            }
        }

    }

    public void activateMenu(){
        inventory.SetActive(!active);
        MainMenu.inInventory = false;
        idMenu = 1;
        active = !active;
        btnInvRigth.gameObject.SetActive(!btnInvRigth.gameObject.active);
        btnInvLeft.gameObject.SetActive(!btnInvLeft.gameObject.active);
        btnInvUp.gameObject.SetActive(!btnInvUp.gameObject.active);
        btnInvDown.gameObject.SetActive(!btnInvDown.gameObject.active);
    }

    void OnTriggerStay2D(Collider2D obj){
        if(obj.CompareTag("item")){
            toTake.text = "Usar E";
            toTake.enabled = true;
            
            takingItem = true;
            item = obj;
        }
    }

        void OnTriggerEnter2D(Collider2D obj){
        if(obj.CompareTag("item")){
            toTake.text = "Usar E";
            toTake.enabled = true;
            
            takingItem = true;
            item = obj;
        }
    }

    void OnTriggerExit2D(Collider2D obj){
        toTake.enabled = false;
        takingItem = false;
        item = null;
    }

    IEnumerator ClearDiary() {
        while (true)
        {
            diaryText.text = "Diario:";
            yield return new WaitForSeconds(5f);
        }
    }

    public void addToDiary(string msg, GameObject obj){
        if (!obj.name.Contains(" ") && !obj.name.Contains("_")) diaryText.text = diaryText.text + "\n"+msg +obj.name.Replace("(Clone)", "");
        else if(!obj.name.Contains(" ") && obj.name.Contains("_")) diaryText.text = diaryText.text + "\n"+msg +obj.name.Replace("_", " ").Replace("(Clone)", "");
        else if(obj.name.Contains(" ") && obj.name.Contains("_")) diaryText.text = diaryText.text + "\n"+msg +obj.name.Replace("_", " ").Substring(0, obj.name.IndexOf(" ")).Replace("(Clone)", "");
        else diaryText.text = diaryText.text + "\n"+msg +obj.name.Substring(0, obj.name.IndexOf(" ")).Replace("(Clone)", "");
    }

    private void TakeItem(){
        if (Input.GetKeyDown(KeyCode.E) || pressedEAndroid){
            for (int i = 0; i < bag.Count; i++){
                if (bag[i].GetComponent<Image>().GetComponentInChildren<Image>().enabled == false)
                {
                    bag[i].GetComponent<Image>().GetComponentInChildren<Image>().enabled = true;//Activamos imagen
                    bag[i].GetComponent<Image>().GetComponentInChildren<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;//Igualamos la imagen al objeto recogido
                    
                    if(item.name.Contains("itemArmor")){
                        bag[i].AddComponent<ArmorStats>();
                        bag[i].GetComponent<ArmorStats>().shield = item.gameObject.GetComponent<ArmorStats>().shield;
                    }
                    else if(item.name.Contains("itemFigth")){
                        bag[i].AddComponent<SwordStats>();
                        bag[i].GetComponent<SwordStats>().damage = item.gameObject.GetComponent<SwordStats>().damage;
                    }



                    if (item.name.Contains("(Clone)")) bag[i].name = item.name.Replace("(Clone)", "");
                    else bag[i].name = item.name;
                    bag[i].tag = item.tag;

                    addToDiary("Has Recogido: ", item.gameObject);
                    Destroy(item.gameObject);
                    break;
                }
            }
            pressedEAndroid = false;
        }
    }

    public void TakeItemAndroid(){
        pressedEAndroid = true;
    }

    public void moveInvRigth() {pressedRigthAndroid = true;}
    public void moveInvLeft() {pressedLeftAndroid = true;}
    public void moveInvUp() {pressedUpAndroid = true;}
    public void moveInvDown() {pressedDownAndroid = true;}

    private void NavegarItems(){

        switch (idMenu){
            
            case 1:
                selector.SetActive(true);
                opcions.SetActive(false);

                if ((Input.GetKeyDown(KeyCode.E) || pressedEAndroid) && bag[ID].GetComponent<Image>().enabled == true){
                    idMenu = 2;
                    pressedEAndroid = false;
                }

                if (active){
                    playerM.enableMove = false;
                    if((Input.GetKeyDown(KeyCode.D) || pressedRigthAndroid) && ID<bag.Count-1){
                        ID++;
                        pressedRigthAndroid = false;
                    }
                    if((Input.GetKeyDown(KeyCode.A) || pressedLeftAndroid) && ID>0) {
                        ID--;
                        pressedLeftAndroid = false;
                    }
                    if((Input.GetKeyDown(KeyCode.W) || pressedUpAndroid) && ID>4){
                        ID-=5;
                        pressedUpAndroid = false;
                    }
                    if((Input.GetKeyDown(KeyCode.S) || pressedDownAndroid) && ID<10){
                        ID+=5;
                        pressedDownAndroid = false;
                    }
                    selector.transform.position = bag[ID].transform.position;
                }
                break;

            case 2:
                opcions.SetActive(true);
                opcions.transform.position = bag[ID].transform.position;
                selector.SetActive(false);

                if((Input.GetKeyDown(KeyCode.W) || pressedUpAndroid) && ID_Selected > 0){
                    ID_Selected--;
                    pressedUpAndroid = false;
                }
                if((Input.GetKeyDown(KeyCode.S) || pressedDownAndroid) && ID_Selected < selection.Length - 1){
                    ID_Selected++;
                    pressedDownAndroid = false;
                }

                switch (ID_Selected)
                {
                    
                    case 0:
                        selection[0].sprite = selectionSprite[1];
                        selection[1].sprite = selectionSprite[0];
                        if(Input.GetKeyDown(KeyCode.E) || pressedEAndroid){
                            if(bag[ID].tag == "item" && bag[ID].name.Contains("food")){
                                playerLife.heal(0.2f);
                                addToDiary("Te has Comido: ",bag[ID]);
                                bag[ID].GetComponent<Image>().GetComponentInChildren<Image>().enabled = false;
                            }
                            else if(bag[ID].tag == "item" && bag[ID].name.Contains("itemFigth")){
                                equip[1].transform.GetChild(0).GetComponent<Image>().enabled = true;
                                equip[1].transform.GetChild(0).name = bag[ID].name;
                                equip[1].transform.GetChild(0).tag = bag[ID].tag;
                                equip[1].transform.GetChild(0).GetComponent<Image>().sprite = bag[ID].GetComponent<Image>().GetComponentInChildren<Image>().sprite;

                                equip[1].transform.GetChild(0).gameObject.AddComponent<SwordStats>();
                                equip[1].transform.GetChild(0).gameObject.GetComponent<SwordStats>().damage = bag[ID].GetComponent<SwordStats>().damage;
                                opcions.SetActive(false);
                            }
                            else if(bag[ID].tag == "item" && bag[ID].name.Contains("itemArmor")){
                                equip[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
                                equip[0].transform.GetChild(0).name = bag[ID].name;
                                equip[0].transform.GetChild(0).tag = bag[ID].tag;
                                equip[0].transform.GetChild(0).GetComponent<Image>().sprite = bag[ID].GetComponent<Image>().GetComponentInChildren<Image>().sprite;

                                equip[0].transform.GetChild(0).gameObject.AddComponent<ArmorStats>();
                                equip[0].transform.GetChild(0).gameObject.GetComponent<ArmorStats>().shield = bag[ID].GetComponent<ArmorStats>().shield;
                                opcions.SetActive(false);
                            }
                            idMenu = 1;
                            pressedEAndroid = false;
                        }
                        break;

                    case 1:
                        selection[0].sprite = selectionSprite[0];
                        selection[1].sprite = selectionSprite[1];
                        if(Input.GetKeyDown(KeyCode.E) || pressedEAndroid){
                            
                            GameObject.Instantiate(Resources.Load("Prefabs/"+bag[ID].name, typeof(GameObject)), new Vector3(playerM.transform.position.x+Random.Range(-2.0f, 2.0f), playerM.transform.position.y+Random.Range(-2.0f, 2.0f), playerM.transform.position.z), playerM.transform.rotation);
                            bag[ID].GetComponent<Image>().enabled = false;
                            for(int i = 0; i < equip.Count; i++){
                                if(equip[i].transform.GetChild(0).name.Equals(bag[ID].name)) equip[i].transform.GetChild(0).GetComponent<Image>().GetComponentInChildren<Image>().enabled = false;
                            }
                            idMenu = 1;
                            pressedEAndroid = false;
                        }
                        break;
                }

                break;
        }
    }

}
