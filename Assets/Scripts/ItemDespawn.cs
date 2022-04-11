using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(despawn());
    }

    IEnumerator despawn(){
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
