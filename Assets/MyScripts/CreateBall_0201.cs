using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class CreateBall_0201 : MonoBehaviour
{
    [SerializeField]
    // GameObject bulletPrefab;
    public HandEnum handEnum;
    // public GameObject originObject;
    private float Power = 0;
    // bool shoot = true;
    GameObject bullet;
    GameObject Cube;

    void Start()
    {
        GameObject Cube_prefab = Resources.Load<GameObject>("Cube");
        Cube = Instantiate(Cube_prefab);
    }

    void Update()
    {
        UpdateGesture();
    }

    void UpdateGesture()
    {
        var handState = NRInput.Hands.GetHandState(handEnum);
        if (handState == null) return;
        Power += handState.pinchStrength * 0.001f;
        Cube.transform.localScale = new Vector3(Power, Power, Power);

        //if (handState.pinchStrength > 0.5)
        //{
        //    Power += Time.deltaTime * 0.05f;
        //    Cube.transform.localScale = new Vector3(Power, Power, Power);
        //}
    }
}
