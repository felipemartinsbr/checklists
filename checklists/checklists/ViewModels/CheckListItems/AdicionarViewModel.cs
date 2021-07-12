using System.Collections.Generic;
using checklists.ViewModels.CheckLists;

namespace checklists.ViewModels.CheckListItems
{
    public class AdicionarViewModel
    {
        public ICollection<CheckList> CheckLists { get; set; }
        public ICollection<CheckListItems> Items { get; set; }

        public string[] msgErros { get; set; }

        public AdicionarViewModel()
        {
            CheckLists = new List<CheckList>();
            Items = new List<CheckListItems>();
        }
    }
}