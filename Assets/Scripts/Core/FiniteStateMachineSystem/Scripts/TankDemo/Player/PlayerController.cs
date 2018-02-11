using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the logic of player tank
/// </summary>
public class PlayerController : Controller
{
    public Renderer trackMeshRender;

    public float rotateSpeed = 50.0f;

    public float moveSpeed = 5.0f;

    private Animator m_Animator;

    private BoxCollider m_BoxCollider;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        m_Animator = GetComponent<Animator>();

        m_BoxCollider = GetComponent<BoxCollider>();     
    }

    void Start()
    {
        //regist the DrawViewEvent function to the event of showPlayerViewEvent in UIManager
        UIManager.GetInstance().showPlayerViewEvent += DrawViewEvent;
    }

    public void DrawViewEvent(bool value)
    {
        this.drawView = value;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");

        float rotateInput = Input.GetAxis("Horizontal");

        float inputTotal = Mathf.Abs(moveInput) + Mathf.Abs(rotateInput);

        //move forward
        this.transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime, Space.Self);

        //rotate tank body
        this.transform.Rotate(Vector3.up * rotateInput * rotateSpeed * Time.deltaTime, Space.Self);

        m_Animator.SetFloat("moveSpeed", inputTotal);


        //open fire
        if (Input.GetButtonDown("Jump"))
        {
            m_Animator.SetTrigger("openfire");

            OpenfireEvent();
        }



        ///offset the track texture of player tank
        float textureOffSetSpeedFactor = 0.05f;

        Material material = trackMeshRender.materials[0];

        float mainTextureOffsetX = material.mainTextureOffset.x + moveInput * textureOffSetSpeedFactor;

        mainTextureOffsetX = Mathf.Repeat(mainTextureOffsetX, 1.0f);

        material.mainTextureOffset = new Vector2(mainTextureOffsetX, 0.0f);

    }

    /// <summary>
    /// openfire function
    /// </summary>
    public void OpenfireEvent()
    {
        RaycastHit hitInfo;

        bool ifHitSomething = Physics.SphereCast(transform.position, m_BoxCollider.bounds.size.x,
        this.transform.forward, out hitInfo, this.attackRange);

        //if the ai tank in player attack range,we reduce the life value of ai tank
        if (ifHitSomething == true && hitInfo.transform.CompareTag("AITank"))
        {
            LifeSystem lifeSystem = hitInfo.transform.GetComponent<LifeSystem>();

            lifeSystem.ChangeCurrentLifeValue(-10.0f);
        }
    }
}
