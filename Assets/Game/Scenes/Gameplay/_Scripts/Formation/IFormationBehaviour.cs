namespace Game
{
    public interface IFormationBehaviour
    {
        void Update(Formation thisFormation, Formation enemyFormation, int entityRow, int entityCol);
    }
}
