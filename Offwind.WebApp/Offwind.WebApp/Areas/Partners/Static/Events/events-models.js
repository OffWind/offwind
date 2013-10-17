var EventsHome;

var Event;
var EventsCollection;
var EventsView;

var Participant;
var ParticipantsCollection;
var ParticipantsView;

(function () {
    Event = Backbone.Model.extend({
        idAttribute: "Id",
        urlRoot: '/api/BusinessPlan/CurrentSituation/Elements',
        initialize: function () {
        }
    });
    EventsCollection = Backbone.Collection.extend({
        model: Element,
        url: '/api/BusinessPlan/CurrentSituation/Elements'
    });
    EventsView = Backbone.View.extend({
        initialize: function () {
            this.tplHead = Handlebars.compile($("#tpl-competitor-head").html());
            this.tplComparison = Handlebars.compile($("#tpl-competitor-comparison").html());

            this.listenTo(this.model.get('Competitors'), 'add', function (a) { this.renderCompetitor(a); });
            this.listenTo(this.model.get('Competitors'), 'remove', function (a, b, c) {
                this.removeCompetitor(a.get('Id'));
            });
        },
        renderAll: function () {
            this.model.get('Competitors').each(function (c) {
                this.renderCompetitor(c);
            }, this);
        },
        renderCompetitor: function (competitor) {
            var jsonCompetitor = competitor.toJSON();

            // Rendering head
            this.renderHead(jsonCompetitor);
            this.setHeaderColspan();
            this.renderComparisons(jsonCompetitor);

            // Rendering comparisons for each element
        },
       
        removeCompetitor: function (id) {
            // Remove title from head
            $('#situation-container table thead tr.subhead th.competitors[data-competitor-id="' + id + '"]').remove();
            this.setHeaderColspan();

            // Remove comparisons for each element
            $('#situation-container table tbody tr td.competitors[data-competitor-id="' + id + '"]').remove();
        }
    });
})();

(function () {
    Participant = Backbone.Model.extend({
        idAttribute: "Id",
        urlRoot: '/api/BusinessPlan/CurrentSituation/Comparisons',
        initialize: function () {
        }
    });
    ParticipantsCollection = Backbone.Collection.extend({
        model: Comparison,
        url: '/api/BusinessPlan/CurrentSituation/Comparisons'
    });
})();
