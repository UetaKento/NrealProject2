using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class IronManHand_R : MonoBehaviour
{
    [SerializeField]
    GameObject NRHandPointer_R;
    public HandEnum handEnum; //ハンドトラッキングの様々な情報を参照できる。
    private float validTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        NRHandPointer_R.SetActive(false); //最初はvalidTimeが蓄積されていないのでレーザーは表示させない。
    }

    // Update is called once per frame
    void Update()
    {
        var handState = NRInput.Hands.GetHandState(handEnum); //Update()の中に記述することで常に手の状態をチェックして更新する。

        if (handState.isTracked) //手をパーにしている間、validTimeの量によって条件分岐。
        {
            if (validTime > 0.0f) //蓄積したvalidTimeがある場合
            {
                NRHandPointer_R.SetActive(true); //レーザーを表示。
                validTime -= Time.deltaTime; //レーザーを表示している間、validTimeをカウントダウンさせる
            }
            else
            {
                NRHandPointer_R.SetActive(false); //蓄積したvalidTimeがない場合、レーザーは表示させない。
            }

        }

        if (handState.isPinching) //手をグーにしている間、validTimeをカウントアップさせる。
        {
            validTime += 3 * Time.deltaTime; //validTimeのままだとレーザーが出る時間が短いので3倍にする。
            NRHandPointer_R.SetActive(false); //validTimeをカウントアップさせてる間はレーザーは表示させない。
        }
    }
}
