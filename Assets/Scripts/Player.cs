using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private bool isGrounded;
    private float timeAttack; // contador
    public float startTimeAttack; //tempo animaçãp
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {  
        // andar para direita
        if(Input.GetKey(KeyCode.RightArrow)) {
            rigidBody.velocity = new Vector2(Speed, rigidBody.velocity.y);

            //Incluindo as animações
            animator.SetBool("isWalking", true);
            //Rotaciona o eixo do player
            sprite.flipX = false;
        } else {
            //Assim que soltar a seta precisa parar inves de continuar a caminhar
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);

            //Não tem tecla pressionada deve ser desativado o walking
            animator.SetBool("isWalking", false);
        }

        //andar para esquerda
        if(Input.GetKey(KeyCode.LeftArrow)) {
            rigidBody.velocity = new Vector2(-Speed, rigidBody.velocity.y);

            //Incluindo as animações
            animator.SetBool("isWalking", true);
            //Rotaciona o eixo do player
            sprite.flipX = true;
        }

        // pular
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) {
            rigidBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        // bicudar
        if(timeAttack <= 0) {
            if(Input.GetKeyDown(KeyCode.Z)) {
                animator.SetTrigger("isAttacking");
                timeAttack = startTimeAttack; // tempo para colocar no unity
            } 
        } else {
            timeAttack -= Time.deltaTime; // decrescer em tempo real
            animator.SetTrigger("isAttacking");
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        // verifica se o personagem esta tocando o chão
        if(coll.gameObject.layer == 8) {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
