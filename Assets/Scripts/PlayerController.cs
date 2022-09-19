using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerModel;
    public GameObject playerMeleeWeapon;
    public GameObject playerRangedWeapon;
    public GameObject playerWeaponHolder;
    private float horizontalInput;
    private float verticalInput; 
    [SerializeField]private float moveSpeed = 10;
    [SerializeField]private float rotationSpeed = 720;
    private float meleeTimer = 0;
    private float xBoundary = 34.5f;
    private float zBoundary = 34.5f;
    private bool isMelee = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerBoundary();
        PlayerRotation();
        PlayerMelee();
        PlayerRanged();
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
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(playerRangedWeapon, transform.position, transform.rotation);
        }
        
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
