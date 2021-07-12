using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using checklists.Models;
using checklists.Models.CheckListItems;
using checklists.Models.CheckLists;
using checklists.ViewModels.Home;

namespace checklists.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CheckListsService _checkListsService;
        private readonly CheckListItemsService _checkListItemsService;

        public HomeController(ILogger<HomeController> logger, CheckListsService checkListsService, CheckListItemsService checkListItemsService)
        {
            _logger = logger;
            _checkListsService = checkListsService;
            _checkListItemsService = checkListItemsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                ObterItemsRealizados = _checkListItemsService.ObterItemsRealizados(),
                ObterItemsNaoRealizados = _checkListItemsService.ObterItemsNaoRealizados(),
                ObterQntdItems = _checkListItemsService.ObterQntdItems(),
                ObterQntdCheckList = _checkListsService.ObterQntdCheckList()
            };
            
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}