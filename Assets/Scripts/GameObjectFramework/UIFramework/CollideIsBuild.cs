using GameManager;
using UnityEngine;
using UnityEngine.UI;
using GameEvent;
using GameEnum;

public class CollideIsBuild : MonoBehaviour {

    public bool IsCanBuilding;
    //public CollisionDetectionType collisionDetectionType
    Image _image;
	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();
        EventManager.Instance.Subscribe(GameEventEnum.PlayerPressLeftMouseEvent, FireIsCanBuilding);
    }
	
	// Update is called once per frame
	void Update () {

        IsCanBuilding = GameObjectsManager.Instance.GetCollisionUnit(transform.position, 12, CollisionDetectionType.AllianceBuilding) == null ? true : false;
        if (IsCanBuilding)
        {
            _image.color = Color.green;
        }
        else
        {
            _image.color = Color.red;
        }
    }

    void FireIsCanBuilding(object sender,GameEventArgs e)
    {
        if (!IsCanBuilding)
        {
            GameMapManager.Instance.IsCanBuilding = false;
        }
    }


}
