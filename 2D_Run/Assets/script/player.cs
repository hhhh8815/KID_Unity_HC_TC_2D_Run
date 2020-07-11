using UnityEngine;

public class player : MonoBehaviour
{
    #region 欄位區域
    //命名規則
    // 1.用有意義的名稱
    // 2.不要使用數字或特殊符號
    // 3.可以使用中文(不建議)

    //欄位語法
    // 修飾詞 類型 欄位名稱 = 值
    //沒有 = 值 會以預設值為主
    //整數，浮點數 預設值 = 0
    //字串 預設值 ""
    //布林值 預設值 false

    // 私人 private - 僅限於此類別使用 | 不會顯示 - 預設值
    // 公開 public  - 所有類別皆可使用 | 會顯示
    [Header("速度"), Tooltip("角色的移動速度"), Range(10, 1500)]
    public int speed = 50;
    [Header("血量"), Tooltip("歸零會死喔"), Range(0, 1000)]
    public float hp = 999.9f;
    [Header("金幣"), Tooltip("獲得多少金幣")]
    public int coin;
    [Header("跳躍高度"), Range(100, 2000)]
    public int height = 500;
    [Header("音效區域")]
    public AudioClip soundJump;
    public AudioClip soundSlide;
    public AudioClip soundDamage;
    [Header("死去"), Tooltip("True 死透，False 沒死")]
    public bool dead;
    [Header("動畫控制器")]
    public Animator ani;
    [Header("膠囊碰撞器")]
    public CapsuleCollider2D cc2d;
    [Header("剛體")]
    public Rigidbody2D rig;

    public bool isGround;

    #endregion

    #region 方法區域
    // C# 符號成對出現
    // 摘要：方法的說明
    // 在方法上面添加三條 /

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (rig.velocity.magnitude < 10)
        {
            // 剛體,添加推力(二維向量)
            rig.AddForce(new Vector2(speed, 0));
        }
    }

    /// <summary>
    /// 跳躍：動畫 音效
    /// </summary>
    private void Jump()
    {
        //呼叫跳躍方法
        bool jump = Input.GetKey(KeyCode.Space);

        // 顛倒運算子
        // 作用：將布林值變成相反
        // !true ===== false

        ani.SetBool("跳躍開關", !isGround);

        // 搬家：Alt + 上 OR 下
        // 格式化：ctrl + K D

        // 如果在地板上
        if (isGround)
        {
            if (jump)
            {
                isGround = false;                     //不在地板上
                rig.AddForce(new Vector2(0, height)); //剛體,添加推力(二維向量
            }
        }
    }
    /// <summary>
    /// 滑行：動畫 音效 碰撞範圍縮小
    /// </summary>
    private void Slide()
    {
        // 布林值 = 輸入,取得按鍵(按鍵代碼列舉,左邊 Z)
        bool key = Input.GetKey(KeyCode.Z);

        // 動畫控制器代號
        ani.SetBool("滑行開關", key);

        if (key)
        {
            cc2d.offset = new Vector2(0.05623484f, -0.8460734f);
            cc2d.size = new Vector2(2.622914f, 2.622916f);
        }
        else
        {
            cc2d.offset = new Vector2(0.05623484f, -0.2061949f);
            cc2d.size = new Vector2(2.622914f, 3.902673f);
        }
    }


    /// <summary>
    /// 受傷：音效 血量減少
    /// </summary>
    private void Hit()
    {
        print("受傷");
    }
    /// <summary>
    /// 獲得金幣：金幣數量增加 音效 更新介面
    /// </summary>
    private void Eatcoin()
    {
        print("獲得金幣");
    }
    /// <summary>
    /// 死亡：動畫 遊戲結束
    /// </summary>
    private void Dead()
    {
        print("死去");
    }

    #endregion

    #region 事件區域
    // 開始 Start
    // 播放遊戲時執行一次
    // 初始化 ：
    private void Start()
    {
        // 呼叫跳躍方法
        Jump();
    }
    // 更新 Update
    // 撥放遊戲後一秒執行 60 次 - 60FPS
    // 移動、監聽玩家鍵盤、滑鼠與觸控
    private void Update()
    {
        Slide();
    }
    /// <summary>
    /// 固定更新事件：一秒固定執行 50 次，只要有剛體就放這裡
    /// </summary>
    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    /// <summary>
    /// 碰撞事件：碰到物體開始執行一次
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果 碰到物件 的 名稱 等於 "地板"
        if (collision.gameObject.name == "地板")
        {
            // 是否在地板上 = 是
            isGround = true;
        }
    }
    #endregion
}
