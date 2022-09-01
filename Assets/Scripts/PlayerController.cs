using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerWeapon;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private float rotationSpeed = 720;
    private float meleeTimer = 0;
    private bool isMelee = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();
        PlayerMelee();
    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * moveSpeed, Space.World);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed, Space.World);

    }

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

    void PlayerMelee()
    {
        if (Input.GetMouseButtonDown(0) && (isMelee == false))
        {
            isMelee = true;
            playerWeapon.SetActive(true);
            meleeTimer = 1;
            StartCoroutine(PlayerMeleeAnimation());
        }
    }

    IEnumerator PlayerMeleeAnimation()
    {
        while (meleeTimer >= 0)
        {
            meleeTimer -= Time.deltaTime;
            playerWeapon.transform.RotateAround(transform.position, Vector3.up, 360 * Time.deltaTime);
            yield return null;
        }

        if (meleeTimer <= 0)
        {
            isMelee = false;
            playerWeapon.SetActive(false);
        }
    }
}
