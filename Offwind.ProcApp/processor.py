from datetime import tzinfo, timedelta, datetime
import sys
import os
from time import sleep
from jobmanager import JobResult
from jobmanager import JobManager
from runner import Runner
from configurator import Configurator
import utils

class Processor:
    runner = None
    jobId = None

    @utils.suppressExceptions1
    def cleanup(self):
        mgr = JobManager()
        rJobs = mgr.getRunningJobs()
        if (len(rJobs) > 0):
            print "Cleaning %s running jobs..." % len(rJobs)
            for job in rJobs: mgr.setJobFinished(job[u'Id'])
            print "OK"
        cJobs = mgr.getCancelledJobs()
        if (len(cJobs) > 0):
            print "Cleaning %s cancelled jobs..." % len(cJobs)
            for job in cJobs: mgr.setJobFinished(job[u'Id'])
            print "OK"

    @utils.suppressExceptions1
    def Do(self):
        mgr = JobManager()
        cfg = Configurator().read()

        # If processing not running, get next started job and run it. If no jobs started, just loop.
        if (self.runner == None):
            jobs = mgr.getStartedJobs()
            for job in jobs:
                mgr.setJobRunning(job[u'Id'])
                self.runner = Runner()
                self.runner.workDir = cfg.workDir
                self.runner.jobId = job[u'Id']
                self.runner.tryRun()
                print "Processing started"
                break # spawn ONLY one process
            return # skip rest of the loop "while 1==1"

        # Here we already know that processing is running. Act according to that.
        self.runner.checkState()
        self.runner.showDebug()

        if (self.runner.isFinished()):
            #if (p.result == JobResult.ERROR):
            #    print "Error occured: " + str(p.resultData)
            mgr.setJobFinished(self.runner.jobId)
            self.runner = None
            print "Processing finished"
            return

        if (mgr.isJobCancelled(self.runner.jobId)):
            self.runner.cancel()
            print "Processing cancelled"
            return

        print "Processing is still running"
