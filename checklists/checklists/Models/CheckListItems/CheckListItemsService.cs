using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using checklists.Data;
using checklists.Models.CheckLists;
using checklists.RequestModels.CheckLists;
using Microsoft.EntityFrameworkCore;

namespace checklists.Models.CheckListItems
{
    public class CheckListItemsService
    {
        private readonly DatabaseContext _databaseContext;
        

        public CheckListItemsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            
        }

        public int ObterQntdItems()
        {
            return _databaseContext.CheckListItem.Count();
        }

        public ICollection<CheckListItemsEntity> ObterListaItemsPorId(int id)
        {
            return _databaseContext.CheckListItem.Where(cl => cl.CheckListId == id).ToList();
        }
        public int ObterItemsRealizados()
        {
            return _databaseContext.CheckListItem.Count(cl => cl.Realizado == "Sim");
        }

        public int ObterItemsNaoRealizados()
        {
            return _databaseContext.CheckListItem.Count(cl => cl.Realizado == "Não");
        }
        public ICollection<CheckListItemsEntity> ObterTodos()
        {
            return _databaseContext.CheckListItem
                .ToList();
            
        }
        
        public CheckListItemsEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.CheckListItem.Find(id);
            }
            catch
            {
                throw new Exception("Item de Id #" + id + " não encontrado");
            }
        }

        public CheckListItemsEntity Adicionar(IdadosBasicosCheckListItemsModel dadosBasicos)
        {
            var novaEntidade = ValidarDadosBasicos(dadosBasicos);
            _databaseContext.CheckListItem.Add(novaEntidade);
            _databaseContext.SaveChanges();

            return novaEntidade;
        }
        
        public CheckListItemsEntity Editar(int id, IdadosBasicosCheckListItemsModel dadosBasicos)
        {
            var entidadeAEditar = ObterPorId(id);
            entidadeAEditar = ValidarDadosBasicos(dadosBasicos, entidadeAEditar);
            _databaseContext.SaveChanges();

            return entidadeAEditar;
        }
        public bool Remover(int id)
        {
            var entidadeARemover = ObterPorId(id);
            _databaseContext.CheckListItem.Remove(entidadeARemover);
            _databaseContext.SaveChanges();

            return true;
        }

        private CheckListItemsEntity ValidarDadosBasicos(IdadosBasicosCheckListItemsModel dadosBasicos,
            CheckListItemsEntity entidadeExistente = null)
        {
            var entidade = entidadeExistente ?? new CheckListItemsEntity();

            if (dadosBasicos.Titulo == null)
            {
                throw new Exception("Título é obrigatório");
            }

            entidade.Titulo = dadosBasicos.Titulo;

            if (dadosBasicos.DataRealizacao == null)
            {
                throw new Exception("Data de realização é obrigatória");
            }

            try
            {
                var data = DateTime.Parse(dadosBasicos.DataRealizacao);
                entidade.DataRealizacao = data;
            }
            catch
            {
                throw new Exception("A data informada não possui um formato válido");
            }


            entidade.Realizado = dadosBasicos.Realizado;
            entidade.CheckListId = Int32.Parse(dadosBasicos.CheckListId);
            entidade.CheckListItemId = Int32.Parse(dadosBasicos.CheckListItemId);
            
            return entidade;
        }
    }
        
        public interface IdadosBasicosCheckListItemsModel
        {
            public string CheckListId { get; set; }
            public string CheckListItemId { get; set; }
            public string Titulo { get; set; }
            public string Realizado { get; set; }
            public string DataRealizacao { get; set; }
            
            
        }
    }
