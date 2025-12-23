using System.Security.Cryptography;
using UnityEngine;

public class Enemylong : MonoBehaviour
{
    public Transform target;
    public float ShootTime = 1.6f;
    public float currShootTime = 0f;
    public GameObject bullet;
    public Transform spawnPos;
    public float EnemyHP = 100f;






    void Update()
    {

        currShootTime += Time.deltaTime;
        if(currShootTime >= ShootTime)
        {
            Shoot();
            currShootTime = 0f;

        }

    }

    public void Shoot()
    {
        GameObject b = Instantiate(bullet, spawnPos.position, spawnPos.rotation);

        // 타깃을 향하게 회전
        if (target != null)
            b.transform.LookAt(target.position);
    }


    public void Enemy_Damages(float Damages)
    {
        EnemyHP -= Damages;
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PBullet")
        {

            Enemy_Damages(50f);
            Destroy(other.gameObject);

        }
    }
}
