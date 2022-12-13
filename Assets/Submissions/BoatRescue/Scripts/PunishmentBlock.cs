namespace Submissions.BoatRescue
{
    public class PunishmentBlock : LevelBlock, IHittable
    {
        public override BlockType Type => BlockType.Punishment;

        public void OnHit()
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}