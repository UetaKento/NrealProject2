using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace NRKernal.NRExamples
{
    /// <summary> A cube interactive test. </summary>
    public class DestroyRotateCube : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        GameObject NRHandPointer;
        [SerializeField]
        Text HitPoint;

        private float scaleParameter;
        private float scaleDownRate = 0.1f;
        private float scaleUpRate = 0.03f;
        bool OnPointerEnterBool = false;
        private float speedParameter = 0.008f;
        private float randomX = 0.0f;
        private float randomY = 0.0f;
        private Vector3 randomPosition;

        void Start()
        {
            scaleParameter = this.transform.localScale.y; //このオブジェクトのスケールを取得。スケールはx,y,zどれも同じなのでどれでもいい。
            //https://zenn.dev/daichi_gamedev/articles/b901ca3a1b4391
            //GameObject child = this.transform.Find("Cube").gameObject;

            //https://qiita.com/Maru60014236/items/d542764af1c30c742d9f
            //child.GetComponent<Renderer>().enabled = false;

            randomX = Random.Range(-2f, 2f);
            randomY = Random.Range(-2f, 2f);
            randomPosition = new Vector3(randomX, randomY, this.transform.localPosition.z); //Update()でif文を使うため最初に一度randomPositionを決める。
        }

        /// <summary> Updates this object. </summary>
        void Update()
        {
            this.transform.Rotate(new Vector3(0, 1, 1));
            if (this.transform.position == randomPosition) //このオブジェクトがrandomPositionについたら
            {
                randomX = Random.Range(-2f, 2f);
                randomY = Random.Range(-2f, 2f);
                randomPosition = new Vector3(randomX, randomY, this.transform.localPosition.z); //また新たにrandomPosition生成。
            }
            else //randomPositionについていなかったら移動。
            {
                // https://qiita.com/OKsaiyowa/items/167a0a6afa536c33fc38
                this.transform.position = Vector3.MoveTowards(this.transform.position, randomPosition, speedParameter);
            }


            if (NRHandPointer.activeSelf) //参照しているレーザーが存在(active)していたら
            {
                if (OnPointerEnterBool) //レーザーが存在しており、このオブジェクトの内側にある場合
                {
                    // https://domicarcatan.pinoko.jp/2021/05/16/unity_buttonaction02/
                    scaleParameter -= scaleDownRate * Time.deltaTime; //オブジェクトを小さくするための変数を減少度合いと経過時間を使って計算
                    this.transform.localScale = new Vector3(scaleParameter, scaleParameter, scaleParameter); //レーザーがこのオブジェクトの内側にある間、オブジェクトの大きさを小さく。
                    HitPoint.text = CalculateAndConvert(scaleParameter - 1); //今の体力を計算して、textに表示。"1~~~"と表示されるのを防ぐため、-1する。
                }
                else //レーザーが存在しており、このオブジェクトの外側にある場合
                {
                    scaleParameter += scaleUpRate * Time.deltaTime;
                    this.transform.localScale = new Vector3(scaleParameter, scaleParameter, scaleParameter);
                    HitPoint.text = CalculateAndConvert(scaleParameter - 1);
                }
            }
            else
            {
                scaleParameter += scaleUpRate * Time.deltaTime;
                this.transform.localScale = new Vector3(scaleParameter, scaleParameter, scaleParameter);
                HitPoint.text = CalculateAndConvert(scaleParameter - 1);
            }

            if (this.transform.localScale.y < 0.5f) //このオブジェクトがある大きさより小さくなったら
            {
                Destroy(this.gameObject); //このオブジェクトを破壊
            }
        }

        /// <summary> when pointer hover, set the cube color to green. </summary>
        /// <param name="eventData"> Current event data.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterBool = true; //レーザーがこのオブジェクトに入ってきたらtrue
        }

        /// <summary> when pointer exit hover, set the cube color to white. </summary>
        /// <param name="eventData"> Current event data.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerEnterBool = false; //レーザーがこのオブジェクトを出たらfalse
        }

        string CalculateAndConvert(float fValue) //floatをintに直して計算して、stringを返す
        {
            fValue = fValue * 1000;
            int iValue = 500;
            iValue = iValue + Mathf.RoundToInt(fValue);
            return iValue.ToString("000");
        }

    }
}
