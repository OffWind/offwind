var svg;
var svgHelper;
(function () {
    svg = {
        init: function (elId) {
            SVGElement.prototype.setTransform = function (value) {
                this.setAttributeNS(null, 'transform', value);
                return this;
            };
            SVGElement.prototype.setVisibility = function (value) {
                this.setAttributeNS(null, 'visibility', value);
                return this;
            };
            SVGElement.prototype.getPosition = function () {
                var firstXForm = this.transform.baseVal.getItem(0);
                if (firstXForm.type == SVGTransform.SVG_TRANSFORM_TRANSLATE) {
                    var xp = firstXForm.matrix.e + this.cx.baseVal.value;
                    var yp = firstXForm.matrix.f + this.cy.baseVal.value;
                    return { x: xp, y: yp };
                }
            };
            var root = document.getElementById(elId);
            var defs = document.createElementNS("http://www.w3.org/2000/svg", 'defs');
            this.addPattern = function (w, h, id, content) {
                defs.appendChild(this.createPattern(w, h, id, content));
            };
            this.addMarker = function (h, w, refX, refY, viewBox, id, content) {
                defs.appendChild(this.createMarker(h, w, refX, refY, viewBox, id, content));
            };

            this.append = function (el) {
                root.appendChild(el);
            };
            root.appendChild(defs);
            return root;
        },
        _setProp: function (e, o) {
            for (var k in o)
                e.setAttributeNS(null, k, o[k]);
        },
        createRect: function (o) {
            var r = document.createElementNS("http://www.w3.org/2000/svg", 'rect');
            this._setProp(r, o);
            return r;
        },
        createCircle: function (o) {
            var c = document.createElementNS("http://www.w3.org/2000/svg", 'circle');
            this._setProp(c, o);
            return c;
        },
        createLine: function (x1, x2, y1, y2, style, markerEnd) {
            var l = document.createElementNS("http://www.w3.org/2000/svg", 'line');
            l.setAttributeNS(null, 'x1', x1);
            l.setAttributeNS(null, 'x2', x2);
            l.setAttributeNS(null, 'y1', y1);
            l.setAttributeNS(null, 'y2', y2);
            l.setAttributeNS(null, 'style', style);
            l.setAttributeNS(null, 'marker-end', markerEnd);
            return l;
        },
        createGroup: function (elements) {
            var g = document.createElementNS("http://www.w3.org/2000/svg", 'g');
            var arr = arguments.length == 1 ? elements : arguments;
            for (var i = 0; i < arr.length; i++) {
                g.appendChild(arr[i]);
            }
            return g;
        },
        createPath: function (o) {
            var p = document.createElementNS("http://www.w3.org/2000/svg", 'path');
            this._setProp(p, o);
            return p;
        },
        createText: function (x, y, fill, text, anchor) {
            var t = document.createElementNS("http://www.w3.org/2000/svg", 'text');
            t.setAttributeNS(null, 'x', x);
            t.setAttributeNS(null, 'y', y);
            t.setAttributeNS(null, 'fill', fill);
            t.setAttributeNS(null, 'text-anchor', anchor);
            var textNode = document.createTextNode(text);
            t.appendChild(textNode);
            return t;
        },
        createMarker: function (h, w, refX, refY, viewBox, id, content) {
            var m = document.createElementNS("http://www.w3.org/2000/svg", 'marker');
            m.setAttributeNS(null, 'markerWidth', w);
            m.setAttributeNS(null, 'markerHeight', h);
            m.setAttributeNS(null, 'refX', 1);
            m.setAttributeNS(null, 'refY', 5);
            m.setAttributeNS(null, 'orient', 'auto');
            m.setAttributeNS(null, 'id', id);
            m.setAttributeNS(null, 'viewBox', viewBox);
            m.appendChild(content);
            return m;
        },
        createPattern: function (w, h, id, content) {
            var p = document.createElementNS("http://www.w3.org/2000/svg", 'pattern');
            p.setAttributeNS(null, 'width', w);
            p.setAttributeNS(null, 'height', h);
            p.setAttributeNS(null, 'id', id);
            p.setAttributeNS(null, 'patternUnits', 'userSpaceOnUse');
            p.appendChild(content);
            return p;
        }
    };
    svgHelper = {
        getCursorPoint: function (el, e, svg) {
            var pt = svg.createSVGPoint();
            pt.x = e.clientX;
            pt.y = e.clientY;
            return pt.matrixTransform(el.getScreenCTM().inverse());
        }
    };
})();