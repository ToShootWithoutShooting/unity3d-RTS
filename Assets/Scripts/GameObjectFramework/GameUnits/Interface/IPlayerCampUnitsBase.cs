using GameEvent;
namespace Interface
{
    public interface IPlayerCampUnitsBase
    {
        void SetSelected(bool Selected);
        void ClickLeftMouseEvent(object sender, GameEventArgs e);
        void ClickRightMouseEvent(object sender, GameEventArgs e);

    }
}
