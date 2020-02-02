using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerScript : MonoBehaviour
{
    PlayerParamater paramater;
    enum PlayerStatus
    {
        Stop,
        Walk,
        Dash,
    }
    PlayerStatus playerStatus = PlayerStatus.Stop;
    enum PlayerCostume
    {
        Normal,
        Costume01,
        Costume02,
    }
    PlayerCostume playerCostume = PlayerCostume.Normal;
    new Rigidbody2D rigidbody;
    SpriteRenderer[] spriteRenderer;
    private const string Player_Paramater_PATH = "ScriptableObjects/Player Paramater";
    bool isStop;
    //スタミナが一度切れたら、走れません。
    bool cannnotDash;
    float timeCount = 0;
    private float inputX, inputY;
    int flamePerSecond = 60;
    public float maxStamina = 100;
    public FloatReactiveProperty nowStamina;
    public IntReactiveProperty playerLife;
    int playerLayer;
    //無敵用レイヤーです。
    int invincibleLayer;
    [SerializeField]
    Text text;
    [SerializeField]
    GameObject Clothes;
    bool isDamage;
    bool isNaked;
    //速度補正のために、獲得アイテム数を参照します。
    [SerializeField]
    ItemPresenter itemPresenter;
    SpriteRenderer clothesRenderer;    // Start is called before the first frame update
    [SerializeField] CameraShake shake;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        clothesRenderer = Clothes.GetComponent<SpriteRenderer>();
        paramater = Resources.Load<PlayerParamater>(Player_Paramater_PATH) as PlayerParamater;
        nowStamina.Value = paramater.StaminaMax;
        playerLayer = this.gameObject.layer;
        invincibleLayer = 2;
        playerLife.Value = paramater.Life;
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemManager.Instance.CollectionCount <= 0)
            isNaked = true;
        else
        {
            isNaked = false;
        }

        if (isStop)
        {
            StopTimeCount();
        }
        if (ItemManager.Instance.CollectionCount > 0)
        {
            var spriteName = ItemManager.Instance.GetItemData().sprite.name;
            if (spriteName == "Costume01_Gray")
            {
                playerCostume = PlayerCostume.Costume01;
            }
            else if (spriteName == "Costume02_Gray")
            {
                playerCostume = PlayerCostume.Costume02;
            }
            else
            {
                playerCostume = PlayerCostume.Normal;
            }
            Debug.Log(playerCostume);
        }
        GetInput();
        text.text = nowStamina.ToString();
        PlayerStaminaManager();
    }

    /// <summary>
    /// プレイヤーの入力処理によって呼ばれます。
    /// </summary>
    private void GetInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX == 0 && inputY == 0 || isStop)
        {
            playerStatus = PlayerStatus.Stop;
        }
        else
        {
            if (Input.GetButton("Dash") && !cannnotDash)
                playerStatus = PlayerStatus.Dash;
            else
                playerStatus = PlayerStatus.Walk;
        }
        //if (isNaked)
        //{

        //}
    }

    private void FixedUpdate()
    {
        var moveVectorBeforeCorrection = new Vector3(inputX, inputY, 0);
        PlayerMove(moveVectorBeforeCorrection);
        Debug.Log(isNaked);
        //Debug.Log(isDamage);
    }

    /// <summary>
    /// 敵に当たった時に、停止時間をカウントします。一定時間で解除します。
    /// </summary>
    private void StopTimeCount()
    {
        //経過時間分を加算します。
        timeCount += Time.deltaTime;
        if (timeCount > paramater.StopTime)
        {
            isStop = false;
            timeCount = 0;
        }
    }

    /// <summary>
    /// プレイヤーを動かします。
    /// </summary>
    /// <param name="moveVectorBeforeCorrection">プレイヤーの入力です。</param>
    private void PlayerMove(Vector3 moveVectorBeforeCorrection)
    {
        switch (playerStatus)
        {
            case PlayerStatus.Stop:
                {
                    rigidbody.MovePosition(this.transform.position);
                }
                break;
            case PlayerStatus.Walk:
                {
                    rigidbody.MovePosition(this.transform.position += moveVectorBeforeCorrection * paramater.Speed * Mathf.Pow(paramater.Costume01_speedAdjust, itemPresenter.i1) * Mathf.Pow(paramater.Costume02_speedAdjust, itemPresenter.i2)* Time.deltaTime);
                    break;
                }
            case PlayerStatus.Dash:
                {
                    rigidbody.MovePosition(this.transform.position += moveVectorBeforeCorrection * paramater.DashSpeed * Mathf.Pow(paramater.Costume01_speedAdjust, itemPresenter.i1) * Mathf.Pow(paramater.Costume02_speedAdjust, itemPresenter.i2) * Time.deltaTime);
                    break;
                }
        }

    }
    /// <summary>
    /// スタミナを管理します。
    /// </summary>
    private void PlayerStaminaManager()
    {
        switch (playerStatus)
        {
            case PlayerStatus.Stop:
                {
                    if (nowStamina.Value < paramater.StaminaMax)
                    {
                        nowStamina.Value += 2 * Time.deltaTime * paramater.StaminaMax / (paramater.DashTime);
                    }
                    else
                    {
                        nowStamina.Value = maxStamina;
                    }
                }
                break;
            case PlayerStatus.Walk:
                {
                    if (nowStamina.Value < paramater.StaminaMax)
                    {
                        nowStamina.Value += Time.deltaTime * paramater.StaminaMax / (paramater.DashTime);
                    }
                    else
                    {
                        nowStamina.Value = maxStamina;
                    }

                }
                break;
            case PlayerStatus.Dash:
                {
                    //DashTime:スタミナ最大から走り続けたと想定したときに、走り続けられる時間です。
                    //最大値100想定です。5秒で0です。
                    nowStamina.Value -= Time.deltaTime * paramater.StaminaMax / (paramater.DashTime);
                }
                break;
            default:
                break;
        }
        if (nowStamina.Value <= 0)
        {
            cannnotDash = true;
            nowStamina.Value = 0;
        }
        else if (nowStamina.Value >= maxStamina)
        {
            cannnotDash = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && this.gameObject.tag == "Player")
        {
            var spriteRenderer = other.transform.GetChild(0).GetComponent<SpriteRenderer>();
            if (ItemManager.Instance.CollectionCount != 0)
            {
                spriteRenderer.sprite = ItemManager.Instance.GetItemData().sprite;
                spriteRenderer.color = ItemManager.Instance.GetItemData().color;
            }
            //服を失います。
            ItemManager.Instance.LoseItem();

            Debug.Log("Enemy collided with player");
            if (isNaked && !isDamage)
            {
                isStop = true;
                StartCoroutine(InvincibleTime());
                playerLife.Value -= 1;
            }
            else
            {
                if (ItemManager.Instance.CollectionCount == 0)
                {
                    clothesRenderer.sprite = null;
                    clothesRenderer.color = Color.white;
                }
                else
                {
                    clothesRenderer.sprite = ItemManager.Instance.GetItemData().sprite;
                    clothesRenderer.color = ItemManager.Instance.GetItemData().color;
                }

            }
            shake.Shake(0.25f, 0.1f);
        }
        if (other.gameObject.tag == "Item")
        {
            GetClothes(other);
        }
    }

    private void GetClothes(Collider2D other)
    {
        var clothes = other.GetComponent<SpriteRenderer>();
        clothesRenderer.sprite = clothes.sprite;
        clothesRenderer.color = clothes.color;
    }

    public IEnumerator InvincibleTime()
    {
        isDamage = true;
        for (int i = 0; i < paramater.InvincibleTime * 10; i++)
        {
            for (int j = 0; j < spriteRenderer.Length; j++)
            {
                spriteRenderer[j].enabled = false;
            }
            yield return new WaitForSeconds(0.05f);
            for (int j = 0; j < spriteRenderer.Length; j++)
            {
                spriteRenderer[j].enabled = true;
            }
            yield return new WaitForSeconds(0.05f);
        }
        isDamage = false;
    }
}
