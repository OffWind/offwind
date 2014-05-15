function initTurbines(options) {
    var spareRows = options.readOnly ? 0 : 1;
    var changed = options.onchanged;
    
    var l = new Layout(4000, 4000, 1000, 100);

    var points = options.data;
    for (var i = 0; i < points.length; i++) {
        points[i].id = 't-' + i;
        points[i].index = i;
    }

    var collection = new Backbone.Collection();
    collection.reset(points);

    var initTable = function (coll) {
        
        function makeTurbine() {
            var m = new Backbone.Model();
            var last = collection.at(collection.length - 1);

            if (!last) {
                m.set('index', collection.length);
                m.set('id', 't-' + collection.length);
                m.set('x', '');
                m.set('y', '');
                collection.add(m);
                return m;
            }

            if (last.get('x') == '' || last.get('y') == '')
                return m;
            
            
            if (!options.readOnly) {
                m.set('index', collection.length);
                m.set('id', 't-' + collection.length);
                m.set('x', '');
                m.set('y', '');
                collection.add(m);
            }
            return m;
        }

        function attr(a) {
            return {
                data: function (turb, value) {

                    if (_.isUndefined(value)) {
                        return turb.get(a);
                    }
                    turb.set(a, value);
                },
                readOnly: options.readOnly
            };
        }

        var $container = $("#dataTable");
        $container.handsontable({
            data: coll.models,
            dataSchema: makeTurbine,
            contextMenu: false,
            stretchH: 'all',
            rowHeaders: true,
            minSpareRows: spareRows,
            currentRowClassName: 'currentRow',
            currentColClassName: 'currentCol',
            onChange: changed,
            columns: [
                attr("x"),
                attr("y")
            ],
            colHeaders: ["<b>X</b>", "<b>Y</b>"],
            fillHandle: true,
            outsideClickDeselects: false,
            autoWrapRow: true,
        });
        $container.find('table').addClass('table table-hover');
        var instance = $container.handsontable('getInstance');
        collection
            .on("add", function (model) {
                $container.handsontable("render");
            })
            .on("remove", function() {
                $container.handsontable("render");
            })
            .on("change", function() {
                $container.handsontable("render");
            });
        $(document).on('turbine_changed', function() {
            changed();
        });
        $(document).on('highlight_row', function(e, data) {
            $container.handsontable('selectCell', data.index, 0, data.index, 1);
        });
        $(document).on('unhighlight_row', function() {
            $container.handsontable('deselectCell');
        });
        var currentInd;
        $container.find('table>tbody>tr').on('mouseenter', function(e) {
            currentInd = $(e.toElement).parent().index();
            $(document).trigger('highlight_marker', { ind: currentInd });
        });
        $container.find('table>tbody>tr').on('mouseleave', function(e) {
            $(document).trigger('unhighlight_marker', { ind: currentInd });
        });

    };

    initTable(collection);

    l.drawPoints(collection, !options.readOnly);
};