import shutil
from datetime import tzinfo, timedelta, datetime
import sys
import os
import zipfile
import stat
from time import sleep
import subprocess
import httplib2
from httplib2 import Http
from jobmanager import JobResult
from jobmanager import HomeUrl

class Runner:
    workDir = None
    tmpDir = ""
    jobId = None
    result = JobResult.NONE
    resultData = None
    finished = None
    process = None
    
    def tryRun(self):
        try:
            self.createTempFolder()
            self.downloadInputData()
            self.copyUtil("/Allrun")
            self.copyUtil("/ParseLogs")
            #self.run()
            self.runUnzip()
            self.runBlockMesh()
            self.startSolver()
        except:
            print sys.exc_info()[0]
            self.result = JobResult.ERROR
            self.resultData = sys.exc_info()[0]
        finally:
            self.finished = datetime.utcnow()                
    
    def showDebug(self):
        if self.process == None: return
        print "PID: %s" % self.process.pid
        print "RCode: %s" % self.process.returncode
        print "Job ID: %s" % self.jobId
    
    def createTempFolder(self):
        self.tmpDir = self.workDir + '/' + self.jobId
        if not os.path.exists(self.tmpDir):
            os.makedirs(self.tmpDir)
            os.chmod(self.tmpDir, 0o777)
            
    def downloadInputData(self):
        url = HomeUrl.baseUrl + '/cfd/downloads/getinputdata/'
        url = url + self.jobId
        print "Downloading: " + url
        targetFile = self.tmpDir + '/input.zip'
        print "Writing to target: " + targetFile
        
        response, content = Http().request(url)
        #print len(content)
        with open(targetFile, "wb") as f:
            f.write(content)

    def copyUtil(self, utilName):
        source = os.path.dirname(os.path.realpath(__file__)) + utilName
        destination = self.tmpDir + utilName
        print source
        print destination
        shutil.copy(source, destination)
        os.chmod(destination, os.stat(destination).st_mode | stat.S_IXUSR)

    def run(self):
        #output, error = subprocess.Popen(["./Allrun"], cwd = self.tmpDir).communicate()
        self.process = subprocess.Popen(["./Allrun"], cwd = self.tmpDir)

    def runUnzip(self):
        print "Unzipping 'input.zip'..."
        with open(self.tmpDir + "/log.unzipping.txt", "w") as log:
            subprocess.call(["unzip", "-o", "input.zip"], cwd = self.tmpDir, stdout=log)
            subprocess.call(["rm", "input.zip"], cwd = self.tmpDir, stdout=log)

    def runBlockMesh(self):
        print "Building mesh with 'blockMesh'..."
	with open(self.tmpDir + "/log.blockMesh.txt", "w") as log:
            subprocess.call(["blockMesh"], cwd = self.tmpDir, stdout=log)

    def startSolver(self):
        print "Started 'OffwindSolver'..."
	with open(self.tmpDir + "/log.solver.txt", "w") as log:
            self.process = subprocess.Popen(["OffwindSolver"], cwd = self.tmpDir, stdout=log)

    def runZip(self):
        print "Zipping results..."
	with open(self.tmpDir + "/log.zipping.txt", "w") as log:
            subprocess.call(["zip", "-r", "result.zip", ".", "-i", "*"], cwd = self.tmpDir, stdout=log)

    def parseLogs(self):
        subprocess.call(["./ParseLogs"], cwd = self.tmpDir, stdout=subprocess.PIPE)

    def checkState(self):
	if self.process == None: return
        self.process.poll()
    
    def isFinished(self):
	if self.process == None: return
        return self.process.returncode != None
    
    def cancel(self):
	if self.process == None: return
        self.process.terminate()

if __name__ == '__main__':
    Runner().tryRun()
