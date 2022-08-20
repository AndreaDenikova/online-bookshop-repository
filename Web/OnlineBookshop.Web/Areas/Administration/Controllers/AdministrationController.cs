namespace OnlineBookshop.Web.Areas.Administration.Controllers
{
    using OnlineBookshop.Common;
    using OnlineBookshop.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
