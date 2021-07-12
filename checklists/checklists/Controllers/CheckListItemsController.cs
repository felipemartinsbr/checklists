using System;
using System.Collections.Generic;
using checklists.Models.CheckListItems;
using checklists.Models.CheckLists;
using checklists.RequestModels.CheckListItems;
using checklists.ViewModels.CheckListItems;
using checklists.ViewModels.CheckLists;
using Microsoft.AspNetCore.Mvc;
using AdicionarViewModel = checklists.ViewModels.CheckListItems.AdicionarViewModel;
using EditarRequestModel = checklists.RequestModels.CheckListItems.EditarRequestModel;
using EditarViewModel = checklists.ViewModels.CheckListItems.EditarViewModel;
using IndexViewModel = checklists.ViewModels.CheckListItems.IndexViewModel;
using RemoverViewModel = checklists.ViewModels.CheckListItems.RemoverViewModel;

namespace checklists.Controllers
{
    public class CheckListItemsController : Controller
    {
        private readonly CheckListItemsService _checkListItemsService;
        private readonly CheckListsService _checkListsService;

        public CheckListItemsController(CheckListItemsService checkListItemsService,
            CheckListsService checkListsService)
        {
            _checkListItemsService = checkListItemsService;
            _checkListsService = checkListsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                MensagemSucesso = (string) TempData["formMensagemSucesso"],
                MensagemErro = (string) TempData["formMensagemErro"]
            };

            var listaDeCheckListItems = _checkListItemsService.ObterTodos();

            foreach (CheckListItemsEntity checkListItemsEntity in listaDeCheckListItems)
            {
                viewModel.CheckListItems.Add(new CheckListItems()
                {
                    Id = checkListItemsEntity.Id.ToString(),
                    Subitem = checkListItemsEntity.CheckListItemId.ToString(),
                    Titulo = checkListItemsEntity.Titulo,
                    Realizado = checkListItemsEntity.Realizado,
                    DataRealizado = checkListItemsEntity.DataRealizacao.ToString("dd/MM/yyyy")
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            var viewModel = new AdicionarViewModel()
            {
                msgErros = (string[]) TempData["formMsgErros"]
            };

            var listaItems = _checkListItemsService.ObterTodos();
            foreach (CheckListItemsEntity checkListItemsEntity in listaItems)
            {
                viewModel.Items.Add(new CheckListItems()
                {
                    Id = checkListItemsEntity.Id.ToString(),
                    Titulo = checkListItemsEntity.Titulo
                });
            }

            var listaCheckLists = _checkListsService.ObterTodos();
            foreach (CheckListsEntity checkListsEntity in listaCheckLists)
            {
                viewModel.CheckLists.Add(new CheckList()
                {
                    Id = checkListsEntity.Id.ToString(),
                    Nome = checkListsEntity.Nome
                });
            }

            return View(viewModel);
        }

        [HttpPost]
        public RedirectToActionResult Adicionar(AdicionarRequestModel requestModel)
        {
            var listaErros = requestModel.ValidarEFiltrar();

            if (listaErros.Count > 0)
            {
                TempData["formMsgErro"] = listaErros;
                return RedirectToAction("Adicionar");
            }

            try
            {
                _checkListItemsService.Adicionar(requestModel);
                TempData["formMsgSucesso"] = "Item adicionado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["formMsgErros"] = new List<string> {e.Message};
                return RedirectToAction("Adicionar");
            }
        }

        [HttpGet]
        public IActionResult Editar(int param)
        {
            try
            {
                var entidadeAEditar = _checkListItemsService.ObterPorId(param);
                var viewModel = new EditarViewModel()
                {
                    msgErros = (string[]) TempData["formMsgErros"],
                    Titulo = entidadeAEditar.Titulo,
                    Realizado = entidadeAEditar.Realizado,
                    DataRealizacao = entidadeAEditar.DataRealizacao.ToString("dd/MM/yyyy"),
                    CheckListId = entidadeAEditar.CheckListId.ToString(),
                    CheckListItemId = entidadeAEditar.CheckListItemId.ToString()
                    
                };

                var listaItems = _checkListItemsService.ObterTodos();
                foreach (CheckListItemsEntity checkListItemsEntity in listaItems)
                {
                    viewModel.CheckListItem.Add(new CheckListItems()
                    {
                        Id = checkListItemsEntity.Id.ToString(),
                        Titulo = checkListItemsEntity.Titulo,
                        
                    });
                }

                var listaCheckLists = _checkListsService.ObterTodos();
                foreach (CheckListsEntity checkListsEntity in listaCheckLists)
                {
                    viewModel.CheckList.Add(new CheckList()
                    {
                        Id = checkListsEntity.Id.ToString(),
                        Nome = checkListsEntity.Nome
                    });
                }

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["formMsgErros"] = new List<string> {e.Message};
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Editar(int param, EditarRequestModel requestModel)
        {
            var listaErros = requestModel.ValidarEFiltrar();
            if (listaErros.Count > 0)
            {
                TempData["formMsgErro"] = listaErros;
                return RedirectToAction("Editar");
            }

            try
            {
                _checkListItemsService.Editar(param, requestModel);
                TempData["formMsgSucesso"] = "Item editado com sucesso!";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["formMsgErros"] = new List<string> {e.Message};
                return RedirectToAction("Editar");
            }
        }

        [HttpGet]
        public IActionResult Remover(int param)
        {
            try
            {
                var entidadeARemover = _checkListItemsService.ObterPorId(param);
                var viewModel = new RemoverViewModel()
                {
                    msgErros = (string[]) TempData["formMsgErros"],
                    Titulo = entidadeARemover.Titulo,
                    Realizado = entidadeARemover.Realizado,
                    DataRealizacao = entidadeARemover.DataRealizacao.ToString("dd/MM/yyyy"),
                    CheckListId = entidadeARemover.CheckListId.ToString(),
                    CheckListItemId = entidadeARemover.CheckListItemId.ToString()
                };
                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["formMsgErros"] = new List<string> {e.Message};
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Remover(int param, object requestModel)
        {
            try
            {
                _checkListItemsService.Remover(param);
                TempData["formMsgSucesso"] = "Item removido com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["formMsgErros"] = new List<string> {e.Message};
                return RedirectToAction("Remover");
            }
        }
        
    }
}