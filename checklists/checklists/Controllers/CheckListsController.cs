using checklists.Models.CheckLists;
using Microsoft.AspNetCore.Mvc;

namespace checklists.Controllers
{
    public class CheckListsController : Controller
    {
        private readonly CheckListsService _checkListsService;
    }
}