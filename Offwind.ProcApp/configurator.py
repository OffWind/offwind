import utils
import ConfigParser

class Configurator:
    workDir = None
    baseUrl = None
    pollTimeOut = None

    @utils.suppressExceptions1
    def init(self):
        config = ConfigParser.RawConfigParser()
        config.add_section('Processing')
        config.set("Processing", "workDir", "/home/vlad/offwind.cfd/work")
        config.set("Processing", "baseUrl", "http://tools.offwind.eu")
        config.set("Processing", "pollTimeOut", "5")

        with open("/etc/offwind-proc.conf", "wt") as configfile:
            config.write(configfile)
        print "/etc/offwind-proc.conf written"

    @utils.suppressExceptions1
    def read(self):
        config = ConfigParser.RawConfigParser()
        config.read("/etc/offwind-proc.conf")
        self.workDir = config.get("Processing", "workDir")
        self.baseUrl = config.get("Processing", "baseUrl")
        self.pollTimeOut = config.get("Processing", "pollTimeOut")
        return self

