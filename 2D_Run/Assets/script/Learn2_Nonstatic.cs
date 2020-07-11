using UnityEngine;

public class Learn2_Nonstatic : MonoBehaviour
{
    // 儲存場景上物件或元件
    public GameObject girl;

    public Transform girlTran;

    public Camera cam;

    public ParticleSystem ps;

    // 靜態與非靜態差異
    // 非靜態需要有物件或元件

    // 存取
    // 非靜態成員

    private void Start()
    {
        // 取得
        print(girl.tag);

        // 存放
        girlTran.position = new Vector3(0, 7, 0);
        // girlTran.localScale = new Vector3(2, 2, 2);
    }

    private void Update()
    {
        // 非靜態方法
        // girlTran.Rotate(0,0,0);
        girlTran.Translate(0, 1f, 0);
    }
}
