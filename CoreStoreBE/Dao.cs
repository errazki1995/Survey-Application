using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using EsisaSurvey.Models;
using System.Web.Mvc;
using EsisaSurvey.CoreStoreBE;
using System.Diagnostics;
using System.Web;
using System.Collections;

namespace EsisaSurvey.CoreStoreBE
{
    public class Dao
    {

        private ISession session;
        private EfCore _context;
        private int commercantId;
        public Dao(ISession session)
        {
            this.session = session;
            _context = new EfCore();

        }

        public Dao(ISession session, int commercantid)
        {
            this.session = session;
            this.commercantId = commercantid;
            _context = new EfCore();

        }

        public T GetFromSession<T>(string storageId, T defaultValue)
        {
            if (string.IsNullOrEmpty(session.GetString(storageId)))
            {
                session.SetString(storageId, JsonConvert.SerializeObject(defaultValue));
            }
            var value = session.GetString(storageId);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public Dictionary<string, string> GetAllSurveys()
        {
            Dictionary<string, string> surveys = new Dictionary<string, string>();
            surveys["Esisa_survey"] = EsisaAddSurvey.EsisaSurvey;

            try
            {
                var surveyList = _context.SurveyStore.ToList();

                if (surveyList != null)
                {
                    foreach (Survey s in surveyList)
                    {
                        surveys[s.Id] = s.Json;
                    }
                }
            }
            catch (Exception e)
            {
            }
            //  return GetFromSession<Dictionary<string, string>>("SurveyStorage", surveys);
            return surveys;
        }
        public Dictionary<string, string> GetSurveys()
        {
            Dictionary<string, string> surveys = new Dictionary<string, string>();

            surveys["Esisa_survey"] = EsisaAddSurvey.EsisaSurvey;

            try
            {
                var surveyList = _context.SurveyStore.Where(s => s.commercantId == commercantId).ToList();
                if (surveyList == null)
                {
                    return null;
                }
                else
                {
                    foreach (Survey s in surveyList)
                    {
                        surveys[s.Id] = s.Json;
                    }
                }

            }
            catch (Exception e)
            {
                return null;
            }
            // surveys["Esisa_survey"] = EsisaAddSurvey.EsisaSurvey;
            //  return GetFromSession<Dictionary<string, string>>("SurveyStorage", surveys);
            return surveys;
        }
        public Dictionary<string, List<string>> GetResults()
        {
            //var surveyAnswerslist = _context.SurveyResult.ToList();
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            // var 
            //find all replies with same idsurvey
            //select from all replies where same surveyid is there
            //foreach(PostSurveyResultModel survey in surveyAnswerslist)
            //{
            // results[survey.postId] = survey.surveyResult;
            //}
            results["Esisa_survey"] = new List<string>(new String[] { EsisaAddSurvey.EsisaAnswerSurvey1, EsisaAddSurvey.EsisaAnswerSurvey2 });
            return GetFromSession<Dictionary<string, List<string>>>("ResultsStorage", results);
        }


        public string GetSurvey(string surveyId)
        {
            
            
            return GetAllSurveys()[surveyId];
        }

        public void JsonGlobalStoreFile(string surveyId, string path, string jsonString)
        {
            string fileName = path + "\\" + surveyId;

            try
            {
                // if File Exists delete to update what the user would create [Not miss anything]
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (FileStream fs = File.Create(fileName))
                {
                    Byte[] json = new UTF8Encoding(true).GetBytes(jsonString);
                    fs.Write(json, 0, json.Length);

                }
                /*read back from file  [Rescue code lol]  
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    */
            }
            catch (Exception)
            {

            }
        }

        public void StoreSurvey(string surveyId, string jsonString, int commercantId)
        {
            try
            {
                var survey = _context.SurveyStore.SingleOrDefault(s => s.Id == surveyId);
                if (survey == null)
                {
                    Survey surveyCreate = new Survey();
                    surveyCreate.Id = surveyId;
                    surveyCreate.Json = jsonString;
                    surveyCreate.commercantId = commercantId;
                    _context.SurveyStore.Add(surveyCreate);
                    _context.SaveChanges();
                }

                if (survey != null)
                {
                    System.Diagnostics.Debug.Write("~~~~~~~~~~~~~SURVEYY NNNNNOT  NULLLLLL");
                    survey.Json = jsonString;
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write("~~~~~~~~~~~~~SURVEYY NULLLLLL");


            }


            var storage = GetAllSurveys();
            if (storage == null)
            {
                Dictionary<string, string> surveys = new Dictionary<string, string>();

                storage = surveys;
            }
           // storage[surveyId] = jsonString;



            //string path = @"C:\Users\ayoub\OneDrive\Desktop\ASP.NET\JsonDebugging\";

            //Store survey in Database with the Commercant Id
            //JsonGlobalStoreFile(surveyId, path, jsonString);

            session.SetString("SurveyStorage", JsonConvert.SerializeObject(storage));
        }

        public void ChangeName(string id, string name)
        {
            Debug.Write("THE CHANGE NAME ID :" + id);
            Debug.Write("THE CHANGE NAME NAME :" + name);
            var storage = GetAllSurveys();
            //if we are updating the survey name we should update the survey result name as well
            var findSurvey = _context.SurveyStore.SingleOrDefault(s => s.Id == id);
            var resultSurvey = _context.SurveyResulLog.Where(sr => sr.postId == id);
            if (resultSurvey != null)
            {
               foreach (SurveyResultLog s in resultSurvey)
                {
                    s.postId = name;
                }
            }
            findSurvey.Id = name;
            _context.SaveChanges();
            storage[name] = storage[id];
            storage.Remove(id);
            session.SetString("SurveyStorage", JsonConvert.SerializeObject(storage));
        }

        public void DeleteSurvey(string surveyId)
        {
            var storage = GetAllSurveys();
            storage.Remove(surveyId);


            var checkSurvey = _context.SurveyStore.Single(s => s.Id == surveyId);
            var checkResSurvey = _context.SurveyResulLog.Where(sr => sr.postId == surveyId);
            if (checkSurvey != null) _context.Remove(checkSurvey);
            else if (checkResSurvey != null) _context.Remove(checkResSurvey);
            _context.SaveChanges();
            session.SetString("SurveyStorage", JsonConvert.SerializeObject(storage));
        }


        public void PostResults(string postId, string resultJson, int clientid)
        {
            var storage = GetResults();
            SurveyResultLog survey = new SurveyResultLog();
            survey.postId = postId;
            survey.surveyanswerJson = resultJson;
            survey.clientId = clientid;
            _context.SurveyResulLog.Add(survey);
            _context.SaveChanges();
            if (!storage.ContainsKey(postId))
            {
                storage[postId] = new List<string>();
            }
            storage[postId].Add(resultJson);
            session.SetString("ResultsStorage", JsonConvert.SerializeObject(storage));
        }

        public List<string> GetResults(string postId)
        {
            //var storage = GetResults();
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            var storage = results;
            List<string> rep = new List<string>();
            var resultsDb = _context.SurveyResulLog.ToList().Where(p => p.postId == postId);
            try
            {
                if (resultsDb != null)
                {
                    foreach (SurveyResultLog survey in resultsDb)
                    {
                        // storage[survey.postId]=
                        rep.Add(survey.surveyanswerJson);
                        Debug.Write("YESS RESSSSULLLTTTT  " + survey.surveyanswerJson);
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch (Exception e)
            {
                return new List<string>();
            }
            storage[postId] = rep;
            return storage.ContainsKey(postId) ? storage[postId] : new List<string>();

            /*
             *
             * Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            var storage = results;
            var resultsDb = _context.SurveyResulLog.ToList().Where(p => p.postId == postId);
            if(resultsDb== null) return  
            
            return storage.ContainsKey(postId) ? storage[postId] : new List<string>();
             * 
             */
        }
    }
}
