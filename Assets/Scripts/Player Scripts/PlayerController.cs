using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerModel;
    public GameObject playerMeleeWeapon;
    public GameObject playerRangedWeapon;
    public GameObject playerWeaponHolder;
    public Health playerHealth;
    public Material playerMaterial;
    public Material healMaterial;
    public int maxHP;
    public int meleeDamage;
    public int rangedDamage;
    private int healValue;
    private Color originalColor;
    private Color lerpedColor;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float rotationSpeed = 720;
    private float meleeTimer = 0;
    private float xBoundary = 34.5f;
    private float zBoundary = 34.5f;
    private float lerpTime = 0;
    private int rangeTimer = 1;
    private int healTimer = 5;
    private bool isMelee = false;
    private bool isRange = false;
    private bool isHeal = false;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = new Color(.03f, .9f, .8f, 1);
        playerMaterial.color = originalColor;
        PlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerBoundary();
        PlayerRotation();
        PlayerMelee();
        PlayerRanged();
        PlayerHeal();
    }

    private void PlayerStats()
    {
        maxHP = 100;
        meleeDamage = 10;
        rangedDamage = 5;
        healValue = 25;
    }

    // Player movement controls
    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * moveSpeed, Space.World);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed, Space.World);

        playerWeaponHolder.transform.position = transform.position;
        playerModel.transform.position = transform.position;


    }

    void PlayerBoundary()
    {
        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }

        if (transform.position.z < -zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }
    }

    // Player faces the direction of movement
    void PlayerRotation()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            Quaternion lookDirection = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime);
        }

    }

    // Player melee attack controls
    void PlayerMelee()
    {
        if (Input.GetMouseButtonDown(0) && (isMelee == false))
        {
            isMelee = true;
            playerMeleeWeapon.SetActive(true);
            meleeTimer = 1;
            StartCoroutine(PlayerMeleeAnimation());
        }
    }

    // Player ranged attack controls
    void PlayerRanged()
    {
        if (Input.GetMouseButtonDown(1) && isRange == false)
        {
            isRange = true;
            Instantiate(playerRangedWeapon, transform.position, transform.rotation);
            StartCoroutine(RangeCooldown());
        }

    }

    void PlayerHeal()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isHeal == false)
        {
            isHeal = true;
            StartCoroutine(HealCooldown());
        }

        if (isHeal == true)
        {
            lerpedColor = Color.Lerp(healMaterial.color, originalColor, lerpTime);
            playerMaterial.color = lerpedColor;
            if (lerpTime < 1)
            {
                lerpTime += Time.deltaTime / 5;
            }
        }
    }

    IEnumerator RangeCooldown()
    {
        yield return new WaitForSeconds(rangeTimer);
        isRange = false;
    }

    IEnumerator HealCooldown()
    {
        playerHealth.HealPlayer(healValue);
        yield return new WaitForSeconds(healTimer);
        isHeal = false;
        lerpTime = 0;
        playerMaterial.color = originalColor;
    }

    // Melee animation plays until the meleeTimer is >= 0 and then resets playerMeleeWeapon position and rotation
    IEnumerator PlayerMeleeAnimation()
    {
        while (meleeTimer >= 0)
        {
            meleeTimer -= Time.deltaTime;
            playerMeleeWeapon.transform.RotateAround(transform.position, Vector3.up, 360 * Time.deltaTime);
            yield return null;
        }

        if (meleeTimer <= 0)
        {
            isMelee = false;
            playerMeleeWeapon.SetActive(false);
            playerMeleeWeapon.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            playerMeleeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
