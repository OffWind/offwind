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

class JobManager:
    def getJobs(self):
        url = 'http://localhost:53963/api/jobs/running'
        u = urllib.urlopen(url)
        # u is a file-like object
        data = u.read()
        return data

class Processor:

    jobId = None
    connection = None
    cursor = None

if __name__ == '__main__':
    print "Hello!"
    while 1==1:
        mgr = JobManager()
        data = mgr.getJobs()
        print data
        sleep(3)
    print "Exiting program... Bye!"
