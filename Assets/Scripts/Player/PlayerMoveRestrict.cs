using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sizeFromCenter = new Vector2(spriteRenderer.bounds.extents.x , spriteRenderer.bounds.extents.y);←プレイヤーの大きさの半分です。
//プレイヤーが画面外に行くのを防ぎます。プレイヤーの大きさを考慮しています。
//カメラが中心にあることが前提です。
public class PlayerMoveRestrict : MonoBehaviour
{
    //複数プレイヤーに対応しています。制限したいものをインスペクター上で指定します。
    [SerializeField]
    GameObject player;
    Vector2 playerSizeFromCenter;
    Vector2 distanceFromCenter;
    public Vector2 playerLimitPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var spriteRenderer = this.GetComponent<SpriteRenderer>();
        playerSizeFromCenter = new Vector2(spriteRenderer.bounds.extents.x, spriteRenderer.bounds.extents.y);
        var screenHeight = Screen.height;
        var screenWidth = Screen.width;
        var orthographicSize = Camera.main.orthographicSize * 2;
        var pixelPerUnit = screenHeight / orthographicSize;
        distanceFromCenter = new Vector2( (screenWidth / 2) / pixelPerUnit , (screenHeight / 2) / pixelPerUnit);
        playerLimitPosition = new Vector2(distanceFromCenter.x - playerSizeFromCenter.x, distanceFromCenter.y - playerSizeFromCenter.y);

    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの挙動を制御します。カメラの座標からの差分です。 Camera.mainからの距離を制限します。
        player.transform.position     //↓xの移動範囲を制限します。     
= new Vector3(Mathf.Clamp(player.transform.position.x,　Camera.main.transform.position.x -　playerLimitPosition.x, Camera.main.transform.position.x + playerLimitPosition.x)
            //↓yの移動範囲を制限します。       
, Mathf.Clamp(player.transform.position.y, Camera.main.transform.position.y - playerLimitPosition.y, Camera.main.transform.position.y + playerLimitPosition.y)
, player.transform.position.z);
    }
}
