using System;
namespace i4prj.SmartCab.Interfaces
{
    public interface ISessionService
    {
        void Update(string token, ICustomer customer);
        void Clear();
    }
}
