using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerCombat : MonoBehaviour
{

    public GameObject combatPanel;
    public Image spriteEnemy, enemyLife, playerLife, playerSprite;
    public GameObject enemyGO;
    public Text enemyName, dialogBattleText;
    public Button btnAttack, btnRun;

    private float playerDamage, playerShiled, enemyDamage, enemyShield;

    private bool killed;
    private PlayerInventory playerInventory;
    private PlayerLife plLife;

    public GameObject background, battleSound;
    public List<GameObject> enemyLoot;
    public Button btnInv, btnE, btnF, btnInvRigth, btnInvLeft, btnInvUp, btnInvDown;
    public Joystick joystick;
    public AudioMixer enemiesMixer;

    void Update(){
        if(enemyGO != null) checkEnemy();
        if(plLife != null) plLife.checkLife();
    }

    void OnTriggerEnter2D(Collider2D enemy){
        if (enemy.tag.Equals("enemy")){
            playerDamage = 0.1f;
            MainMenu.paused = true;
            
            enemiesMixer.SetFloat("Volume", -80f);
            background.SetActive(false);
            battleSound.SetActive(true);
            combatPanel.SetActive(true);
            killed = false;
            enemyGO = enemy.gameObject;

            spriteEnemy.sprite = enemy.gameObject.GetComponent<SpriteRenderer>().sprite;
            
            if(enemy.gameObject.name.Contains("(Clone)")) enemyName.text = enemy.gameObject.name.Replace("(Clone)", "");
            else enemyName.text = enemy.gameObject.name;
            

            enemyLife.fillAmount = enemy.gameObject.GetComponent<EnemyStats>().amountLife;
            enemyDamage = enemy.gameObject.GetComponent<EnemyStats>().damage;
            enemyShield = enemy.gameObject.GetComponent<EnemyStats>().shield;
            enemyLoot = enemy.gameObject.GetComponent<EnemyStats>().loot;

            plLife = GetComponent<PlayerLife>();
            playerLife.fillAmount = plLife.lifeAmount;
            playerInventory = GetComponent<PlayerInventory>();

            btnInvRigth.gameObject.SetActive(false);
            btnInvLeft.gameObject.SetActive(false);
            btnInvUp.gameObject.SetActive(false);
            btnInvDown.gameObject.SetActive(false);
            joystick.gameObject.SetActive(false);
            btnInv.gameObject.SetActive(false);
            btnE.gameObject.SetActive(false);
            btnF.gameObject.SetActive(false);
        }
    }

    public void turne(){
        dialogBattleText.text = "";
        if (playerInventory.equip[0].transform.GetChild(0).GetComponent<Image>().enabled) playerShiled = playerInventory.equip[0].transform.GetChild(0).GetComponent<ArmorStats>().shield;
        if (playerInventory.equip[1].transform.GetChild(0).GetComponent<Image>().enabled) playerDamage = playerInventory.equip[1].transform.GetChild(0).GetComponent<SwordStats>().damage;
        btnAttack.enabled = false;
        btnAttack.image.color = Color.gray;
        btnRun.enabled = false;
        btnRun.image.color = Color.gray;
        if(UnityEngine.Random.Range(0,2) == 0){
            StartCoroutine(playerAttack());
        }
        else{
            StartCoroutine(enemyAttack());
        }
    }

    public void runOut(){
        background.SetActive(true);
        battleSound.SetActive(false);
        combatPanel.SetActive(false);
        enemiesMixer.SetFloat("Volume", PlayerPrefs.GetFloat("audio"));
        MainMenu.paused = false;
        joystick.gameObject.SetActive(true);
        btnInv.gameObject.SetActive(true);
        btnE.gameObject.SetActive(true);
        btnF.gameObject.SetActive(true);
    }

    private IEnumerator playerAttack(){
        if(UnityEngine.Random.Range(0,16) < 11){
            float damageToEnemy = Math.Abs(enemyShield - playerDamage);
            dialogBattleText.text = "Le has quitado "+damageToEnemy*100;
            yield return new WaitForSeconds(1f);
            enemyLife.fillAmount -= damageToEnemy;
            spriteEnemy.color = new Color32(147,147,248, 255);
            yield return new WaitForSeconds(1f);
            spriteEnemy.color = Color.white;
            plLife.checkLife();
            if(UnityEngine.Random.Range(0,16) < 11){
                float totalDamage = enemyDamage - playerShiled;
                if(totalDamage < 0) totalDamage = 0;
                dialogBattleText.text = "Te ha quitado "+totalDamage*100;
                yield return new WaitForSeconds(1f);
                plLife.damage(totalDamage);
                playerLife.fillAmount -= totalDamage;
                playerSprite.color = new Color32(147,147,248, 255);
                yield return new WaitForSeconds(1f);
                playerSprite.color = Color.white;
            }
            else{
                dialogBattleText.text = "El enemigo fallo el ataque!";
                yield return new WaitForSeconds(1f);
            }
        }
        else{
            dialogBattleText.text = "Has fallado el ataque!";
            yield return new WaitForSeconds(1f);
            if(UnityEngine.Random.Range(0,16) < 11){
                float totalDamage = enemyDamage - playerShiled;
                if(totalDamage < 0) totalDamage = 0;
                dialogBattleText.text = "Te ha quitado "+totalDamage*100;
                yield return new WaitForSeconds(1f);
                plLife.damage(totalDamage);
                playerLife.fillAmount -= totalDamage;
                playerSprite.color = new Color32(147,147,248, 255);
                yield return new WaitForSeconds(1f);
                playerSprite.color = Color.white;
            }
            else{
                dialogBattleText.text = "El enemigo fallo el ataque!";
                yield return new WaitForSeconds(1f);
            }
        }
        btnAttack.enabled = true;
        btnAttack.image.color = Color.white;
        btnRun.enabled = true;
        btnRun.image.color = Color.white;
    }

    private IEnumerator enemyAttack() {
        if(UnityEngine.Random.Range(0,16) < 11){
            float totalDamage = enemyDamage - playerShiled;
            if(totalDamage < 0) totalDamage = 0;
            dialogBattleText.text = "Te ha quitado "+totalDamage*100;
            yield return new WaitForSeconds(1f);
            plLife.damage(totalDamage);
            playerLife.fillAmount -= totalDamage;
            playerSprite.color = new Color32(147,147,248, 255);
            yield return new WaitForSeconds(1f);
            playerSprite.color = Color.white;
            if(UnityEngine.Random.Range(0,16) < 11){
                float damageToEnemy = Math.Abs(enemyShield - playerDamage);
                dialogBattleText.text = "Le has quitado "+damageToEnemy*100;
                yield return new WaitForSeconds(1f);
                enemyLife.fillAmount -= damageToEnemy;
                spriteEnemy.color = new Color32(147,147,248, 255);
                yield return new WaitForSeconds(1f);
                spriteEnemy.color = Color.white;
                plLife.checkLife();
            }else{
                dialogBattleText.text = "Has fallado el ataque!";
                yield return new WaitForSeconds(1f);
            }
        }
        else{
            dialogBattleText.text = "El enemigo fallo el ataque!";
            yield return new WaitForSeconds(2f);
            if(UnityEngine.Random.Range(0,16) < 11){
                float damageToEnemy = Math.Abs(enemyShield - playerDamage);
                dialogBattleText.text = "Le has quitado "+damageToEnemy*100;
                yield return new WaitForSeconds(1f);
                enemyLife.fillAmount -= damageToEnemy;
                spriteEnemy.color = new Color32(147,147,248, 255);
                yield return new WaitForSeconds(1f);
                spriteEnemy.color = Color.white;
                plLife.checkLife();
            }else{
                dialogBattleText.text = "Has fallado el ataque!";
                yield return new WaitForSeconds(1f);
            }
        }
        btnAttack.enabled = true;
        btnAttack.image.color = Color.white;
        btnRun.enabled = true;
        btnRun.image.color = Color.white;
    }

    private void checkEnemy(){
        if(!killed){
            if(Math.Round(enemyLife.fillAmount, 2) <= 0.0f){
                GameObject.Instantiate(Resources.Load("Prefabs/"+enemyLoot[UnityEngine.Random.Range(0,enemyLoot.Count)].name), new Vector3(enemyGO.transform.position.x+UnityEngine.Random.Range(-2.0f, 2.0f), enemyGO.transform.position.y+UnityEngine.Random.Range(-2.0f, 2.0f), enemyGO.transform.position.z), enemyGO.transform.rotation);
                Destroy(enemyGO);
                SpawnScript.numEnemies--;
                combatPanel.SetActive(false);
                background.SetActive(true);
                battleSound.SetActive(false);
                enemiesMixer.SetFloat("Volume", PlayerPrefs.GetFloat("audio"));
                MainMenu.paused = false;
                killed = true;
                joystick.gameObject.SetActive(true);
                btnInv.gameObject.SetActive(true);
                btnE.gameObject.SetActive(true);
                btnF.gameObject.SetActive(true);
            }
        }
    }
}
