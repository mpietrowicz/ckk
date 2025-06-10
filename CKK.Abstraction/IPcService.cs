using System.Threading.Tasks;

namespace CKK.Abstraction
{
    public interface IPcService
    {
        Task HibernatePc();
        Task LockPc();
        Task LogOffPc();
        Task RestartPc();
        Task ShutdownPc();
        Task SleepPc();
    }
}