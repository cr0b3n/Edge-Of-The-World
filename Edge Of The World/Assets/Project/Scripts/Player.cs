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

    [Header("Game Over")]
    public Renderer graphicRender;
    public Material deathMaterial;
    public float pitch = .25f;
    //public float relativeRotation = 180f;

    private bool isJumping;
    private float stateTimeElapse = 0f;
    private bool gameOver;
    private Transform camTransform;
    private bool canRotate;

    private static readonly int moveAnim = Animator.StringToHash("speedPercent");
    private static readonly int jumping = Animator.StringToHash("isJumping");

    #region Unity Execusions
    private void Start() {
        //walkable = LayerMask.NameToLayer("Walkable");
        camTransform = Camera.main.transform;
    }

    private void Update() {


        if (gameOver) {
            GameOverMovement();
            return;
        }

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
        AudioManager.PlayPlayerAudio(PlayerAudio.Land);
    }

    private void CheckJump() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            if(IsGrounded()) {
                myRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);              
                isJumping = true;
                myAnim.SetBool(jumping, isJumping);
                jumpEffectPool.GetPooledObject(transform.position, transform.rotation);
                AudioManager.PlayPlayerAudio(PlayerAudio.Jump);
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

    private void RotateCamera() {

        if (!canRotate) {

            if(CheckIfCountDownElapsed(2f)) {
                canRotate = true;
                GameManager.Instance.ActivateEnding();
            }           
            return;
        }

        Vector3 targetCenter = transform.position; //- offset;
        Vector3 dToCenter = camTransform.transform.position - targetCenter;
        //Vector3 angles = new Vector3(0f, 1f);
        Quaternion newRot = Quaternion.Euler(Vector3.up);
        Vector3 newDir = newRot * dToCenter;
        camTransform.position = targetCenter + newDir;
        camTransform.LookAt(transform.position);
    }

    private void GameOverMovement() {
        transform.Translate(Vector3.up * Time.deltaTime);
        RotateCamera();
    }

    public void ActivateEndGame() {
        PlanetMovingObject pmo = GetComponent<PlanetMovingObject>();

        if (pmo != null)
            pmo.enabled = false;

        gameOver = true;
        myRB.isKinematic = false;
        myRB.velocity = Vector3.zero;
        myRB.angularVelocity = Vector3.zero;
        graphicRender.material = deathMaterial;
        myAnim.SetBool("gameOver", true);

        TraveledDistance td = GetComponent<TraveledDistance>();

        if (td != null)
            td.isActive = false;

        GUIManager.Instance.gameOver = true;
    }

    #endregion /My Methods

}
