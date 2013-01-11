var td_renderer = function (instance, td) {
    Handsontable.TextCell.renderer.apply(this, arguments);
    $(td).css({ textAlign: "right" });
    return td;
};

function td_before_change(value, tview, template) {
    var instance = $(tview).data('handsontable');
    for (var i = 0; i < value.length; i++) {
        var r = value[i][0];
        var c = value[i][1];
        /* check that the old value was null */
        if ((value[i][3] !== null) && (value[i][2] === null)) {
            /* inspect all items in row are not empty*/
            var row = instance.getData()[r];
            for (var j = 0; j < row.length; j++) {
                if ((c !== j) && (row[j] === null)) {
                    /* if empty, set new value from the template*/
                    value.push([r, j, null, template[j]]);
                }
            }
            return true;
        }
        else if (value[i][3] === "") return false; // undo if new value is empty
    }
    return true;
}

