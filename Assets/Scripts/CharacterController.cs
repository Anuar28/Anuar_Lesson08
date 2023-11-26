using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _speedMulti = 2;
    private managerJoystick _mngrJoyStick;

    [SerializeField] private int maxHealth;
    private int health;
    public int Health
    { 
        get 
        { 
            return health; 
        } 
        set 
        {  
            health = value;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            healthbar.UpdateHealthbar(maxHealth, health);
        } 
    }

    public void Die()
    {
        
    }
    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
    }

    private void Awake() 
    {
        tag = "Player";
    }

    [SerializeField] private HealthBar healthbar;

    // Start is called before the first frame update
    private void Start()
    {
        _mngrJoyStick = GameObject.Find("ImgJoystickBg").GetComponent<managerJoystick>();
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
      // float verticalInput = Input.GetAxis ("Vertical");
        float horizontalInput = _mngrJoyStick.inputHorizontal();
        float verticalInput = _mngrJoyStick.InputVertical();
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }


        Vector3 movment = new Vector3(horizontalInput, 0, verticalInput);
        movment.Normalize();

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movment, Time.deltaTime * _moveSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

        float calculatedSpeed = Mathf.Clamp(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput), 0, 1);


       
            if (Input.GetKey(KeyCode.LeftShift))
        {
            calculatedSpeed *= _speedMulti;
            _moveSpeed = Mathf.Clamp(_moveSpeed, 2, _moveSpeed * _speedMulti);
        }
        _animator.SetFloat("MovmentSpeed", calculatedSpeed);
        

    }
    public void OnJumpButtonDown()
    {
        _animator.SetTrigger("Jump");
    }

    public void TakeDamageButton() 
    {
        TakeDamage(10);
    }
}
