using System;
using checklists.Models.CheckLists;

namespace checklists.Models.CheckListItems
{
    public class CheckListItemsEntity
    {
        public int Id { get; set; }
        public int CheckListId { get; set; }
        public int CheckListItemId { get; set; }
        public string Titulo { get; set; }
        public string Realizado  { get; set; }
        public DateTime DataRealizacao { get; set; }

        public CheckListItemsEntity()
        {
        }

        public CheckListItemsEntity(int id, int checkListId, int checkListItemId, string titulo, string realizado, DateTime dataRealizacao)
        {
            Id = id;
            CheckListId = checkListId;
            CheckListItemId = checkListItemId;
            Titulo = titulo;
            Realizado = realizado;
            DataRealizacao = dataRealizacao;
        }
    }
}