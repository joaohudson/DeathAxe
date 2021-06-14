using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour
{
    [SerializeField]
    private float gravityModifer = 6f;
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float jumpForce = 20f;

    private CharacterAnimator animator;
    private CharacterController controller;
    private Vector3 velocity = Vector3.zero;
    private Vector3 knockback = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private float knockbackDuration = 0f;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = direction * speed;

        if(knockbackDuration > 0f)
        {
            knockbackDuration -= Time.deltaTime;
            knockbackDuration = Mathf.Max(knockbackDuration, 0f);
            knockback = Vector3.Lerp(Vector3.zero, knockback, knockbackDuration * 2f);
        }

        if(!isGrounded)
            velocity += Physics.gravity * gravityModifer * Time.deltaTime;
        
        move += velocity + knockback;

        controller.Move(move * Time.deltaTime);
        isGrounded = controller.isGrounded || velocity.y == 0f;

        if (animator != null)
        {
            animator.Speed = direction.sqrMagnitude;
        }
    }

    /// <summary>
    /// Direção do movimento deste personagem,
    /// para fazer o personagem parar, atribuir Vector3.zero.
    /// </summary>
    public Vector3 Direction
    {
        get => direction;
        set => direction = value;
    }

    /// <summary>
    /// Orientação do personagem, para onde ele está virado.
    /// </summary>
    public Vector3 Orientation
    {
        get => transform.forward;
        set => transform.forward = value;
    }

    /// <summary>
    /// Faz o personagem pular.
    /// </summary>
    public void Jump()
    {
        if(isGrounded)
            velocity.y = jumpForce;
    }

    /// <summary>
    /// Adiciona um empurrão a este personagem.
    /// </summary>
    /// <param name="knockback">A força do empurrão.</param>
    public void AddKnockback(Vector3 knockback)
    {
        this.knockback = knockback;
        this.knockbackDuration = .5f;
    }

    public void Dash(Vector3 dash)
    {
        AddKnockback(dash);
    }
}
