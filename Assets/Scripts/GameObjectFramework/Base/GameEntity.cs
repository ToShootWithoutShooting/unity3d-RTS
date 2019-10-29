using UnityEngine;
using GameEnum;

namespace RTS
{
    /// <summary>
    /// 游戏实体类，所有具体游戏对象都派生此类
    /// </summary>
    public abstract class GameEntityBase : MonoBehaviour
    {
        public virtual GameUnitName Object_UnitName { get; protected set; }  //游戏对象名字

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            
        }
        void OnEnable()
        {
            OnActivate();
            BindEvent();
        }
        protected virtual void OnActivate()
        {

        }
        protected virtual void BindEvent()
        {

        }
        protected virtual void UnBindEvent()
        {

        }
        private void OnDisable()
        {
            OnDispose();
        }
        protected virtual void OnDispose()
        {
            StopAllCoroutines();
            UnBindEvent();
        }
        
    }
}
