using System.Collections.Generic;


namespace checklists.ViewModels.CheckLists
{
    public class IndexViewModel
    {
        public ICollection<CheckList> CheckLists { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemErro { get; set; }

        public IndexViewModel()
        {
            CheckLists = new List<CheckList>();
        }
    }

    public class CheckList
    {
        public string Id { get; set; }
        public string Nome { get; set; }
    }
}