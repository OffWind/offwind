/*
 Usage: Just include this script after Marionette and Handlebars loading
 IF you use require.js add script to shim and describe it in the requirements
*/
(function (Handlebars, Marionette) {

    Marionette.Handlebars = {
        path: '/template/',
        //extension: '.html'
    };

    Marionette.TemplateCache.prototype.loadTemplate = function (templateId) {
        //console.log(this.raw);
        if (this.raw) return this.raw;
        
        var template, templateUrl;
        if (!template || template.length === 0) {
            templateUrl = Marionette.Handlebars.path + templateId;// + Marionette.Handlebars.extension;
            Marionette.$.ajax({
                url: templateUrl,
                success: function (data) {
                    template = data;
                },
                async: false
            });
            if (!template || template.length === 0) {
                throw "NoTemplateError - Could not find template: '" + templateUrl + "'";
            }
        }
        return template;
    };

    Marionette.TemplateCache.prototype.compileTemplate = function (rawTemplate) {
        if (typeof rawTemplate === "function") {
            return rawTemplate;
        }
        else {
            return Handlebars.compile(rawTemplate);
        }
    };

}(Handlebars, Marionette));