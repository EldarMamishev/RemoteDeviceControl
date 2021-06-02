using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Field
{
    public class FieldModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<FieldPossibleValueModel> PossibleValues { get; set; }
    }
}
