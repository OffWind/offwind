#!/usr/bin/python
# -*- coding: utf-8 -*-
import argparse
from datetime import tzinfo, timedelta, datetime
from time import sleep
import daemon
from processor import Processor
from configurator import Configurator
import utils
from daemon import Daemon
 
class MyDaemon(Daemon):
    def run(self):
        processor = Processor()
        config = Configurator().read()
        processor.config = config
        to = float(config.pollTimeOut)
        print "Offwind processing service started. Polling queue... timeout " + config.pollTimeOut
        while 1==1:
            print datetime.utcnow()
            sleep(to)
            processor.Do()
        
        print "Exiting program... Bye!"

if __name__ == '__main__':
    
    parser = argparse.ArgumentParser(description = "Offwind processing application v.1.0. Please, send all questions and suggestions to vlad.ogay@nrg-soft.com")
    parser.add_argument("action",
                        help = "action to perform",
                        choices = ["init", "cleanup", "run", "start", "stop", "restart"],
                        nargs = '?',
                        default = "run")
    args = parser.parse_args()

    if (args.action == "init"):
        Configurator().init()
    elif (args.action == "cleanup"):
        config = Configurator().read()
        processor = Processor()
        processor.config = config        
        processor.cleanup()
        print "Cleanup finished"
    elif (args.action == "start"):
        MyDaemon('/tmp/offwind-proc.pid').start()
    elif (args.action == "stop"):
        MyDaemon('/tmp/offwind-proc.pid').stop()
    elif (args.action == "run"):
        MyDaemon('/tmp/offwind-proc.pid').run()
    elif (args.action == "restart"):
        MyDaemon('/tmp/offwind-proc.pid').restart()
