namespace CodeBase.Services.Unity
{
    public interface ITimeCounter
    {
        void StartCountingTime();
        float GetCurrentTimeDifference();
    }
}