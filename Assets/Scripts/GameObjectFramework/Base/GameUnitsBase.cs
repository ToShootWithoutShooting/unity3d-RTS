using GameEvent;
using GameManager;
using Interface;
using UnityEngine;
using GameEnum;
using GameManager.UI;
using System.Collections;

namespace RTS
{
    /// <summary>
    /// 游戏阵营单位基类
    /// </summary>
    public abstract partial class GameUnitsBase : GameEntityBase, IGameUnitDataFun
    {
        protected float Object_Radius;   //物体半径
        protected CampEnum Object_Camp; //游戏单位阵营
        protected UnitTypeEnum Object_UnitType;//游戏对象单位类型
        public float Object_HP;//单位生命值
        protected float Max_HP; //满血生命值
        public bool IsPlayerCamp;   //是否是玩家阵营
        public bool Is_AI;   //是否是AI
        protected TrollDrawLine trollDrawLine;
        protected HighlightableObject highlightable;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; SetSelectEffect(); } } //是否被选中
        public bool isSelected = false;

        protected override void Init()
        {
            base.Init();
            if (null != GetComponent<TrollDrawLine>())
                trollDrawLine = GetComponent<TrollDrawLine>();
            GameMapManager.UnitData UnitData = GameMapManager.Instance.GetUnitData(gameObject.tag);
            if (UnitData.object_UnitName != "")
            {
                Object_UnitName = EnumChange<GameUnitName>.StringToEnum(UnitData.object_UnitName);
                Object_Camp = EnumChange<CampEnum>.StringToEnum(UnitData.object_Camp);
                Object_UnitType = EnumChange<UnitTypeEnum>.StringToEnum(UnitData.object_UnitType);
                Object_HP = UnitData.object_HP;
                Max_HP = UnitData.object_HP;
                Object_Radius = UnitData.unit_Radius;
                if (Object_Camp == GameMapManager.Instance.PlayerCamp)
                    IsPlayerCamp = true;
                else
                    IsPlayerCamp = false;
            }
            if (IsPlayerCamp)
            {
                gameObject.AddComponent<HighlightableObject>();
                highlightable = GetComponent<HighlightableObject>();
            }
            else
            {
                StartCoroutine(StartAI());
            }
            Is_AI = !IsPlayerCamp;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Object_HP = Max_HP;
            IsSelected = false;
        }

        public virtual void SetGameUnitHP(float Damage)
        {
            Object_HP -= Damage;
            if (Object_HP <= 0)
                gameObject.SetActive(false);
        }

        public void AddToGameObjectManager()
        {
            GameObjectsManager.Instance.AddGameObject(gameObject, Object_Radius);
        }
        public void RemoveToGameObjectManager()
        {
            GameObjectsManager.Instance.RemoveGameObject(gameObject);
        }
        public CampEnum GetUnitCamp()
        {
            return Object_Camp;
        }
        public UnitTypeEnum GetGameUnitType()
        {
            return Object_UnitType;
        }
        public float GetUnitRaduis()
        {
            return Object_Radius;
        }

        public bool GetIsPlayerCamp()
        {
            return IsPlayerCamp;
        }
        protected override void OnDispose()
        {
            base.OnDispose();
            StopAllCoroutines();
            RemoveToGameObjectManager();
        }

        #region AI
        protected IEnumerator StartAI()
        {
            yield return new WaitForSeconds(0.1f);
            InitAI();
        }

        protected virtual void InitAI()
        {

        }
        #endregion
    }
}
