using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour {

    [Header("Dependencies")]
    public Rigidbody myRB;
    public Animator myAnim;
    public Transform graphicTransform;

    [Header("Movement")]
    public float speed = 15f;
    public float jumpForce = 30f;
    public float groundCheckRadius = 0.2f;
    public float jumpCheckDelay = 0.1f;
    public LayerMask walkableLayer;
    public ObjectPooler jumpEffectPool;

    private bool isJumping;
    private float stateTimeElapse = 0f;

    private static readonly int moveAnim = Animator.StringToHash("speedPercent");
    private static readonly int jumping = Animator.StringToHash("isJumping");

    #region Unity Execusions
    //private void Start() {
    //    walkable = LayerMask.NameToLayer("Walkable");
    //}

    private void Update() {

        PlayerMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        CheckJumpStatus();
        CheckJump();
    }

    //private void OnCollisionEnter(Collision collision) {

    //    if (!isJumping) return;

    //    if (collision.gameObject.layer != walkable) return;

    //    isJumping = false;
    //    myAnim.SetBool(jumping, isJumping);
    //}

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }

    #endregion /Unity Execusions

    #region My Methods

    public bool CheckIfCountDownElapsed(float duration) {

        stateTimeElapse += Time.deltaTime;
        return stateTimeElapse >= duration;
    }

    private void CheckJumpStatus() {
        
        if (!isJumping) return;

        if (!CheckIfCountDownElapsed(jumpCheckDelay))
            return;

        if (!IsGrounded()) return;

        stateTimeElapse = 0f;
        isJumping = false;
        myAnim.SetBool(jumping, isJumping);
        jumpEffectPool.GetPooledObject(transform.position, transform.rotation);
    }

    private void CheckJump() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(IsGrounded()) {
                myRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);              
                isJumping = true;
                myAnim.SetBool(jumping, isJumping);
                jumpEffectPool.GetPooledObject(transform.position, transform.rotation);
            }              
        }
    }

    private bool IsGrounded() {
        return Physics.CheckSphere(transform.position, groundCheckRadius, walkableLayer);
    }

    private void PlayerMovement(float xMovement, float zMovement) {

        Vector3 movement = new Vector3(xMovement, 0f,zMovement);
        transform.Translate(movement * speed * Time.deltaTime);

        if (movement != Vector3.zero) {
            graphicTransform.localRotation = Quaternion.LookRotation(movement);
        }

        float movementMagnitude = Mathf.Clamp(movement.magnitude, 0f, 1f);
        myAnim.SetFloat(moveAnim, movementMagnitude, .1f, Time.deltaTime);
    }

    #endregion /My Methods

}
