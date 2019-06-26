using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsisaSurvey.CoreStoreBE
{
    public class EsisaAddSurvey
    {
        public const string EsisaSurvey = "{ \"pages\": [  {   \"name\": \"page1\",   \"elements\": [    {     \"type\": \"text\",     \"name\": \"Q1\",     \"title\": \"Can you tell me more about Essia school?\"    },    {     \"type\": \"checkbox\",     \"name\": \"What is esisa More about?\",     \"choices\": [      {       \"value\": \"item1\",       \"text\": \"iT in general\"      },      {       \"value\": \"item2\",       \"text\": \"Cybersecurity\"      },      {       \"value\": \"item3\",       \"text\": \"Software engineering\"      }     ]    },    {     \"type\": \"rating\",     \"name\": \"question1\",     \"title\": \"How would you rate esisa on a scale to 5?\"    }   ]  } ]}";

        public const string EsisaAnswerSurvey1 = "{    \"Q1\": \"Nice school\",    \"What is esisa More about?\": [        \"item1\",        \"item3\"    ],    \"question1\": 4}";
        public const string EsisaAnswerSurvey2 = "{    \"Q1\": \"Hello Esisa How are you? \",    \"What is esisa More about?\": [        \"item1\",        \"item3\"    ],    \"question1\": 4}";


    }
}
