var Layout;
(function () {
    Layout = function (xscale, yscale, scaleInterval, intervalLen) {
        var xCellCount = xscale / scaleInterval;
        var yCellCount = yscale / scaleInterval;
        var wsHeight = yCellCount * intervalLen;
        var wsWidth = xCellCount * intervalLen;
        var selectionColor = 'blue';
        var defColor = 'red';
        var plane;
        var workspace;

        var root = svg.init('layout');

        var res = coordinatePlane(wsHeight, wsWidth, xscale, yscale, scaleInterval, intervalLen);
        plane = res.plane;
        workspace = res.workspace;

        this.drawPoints = function (points, draggable) {
            var f = [];
            var container;
            var addDragListener;
            var innerChanging;
            var dragging;
            var convertToFarmPoint = function (point) {
                var x = xscale * point.x / wsWidth;
                var y = -yscale * (point.y - wsHeight) / wsHeight;
                return { x: x, y: y };
            };
            var convertToScreenPoint = function (point) {
                var x = point.x / xscale * wsWidth;
                var y = wsHeight - (point.y / yscale * wsHeight);
                return { x: x, y: y };
            };

            var initDragDrop = function () {
                var startpoint;
                var currentEl;
                var curPoint;
                addDragListener = function (el) {
                    el.addEventListener("mousedown", function (e) {
                        if (!startpoint) {
                            startpoint = svgHelper.getCursorPoint(this, e, root);
                            currentEl = this;
                            dragging = true;
                            $(currentEl).attr('fill', selectionColor);
                            curPoint = points.findWhere({ id: currentEl.id });
                            $(document).trigger('highlight_row', curPoint.toJSON());
                        }
                    }, false);
                };

                f.forEach(addDragListener);

                document.addEventListener("mousemove", function (e) {
                    if (!startpoint || !currentEl) return;

                    var point = svgHelper.getCursorPoint(workspace, e, root);
                    var x = point.x - startpoint.x;
                    var y = point.y - startpoint.y;

                    if (x - 10 <= 0)
                        x = 0 + 10;
                    if (x + 10 >= wsWidth)
                        x = wsWidth - 10;
                    if (y - 10 <= 0)
                        y = 0 + 10;
                    if (y + 10 >= wsHeight)
                        y = wsHeight - 10;

                    currentEl.setTransform('translate( ' + x + ',' + y + ')');
                    var sp = convertToFarmPoint({ x: x, y: y });
                    innerChanging = true;
                    curPoint.set({ x: sp.x.toFixed(), y: sp.y.toFixed() });
                    innerChanging = false;

                }, false);
                document.addEventListener("mouseup", function (e) {
                    if (!currentEl) return;
                    dragging = false;
                    startpoint = null;
                    $(currentEl).attr('fill', defColor);
                    currentEl = null;
                    innerChanging = false;
                    $(document).trigger('unhighlight_row', curPoint.toJSON());
                    $(document).trigger('turbine_changed');
                }, false);
            };
            var createEl = function (data) {
                var sp = convertToScreenPoint(data);
                var visibility = 'visible';
                if (sp.x == '' || sp.y === '')
                    visibility = 'hidden';
                
                var transform = 'translate(' + sp.x + ',' + sp.y + ')';
                var options = {
                    r: 10,
                    fill: 'red',
                    id: data.id,
                    transform: transform,
                    visibility: visibility
                };
                var el = svg.createCircle(options);
                if (draggable)
                    el.setAttribute('class', 'draggable');
                
                var p;
                el.addEventListener('mouseenter', function (e) {
                    var toElement = (!(e.toElement == null) ? e.toElement : e.target);
                    p = points.findWhere({ id: toElement.id });
                    $(document).trigger('highlight_row', p.toJSON());
                }, false);
                
                el.addEventListener('mouseleave', function (e) {
                    if (!dragging)
                        $(document).trigger('unhighlight_row', p.toJSON());
                }, false);
                
                return el;
            };
            var init = function (collection) {
                collection.forEach(function(entry) {
                    if ('x' in entry || 'y' in entry)
                        f.push(createEl(entry));
                });
                $(document).on('highlight_marker', function (e, data) {
                    $('#t-' + data.ind).attr('fill', selectionColor);

                });
                $(document).on('unhighlight_marker', function (e, data) {
                    $('#t-' + data.ind).attr('fill', defColor);
                });
            };

            if (points instanceof Backbone.Collection) {
                init(points.toJSON());
                points.on('add', function (model) {
                    var el = createEl(model.toJSON());
                    if (addDragListener)
                        addDragListener(el);
                    container.appendChild(el);
                });
                points.on('change', function (model) {
                    if (innerChanging) return;
                    var el = document.getElementById(model.get('id'));
                    if (model.get('x') == '' && model.get('y') == '') {
                        points.remove(model);
                        if(el) el.parentNode.removeChild(el);
                        return;
                    }
                    var p = convertToScreenPoint(model.toJSON());
                  
                    if (el) {
                        el.setVisibility('visible');
                        el.setTransform('translate( ' + p.x + ',' + p.y + ')');
                    }
                });
                points.on('remove', function(model) {
                    var el = document.getElementById(model.get('id'));
                    if (el) el.parentNode.removeChild(el);
                    return;
                });
                points.on('reset', function() {
                    $(container).empty();
                });
            }
            else if (points instanceof Array) {
                init(points);
            }

            if (draggable)
                initDragDrop();

            container = svg.createGroup(f);
            plane.appendChild(container);
        };

        svg.append(plane);
    };

    var coordinatePlane = function (h, w, xscale, yscale, scaleInterval, intervalLen) {
        var offsetX = 40;
        var offsetY = 5;
        var workspace;
        var plane;
        var drawGrid = function () {

            var path = svg.createPath({
                d: 'M 10 0 L 0 0 0 10',
                fill: 'none',
                stroke: 'gray',
                'stroke-width': 0.5
            });

            svg.addPattern(10, 10, 'smallGrid', path);
            var p = 'M' + intervalLen + ' 0 L 0 0 0 ' + intervalLen;
            var patternGroup = svg.createGroup(
                                    svg.createRect({
                                        width: intervalLen,
                                        height: intervalLen,
                                        fill: 'url(#smallGrid)'
                                    }),
                                    svg.createPath({
                                        d: p,
                                        fill: 'none',
                                        stroke: 'gray',
                                        'stroke-width': 1
                                    }));

            svg.addPattern(intervalLen, intervalLen, 'grid', patternGroup);

            workspace = svg.createRect({
                width: w,
                height: h,
                fill: 'url(#grid)'
            });
        };
        var drawVectors = function () {
            svg.addMarker(6, 6, 1, 5, '0 0 10 10', 'triangle', svg.createPath({
                d: 'M 0 0 L 10 5 L 0 10 z'
            }));
            var l1 = svg.createLine(0, 0, h, 0, 'stroke:rgb(0,0,0);stroke-width:1', 'url(#triangle)');
            var l2 = svg.createLine(0, w, h, h, 'stroke:rgb(0,0,0);stroke-width:1', 'url(#triangle)');
            plane = svg.createGroup(workspace, l1, l2);
        };
        var drawLabels = function () {
            var val = yscale;
            var scaleLen = 0;
            var verticalScale = svg.createGroup();
            do {
                var d = svg.createText(0, scaleLen, 'black', val, 'end');
                verticalScale.appendChild(d);
                val -= scaleInterval;
                scaleLen += intervalLen;
            } while (val >= 0);
            scaleLen = w;
            val = xscale;
            var horizontalScale = svg.createGroup();
            do {
                var ds = svg.createText(scaleLen, h, 'black', val, 'middle');
                horizontalScale.appendChild(ds);
                val -= scaleInterval;
                scaleLen -= intervalLen;
            } while (val > 0);
            verticalScale.setTransform('translate(-5,5)');
            horizontalScale.setTransform('translate(0,15)');
            plane.appendChild(verticalScale);
            plane.appendChild(horizontalScale);
        };
        drawGrid();
        drawVectors();
        drawLabels();
        plane.setTransform('translate(' + offsetX + ',' + offsetY + ')');
        return { workspace: workspace, plane: plane };
    };
})();