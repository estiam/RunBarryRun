using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float jumpForce = 1200f;

    public bool run = false;

    public string collisionSide = string.Empty;
    public bool hitWall;

    public float horizontalJumpPower = 40;
    public float wallJumpVerticalBoost = 1000f;

    [Range(0, 10)]
    public int maxNbJump = 2;
    private int nbJump = 0;

    private bool gotHit = false;
    public float time = 3f;
    private float timeStore;


    private Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        timeStore = time;
    }

    void FixedUpdate()
    {
        if (!gotHit && run && GetComponent<PhysicsHelper>().IsGrounded && !hitWall)
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
        else
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y);


        if (GetComponent<PhysicsHelper>().IsGrounded)
        {
            GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
            GetComponent<Animator>().SetBool("Jump", false);
            nbJump = 0;
            if (collisionSide == string.Empty)
                hitWall = false;
        }

        //
        if (!GetComponent<PhysicsHelper>().IsGrounded && Mathf.Abs(playerRB.velocity.x) > 0)
        {
            GetComponent<Animator>().SetFloat("Speed", 0);
        }

        if(transform.position.y < -10)
        {
            LifeManager.Die();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!MenuHandler.isPaused)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (collisionSide != string.Empty)
                    WallJump();
                else
                    Jump();
            }
        }

        if(gotHit)
        {
            if (time > 0)
                time -= Time.deltaTime;
            else
            {
                gotHit = false;
                time = timeStore;
            }
        }
        GetComponent<Animator>().SetBool("Hurt", gotHit);
    }

    void OnCollisionEnter(Collision hit)
    {

        if (hit.gameObject.tag == "wall")
        {
            collisionSide = transform.InverseTransformPoint(hit.transform.position).x > 0f ? "left" : "right";
            hitWall = true;
        }
        if(hit.gameObject.tag == "baddy")
        {
            if(LifeManager.coins == 0)
            {
                LifeManager.Die();
                GetComponent<MenuHandler>().RestartGame();
                return;
            }
            gotHit = true;
            GetComponent<Rigidbody>().AddForce(new Vector2(-10f, 10f), ForceMode.Impulse);

        }
    }

    void OnCollisionExit(Collision hit)
    {
        collisionSide = string.Empty;
    }

    void Jump()
    {
        if (nbJump < maxNbJump - 1)
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * (jumpForce / (nbJump + 1)));
            GetComponent<Animator>().SetBool("Jump", true);
            nbJump++;
        }
    }

    void WallJump()
    {
        Debug.Log("Wall Jump");
        hitWall = false;

        var hjp = GetComponent<PhysicsHelper>().IsGrounded ? horizontalJumpPower * 4 : horizontalJumpPower;
        playerRB.AddForce(new Vector2(collisionSide == "right" ? -hjp : hjp, jumpForce + wallJumpVerticalBoost));

    }
}
