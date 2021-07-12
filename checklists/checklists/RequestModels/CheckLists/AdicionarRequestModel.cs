using System.Collections;
using System.Collections.Generic;
using checklists.Models.CheckLists;

namespace checklists.RequestModels.CheckLists
{
    public class AdicionarRequestModel : CheckListsService.IdadosBasicosCheckListsModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public ICollection ValidarEFiltrar()
        {
            var listaErros = new List<string>();
            return listaErros;
        }
    }
}