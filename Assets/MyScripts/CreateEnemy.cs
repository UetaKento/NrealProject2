using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CreateEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemyObject;
    private float randomPosition = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GenerateEveryNSec", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GenerateEveryNSec(float n)
    {
        while (true)
        {
            randomPosition = Random.Range(-2, 2);
            yield return new WaitForSeconds(n);
            Instantiate(enemyObject, new Vector3(0, randomPosition, 3), this.transform.rotation);
            //GameObject obj = GameObject.Find("AkabekoPrefab1").gameObject;
            //GameObject newObj = Instantiate(obj);
            //newObj.transform.position = new Vector3(0, 0.1f, 3);
        }
    }
}
