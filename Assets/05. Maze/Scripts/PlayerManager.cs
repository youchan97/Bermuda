using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IHitable
{
    private Rigidbody rigid;
    private bool isJumping;
    public int gameHp;
    public float moveSpeed;
    public PlayerData playerData;
    public bool isCheck;
    private float RotateSpeed;
    public bool isInvisible;
    public PlayerInput input;
    InputAction useAction;
    InputValue value;

    public int GameHp
    {
        get => gameHp;
        set
        {
            gameHp = value;
        }
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        useAction = input.actions["Use"];
    }

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();     
        gameHp = playerData.hp;
        moveSpeed = 3f;
        rigid = GetComponent<Rigidbody>();
        transform.position = Vector3.up * 5 + Vector3.forward * 75 + Vector3.right * -12;
        isJumping = false;
        isCheck = false;
        RotateSpeed = 250f;
        isInvisible = false;
        GameController.instance.OnGameEnd += () => { Die(); };
    }

    private void Update()
    {
        if (useAction.triggered)
            Debug.Log(useAction);
    }

    /*public void OnUse(InputValue value)
    {
        KeyInput(value);
    }

    public void KeyInput(int key)
    {
        keyCode = key;
        Debug.Log(keyCode.);
    }*/

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -RotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * 3.5f, ForceMode.Impulse);
        }
        else
        {
            return;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void Hit(int damage)
    {
        GameHp -= damage;
    }

    public void Die()
    {
        SceneManager.LoadScene("Die");
    }
}
