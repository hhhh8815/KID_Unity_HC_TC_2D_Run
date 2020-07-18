using UnityEngine;
using UnityEngine.UI;

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
    public AudioClip soundCoin;
    public AudioClip soundHit;
    [Header("死去"), Tooltip("True 死透，False 沒死")]
    public bool dead;
    [Header("動畫控制器")]
    public Animator ani;
    [Header("膠囊碰撞器")]
    public CapsuleCollider2D cc2d;
    [Header("剛體")]
    public Rigidbody2D rig;
    [Header("血條")]
    public Image imgHp;
    private float hpMax;
    public bool isGround;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("結束畫面")]
    public GameObject final;
    [Header("標題")]
    public Text textTitle;
    [Header("本次的金幣數量")]
    public Text textCurrent;

    #endregion

    [Header("金幣文字")]
    public Text textCoin;

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
        bool jump = Input.GetKeyDown(KeyCode.Space);

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
                aud.PlayOneShot(soundJump);
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

        // 如果 按下 左邊 Ctrl 撥放一次音效
        if (Input.GetKeyDown(KeyCode.LeftControl)) aud.PlayOneShot(soundSlide);

        if (key)
        {
            cc2d.offset = new Vector2(0.05623484f, -0.8460734f);
            cc2d.size = new Vector2(2.622914f, 2.622916f);
            aud.PlayOneShot(soundSlide);
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
        hp -= 10;
        imgHp.fillAmount = hp / hpMax;
        aud.PlayOneShot(soundHit, 5);

        if (hp <= 0) Dead();
    }

    /// <summary>
    /// 獲得金幣：金幣數量增加 音效 更新介面
    /// </summary>
    private void Eatcoin(Collider2D collision)
    {
        coin++;                         // 金幣數量遞增1
        Destroy(collision.gameObject);  // 刪除(碰到物件.遊戲物件)
        textCoin.text = "金幣：" + coin;// 文字介面文字 = "金幣：" + 金幣數量
        aud.PlayOneShot(soundCoin);
    }
    /// <summary>
    /// 死亡：動畫 遊戲結束
    /// </summary>
    private void Dead()
    {
        if (dead) return;               // 如果 死亡 就 跳出

        speed = 0;
        dead = true;
        ani.SetTrigger("死亡觸發");     // 死亡觸發
        final.SetActive(true);          // 結束畫面.啟動設定(是)
        textTitle.text = "恭喜你死掉了~";
        textCurrent.text = "本次的金幣數量：" + coin;
    }

    private void Pass()
    {
        final.SetActive(true);
        textTitle.text = "恭喜你獲勝了~";
        textCurrent.text = "本次的金幣數量：" + coin;
        speed = 0;
        rig.velocity = Vector3.zero;
    }

    #endregion

    #region 事件區域
    // 開始 Start
    // 播放遊戲時執行一次
    // 初始化 ：
    private void start()
    {
        hpMax = hp;
    }

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
        if (dead) return;

        Slide();
    }
    /// <summary>
    /// 固定更新事件：一秒固定執行 50 次，只要有剛體就放這裡
    /// </summary>
    private void FixedUpdate()
    {
        if (dead) return;

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

        // 如果 碰到物件 的 名稱 等於 "懸空地板" 並且 玩家的 Y 大於 地板的 Y
        if (collision.gameObject.name == "懸空地板")
        {
            // 是否在地板上 = 是
            isGround = true;
        }
    }

    /// <summary>
    /// 觸發事件：碰到勾選 Is Trigger 的物件執行一次
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "金幣")        // 如果 碰到物件.標籤 == "金幣"
        {
            Eatcoin(collision);
        }

        if (collision.tag == "障礙物")
        {
            Hit();
        }

        if (collision.tag == "傳送門")
        {
            Pass();
        }
    }
    #endregion
}
