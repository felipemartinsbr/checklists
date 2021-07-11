using System;

namespace checklists.Models.CheckListItems
{
    public class CheckListItemsEntity
    {
        public int Id { get; set; }
        public int CheckListId { get; set; }
        public int CheckListItemId { get; set; }
        public string Titulo { get; set; }
        public bool Realizado  { get; set; }
        public DateTime DataRealizacao { get; set; }
    }
}