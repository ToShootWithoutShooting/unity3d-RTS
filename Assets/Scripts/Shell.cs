using Interface;
using UnityEngine;
public class Shell : MonoBehaviour
{
    public float speed;
    private float damage;//炮弹的伤害
    private GameObject aim;
    public string shell_Type;
    private string Explosion;

    public void Init(GameObject aim, float damage)
    {
        this.aim = aim;
        this.damage = damage;
        if (shell_Type == "shell")
            Explosion = "ShellExplosion";
        else
            Explosion = "BulletHit";
    }


    void Update()
    {
        if (aim != null && aim.activeSelf)
        {
            transform.LookAt(aim.transform.position);
            transform.Translate(Vector3.forward * speed);//炮弹向前发射
            if (Vector3.Distance(transform.position, aim.transform.position) <= 10)
            {
                Vector3 position = aim.transform.position;
                position.y += 10;
                position.z += 2;
                GameObject explosion = PoolManager.Instance.GetInstance(Explosion, position, transform.rotation);
                gameObject.SetActive(false);
                aim.GetComponent<IGameUnitDataFun>().SetGameUnitHP(damage);
            }
        }
        else
            gameObject.SetActive(false);
        
    }


}
