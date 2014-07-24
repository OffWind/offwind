var Settings = {};
(function () {
    Settings.fontFamily = 'Arial';
    Settings.fontColor = '#585868';
    Settings.textMargin = 20;
    Settings.borderColor = '#585868';

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    Utils = {};

    Utils.merge = function (obj1, obj2) {
        var obj3 = {};
        for (var attrname in obj1) {
            obj3[attrname] = obj1[attrname];
        }
        for (var attrname in obj2) {
            obj3[attrname] = obj2[attrname];
        }
        return obj3;
    };

    Utils.fixDateTime = function (obj, propName) {
        try {
            obj[propName] = new Date(parseInt(obj[propName].replace("/Date(", "").replace(")/", ""), 10));
        } catch (e) {
            // Seems it's normal DateTime. Nasty, but works.
        }
    };

    Handlebars.registerHelper('userLink', function (userId, fullName) {
        if (userId) {
            return new Handlebars.SafeString('<a href="/Users/Profile/' + userId + '" target="_blank">' + fullName + '</a>');
        }
        return fullName;
    });

    Handlebars.registerHelper('userUrl', function (userId) {
        return '/Users/Profile/' + userId;
    });

    Handlebars.registerHelper('userPicUrl', function (userId) {
        return '/Files/GetUserPhoto/' + userId;
    });

    Handlebars.registerHelper('GetTypeCategoryValue', function (CategoryId) {
        if (CategoryId == 1) return "Market";
        else if (CategoryId == 2) return "Technology";
        else if (CategoryId == 3) return "Personnel";
        else if (CategoryId == 4) return "Finance";

    });

    Handlebars.registerHelper('GetTypeConsequenceValue', function (ConsequenceId) {
        if (ConsequenceId == 0) return "High";
        else if (ConsequenceId == 1) return "Medium";
        else if (ConsequenceId == 2) return "Low";

    });

    Handlebars.registerHelper('GetTypeProbabilityValue', function (ProbabilityId) {
        if (ProbabilityId == 0) return "High";
        else if (ProbabilityId == 1) return "Medium";
        else if (ProbabilityId == 2) return "Low";

    });

    Handlebars.registerHelper('GetTypeTypeIdValue', function (TypeId) {
        return TypeId == 0 ? "Positive" : "Negative";

    });

    Handlebars.registerHelper('GetApproveIdValue', function (ApproveId) {
        return ApproveId == 1 ? "Yes" : "No";

    });

    Handlebars.registerHelper('GetDateValue', function (Month) {
        if (Month == 1) return "January";
        else if (Month == 2) return "February";
        else if (Month == 3) return "March";
        else if (Month == 4) return "April";
        else if (Month == 5) return "May";
        else if (Month == 6) return "June";
        else if (Month == 7) return "July";
        else if (Month == 8) return "August";
        else if (Month == 9) return "September";
        else if (Month == 10) return "October";
        else if (Month == 11) return "November";
        else if (Month == 12) return "December";

    });
    Handlebars.registerHelper('GetStatusValue', function (StatusId) {
        if (StatusId == 1) return "Planned";
        else if (StatusId == 2) return "Started";
        else if (StatusId == 3) return "Completed";

    });
    Handlebars.registerHelper("dtFromNow", function (dt) {
        if (dt == null) {
            return 'never';
        }
        return moment(dt).fromNow();
    });

    Handlebars.registerHelper("dtMonthYear", function (dt) {
        return moment(dt).format("MMM YYYY");
    });

    Handlebars.registerHelper("dtDayMonthYear", function (dt) {
        return moment(dt).format("DD MMM YYYY");
    });

    Handlebars.registerHelper("dtDate", function (dt) {
        return moment(dt).fromNow();

    });

    Handlebars.registerHelper("stickerCategory", function (catid) {
        if (catid == 1) return "Customer Segment";
        if (catid == 2) return "Solution";
        if (catid == 3) return "Delivery";
        return "Channels";
    });


    Handlebars.registerHelper('GetAverageCost', function (monthlyCosts) {
        var summ = 0;
        for (var i = 0; i < monthlyCosts.length; i++) {
            summ += monthlyCosts[i].Sum;
        }
        var average = summ / monthlyCosts.length;
        return average.toFixed(2);
    });

    $('.panel-collapse').on('hidden.bs.collapse', function () {
        var id = $(this).attr('id');
        $('a[href="#' + id + '"]')
            .find('i')
            .removeClass('icon-sort-up')
            .addClass('icon-sort-down');
    });
    $('.panel-collapse').on('shown.bs.collapse', function () {
        var id = $(this).attr('id');
        $('a[href="#' + id + '"]')
            .find('i')
            .removeClass('icon-sort-down')
            .addClass('icon-sort-up');
    });

    $("[rel=tooltip]").tooltip();

    function toggleSidebar(name, min, max) {
        var duration = 500;
        var sidebar = $('.' + name + '-sidebar');
        var isOut = sidebar.hasClass('out');
        if (isOut) {
            sidebar.animate({ width: max }, duration, function () {
                sidebar.toggleClass('out');
                $('#content').toggleClass(name + '-out');
            });
        } else {
            sidebar.animate({ width: min }, duration, function () {
                sidebar.toggleClass('out');
                $('#content').toggleClass(name + '-out');
            });
        }
    }
    $('.collapse-button a[href="#collapse-left-sidebar"]').on('click', function () {
        toggleSidebar('left', '31px', '190px');
    });

    $('.collapse-button a[href="#collapse-right-sidebar"]').on('click', function () {
        toggleSidebar('right', '8px', '190px');
    });
})();
