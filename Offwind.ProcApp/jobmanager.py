import httplib2
from httplib2 import Http
import json

class JobResult:
    NONE = "None"
    OK = "OK"
    ERROR = "Error"

class JobManager:
    baseUrl = "http://tools.offwind.eu"

    def getStartedJobs(self):
        url = self.baseUrl + "/jobs/GetStartedJobs"
        resp, content = Http().request(url, "GET")
        return json.loads(content)[u'data']

    def getRunningJobs(self):
        url = self.baseUrl + "/jobs/GetRunningJobs"
        resp, content = Http().request(url, "GET")
        return json.loads(content)[u'data']

    def getCancelledJobs(self):
        url = self.baseUrl + "/jobs/GetCancelledJobs"
        resp, content = Http().request(url, "GET")
        return json.loads(content)[u'data']

    def getSingleJob(self, jobId):
        url = self.baseUrl + "/jobs/GetSingleJob?jobId=" + jobId
        resp, content = Http().request(url, "GET" )
        return json.loads(content)[u'data']

    def isJobCancelled(self, jobId):
        url = self.baseUrl + "/jobs/IsJobCancelled?jobId=" + jobId
        resp, content = Http().request(url, "GET" )
        res = json.loads(content)[u'data'] == True
        return res

    def setJobRunning(self, jobId):
        url = self.baseUrl + "/jobs/SetJobRunning?jobId=" + jobId
        resp, content = Http().request(url, "POST", headers={'content-type':'application/json', 'content-length':'0'} )

    def setJobFinished(self, jobId):
        url = self.baseUrl + "/jobs/SetJobFinished?jobId=" + jobId
        resp, content = Http().request(url, "POST", headers={'content-type':'application/json', 'content-length':'0'} )

    #def updateJob(self, job, state):
    #    url = 'http://tools.offwind.eu/jobs/update/' + job[u'Id']
    #    #print url
    #    job[u'State'] = state
    #    h = httplib2.Http(".cache")
    #    #h.add_credentials('name', 'password')
    #    resp, content = h.request(url, "POST", body=json.dumps(job), headers={'content-type':'application/json'} )
