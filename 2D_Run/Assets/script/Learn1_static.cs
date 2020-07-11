using UnityEngine;

public class Learn1 : MonoBehaviour
{
    private void Start()
    {
        // 靜態成員用法
        // 靜態屬性 property = 欄位 Field (儲存資料)
        // 語法 : 類別名稱.靜態屬性名稱
        print(Mathf.PI);
        print(Mathf.Infinity);

        print(Random.value); // 0 - 1 隨機值

        print(Mathf.Abs(-999));

        // 取得 100 - 500 隨機值
        print(Random.Range(100, 501));

        // 呼叫方法
        Test();
        Test();
    }

    private void Update()
    {
        //print(Time.time);
    }

    // 方法
    // 1. 可以被按鈕呼叫

    // 語法
    // 修飾詞 傳會類型 方法名稱 () { 程式內容 }
    // void 無傳回
    public void Test()
    {
        print("我是測試方法");
    }
}
