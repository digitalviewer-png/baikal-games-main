namespace DoodleJump
{
    public interface IRandomSpawnable
    {
        float GetNormalizedSpawnWeight(float currentProgress);
    }
}