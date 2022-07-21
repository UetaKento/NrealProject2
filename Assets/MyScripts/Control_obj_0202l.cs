using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class Control_obj_0202l : MonoBehaviour
{
    [SerializeField]
    public HandEnum handEnum;
    [SerializeField]
    public Attach_obj_0202 attach_obj_0202;
    private float Power = 0;

    void Start()
    {
        attach_obj_0202.Create();
    }

    void Update()
    {
        var handState = NRInput.Hands.GetHandState(handEnum);
        if (handState == null) return;
        Power += handState.pinchStrength * 0.001f;
        attach_obj_0202.VolumeUp(Power);
    }
}
