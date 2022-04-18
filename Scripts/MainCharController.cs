using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MainCharController : MonoBehaviour
{
    float horizontal = 0, vertical = 0;
    Animator animator;
    Rigidbody fizik;
    float Speed=5;
    public GameObject mainCam;
    public GameObject thirdPersonCam,firstPersonCam,camLookAt,archeryAimCam;
    CapsuleCollider col;
    public Transform weapon,bowWeapon, axe_eqd_pos, axe_uneqd_pos,sword_eqd_pos,sword_uneqd_pos,CharHand,CharBack,torchPos,shieldPos,bow_eqd_pos,bow_uneqd_pos,string_pulling_pos,arrow_pos,arrow_fire_pos;
    public GameObject fpsPos, tpsPos;
    public GameObject Head;
    public GameObject crosshair;
    public GameObject inventoryPanel;
    public Canvas canvas;
    bool isTps=true;
    bool isFalling = false;
    float AttackSpeed_1;
    float AttackSpeed_2=1f;
    float timer1;
    float timer2;

    int WTapTimes;
    float WResetTimer=0.3f;
    int STapTimes;
    float SResetTimer=0.3f;
    int ATapTimes;
    float AResetTimer=0.3f;
    int DTapTimes;
    float DResetTimer=0.3f;


    bool onGround;
    bool isSword;
    bool isAxe;
    bool isBow;
    bool isThereShield;
    bool isThereWeapon;
    bool isThereBow;
    bool isInventoryOpened;

    Transform skeleton;
    public Vector3 ChestOffset;

    Arrow arrow;
    RaycastHit hit;
    RaycastHit hitCrosshair;

    [SerializeField]
    private InventoryUI inventoryUi;


    private void Start()
    {
        animator = GetComponent<Animator>();
        fizik = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator.SetBool("isEquiped", false);
        skeleton = animator.GetBoneTransform(HumanBodyBones.Chest);
    }
    private void LateUpdate()
    {
        if (animator.GetBool("isBowAiming"))
        {
            
            skeleton.LookAt(hitCrosshair.point);
            skeleton.transform.rotation *= Quaternion.Euler(ChestOffset);
        }

    }
    private void Update()
    {
        
        Movement();
        CameraRotation();
        Dodge();
        Keys();
        
        //Weapon equip in scene
        if (Inventory.instance.weaponHasChanged)
        {
            Debug.Log("girdi: ");
            if (weapon.childCount > 0)
            {
                Destroy(weapon.GetChild(0).gameObject);
                isThereWeapon = false;
                animator.SetBool("isThereWeapon", isThereWeapon);
                
                
                animator.SetBool("isEquiped", false);
                animator.SetBool("isShielding", false);
                
               
                
                if (isThereShield)
                {
                    animator.SetBool("isShielding", true);
                }
                
                    
                
            }
            else if (weapon.childCount == 0)
            {
                if (!isThereShield)
                    animator.SetBool("isEquiped", false);
            }
            WeaponEquipment();
        }
        if (Inventory.instance.weaponHasChanged)
            Inventory.instance.weaponHasChanged = false;
        //Weapon equip in scene

        //Shield equip in scene
        if (Inventory.instance.shieldHasChanged)
        {
            if (torchPos.childCount > 0)
                Destroy(torchPos.GetChild(0).gameObject);
            if (shieldPos.childCount > 0)
            {
                Destroy(shieldPos.GetChild(0).gameObject);
                isThereShield = false;
                animator.SetBool("isShielding", false);
                animator.SetBool("isThereShield", isThereShield);
            }
            if (bowWeapon.childCount > 0)
            {
                Destroy(bowWeapon.GetChild(0).gameObject);
            }


            animator.SetBool("isThereBow", false);

            ShieldEquipment();
        }
        if (Inventory.instance.shieldHasChanged)
            Inventory.instance.shieldHasChanged = false;
        //Shield equip in scene


        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            onGround = true;
        }
    }
    
    void Movement()
    {
        if (!isFalling)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        

        Vector3 vec = new Vector3(horizontal, 0, vertical);
        vec = transform.TransformDirection(vec);
        fizik.position += vec * Time.deltaTime * Speed;

        animator.SetFloat("HorizontalP", horizontal);
        animator.SetFloat("VerticalP", vertical);

        animator.SetFloat("VelocityY", fizik.velocity.y);

        
        
        
        if (fizik.velocity.y < -2 || fizik.velocity.y > 2)
        {
            animator.SetBool("isGrounded", false);
            onGround = false;
        }

        if (fizik.velocity.y > -1 && fizik.velocity.y < 1)
        {
            animator.SetBool("isGrounded", true);



        }



        if (onGround && horizontal == 0 && vertical == 0)
        {
            fizik.velocity = new Vector3(0, 0, 0);
        }
    }

    void CameraRotation()
    {
        if (isTps)
        {
            if (!animator.GetBool("isBowAiming"))
            {
                if ((horizontal != 0 || vertical != 0))
                {
                    Quaternion lookrotation = Quaternion.LookRotation(new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z));
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookrotation, Time.deltaTime * 5);
                }
            }
            else
            {
                Quaternion lookrotation = Quaternion.LookRotation(new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z));
                transform.rotation = Quaternion.Lerp(transform.rotation, lookrotation, Time.deltaTime * 50);

                Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f));
                Physics.Raycast(ray, out hitCrosshair);


            }
           
        }
        else if (!isTps)
        {
            Quaternion lookrotation = Quaternion.LookRotation(new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookrotation, Time.deltaTime * 5);

        }
    }

    void Dodge()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {

            StartCoroutine("SResetTapTimes");
            STapTimes++;
        }
        if (STapTimes >= 2)
        {
            animator.SetTrigger("Dodge");
            animator.SetBool("isDodging", true);
            animator.SetFloat("DodgeType", 1);
            STapTimes = 0;
            animator.SetBool("isDodging", false);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine("WResetTapTimes");
            WTapTimes++;
        }
        if (WTapTimes >= 2)
        {
            animator.SetTrigger("Dodge");
            animator.SetBool("isDodging", true);
            animator.SetFloat("DodgeType", 2);
            WTapTimes = 0;
            animator.SetBool("isDodging", false);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            StartCoroutine("AResetTapTimes");
            ATapTimes++;
        }
        if (ATapTimes >= 2)
        {
            animator.SetTrigger("Dodge");
            animator.SetBool("isDodging", true);
            animator.SetFloat("DodgeType", 3);
            ATapTimes = 0;
            animator.SetBool("isDodging", false);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            StartCoroutine("DResetTapTimes");
            DTapTimes++;
        }
        if (DTapTimes >= 2)
        {
            animator.SetTrigger("Dodge");
            animator.SetBool("isDodging", true);
            animator.SetFloat("DodgeType", 4);
            DTapTimes = 0;
            animator.SetBool("isDodging", false);
        }
    }

    void Keys()
    {
        if (inventoryPanel.activeSelf == false)
        {
            if (Input.GetButtonDown("Fire1") && animator.GetBool("isGrounded") == true)
            {
                if (Time.time > AttackSpeed_1)
                {


                    animator.SetTrigger("Attack");
                    animator.SetFloat("AttackType", Random.Range(1, 4));
                    AttackSpeed_1 = Time.time + AttackSpeed_2;


                }


            }

            if (animator.GetBool("isEquiped"))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    animator.SetTrigger("UnEquip");
                }


            }
            if (!animator.GetBool("isEquiped"))
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    animator.SetTrigger("Equip");
                }


            }

            if (!Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Speed = 15;
                    animator.SetBool("isRunning", true);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Speed = 5;
                    animator.SetBool("isRunning", false);
                }
            }

            if (Input.GetButtonDown("Jump") && animator.GetBool("isGrounded"))
            {
                if (Time.time > AttackSpeed_1)
                {


                    animator.SetTrigger("Jump");
                    animator.SetBool("isGrounded", false);
                    AttackSpeed_1 = Time.time + AttackSpeed_2;


                }
                
            }


            if (Input.GetKeyDown(KeyCode.Mouse1))
            {

                if (!animator.GetBool("isThereBow"))
                {
                    if (!animator.GetBool("isThereShield"))
                    {
                        animator.SetBool("isBlocking", true);
                        animator.SetBool("isRunning", false);
                    }
                    else
                    {
                        animator.SetBool("isShieldBlocking", true);
                        animator.SetBool("isRunning", false);
                    }
                }
                else
                {
                    if (animator.GetBool("isEquiped"))
                    {

                        animator.SetBool("isBowAiming", true);
                        crosshair.SetActive(true);
                        if (thirdPersonCam.activeSelf)
                        {
                            thirdPersonCam.SetActive(false);
                            archeryAimCam.SetActive(true);
                        }



                    }





                }


            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                animator.SetBool("isShieldBlocking", false);
                animator.SetBool("isBlocking", false);
                animator.SetBool("isBowAiming", false);
                crosshair.SetActive(false);
                if (!firstPersonCam.activeSelf)
                {
                    thirdPersonCam.SetActive(true);
                    archeryAimCam.SetActive(false);
                }

                if (arrow_pos.childCount > 0)
                {
                    for (int i = 0; i < arrow_pos.childCount; i++)
                    {
                        Destroy(arrow_pos.GetChild(i).gameObject);
                    }
                }


            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                if (arrow_pos.childCount > 0)
                {
                    for (int i = 0; i < arrow_pos.childCount; i++)
                    {
                        Destroy(arrow_pos.GetChild(i).gameObject);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                Speed = 5;
                animator.SetBool("isRunning", false);
            }


            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouching", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouching", false);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {


                if (isTps)
                    isTps = false;
                else
                    isTps = true;
                if (!isTps)
                {
                    thirdPersonCam.SetActive(false);
                    firstPersonCam.SetActive(true);
                    camLookAt.transform.SetParent(fpsPos.transform);


                }
                if (isTps)
                {
                    thirdPersonCam.SetActive(true);
                    firstPersonCam.SetActive(false);
                    camLookAt.transform.SetParent(tpsPos.transform);

                }



            }
        }
        
    }

    public void WeaponEquipment()
    {
        foreach(Item item in Inventory.instance.equipbarItemList)
        {
            if(item.itemType == Item.ItemType.WeaponItem)
            {
                isThereWeapon = true;
                animator.SetBool("isThereWeapon", true);
                if (item.name.Contains("Axe"))
                {
                    isAxe = true;
                    isSword = false;
                    isBow = false;
                    if (animator.GetBool("isEquiped"))
                    {
                        weapon.SetParent(axe_eqd_pos);
                        weapon.position = axe_eqd_pos.position;
                        weapon.rotation = axe_eqd_pos.rotation;
                        Instantiate(item.itemObj, weapon);
                        weapon.GetChild(0).transform.position = weapon.position;
                        weapon.GetChild(0).transform.rotation = weapon.rotation;

                    }
                    else if (!animator.GetBool("isEquiped"))
                    {
                        weapon.SetParent(axe_uneqd_pos);
                        weapon.position = axe_uneqd_pos.position;
                        weapon.rotation = axe_uneqd_pos.rotation;
                        Instantiate(item.itemObj, weapon);
                        weapon.GetChild(0).transform.position = weapon.position;
                        weapon.GetChild(0).transform.rotation = weapon.rotation;

                    }
                }
                else if (item.name.Contains("Sword"))
                {
                    isAxe = false;
                    isSword = true;
                    isBow = false;
                    
                    if (animator.GetBool("isEquiped"))
                    {
                        weapon.SetParent(sword_eqd_pos);
                        weapon.position = sword_eqd_pos.position;
                        weapon.rotation = sword_eqd_pos.rotation;
                        Instantiate(item.itemObj, weapon);
                        weapon.GetChild(0).transform.position = weapon.position;
                        weapon.GetChild(0).transform.rotation = weapon.rotation;

                    }
                    else if (!animator.GetBool("isEquiped"))
                    {
                        weapon.SetParent(sword_uneqd_pos);
                        weapon.position = sword_uneqd_pos.position;
                        weapon.rotation = sword_uneqd_pos.rotation;
                        Instantiate(item.itemObj, weapon);
                        weapon.GetChild(0).transform.position = weapon.position;
                        weapon.GetChild(0).transform.rotation = weapon.rotation;

                    }
                }
            }
        }
    }

    public void ShieldEquipment()
    {
        foreach (Item item in Inventory.instance.equipbarItemList)
        {

            if (item.itemType == Item.ItemType.ShieldItem)
            {
                
                if (item.name.Contains("Torch"))
                {
                    isThereBow = false;
                    animator.SetBool("isThereBow", false);
                    animator.SetBool("isTorching", true);
                    Instantiate(item.itemObj, torchPos);
                    torchPos.GetChild(0).transform.position = torchPos.position;
                    torchPos.GetChild(0).transform.rotation = torchPos.rotation;
                    animator.SetBool("isShielding", false);
                }
                else if(item.name.Contains("Shield"))
                {
                    isThereBow = false;
                    animator.SetBool("isThereBow", false);
                    animator.SetBool("isTorching", false);
                    isThereShield = true;
                    animator.SetBool("isThereShield", isThereShield);
                    animator.SetBool("isShielding", true);
                    Instantiate(item.itemObj, shieldPos);
                    shieldPos.GetChild(0).transform.position = shieldPos.position;
                    shieldPos.GetChild(0).transform.rotation = shieldPos.rotation;
                    
                }
                else if (item.name.Contains("Bow"))
                {
                    isThereBow = true;
                    animator.SetBool("isThereBow", true);
                    animator.SetBool("isTorching", false);
                    animator.SetBool("isShielding", false);
                    animator.SetBool("isThereShield", false);
                    if (animator.GetBool("isEquiped"))
                    {
                        bowWeapon.SetParent(bow_eqd_pos);
                        bowWeapon.position = bow_eqd_pos.position;
                        bowWeapon.rotation = bow_eqd_pos.rotation;
                        Instantiate(item.itemObj, bowWeapon);
                        bowWeapon.GetChild(0).transform.position = bowWeapon.position;
                        bowWeapon.GetChild(0).transform.rotation = bowWeapon.rotation;
                    }
                    else if(!animator.GetBool("isEquiped"))
                    {
                        bowWeapon.SetParent(bow_uneqd_pos);
                        bowWeapon.position = bow_uneqd_pos.position;
                        bowWeapon.rotation = bow_uneqd_pos.rotation;
                        Instantiate(item.itemObj, bowWeapon);
                        bowWeapon.GetChild(0).transform.position = bowWeapon.position;
                        bowWeapon.GetChild(0).transform.rotation = bowWeapon.rotation;
                    }
                }
               
                
                
            }
            

        }
    }


    private void FixedUpdate()
    {
       

    }

    //Animation functions
    void Jump()
    {
        if(!isFalling)
            fizik.AddForce(Vector3.up * 8, ForceMode.Impulse);
        
    }


    void isAttackingFalse()
    {
        animator.SetBool("isAttacking", false);
    }

    void isAttackingTrue()
    {
        animator.SetBool("isAttacking", true);
    }

    void FallingStart()
    {
        isFalling = true;
        col.direction = 3;
 
    }
    void FallingEnd()
    {
        isFalling = false;
        col.direction = 1;
    }

    void Equip()
    {
        if (!isFalling)
        {
            if (isAxe)
            {
                weapon.position = axe_eqd_pos.position;
                weapon.rotation = axe_eqd_pos.rotation;
                weapon.SetParent(axe_eqd_pos);
            }
            else if (isSword)
            {
                weapon.position = sword_eqd_pos.position;
                weapon.rotation = sword_eqd_pos.rotation;
                weapon.SetParent(sword_eqd_pos);
            }
           
            animator.SetBool("isEquiped", true);
            if (isThereShield)
            {
                animator.SetBool("isShielding", true);
            }

            if (animator.GetBool("isThereBow"))
            {
                bowWeapon.position = bow_eqd_pos.position;
                bowWeapon.rotation = bow_eqd_pos.rotation;
                bowWeapon.SetParent(bow_eqd_pos);
            }
        }
        
    }
    void UnEquip()
    {
        if (!isFalling)
        {
            if (isAxe)
            {
                weapon.position = axe_uneqd_pos.position;
                weapon.rotation = axe_uneqd_pos.rotation;
                weapon.SetParent(axe_uneqd_pos);
            }
            else if (isSword)
            {
                weapon.position = sword_uneqd_pos.position;
                weapon.rotation = sword_uneqd_pos.rotation;
                weapon.SetParent(sword_uneqd_pos);
            }
           
            animator.SetBool("isEquiped", false);
            animator.SetBool("isShielding", false);

            if (animator.GetBool("isThereBow"))
            {
                bowWeapon.position = bow_uneqd_pos.position;
                bowWeapon.rotation = bow_uneqd_pos.rotation;
                bowWeapon.SetParent(bow_uneqd_pos);
            }
        }
        
    }

    IEnumerator SResetTapTimes()
    {
        yield return new WaitForSeconds(SResetTimer);
        STapTimes = 0;
    }
    IEnumerator WResetTapTimes()
    {
        yield return new WaitForSeconds(WResetTimer);
        WTapTimes = 0;
    }
    IEnumerator AResetTapTimes()
    {
        yield return new WaitForSeconds(AResetTimer);
        ATapTimes = 0;
    }
    IEnumerator DResetTapTimes()
    {
        yield return new WaitForSeconds(DResetTimer);
        DTapTimes = 0;
    }

    void DodgeForward()
    {
        fizik.AddForce(new Vector3(0, 0, 15), ForceMode.Impulse);
    }
    void DodgeBackward()
    {
        fizik.AddForce(new Vector3(0, 0, -15), ForceMode.Impulse);
        
    }
    void DodgeLeft()
    {
        fizik.AddForce(new Vector3(-15, 0, 0), ForceMode.Impulse);
    }
    void DodgeRight()
    {
        fizik.AddForce(new Vector3(15, 0, 0), ForceMode.Impulse);
    }

    void TakeArrow()
    {

        foreach (Item item in Inventory.instance.inventoryItemList)
        {
            if (item.name.Contains("Arrow"))
            {
                Instantiate(item.itemObj, arrow_pos);
                arrow_pos.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                break;
            }
        }
    }
    void PullString()
    {
        
         bow_eqd_pos.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(7).transform.SetParent(string_pulling_pos);
        if (string_pulling_pos.childCount > 0)
            string_pulling_pos.transform.GetChild(0).transform.ResetLocal();


    }
    void RelaseString()
    {
       
       if(string_pulling_pos.childCount > 0)
            string_pulling_pos.transform.GetChild(0).SetParent(bow_eqd_pos.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0));
       bow_eqd_pos.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(7).transform.position = bow_eqd_pos.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform.position;
       bow_eqd_pos.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(7).transform.rotation = bow_eqd_pos.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform.rotation;
            
    }

    void FireArrow()
    {
        Debug.Log("girdi3");
        arrow_pos.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        arrow_pos.transform.GetChild(0).GetComponent<Rigidbody>().AddForce((hitCrosshair.point - arrow_pos.position).normalized * 150, ForceMode.VelocityChange);
        arrow_pos.transform.GetChild(0).GetComponent<CapsuleCollider>().isTrigger = false;
        arrow_pos.transform.GetChild(0).SetParent(arrow_fire_pos.parent);
        
    }
}
