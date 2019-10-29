using UnityEngine;
using GameManager;
using GameEnum;
using GameEvent;
using GameManager.UI;

public class MainCamera : MonoBehaviour
{
    public float minFov = 85f;//最低距离
    public float maxFov = 265f;//最高距离
    public float minRot = 45;
    public float maxRot = 60;
    public float roate_Speed = 10f;//旋转速度 
    public float speed = 50;
    Vector3 position = new Vector3();
    private LayerMask mask;

    public delegate void ChangePos(float x, float y);

    public ChangePos changePos;

    void Start()
    {
        position = transform.position;
        EventManager.Instance.Subscribe(GameEventEnum.Click_MiniMap, ClickMiniMap);
        changePos += UIManager.Instance.mainUI.SetFramePos;

    }

    void ClickMiniMap(object sender, GameEventArgs e)
    {
        Vector3 pos = (Vector3)e.data;
        transform.position = new Vector3(3000 + pos.x, transform.position.y, 2570 + pos.z);

    }   
        
    void Update()
    {

        if (Input.mousePosition.y > Screen.height * 0.98)
        {
            if (transform.position.z >= 370)
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.y < Screen.height * 0.02)
        {
            if (transform.position.z <= 2570)
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x > Screen.width * 0.98)
        {
            if (transform.position.x >= 1200)
                transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.mousePosition.x < Screen.height * 0.02)
        {
            if (transform.position.x <= 3000)
                transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }

        if (transform.position.x > 3000)
            transform.position = new Vector3(3000,transform.position.y,transform.position.z);
        if(transform.position.x<1200)
            transform.position = new Vector3(1200, transform.position.y, transform.position.z);
        if (transform.position.z < 370)
            transform.position = new Vector3(transform.position.x, transform.position.y, 370);
        if (transform.position.z > 2570)
            transform.position = new Vector3(transform.position.x, transform.position.y, 2570);

        float fov = Input.GetAxis("Mouse ScrollWheel") * 10 * roate_Speed + transform.position.y;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        transform.position = new Vector3(transform.position.x, fov, transform.position.z);
        float rot = Input.GetAxis("Mouse ScrollWheel") * roate_Speed + transform.eulerAngles.x;
        rot = Mathf.Clamp(rot, minRot, maxRot);
        transform.eulerAngles = new Vector3(rot, transform.eulerAngles.y, transform.eulerAngles.z);

        float x = (3000 - transform.position.x) * 330 / 1800;
        float y = (transform.position.z - 370) * 330 / 2200;
        changePos(x, y);

    }
}

