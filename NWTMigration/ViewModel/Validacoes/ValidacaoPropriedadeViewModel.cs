using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NWTMigration.ViewModel.Validacoes
{
    //[PropertyChanged.ImplementPropertyChanged] //comentei pq já fizemos a mão o PropertyChanged

    public abstract class ValidacaoPropriedadeViewModel : IDataErrorInfo
    {
        // check for general model error    
        public string Error { get { return null; } }

        // check for property errors    
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }
        public bool ValidarCampos()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            if (!Validator.TryValidateObject(this, context, results, true))
            {
               var erros = results.Select(x => $"{x.MemberNames.First()}: {x.ErrorMessage}").ToList();
                MessageBox.Show($"{string.Join("\n", erros)}", "Erro de validação");

                return false;
            }

            return true;
        }
    }
}
