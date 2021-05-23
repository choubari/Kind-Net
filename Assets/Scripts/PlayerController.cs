using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Cinemachine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]


public class PlayerController : MonoBehaviour
{
	int gemCount=0;
	int peopleCount=0;
	// Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
   // public Camera mainCamera;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = true;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    
    public AudioSource audioSourceDiamond;
    public AudioSource audioSourcePeople;

    public Text diamondUICount;
    public Text peopleUICount;


    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera endCamera;
    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        /*if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
    	//move();
        // Movement controls
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera follow
       /* if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }*/
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }
    
	void OnTriggerEnter2D(Collider2D col) {        
        if (col.gameObject.tag == "diamond") {
         	gemCount+=1;
         	audioSourceDiamond.Play();
            UpdateDiamondCountUI();
            Destroy (col.gameObject);
        }
        if (col.gameObject.tag == "sad" && gemCount>0) {
        	spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
         	spriteRenderer.sprite = newSprite;
         	audioSourcePeople.Play();
         	gemCount-=1;
         	peopleCount+=1;
         	UpdatePeopleCountUI();
			UpdateDiamondCountUI();         
         	col.gameObject.tag="happy";
         	//print(col.gameObject.tag);
            //Destroy (col.gameObject);
        }
        if (col.gameObject.tag == "end") {
            endCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);    
        }
        /* 
        if (col.gameObject.tag == "enemy") {
         	Destroy (gameObject);

        }
        */
     }

     void UpdateDiamondCountUI(){
     	diamondUICount.text=gemCount.ToString();
     }
    void UpdatePeopleCountUI(){
     	peopleUICount.text=peopleCount.ToString();
     }
}