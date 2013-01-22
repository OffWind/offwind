#!/usr/bin/python
# -*- coding: utf-8 -*-
import shutil
from datetime import tzinfo, timedelta, datetime
import sys
import os
import zipfile
import stat
import tempfile
from time import sleep
import subprocess
import urllib
import urllib2
import json

class JobManager:
    def getJobs(self):
        url = 'http://tools.offwind.eu/simplejobs/started'
        u = urllib.urlopen(url)
        # u is a file-like object
        data = u.read()
        return data

    def setRunning(self, job):
        url = 'http://tools.offwind.eu/simplejobs/put/' + job['Id']
        job['State'] = 'Running'
        opener = urllib2.build_opener(urllib2.HTTPHandler)
        request = urllib2.Request(url, data = json.dumps(job))
        request.add_header('Content-Type', 'application/json')
        request.get_method = lambda: 'PUT'
        url = opener.open(request)

class Processor:
    jobId = None
    connection = None
    cursor = None

if __name__ == '__main__':
    print "Hello!"
    print ""
    while 1==1:
        mgr = JobManager()
        jobs = mgr.getJobs()
        jobs = json.loads(jobs)
        print len(jobs)
        if len(jobs):
            #job = jobs[0]
            print jobs
            #mgr.setRunning(job)

    print "Exiting program... Bye!"
