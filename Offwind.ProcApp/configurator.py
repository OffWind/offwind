import utils
import ConfigParser

class Configurator:
    workDir = None

    @utils.suppressExceptions1
    def init(self):
        config = ConfigParser.RawConfigParser()
        config.add_section('Processing')
        config.set("Processing", "workDir", "/home/vlad/offwind.cfd/work")

        with open("/etc/offwind-proc.conf", "wt") as configfile:
            config.write(configfile)
        print "/etc/offwind-proc.conf written"

    @utils.suppressExceptions1
    def read(self):
        config = ConfigParser.RawConfigParser()
        config.read("/etc/offwind-proc.conf")
        self.workDir = config.get("Processing", "workDir")
        return self
