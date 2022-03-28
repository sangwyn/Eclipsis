// using System;
// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using UnityEditor;
// using UnityEngine;
//
// [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
// public class PlayerController : MonoBehaviour
// {
//     static public PlayerController instance;
//     
//     [SerializeField] private Rigidbody2D _rigidbody2D;
//
//     [SerializeField] private FloatingJoystick _joystick;
//     
//     private Animator _animator;
//     public Transform GunTransform;
//
//     [SerializeField] private GameObject PlayerSpriteGameObject;
//     
//     private Transform PlayerSpriteTransform;
//
//     [SerializeField] private float _moveSpeed;
//     [SerializeField] private float _dashSpeed;
//     
//     private GameObject _lookingAt = null;
//
//     private Vector2 _previousJoystickPosition = new Vector2(1, 0);   //значния джойстика, когда он был сдвинут. В x - horizonatl, в y - vertical
//     // Update is called once per frame
//
//     private bool isMovingLeft = false;
//     
//     [SerializeField] private UIBar healthBar;
//     [SerializeField] private UIBar manaBar;
//     [SerializeField] private int maxHealth = 10;
//     [SerializeField] private int health;
//     [SerializeField] private int maxMana = 10;
//     [SerializeField] private int mana;
//     [SerializeField] private int gold = 0;
//     [SerializeField] private int strength = 1;
//     [SerializeField] private int dexterity = 1;
//     [SerializeField] private int intelligence = 1;
//
//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(this);
//             health = maxHealth;
//             mana = maxMana;
//         }
//         else
//         {
//             Destroy(this.gameObject);
//         }
//     }
//
//     private void Start()
//     {
//         _animator = PlayerSpriteGameObject.GetComponent<Animator>();
//         PlayerSpriteTransform = PlayerSpriteGameObject.GetComponent<Transform>();
//     }
//
//     private void Update()
//     {
//         healthBar.SetValue(health, maxHealth);
//         manaBar.SetValue(mana, maxMana);
//         if (Input.GetKeyDown(KeyCode.LeftShift))
//         {
//             DoDash();
//         }
//
//         if (_joystick.Direction != Vector2.zero)
//         {
//             _animator.SetBool("isRunning", true);            
//         }
//         else
//         {
//             _animator.SetBool("isRunning", false);
//         }
//
//         if (_joystick.Horizontal < -0.1f)
//         {
//             //GunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
//             if (!isMovingLeft)
//             {
//                 PlayerSpriteTransform.localScale = new Vector3(-1, 1, 1);
//                 //GunTransform.Rotate(0, 0, 180 - GunTransform.rotation.z, Space.Self);
//                 if (GunTransform.rotation.z >= 0)
//                 {
//                     
//                     GunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - TransformUtils.GetInspectorRotation(GunTransform).z));
//                 }
//                 else
//                 {
//                     GunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -180 + Mathf.Abs(TransformUtils.GetInspectorRotation(GunTransform).z)));
//                 }
//             }
//             isMovingLeft = true;
//         }
//         else if (_joystick.Horizontal > 0.1f)
//         {
//             //GunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
//             if (isMovingLeft)
//             {
//                 PlayerSpriteTransform.localScale = Vector3.one;
//                 if (GunTransform.rotation.z >= 0)
//                 {
//                     
//                     GunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 - TransformUtils.GetInspectorRotation(GunTransform).z));
//                 }
//                 else
//                 {
//                     GunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -180 + Mathf.Abs(TransformUtils.GetInspectorRotation(GunTransform).z)));
//                 }
//                 //GunTransform.Rotate(0, 0, -180 + GunTransform.rotation.z, Space.Self);
//             }
//
//             isMovingLeft = false;
//         }
//     }
//
//     private void FixedUpdate()
//     {
//         _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _moveSpeed * Time.deltaTime, _joystick.Vertical * _moveSpeed * Time.deltaTime);
//         if (Mathf.Abs(_joystick.Horizontal) > 0.01f || Mathf.Abs(_joystick.Vertical) > 0.01f)   // проверка, что джойтик сдвинут
//         {
//             _previousJoystickPosition = new Vector2(_joystick.Horizontal, _joystick.Vertical);  // записал эти занчения
//         }
//     }
//
//     public void DoDash()
//     {
//         Vector3 newPosition = transform.position;
//         var horizontal = _joystick.Horizontal;  //Значение позиции джойстика по горизонтали
//         var vertical = _joystick.Vertical;  //Значение позиции джойстика по вертикали
//         if (Mathf.Abs(horizontal) < 0.01f && Mathf.Abs(vertical) < 0.01f)  // Проверка на то, что джойстик неподвижен
//         {
//             horizontal = _previousJoystickPosition.x;   // присваиваиваю значения положения джойстика, когда он был сдвинут
//             vertical = _previousJoystickPosition.y;
//         }
//         newPosition += transform.right * (_dashSpeed * horizontal) + transform.up * (_dashSpeed * vertical);
//         transform.DOMove(newPosition, 0.5f);
//     }
//     
//     public void PickUpItem()
//     {
//         if (_lookingAt != null)
//         {
//             _lookingAt.GetComponent<Pickable>().PickUp();
//         }
//     }
//     
//     public void GiveHealth(int value)
//     {
//         health += value;
//         health = Math.Min(health, maxHealth);
//         healthBar.SetValue(health, maxHealth);
//         if (health <= 0)
//         {
//             Die();
//         }
//     }
//
//     public void Die()
//     {
//         Debug.Log("YOU DIED");
//         Destroy(this.gameObject);
//     }
//
//     public void GiveGold(int value)
//     {
//         gold += value;
//     }
//
//     public bool GiveMana(int value)
//     {
//         if (mana + value < 0)
//             return false;
//         mana += value;
//         mana = Math.Min(mana, maxMana);
//         manaBar.SetValue(mana, maxMana);
//         return true;
//     }
//
//     public void GiveStrength(int value)
//     {
//         strength += value;
//     }
//     
//     public void GiveDexterity(int value)
//     {
//         dexterity += value;
//     }
//     
//     public void GiveIntelligence(int value)
//     {
//         intelligence += value;
//     }
//
//     public int GetStrength()
//     {
//         return strength;
//     }
//
//     public int GetDexterity()
//     {
//         return dexterity;
//     }
//
//     public int GetIntelligence()
//     {
//         return intelligence;
//     }
//
//     public int GetGold()
//     {
//         return gold;    
//     }
//     
//     private void OnTriggerEnter2D(Collider2D col)
//     { 
//         if (col.gameObject.GetComponent<Pickable>())
//         {
//             _lookingAt = col.gameObject;
//         }
//     }
//
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.gameObject.GetComponent<Pickable>())
//         {
//             _lookingAt = null;
//         }
//     }
// }

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private FloatingJoystick _movementJoystick;
    [SerializeField] private FloatingJoystick _attackJoystick;

    private Animator _animator;
    public Transform GunTransform;

    [SerializeField] private GameObject PlayerSpriteGameObject;

    private Transform PlayerSpriteTransform;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashSpeed;

    private GameObject _lookingAt = null;

    private Vector2
        _previousJoystickPosition =
            new Vector2(1, 0); //значния джойстика, когда он был сдвинут. В x - horizonatl, в y - vertical
    // Update is called once per frame

    private bool isMovingLeft = false;

    [SerializeField] private UIBar healthBar;
    [SerializeField] private UIBar manaBar;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int health;
    [SerializeField] private int maxMana = 10;
    [SerializeField] private int mana;
    [SerializeField] private int gold = 0;
    [SerializeField] private int strength = 1;
    [SerializeField] private int dexterity = 1;
    [SerializeField] private int intelligence = 1;

    //ПЕРЕМЕННЫЕ АУДИО
    public AudioSource Footstep1, Footstep2, Footstep3, Footstep4, Footstep5, Footstep6, Footstep7;
    public AudioSource DashSound;
    public AudioSource PickupSound;
    public AudioSource SoundS;

    private float soundStartTime = -1;
    private bool isSoundPlaying = false;
    private int prevFootstep = 8;
    
    
    private GameObject weapons;
    private int current_weapon = 0;
    private GameObject weaponSwitchBtn;
    private Image weaponSwitchBtnImage;
    private RectTransform weaponSwitsdchBtnRect;
    private GameObject inventory;

    private bool isCanMove = true;
    private bool isCanDoDash = true;

    private void Awake()
    {
        SoundS.Play();
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 30;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            health = maxHealth;
            mana = maxMana;
        }
        else
        {
            Destroy(this.gameObject);
        }

        weapons = GameObject.Find("Weapons");
        weaponSwitchBtn = GameObject.Find("WeaponSwitchButton");
        weaponSwitchBtnImage = weaponSwitchBtn.GetComponent<Image>();
        weaponSwitsdchBtnRect = weaponSwitchBtn.GetComponent<RectTransform>();
        weaponSwitchBtn.SetActive(false);
        inventory = GameObject.Find("InventoryGO");
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
         _animator = PlayerSpriteGameObject.GetComponent<Animator>();
        //_animator = GetComponent<Animator>();
        PlayerSpriteTransform = PlayerSpriteGameObject.GetComponent<Transform>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (weapons.transform.childCount > 0)
        {
            weaponSwitchBtn.SetActive(true);
            current_weapon = 0;
            GunTransform = weapons.transform.GetChild(0);
            Sprite currentSprite = weapons.transform.GetChild(current_weapon).gameObject.transform.GetChild(0)
                .GetComponent<SpriteRenderer>().sprite;
            weaponSwitchBtnImage.sprite = currentSprite;
            weaponSwitsdchBtnRect.sizeDelta = new Vector2(currentSprite.rect.width, currentSprite.rect.height);
        }
    }

    private void Update()
    {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        //print(fps);
        healthBar.SetValue(health, maxHealth);
        manaBar.SetValue(mana, maxMana);
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DoDash();
        }*/

        if (_movementJoystick.Direction != Vector2.zero)
        {
            if (!isSoundPlaying)
            {
                isSoundPlaying = true;
                PlayFootstepSound(Random.Range(0, 7));
                
                soundStartTime = Time.time;
            }

            if ((Time.time - soundStartTime) > 0.33)
            {
                isSoundPlaying = false;
            }
            
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        if (_movementJoystick.Horizontal < -0.1f)
        {
            if (!isMovingLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        
            isMovingLeft = true;
        }
        else if (_movementJoystick.Horizontal > 0.1f)
        {
            if (isMovingLeft)
            {
                transform.localScale = Vector3.one;
            }
        
            isMovingLeft = false;
        }
    }

    private void FixedUpdate()
    {
        if(!isCanMove) return;
        _rigidbody2D.velocity = new Vector2(_movementJoystick.Horizontal * _moveSpeed * Time.deltaTime,
            _movementJoystick.Vertical * _moveSpeed * Time.deltaTime);
        if (Mathf.Abs(_movementJoystick.Horizontal) > 0.01f ||
            Mathf.Abs(_movementJoystick.Vertical) > 0.01f) // проверка, что джойтик сдвинут
        {
            _previousJoystickPosition =
                new Vector2(_movementJoystick.Horizontal, _movementJoystick.Vertical); // записал эти занчения
        }
    }

    public void SwitchWeapon()
    {
        if (weapons.transform.childCount == 0)
            return;
        if (!inventory.activeSelf)
            weaponSwitchBtnImage.gameObject.SetActive(true);
        Sprite currentSprite = weapons.transform.GetChild(current_weapon).gameObject.transform.GetChild(0)
            .GetComponent<SpriteRenderer>().sprite;
        weaponSwitchBtnImage.sprite = currentSprite;
        weaponSwitsdchBtnRect.sizeDelta = new Vector2(currentSprite.rect.width, currentSprite.rect.height);
        if (weapons.transform.childCount < 2)
            return;
        weapons.transform.GetChild(current_weapon).GetComponent<ShootingWeapon>().BeforeSwitch();
        weapons.transform.GetChild(current_weapon).gameObject.SetActive(false);
        current_weapon = (current_weapon == 0 ? 1 : 0);
        weapons.transform.GetChild(current_weapon).gameObject.SetActive(true);
        currentSprite = weapons.transform.GetChild(current_weapon).gameObject.transform.GetChild(0)
            .GetComponent<SpriteRenderer>().sprite;
        weaponSwitchBtnImage.sprite = currentSprite;
        weaponSwitsdchBtnRect.sizeDelta = new Vector2(currentSprite.rect.width, currentSprite.rect.height);
    }

    public void RemoveWeapon(int i)
    {
        if (current_weapon == i)
            SwitchWeapon();
        current_weapon = 0;
        if (weapons.transform.childCount == 0)
            weaponSwitchBtn.SetActive(false);
    }

    public void ReloadWeapon()
    {
        if (weapons.transform.childCount == 0)
            return;
        weapons.transform.GetChild(current_weapon).GetComponent<ShootingWeapon>().ReloadFunction();
    }
    
    public int GetWeaponsCount()
    {
        return weapons.transform.childCount;
    }

    public void DoDash()
    {
        DashSound.Play();
        
        if (!isCanDoDash) {
            return;
        }
        isCanDoDash = false;
        Vector3 newPosition = transform.position;
        var horizontal = _movementJoystick.Horizontal; //Значение позиции джойстика по горизонтали
        var vertical = _movementJoystick.Vertical; //Значение позиции джойстика по вертикали
        if (Mathf.Abs(horizontal) < 0.01f && Mathf.Abs(vertical) < 0.01f) // Проверка на то, что джойстик неподвижен
        {
            horizontal =
                _previousJoystickPosition.x; // присваиваиваю значения положения джойстика, когда он был сдвинут
            vertical = _previousJoystickPosition.y;
        }

        newPosition += transform.right * (_dashSpeed * horizontal) + transform.up * (_dashSpeed * vertical);
        // var direction = newPosition - transform.position;
        // _rigidbody2D.AddForce(direction * 1000);
        transform.DOMove(newPosition, 0.5f);
        Invoke("SetIsCanDoDashTrue", 1f);
    }

    private void SetIsCanDoDashTrue() {
        isCanDoDash = true;
    }

    public void PickUpItem()
    {
        PickupSound.Play();
        
        if (_lookingAt != null)
        {
            if (_lookingAt.CompareTag("HealingPotion"))
            {
                health = maxHealth;
                Destroy(_lookingAt);
                _lookingAt = null;
                return;
            }
            _lookingAt.GetComponent<Pickable>().PickUp();
        }
    }

    public void GiveHealth(int value)
    {
        if (health <= 0)
            return;
        health += value;
        health = Math.Min(health, maxHealth);
        healthBar.SetValue(health, maxHealth);
        if (health <= 0)
        {
            Invoke("Die", 0.5f);
        }
    }
    
    public void Die()
    {
        weapons.SetActive(false);
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isDying", true);
        SetIsCanMove(false);
        Invoke("DestroyPlayer", 2.5f);
    }

    public void SetIsCanMove(bool value)
    {
        isCanMove = value;
        if (!isCanMove)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
    private void DestroyPlayer()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("GameOver");
    }
    
    public void GiveGold(int value)
    {
        gold += value;
    }

    public bool GiveMana(int value)
    {
        if (mana + value < 0)
            return false;
        mana += value;
        mana = Math.Min(mana, maxMana);
        manaBar.SetValue(mana, maxMana);
        return true;
    }

    public void GiveStrength(int value)
    {
        strength += value;
    }

    public void GiveDexterity(int value)
    {
        dexterity += value;
    }

    public void GiveIntelligence(int value)
    {
        intelligence += value;
        if (health >= maxHealth)
            health = 10 + intelligence / 2;
        maxHealth = 10 + intelligence / 2;  
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetDexterity()
    {
        return dexterity;
    }

    public int GetIntelligence()
    {
        return intelligence;
    }

    public int GetGold()
    {
        return gold;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Pickable>() || col.CompareTag("HealingPotion"))
        {
            _lookingAt = col.gameObject;
        }

        if (col.CompareTag("EnemyProjectile"))
        {
            GiveHealth(-col.gameObject.GetComponent<Bullet>().GetDamage());
            Destroy(col.gameObject);
        }

        /*if (col.CompareTag("Enemy") || col.CompareTag("EnemyDistanceAttack"))
        {
            GiveHealth(-1);
        }*/
    }

    public void TakeDamage(int value) {
        GiveHealth(-1);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Boarder"))
        {
            DOTween.KillAll();
        }
        /*else if (other.CompareTag("Enemy") || other.CompareTag("EnemyDistanceAttack"))
        {
            GiveHealth(-1);
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Pickable>())
        {
            _lookingAt = null;
        }
    }

    public FloatingJoystick GetAttackJoystick()
    {
        return _attackJoystick;
    }
    
    private void PlayFootstepSound(int soundNum)
    {
        if (soundNum == prevFootstep)
        {
            soundNum = (soundNum + 1) % 7;
        }
        prevFootstep = soundNum;
        if (soundNum == 0)
        {
            Footstep1.Play();
        }
        if (soundNum == 1)
        {
            Footstep2.Play();
        }
        if (soundNum == 2)
        {
            Footstep3.Play();
        }
        if (soundNum == 3)
        {
            Footstep4.Play();
        }
        if (soundNum == 4)
        {
            Footstep5.Play();
        }
        if (soundNum == 5)
        {
            Footstep6.Play();
        }
        if (soundNum == 6)
        {
            Footstep7.Play();
        }
        
    }
}