using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_csharp.Models
{
    internal class ValidaCampoViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidaCampoViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}