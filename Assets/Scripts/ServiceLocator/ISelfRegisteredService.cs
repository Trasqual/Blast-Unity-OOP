namespace BlastGame.ServiceManagement
{
    public interface ISelfRegisteredService : IService
    {
        public void RegisterSelf();
    }
}
