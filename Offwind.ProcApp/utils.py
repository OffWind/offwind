import sys

def suppressExceptions1(fn):
    def wrapped(a1):
        return fn(a1)
    
        try:
            return fn(a1)
        except IOError as e:
            print "I/O error({0}): {1}".format(e.errno, e.strerror)
        except:
            print sys.exc_info()[0]
    return wrapped

def suppressExceptions2(fn):
    def wrapped(a1, a2):
        try:
            return fn(a1, a2)
        except IOError as e:
            print "I/O error({0}): {1}".format(e.errno, e.strerror)
        except:
            print sys.exc_info()[0]
    return wrapped
