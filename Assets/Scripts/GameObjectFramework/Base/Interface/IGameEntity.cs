using GameEnum;

namespace Interface {
    /// <summary>
    /// 实体使用的接口
    /// </summary>
    public interface IGameEntity {
        void Init();
        void BindEvent();
        void UnBindEvent();
        void OnDispose();
    }
}
