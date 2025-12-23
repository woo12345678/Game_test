using UnityEngine;
using UnityEngine.UI;

public class PlayerCont : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Rigidbody rb;
    private Vector3 inputDir;

    public float HP = 100f;
    public float maxHP = 100f;
    public float DamaGesTime = 0f;

    public Slider slider;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public Transform firePoint;


    public bool isAuto;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        
        if (slider != null)
        {
            slider.maxValue = maxHP;
            slider.value = HP;
        }
    }

    void Update()
    {
        DamaGesTime += Time.deltaTime;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        inputDir = new Vector3(x, 0f, z).normalized;

        // 마우스 클릭하면 발사
        if (Input.GetMouseButtonDown(0))
        {

            if (!isAuto)
            {
                ShootToMouseGround();
            }
            else 
            {
                ShootNearestMonster();
            }
            

           
        }
    }

    void FixedUpdate()
    {
        Vector3 move = inputDir * moveSpeed;
        Vector3 velocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(move.x, velocity.y, move.z);
    }

    void ShootToMouseGround()
    {
        Camera cam = Camera.main;
        if (cam == null || bulletPrefab == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        
        float planeY = (firePoint != null) ? firePoint.position.y : transform.position.y;
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, planeY, 0f));

        if (!groundPlane.Raycast(ray, out float enter)) return;

        Vector3 hitPoint = ray.GetPoint(enter);

        Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position + Vector3.up * 1.0f;
        Vector3 dir = (hitPoint - spawnPos);
        dir.y = 0f; 
        if (dir.sqrMagnitude < 0.0001f) return;

        dir.Normalize();

        GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(dir));
        PayerBuillet bullet = b.GetComponent<PayerBuillet>();
        if (bullet != null)
        {
            bullet.SetDirection(dir);
            // bullet.speed = bulletSpeed; 
        }
    }

    public void Damages(float Damag)
    {
        HP -= Damag;

        if (slider != null)
            slider.value = Mathf.Clamp(HP, 0f, maxHP);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Damages(50f);
            Destroy(other.gameObject);
        }
    }



    void ShootNearestMonster()
    {
        GameObject target = FindNearestMonster();
        if (target == null) return;

        Vector3 firePos = firePoint != null ? firePoint.position : transform.position + Vector3.up * 1f;
        Vector3 dir = (target.transform.position - firePos);
        dir.y = 0f; 

        if (dir.sqrMagnitude < 0.01f) return;

        GameObject b = Instantiate(bulletPrefab, firePos, Quaternion.LookRotation(dir));
        PayerBuillet bullet = b.GetComponent<PayerBuillet>();
        if (bullet != null)
        {
            bullet.SetDirection(dir);
            // bullet.speed = bulletSpeed; 
        }
    }

    
    GameObject FindNearestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        if (monsters.Length == 0) return null;

        GameObject nearest = null;
        float minDist = float.MaxValue;

        Vector3 myPos = transform.position;

        foreach (GameObject m in monsters)
        {
            float dist = (m.transform.position - myPos).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = m;
            }
        }

        return nearest;
    }

}
