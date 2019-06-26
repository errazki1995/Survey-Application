//debugging purposes
var allres = '';
var G;
var B;
var dataArray = [];
var choicesArray = [];
var title = [];
var colorsdb = ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850","#eb7b29", "#f368e0", "#ee5253", "#222f3e", "#d0524e", "#1bf1ab", "#620f2c", "#8e5ea2", "#3e95cd", "#c45850"]


var surveyId = '';
function addQuestionIntoResults(q, results, namePrefix) {
    var prefix = !!namePrefix ? namePrefix + questionPrefix : "";
    results.push({
        surveyId: surveyId,
        question: prefix + q.name,
        answer: q.value,
        questionType: q.getType()
    });
}


function getQuestions(questions) {
    /*
     var Qobject ={
                   surveyid:surveyId,
                  name:"",
                   title:"",
                   choices: [],
                  aType: ""
                  };
      */

    var Qchoices = [];
    var Qobject = [];

    for (var i = 0; i < questions.length; i++) {
        var Q = questions[i];
        alert("inside boucle " + i + "question type:" + Q.getType())

        if (Q.getType() == "text") {
            Qobject.push({
                surveyid: surveyId,
                name: Q.name,
                title: Q.title,
                choices: [],
                aType: Q.getType()
            });


        }

        if (Q.getType() == "checkbox") {
            alert(Q.name)
            for (var j = 0; j < Q.choices.length; j++) {
                Qchoices.push({
                    text: Q.choices[j].text,
                    value: Q.choices[j].value
                });

            }
            alert("testing Qchoices text" + Qchoices[1].text);
            Qobject.push({
                surveyid: surveyId,
                name: Q.name,
                title: Q.title,
                choices: Qchoices,
                aType: Q.getType()
            });
            Qchoices = [];
        }
        if (Q.getType() == "radiogroup") {
            alert(Q.name);
            for (var j = 0; j < Q.choices.length; j++) {
                Qchoices.push({
                    text: Q.choices[j].text,
                    value: Q.choices[j].value
                });

            }
            alert("testing Qchoices radio text " + Qchoices[1].text);
            Qobject.push({
                surveyid: surveyId,
                name: Q.name,
                title: Q.title,
                choices: Qchoices,
                aType: Q.getType()
            });
            //Qchoices=[];
        }
        alert("done in radio");
        alert("length:" + qs.length);
    }
}


function addQuestionListIntoResults(questions, results, namePrefix) {
    for (var i = 0; i < questions.length; i++) {
        var q = questions[i];
        if (q.getType() == "matrixdynamic") {
            for (var rowIndex = 0; rowIndex < q.visibleRows.length; rowIndex++) {
                for (var colIndex = 0; colIndex < q.columns.length; colIndex++) {
                    addQuestionIntoResults(q.visibleRows[rowIndex].cells[colIndex].question, results, namePrefix + q.name + "row_" + (rowIndex + 1).toString() + questionPrefix + q.columns[colIndex].name);
                }
            }
        } else
            if (q.getType() == "paneldynamic") {
                for (var panelIndex = 0; panelIndex < q.panels.length; panelIndex++) {
                    addQuestionListIntoResults(q.panels[panelIndex].questions, results, namePrefix + q.name + questionPrefix + "panel_" + (panelIndex + 1).toString());
                }
            } else {
                addQuestionIntoResults(q, results, namePrefix);
            }
    }
}


var EndDa;

function getParams() {
    var url = window.location.href
        .slice(window.location.href.indexOf("?") + 1)
        .split("&");
    var result = {};
    url.forEach(function (item) {
        var param = item.split("=");
        result[param[0]] = param[1];
    });
    return result;
}



function addQuestionListIntoResults(questions, result, namePrefix) {

    for (var i = 0; i < questions.length; i++) {
        alert("There are " + questions.length + " in the survey");
        var q = questions[i];
        if (q.getType() == "checkbox") {
            alert(q.name);
            alert()
            alert("you are inside checbox");
            //alert(questions[i].choices);
            //alert(Object.keys(q.choices));
            for (var j = 0; j < q.choices.length; j++) {
                alert(q.choices[j].text);
            }
        }
        if (q.getType() == "radiogroup") {
            alert(q.name);
        }
        if (q.getType() == "matrixdynamic") {
            for (var rowIndex = 0; rowIndex < q.visibleRows.length; rowIndex++) {
                for (var colIndex = 0; colIndex < q.columns.length; colIndex++) {
                    addQuestionIntoResults(q.visibleRows[rowIndex].cells[colIndex].question, result, namePrefix + q.name + "row_" + (rowIndex + 1).toString() + questionPrefix + q.columns[colIndex].name);
                }
            }
        } else
            if (q.getType() == "paneldynamic") {
                for (var panelIndex = 0; panelIndex < q.panels.length; panelIndex++) {
                    addQuestionListIntoResults(q.panels[panelIndex].questions, result, namePrefix + q.name + questionPrefix + "panel_" + (panelIndex + 1).toString());
                }
            } else {
                addQuestionIntoResults(q, result, namePrefix);
            }


    }
}

//Graphical Stats
function generaterandomColors(colorsdb,lengthparam) {
    var temp = new Array();
    for (c = 0; c < lengthparam; c++) {
        var random = Math.floor(Math.random() * lengthparam);
        temp.push(colorsdb[random]);
    }

    return temp;
}

function setChart(dataarray,choicesarray,texttitle,element) {
    new Chart(document.getElementById(element), {
        type: 'doughnut',
        data: {
            labels: choicesArray,
            datasets: [
                {
                    label: texttitle,
                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                    data: dataarray
                }
            ]
        },
        options: {
            title: {
                display: true,
                text: texttitle
            }
        }
    });
}
function ExtractForstats(questions, result) {

    for (var i = 0; i < questions.length; i++) {
        var Q = questions[i];
        for (var j = 0; j < result.length; j++) {
            var R = result[j];
            alert(R);
        }
    }
}

function countvalues(a) {
    var b = {}, i = a.length, j;
    while (i--) {
        j = b[a[i]];
        b[a[i]] = j ? j + 1 : 1;
    }
    return b;
}

function test(questionarray, resultarray, surveyquestions) {
    var dynamicQuestion = []
    var dynamicAnswer = [];
    var tempstats = [];
    for (var qi = 0; qi < questionarray.length; qi++) {
        tempstats[questionarray[qi]] = new Array();
        dynamicQuestion[questionarray[qi]] = new Array();
        dynamicQuestion[questionarray[qi]].push(questionarray[qi]);
        tempstats[questionarray[qi]].push(questionarray[qi]);
    }
    //debugging purposes
    B = tempstats;
    for (var aa = 0; aa < resultarray.length; aa++) {
        for (var qa = 0; qa < questionarray.length; qa++) {

            dynamicQuestion[questionarray[qa]].push(JSON.parse(resultarray[aa])[questionarray[qa]]);
        }
    }
    G = dynamicQuestion;
    console.log(dynamicQuestion);
     for (var sq = 0; sq < surveyquestions.length; sq++) {
         var q = surveyquestions[sq];
        
        dynamicQuestion[q.name] = dynamicQuestion[q.name].toString().split(",");
        //les reponses de chaque question [par name]
        G = dynamicQuestion[q.name];
         for (var k = 0; k < G.length; k++) {
             var uniq = [];
             uniq = [...new Set(G)];
             for (var u = 1; u < uniq.length; u++) {
                 var s = uniq[u] + ":" + countvalues(G)[uniq[u]];
                     if (!tempstats[G[0]].includes(s)) {
                         tempstats[G[0]].push(uniq[u] + ":" + countvalues(G)[uniq[u]]);
                     }
              
             }
                 var availablechoices = [];
                 var availabledata = [];
                 var statsTitle = q.title;
             //Recuperation
                 for (var sindex = 1; sindex < tempstats[G[0]].length; sindex++) {
                     var token = tempstats[G[0]][sindex];
                     var temp = token.split(":");
                     if (!availablechoices.includes(temp[0])) {
                         if (q.getType() == "checkbox" || q.getType() == "radiogroup") {
                             for (var j = 0; j < q.choices.length; j++) {
                                 if (temp[0] == q.choices[j].value) {
                                     availablechoices.push(q.choices[j].text);
                                 }
                             }
                             availabledata.push(temp[1]);
                         }
                         
                     }
                     if (q.getType() == "rating") {
                         availablechoices.push(temp[0]);
                         availabledata.push(temp[0]);
                     }
                    
                 }
               
         }
         //debugging purposes
          dataArray = availabledata;
          choicesArray = availablechoices;
         title = statsTitle;
        
             var divTag = document.createElement("div");
         divTag.id = "div" + sq;
        
         
         //divTag.innerHTML = Date();
         divTag.innerHTML = "<canvas id='div" + sq + sq + "' width='400' height='250'></canvas>";
         
         document.getElementById("stats").appendChild(divTag);
         var elem = document.getElementById('div' + sq);
         elem.classList.add('flex-item');
         var colors = generaterandomColors(colorsdb, dataArray.length + 1);
         setChart(dataArray, choicesArray, title, "div"+sq+sq);
         
       //  if (dataArray != null) 
        // setChart(dataArray, choicesArray, title, "doughnut-chart"); 
         // <canvas id="doughnut-chart" width="400" height="250"></canvas>
         /*
          *
             var divTag = document.createElement("div");
             divTag.id = "div" + sq;
         alert(sq);
         divTag.innerHTML = "<canvas id=div'"+sq+sq+"' width='400' height='250'></canvas>";
         document.getElementById("stats").appendChild(divTag);
        // setChart(dataArray, choicesArray, title,"div"+sq+sq);

       //  if (dataArray != null)
        //
         // <canvas id="doughnut-chart" width="400" height="250"></canvas>

          */
        
    }
   
}

function fillQuestionNames(questionsNames) {
    var questionNamesArray = new Array();
    for (var q = 0; q < questionsNames.length; q++) {
        questionNamesArray.push(questionsNames[q].name);
    }
    return questionNamesArray;
}
function fillAnswers(allresults, questionsArray) {
    var AnswerArray = new Array();
    for (var index = 0; index < allresults.length; index++) {
        var obj = Object.values(allresults)[index];
        AnswerArray.push(obj);
    }
    return AnswerArray;
}

function SurveyManager(baseUrl, accessKey) {
    var self = this;
    self.surveyId = decodeURI(getParams()["id"]);
    self.results = ko.observableArray();
    Survey.dxSurveyService.serviceUrl = "";
    var survey = new Survey.Model({
        surveyId: self.surveyId,
        surveyPostId: self.surveyId
    });
    self.columns = ko.observableArray();

    self.loadResults = function () {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", baseUrl + "/results?postId=" + self.surveyId);
        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhr.onload = function () {
            var result = xhr.response ? JSON.parse(xhr.response) : [];
            self.results(
                result.map(function (r) {
                    return JSON.parse(r || "{}");
                })
            );

            var questionarray = new Array();
            var resultsarray = new Array();
            var surveyQuestions = survey.getAllQuestions();
            
            questionarray = fillQuestionNames(surveyQuestions);
            resultsarray = fillAnswers(result, questionarray,surveyQuestions);
            allres = result;
            //getQuestions(surveyQuestions);
            test(questionarray, resultsarray,surveyQuestions);
            //addQuestionListIntoResults(survey.getAllQuestions(), result, "");
            self.columns(
                survey.getAllQuestions().map(function (q) {
                    return {
                        data: q.name,
                        sTitle: (q.title || "").trim(" ") || q.name,
                        mRender: function (rowdata) {
                            return (
                                (typeof rowdata === "string"
                                    ? rowdata
                                    : JSON.stringify(rowdata)) || ""
                            );
                        }
                    };
                })
            );

            self.columns.push({
                targets: -1,
                data: null,
                sortable: false,
                defaultContent:
                    "<button style='min-width: 150px;'>Show in Survey</button>"
            });
            var table = $("#resultsTable").DataTable({
                columns: self.columns(),
                data: self.results()
            });

            var json = new Survey.JsonObject().toJsonObject(survey);

            var windowSurvey = new Survey.SurveyWindow(json);
            windowSurvey.survey.mode = "display";
            windowSurvey.survey.title = self.surveyId;
            windowSurvey.show();

            $(document).on("click", "#resultsTable td", function (e) {
                var row_object = table.row(this).data();
                windowSurvey.survey.data = row_object;

                windowSurvey.isExpanded = true;
            });
        };
        xhr.send();

    };

    survey.onLoadSurveyFromService = function () {
        self.loadResults();
    };
}
ko.applyBindings(new SurveyManager(""), document.body);
