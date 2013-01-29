import httplib2
from httplib2 import Http
import json

class HomeUrl:
    baseUrl = "http://tools.offwind.eu"
    #baseUrl = "http://192.168.1.10:59216"

class JobResult:
    NONE = "None"
    OK = "OK"
    ERROR = "Error"

class JobManager:
    

    def getStartedJobs(self):
        url = HomeUrl.baseUrl + "/jobs/GetStartedJobs"
        resp, content = Http().request(url, "GET")
        return json.loads(content)[u'data']

    def getRunningJobs(self):
        url = HomeUrl.baseUrl + "/jobs/GetRunningJobs"
        resp, content = Http().request(url, "GET")
        return json.loads(content)[u'data']

    def getCancelledJobs(self):
        url = HomeUrl.baseUrl + "/jobs/GetCancelledJobs"
        resp, content = Http().request(url, "GET")
        return json.loads(content)[u'data']

    def getSingleJob(self, jobId):
        url = HomeUrl.baseUrl + "/jobs/GetSingleJob?jobId=" + jobId
        resp, content = Http().request(url, "GET" )
        return json.loads(content)[u'data']

    def isJobCancelled(self, jobId):
        url = HomeUrl.baseUrl + "/jobs/IsJobCancelled?jobId=" + jobId
        resp, content = Http().request(url, "GET" )
        res = json.loads(content)[u'data'] == True
        return res

    def setJobRunning(self, jobId):
        url = HomeUrl.baseUrl + "/jobs/SetJobRunning?jobId=" + jobId
        resp, content = Http().request(url, "POST", headers={'content-type':'application/json', 'content-length':'0'} )

    def setJobFinished(self, jobId):
        url = HomeUrl.baseUrl + "/jobs/SetJobFinished?jobId=" + jobId
        resp, content = Http().request(url, "POST", headers={'content-type':'application/json', 'content-length':'0'} )

    #def updateJob(self, job, state):
    #    url = 'http://tools.offwind.eu/jobs/update/' + job[u'Id']
    #    #print url
    #    job[u'State'] = state
    #    h = httplib2.Http(".cache")
    #    #h.add_credentials('name', 'password')
    #    resp, content = h.request(url, "POST", body=json.dumps(job), headers={'content-type':'application/json'} )
