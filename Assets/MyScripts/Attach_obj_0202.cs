using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach_obj_0202 : MonoBehaviour
{

    GameObject Cube_prefab;
    GameObject Cube;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create()
    {
        Cube_prefab = Resources.Load<GameObject>("Cube");
        Cube = Instantiate(Cube_prefab);
    }

    public void VolumeUp(float p)
    {
        Cube.transform.localScale = new Vector3(p, p, p);
        if (p > 0.5)
        {
            Instantiate(Explosion, Cube.transform.position, Quaternion.identity);
            Destroy(Cube);
        }
    }

}
