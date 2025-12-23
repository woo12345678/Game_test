using UnityEngine;

public class Enemyclose : MonoBehaviour
{
    public Transform target;      // 플레이어
    public float speed = 4f;
    public PlayerCont player;
    public float AttackTime;
    public float EnemyHP = 100f;



    void Update()
    {
        if (target == null) return;
        
        player = target.GetComponent<PlayerCont>();
        Vector3 t = target.position;
        t.y = transform.position.y; 

        transform.position = Vector3.MoveTowards(transform.position, t, speed * Time.deltaTime);
        //여기서 초 세고 
        AttackTime += Time.deltaTime;
    }

    public void Enemy_Damages(float Damages)
    {
        EnemyHP -= Damages;
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Damages(30f);

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

    public void OnCollisionStay(Collision collision) //계속 붙어있을때 데미지 주기위함
    {
        if (collision.gameObject.tag == "Player")
        {

            if(AttackTime >= 1f)
            {
                player.Damages(30f);
                AttackTime = 0f;


            }
           
            

        }


    }

   
}
