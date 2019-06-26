using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EsisaSurvey.Models
{
    public class Survey
    {
        [Key]
        public int surveyIdBE { get; set; }
        public string Id { get; set; }
        public string Json { get; set; }
        public string Text { get; set; }
        public int commercantId { get; set; }
        public Commercant Commercant { get; set; }
    }
}
