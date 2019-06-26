using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EsisaSurvey.Models
{
    public class SurveyResultLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string postId { get; set; }

        public string surveyanswerJson { get; set; }
        public int clientId { get; set; }
    }
}
