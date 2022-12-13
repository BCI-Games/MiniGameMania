using UnityEngine;

namespace Submissions.BoatRescue
{
    public class NavigationSpoButton : SpoButton
    {
        [SerializeField] private PlayerMoveDirection _direction;

        protected override void Register()
        {
            includeMe = true;
        }

        protected override void Unregister()
        {
            if (InputManager.Instance == null)
            {
                return;
            }

            includeMe = false;
        }

        public override void OnSelection()
        {
            GameManager.Instance.MovePlayers(_direction);
            base.OnSelection();
        }
    }
}