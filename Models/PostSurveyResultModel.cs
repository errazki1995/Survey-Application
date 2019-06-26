using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EsisaSurvey.Models
{
    public class PostSurveyResultModel
    {
        [Key]
        public int id { get; set; }
        public string postId { get; set; }

        public string surveyResult { get; set; }


        public int clientid { get; set; }
        public Client Client { get; set; }


    }
}
