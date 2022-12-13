namespace Submissions.BoatRescue
{
    public class RewardBlock : LevelBlock, IHittable
    {
        public override BlockType Type => BlockType.Reward;

        public void OnHit()
        {
            GameManager.Instance.UpdateRewardScore();
            Destroy(gameObject);
        }
    }
}