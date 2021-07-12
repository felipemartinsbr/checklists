using System;
using System.Collections.Generic;
using checklists.Models.CheckLists;
using checklists.RequestModels.CheckLists;
using checklists.ViewModels.CheckLists;
using Microsoft.AspNetCore.Mvc;

namespace checklists.Controllers
{
    public class CheckListsController : Controller
    {
        private readonly CheckListsService _checkListsService;

        public CheckListsController(CheckListsService checkListsService)
        {
            _checkListsService = checkListsService;
        }


        public IActionResult Index()
        {
            // Criar um ViewModel para conter a lista de dados do CheckList a serem exibidos aos usuários
            var viewModel = new IndexViewModel()
            {
                MensagemSucesso = (string) TempData["formMensagemSucesso"],
                MensagemErro = (string) TempData["formMensagemErro"]
            };
            
            // Obter lista de entidade do tipo CheckList
            var listaDeCheckLists = _checkListsService.ObterTodos();
            
            //Processar a lista de entidade para coletar para a ViewModel apenas informações necessárias
            foreach  (CheckListsEntity checkListsEntity in listaDeCheckLists)
            {
                viewModel.CheckLists.Add(new CheckList()
                {
                    Id = checkListsEntity.Id.ToString(),
                    Nome = checkListsEntity.Nome,
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
                _checkListsService.Adicionar(requestModel);
                TempData["formMsgSucesso"] = "CheckList adicionada com sucesso!";

                return RedirectToAction("Index");
            }catch (Exception e)
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
                var entidadeAEditar = _checkListsService.ObterPorId(param);
                var viewModel = new EditarViewModel()
                {
                    msgErros = (string[]) TempData["formMsgErros"],
                    Id = entidadeAEditar.Id.ToString(),
                    Nome = entidadeAEditar.Nome
                };
                return View(viewModel);
                
            }catch (Exception e)
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
                TempData["formMsgErros"] = listaErros;
                return RedirectToAction("Editar");
            }

            try
            {
                _checkListsService.Editar(param, requestModel);
                TempData["formMsgSucesso"] = "CheckList editado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Remover(int param)
        {
            try
            {
                var entidadeARemover = _checkListsService.ObterPorId(param);
                var viewModel = new RemoverViewModel()
                {
                    msgErros = (string[]) TempData["formMsgErros"],
                    Id = entidadeARemover.Id.ToString(),
                    Nome = entidadeARemover.Nome
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
                _checkListsService.Remover(param);
                TempData["formMsgSucesso"] = "CheckList enxcluído com sucesso!";

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