using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dchv_api.Controllers;
public class BaseController : ControllerBase
{

    protected ActionResult<uint> getLoginId()
    {
        string val;
        try {
            val = this.User.Claims.First(i => i.Type == ClaimTypes.Sid).Value;
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Unauthorized("User is not authorized");
        }
        return uint.Parse(val);
    }

    protected string getLoginUsername()
    {
        string val;
        try {
            val = this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
        } catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return val;
    }

}
