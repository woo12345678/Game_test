using UnityEngine;
using UnityEngine.UI;

public class Enemyclose : MonoBehaviour
{
    public Transform target;      
    public float speed = 4f;
    public PlayerCont player;
    public float AttackTime;
    public float EnemyHP = 100f;
    public float maxHP = 100f;
    public Slider slider;

    void Start()
    {
        if (slider != null)
        {
            slider.maxValue = maxHP;
            slider.value = EnemyHP;
        }
    }


    void Update()
    {
        if (target == null) return;
        
        player = target.GetComponent<PlayerCont>();
        Vector3 t = target.position;
        t.y = transform.position.y; 

        transform.position = Vector3.MoveTowards(transform.position, t, speed * Time.deltaTime);
        
        
    }

    public void Enemy_Damages(float Damages)
    {
        EnemyHP -= Damages;
        slider.value = Mathf.Clamp(EnemyHP, 0f, maxHP);
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
            AttackTime += Time.deltaTime;

            if (AttackTime >= 1.4f)
            {
                player.Damages(30f);
                AttackTime = 0f;


            }
           
            

        }


    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            AttackTime = 0f;
          
        }
    }



}
