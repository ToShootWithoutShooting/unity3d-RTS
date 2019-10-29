using UnityEngine;

namespace GameManager {
    /// <summary>
    /// 单例抽象类
    /// </summary>
    public class Singleton<T> where T :  class, new() {

        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        protected Singleton()
        {
            if (null != _instance)
            {
                Debug.Log("This Singleton Instance is null");
                return;
            }
            Init();
        }

        ~Singleton()
        {
            UnBindEvent();
            OnDispose();
        }

        public virtual void Init()
        {
            BindEvent();
        }

        protected virtual void BindEvent()
        {

        }

        protected virtual void UnBindEvent()
        {

        }

        protected virtual void OnDispose()
        {

        }


    }
}