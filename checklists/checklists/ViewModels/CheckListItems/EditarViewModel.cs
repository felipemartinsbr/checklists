using System.Collections.Generic;
using checklists.Models.CheckListItems;
using checklists.ViewModels.CheckLists;

namespace checklists.ViewModels.CheckListItems
{
    public class EditarViewModel : IdadosBasicosCheckListItemsModel
    {
        public string Id { get; set; }
        public string CheckListId { get; set; }
        public string CheckListItemId { get; set; }
        public string Titulo { get; set; }
        public string Realizado { get; set; }
        public string DataRealizacao { get; set; }

        public string[] msgErros { get; set; }

        public ICollection<CheckList> CheckList { get; set; }
        public ICollection<CheckListItems> CheckListItem { get; set; }

        public EditarViewModel()
        {
            CheckList = new List<CheckList>();
            CheckListItem = new List<CheckListItems>();
        }
    }
}