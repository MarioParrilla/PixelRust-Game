using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public float maxX, maxY, minX, minY;
    public static int numEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    IEnumerator spawnEnemy() {
        while (numEnemies<10)
        {
            float rate = UnityEngine.Random.Range(0,100);

            if(rate >= 0 && rate < 50) GameObject.Instantiate(Resources.Load("Prefabs/SlimeEnemy"), new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), 0), transform.rotation);
            else if(rate >= 50 && rate < 80) GameObject.Instantiate(Resources.Load("Prefabs/MushroomEnemy"), new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), 0), transform.rotation);
            else GameObject.Instantiate(Resources.Load("Prefabs/ElfEnemy"), new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), 0), transform.rotation);
            numEnemies++;
            yield return new WaitForSeconds(10f);
        }
    }

    public void spawnTree() {
        GameObject.Instantiate(Resources.Load("Prefabs/Tree"), new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), ((GameObject) Resources.Load("Prefabs/Tree")).transform.position.z), ((GameObject) Resources.Load("Prefabs/Tree")).transform.rotation);
    }
}