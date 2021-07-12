using System.Collections;
using System.Collections.Generic;
using checklists.Models.CheckListItems;

namespace checklists.RequestModels.CheckListItems
{
    public class AdicionarRequestModel : IdadosBasicosCheckListItemsModel
    {
        public string id { get; set; }
        public string CheckListId { get; set; }
        public string CheckListItemId { get; set; }
        public string Titulo { get; set; }
        public string Realizado { get; set; }
        public string DataRealizacao { get; set; }
        public string CheckList { get; set; }

        public ICollection ValidarEFiltrar()
        {
            var listaErros = new List<string>();
            return listaErros;
        }
    }
}