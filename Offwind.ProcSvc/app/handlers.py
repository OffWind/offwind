import sys, os
import web
import ConfigParser
import re
import json

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
        dir = cfg.workDir + '/' + jobId + '/logs'
        t = []
        ok = 0
        for file in os.listdir(dir):
            if file == 'executionTime_0':
                ok = 1
                break
        if ok == 0:
            t.append('Time_0')
        else:
            for file in os.listdir(dir):
                if file != 'foamLog.awk':
                    t.append(file)
        text = json.dumps(t)
        return 'graphlist(' + text + ')'

class read:
    def GET(self, jobId, position, reqfiles):
        cfg = Configurator().read()
        flist = re.findall(r'\b[a-zA-Z_0-9]*', reqfiles);
        sumlen = 0
        data = []
        for name in flist:
            if name:
                blocklen = 0
                idx = int(position)
                fpath = cfg.workDir + '/' + jobId + '/logs/' + name
                with open(fpath, 'r') as content_file:
                    for line in content_file:
                        if idx == 0:
                            p = line.split()
                            blocklen = blocklen + 1
                            data.append([float(p[1])])
                        else:
                            idx = idx - 1
                data.insert(sumlen, [int(blocklen)])
                sumlen = sumlen + blocklen + 1
        text = json.dumps(data)
        # convert data into JSONP, on the other side we MUST have
        # implemented callback function named "plotter"
        return 'plotter(' + text + ')'

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
        return 'Hello, ' + name + \
               '\nWorkdir: ' + cfg.workDir + \
                '\nUsage: \
                \nhttp://calculator/list/{job id} - show files for selected job \
                \nhttp://calculator/read/{job id}/{file}/{pos} - read speciefied file from selected position'
