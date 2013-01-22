import sys, os
import web
import ConfigParser

class Configurator:
    workDir = None

    def init(self):
        config = ConfigParser.RawConfigParser()
        config.add_section('Processing')
        config.set("Processing", "workDir", "/home/vlad/offwind.cfd/work")

        with open("/etc/offwind-proc.conf", "wt") as configfile:
            config.write(configfile)
        print "/etc/offwind-proc.conf written"

    def read(self):
        config = ConfigParser.RawConfigParser()
        config.read("/etc/offwind-proc.conf")
        self.workDir = config.get("Processing", "workDir")
        return self

class list:
    def GET(self, jobId):
        cfg = Configurator().read()
        dir = cfg.workDir + jobId + '/logs'
        t = ""
        for file in os.listdir(dir):
            t = t + " | " + file
        return t

class read:
    def GET(self, jobId, param):
        cfg = Configurator().read()
        fpath = cfg.workDir + jobId + '/logs/' + param
        data = []
        import json
        with open(fpath, 'r') as content_file:
            for line in content_file:
                p = line.split()
                data.append([float(p[0]), float(p[1])])
            #content = content_file.read()
        return json.dumps(data)

class plot:
    def GET(self, jobId, param):
        cfg = Configurator().read()
        import cStringIO
        from matplotlib.figure import Figure                      
        from matplotlib.backends.backend_agg import FigureCanvasAgg

        fig = Figure(figsize=[4,4])                               
        ax = fig.add_axes([.1,.1,.8,.8])                          
        ax.scatter([1,2], [3,4])                                  
        canvas = FigureCanvasAgg(fig)

        # write image data to a string buffer and get the PNG image bytes
        buf = cStringIO.StringIO()
        canvas.print_png(buf)
        data = buf.getvalue()

        web.header("Content-Type", "image/png")
        web.header("Content-Length", len(data))
        return data

class hello:        
    def GET(self, name):
        cfg = Configurator().read()
        if not name: 
            name = 'World'
        return 'Hello, ' + name + '!<br/> Workdir: ' + cfg.workDir
