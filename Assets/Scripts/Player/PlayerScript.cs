using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    new Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    private const string Player_Paramater_PATH = "ScriptableObjects/Player Paramater";
    bool isStop;
    //スタミナが一度切れたら、走れません。
    bool cannnotDash;
    float timeCount = 0;
    private float inputX, inputY;
    float nowStamina;
    bool isNaked  =true;
    int playerLife;
    int playerLayer;
    //無敵用レイヤーです。
    int invincibleLayer;
    [SerializeField]
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        paramater = Resources.Load<PlayerParamater>(Player_Paramater_PATH) as PlayerParamater;
        nowStamina = paramater.StaminaMax;
        playerLayer =this.gameObject.layer;
        invincibleLayer = 2;
        playerLife = paramater.Life;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            StopTimeCount();
        }
        GetInput();
        Debug.Log(playerLife);
        text.text = nowStamina.ToString() ;
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
        PlayerStaminaManager();
    }

    /// <summary>
    /// 敵に当たった時に、停止時間をカウントします。一定時間で解除します。
    /// </summary>
    private void StopTimeCount()
    {
        //経過時間分を加算します。
        timeCount += Time.deltaTime;
        if (timeCount > paramater.StopTime )
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
                    rigidbody.MovePosition(this.transform.position += moveVectorBeforeCorrection * paramater.Speed * Time.deltaTime);
                }
                break;
            case PlayerStatus.Dash:
                {
                    rigidbody.MovePosition(this.transform.position += moveVectorBeforeCorrection * paramater.DashSpeed * Time.deltaTime);
                }
                break;
            default:
                break;
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
                    if (nowStamina < paramater.StaminaMax)
                    {
                        nowStamina += 2 * Time.deltaTime * paramater.StaminaMax / (paramater.DashTime);
                    }
                    else
                    {
                        nowStamina = 100;
                    }
                }
                break;
            case PlayerStatus.Walk:
                {
                    if (nowStamina < paramater.StaminaMax)
                    {
                        nowStamina += Time.deltaTime * paramater.StaminaMax / (paramater.DashTime);
                    }
                    else
                    {
                        nowStamina = 100;
                    }

                }
                break;
            case PlayerStatus.Dash:
                {
                    //DashTime:スタミナ最大から走り続けたと想定したときに、走り続けられる時間です。
                    //最大値100想定です。5秒で0です。
                    nowStamina -= Time.deltaTime * paramater.StaminaMax / ( paramater.DashTime);
                }
                break;
            default:
                break;
        }
        if (nowStamina <= 0)
        {
            cannnotDash = true;
            nowStamina = 0;
        }
        else if (nowStamina >= 100)
        {
            cannnotDash = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && this.gameObject.tag == "Player")
        {
            isStop = true;
            Debug.Log("Enemy collided with player");

            if (isNaked && this.gameObject.layer == playerLayer )
            {
                StartCoroutine(InvincibleTime());
                playerLife -= 1;
            }
        }
    }

    /// <summary>
    /// 無敵になった時に呼ばれます。
    /// </summary>
    /// <returns></returns>
    public IEnumerator InvincibleTime()
    {
        this.gameObject.layer = invincibleLayer;
        for (int i = 0; i < paramater.InvincibleTime * 10; i++)
        {
            spriteRenderer.color = new Vector4(1.0f, 1.0f, 1.0f, 0.5f);
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
        this.gameObject.layer = playerLayer;
    }
}
