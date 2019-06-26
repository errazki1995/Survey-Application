using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace EsisaSurvey.Models
{
    public class ChangeSurveyModel
    {
        [Key]
        public string Id { get; set; }
        public string Json { get; set; }
        public string Text { get; set; }
        public int commercantId { get; set; }
        public Commercant Commercant { get; set; }

    }
}
