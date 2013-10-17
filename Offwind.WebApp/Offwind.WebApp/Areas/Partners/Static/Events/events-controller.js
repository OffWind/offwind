var CurrentSituationController = {};

(function() {

    CurrentSituationController = function () {
    };

    CurrentSituationController.prototype.initialize = function (serverModel) {
        var model = new CurrentSituation().set2(serverModel);
        var tplHeader = Handlebars.compile($("#tpl-table-header").html());
        var tplElements = Handlebars.compile($("#tpl-elements").html());
        var tplAssessments = Handlebars.compile($("#tpl-assessments").html());

        Handlebars.registerHelper('renderAssessments', function (elementId) {
            var assessments = model.get('Assessments').toJSON();
            assessments = $.grep(assessments, function (e) {
                return e.ElementId == elementId;
            });
            return tplAssessments(assessments);
        });

        var json = model.toJSON2();
        $("#situation-container table thead").html(tplHeader(json));
        $("#situation-container table tbody").html(tplElements(json));

        // Only after main table rendered we can render competitors
        var cView = new CompetitorsView({ model: model });
        cView.renderAll();

        $(document).on('click', 'a.action[href="#add-competitor"]', function (event) {
            event.preventDefault();
            var competitor = new Competitor();
            var counter = model.get('Competitors').length + 1;
            competitor.set('Name', 'Competitor ' + counter);
            model.get('Competitors').add(competitor);
            return;
            competitor.save({}, {
                success: function() {
                    model.get('Competitors').add(competitor);
                }
            });
        });

        $(document).on('click', 'a.action[href="#delete-competitor"]', function (event) {
            event.preventDefault();
            if (model.get('Competitors').length <= 2) {
                $('#competitors-notification').show();
                setTimeout(function () {
                    $('#competitors-notification').fadeOut(500);
                }, 3000);
                return;
            }

            var id = $(this).attr('data-competitor-id');
            var competitor = model.get('Competitors').get(id);
            model.get('Competitors').remove(competitor);
        }); return;

        this.model.Situations = [];
        for (var i = 0; i < this.model.Stickers.length; i++) {
            var sticker = this.model.Stickers[i];
            var situation = {
                Id: sticker.Id,
                Title: sticker.Title,
                CategoryId: sticker.CategoryId
            };
            this.model.Situations.push(situation);

            situation.CompetitorComparisons = [];
            for (var j = 0; j < this.model.Competitors.length; j++) {
                var competitor = this.model.Competitors[j];
                situation.CompetitorComparisons.push({
                    Id: competitor.Id,
                    StickerId: sticker.StickerId,
                    Title: ''
                });
            }
        }
    };
})();

