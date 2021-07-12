using System;
using System.Collections.Generic;
using System.Linq;
using checklists.Data;
using checklists.Models.CheckListItems;

namespace checklists.Models.CheckLists
{
    public class CheckListsService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly CheckListItemsService _checkListItemsService;

        public CheckListsService(DatabaseContext databaseContext, CheckListItemsService checkListItemsService)
        {
            _databaseContext = databaseContext;
            _checkListItemsService = checkListItemsService;
        }

        public int ObterQntdCheckList()
        {
            return _databaseContext.CheckList.Count();
        }
        public ICollection<CheckListsEntity> ObterTodos()
        {
            return _databaseContext.CheckList
                .ToList();
        }
        
        public CheckListsEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.CheckList.Find(id);
            }
            catch
            {
                throw new Exception("CheckList de Id #" + id + " não encontrado");
            }
        }
        
        public CheckListsEntity Adicionar(IdadosBasicosCheckListsModel dadosBasicos)
        {
            var novaEntidade = ValidarDadosBasicos(dadosBasicos);
            _databaseContext.CheckList.Add(novaEntidade);
            _databaseContext.SaveChanges();

            return novaEntidade;
        }

        public CheckListsEntity Editar(int id, IdadosBasicosCheckListsModel dadosBasicos)
        {
            var entidadeAEditar = ObterPorId(id);
            entidadeAEditar = ValidarDadosBasicos(dadosBasicos, entidadeAEditar);
            _databaseContext.SaveChanges();

            return entidadeAEditar;
        }
        
        public bool Remover(int id)
        {
            var entidadeARemover = ObterPorId(id);
            _databaseContext.CheckList.Remove(entidadeARemover);
            _databaseContext.SaveChanges();

            var listaItens = _checkListItemsService.ObterListaItemsPorId(id);
            foreach (CheckListItemsEntity checkListItemsEntity in listaItens)
            {
                _checkListItemsService.Remover(checkListItemsEntity.Id);
            }
            return true;
        }

        private CheckListsEntity ValidarDadosBasicos(IdadosBasicosCheckListsModel dadosBasicos,
            CheckListsEntity entidadeExistente = null)
        {
            var entidade = entidadeExistente ?? new CheckListsEntity();

            if (dadosBasicos.Nome == null)
            {
                throw new Exception("O nome é obrigatório");
            }

            entidade.Nome = dadosBasicos.Nome;
            
            return entidade;
        }
        
        public interface IdadosBasicosCheckListsModel
        {
            public string Id { get; set; }
            public string Nome { get; set; }
        }
    }
}