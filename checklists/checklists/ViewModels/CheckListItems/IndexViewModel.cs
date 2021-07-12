using System.Collections.Generic;

namespace checklists.ViewModels.CheckListItems
{
    public class IndexViewModel
    {
        public ICollection<CheckListItems> CheckListItems { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemErro { get; set; }

        public IndexViewModel()
        {
            CheckListItems = new List<CheckListItems>();
        }
    }

    public class CheckListItems
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Realizado { get; set; }
        public string DataRealizado { get; set; }
        public string Subitem { get; set; }
    }
}