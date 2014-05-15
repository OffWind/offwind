function initTurbines(options) {
    var spareRows = options.readOnly ? 0 : 1;
    var changed = options.onchanged;
    var selectedInd;
    var l = new Layout(4000, 4000, 1000, 100);
    var $deleteButton = $('#delete-turbine');
    var $resetModal = $('#reset_dlg');
    var points = options.data;
   
    for (var i = 0; i < points.length; i++) {
        points[i].id = 't-' + i;
        points[i].index = i;
    }
   
    var collection = new Backbone.Collection();

    $('#add-turbine').on('click', function () {
        collection.add({
            index: collection.length,
            id: 't-' + collection.length,
            x: 100,
            y: 100
        });
    });
    
    $deleteButton.on('click', function () {
        var m = collection.at(selectedInd);
        collection.remove(m);
    });
    $('#reset-turbines').on('click', function () {
        $resetModal.modal('show');
    });
    $resetModal.find('#confirm-reset').on('click', function () {
        $resetModal.modal('hide');
        collection.reset();
    });
    collection.reset(points);
    var initTable = function (coll) {
        function makeTurbine() {
            var m = new Backbone.Model();
            //var last = collection.at(collection.length - 1);

            //if (!last) {
            //    m.set('index', collection.length);
            //    m.set('id', 't-' + collection.length);
            //    m.set('x', '');
            //    m.set('y', '');
            //    collection.add(m);
            //    return m;
            //}

            //if (last.get('x') == '' || last.get('y') == '')
            //    return m;
            
            
            //if (!options.readOnly) {
            //    m.set('index', collection.length);
            //    m.set('id', 't-' + collection.length);
            //    m.set('x', '');
            //    m.set('y', '');
            //    collection.add(m);
            //}
            return m;
        }

        function attr(a) {
            return {
                data: function (turb, value) {

                    if (_.isUndefined(value)) {
                        if (!turb) return;
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
            minSpareRows:0,// spareRows,
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
        coll.on("add", function (model) {
            $container.handsontable("loadData", coll.models);
            $container.handsontable("render");
            initHandlers();
            changed();
        })
            .on("remove", function () {
            $container.handsontable("render");
            initHandlers();
            changed();
        }).on("change", function() {
            $container.handsontable("render");
        }).on('reset', function () {
            $container.handsontable("loadData",[]);
            $container.handsontable("render");
            initHandlers();
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
        
        var initHandlers = function () {
            var $trs = $container.find('table>tbody>tr');
            $trs.off('mouseenter');
            $trs.off('mouseleave');
            $trs.off('click');
            $trs.removeClass('selectedRow');
            $deleteButton.addClass('disabled');
            var currentInd;
            $trs.on('mouseenter', function (e) {
                currentInd = $(e.toElement).parent().index();
                $(document).trigger('highlight_marker', { ind: currentInd });
            });
            $trs.on('mouseleave', function (e) {
                $(document).trigger('unhighlight_marker', { ind: currentInd });
            });
            selectedInd = null;
            $trs.on('click', function (e) {
                $trs.removeClass('selectedRow');
                $(e.currentTarget).addClass('selectedRow');
                selectedInd = $(e.currentTarget).index();
                $deleteButton.removeClass('disabled');
            });
        };
        initHandlers();
    };

    initTable(collection);

    l.drawPoints(collection, !options.readOnly);
};