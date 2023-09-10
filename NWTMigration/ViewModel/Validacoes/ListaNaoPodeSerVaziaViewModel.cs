using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWTMigration.ViewModel.Validacoes
{
    public class ListaNaoPodeSerVaziaAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is IEnumerable lista)
            {
                return lista.GetEnumerator().MoveNext();
            }

            return false;
        }
    }
}