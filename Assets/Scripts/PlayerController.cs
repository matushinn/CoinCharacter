using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //歩く速度
    public float wallkSpeed = 3.0f;

    //重力加速度
    public float gravity = 20.0f;

    //ジャンプの加速
    float jumpSpeed = 8.0f;


    //現在の加速度
    Vector3 velocity;

    Animation animation;

    // Use this for initialization
    void Start()
    {

        animation = this.gameObject.GetComponent<Animation>();

        //歩行アニメーションを若干小走りにする
        this.gameObject.GetComponent<Animation>()["Walk"].speed = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        //接地中のみ行う処理
        if (controller.isGrounded)
        {
            //キー入力から速度を決める
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            velocity *= wallkSpeed;

            if(Input.GetKeyDown("space")){
                //ジャンプ開始。縦方向の初速を与えて、ジャンプを再生する。
                velocity.y = jumpSpeed;
                animation.Play("Jump");
            }else if(velocity.magnitude > 0.5f){
                //歩行アニメーションに切り替えつつ、侵攻方向に旋回する
                animation.CrossFade("Walk", 0.1f);
                transform.LookAt(transform.position + velocity);

            }else{
                animation.CrossFade("Idle", 0.1f);
            }

            //重力による下方向への加速
            velocity.y -= gravity * Time.deltaTime;

            //キャラクターコントローラーの移動
            controller.Move(velocity * Time.deltaTime);

        }

    }


}
