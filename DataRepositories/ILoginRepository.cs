using dchv_api.Models;

namespace dchv_api.DataRepositories;

public interface ILoginRepository : IBaseRepository<Login>
{
    Login? LoginUser(Login entity);
}
