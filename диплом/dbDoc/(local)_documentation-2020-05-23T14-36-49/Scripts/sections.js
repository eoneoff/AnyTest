var _sectionsOnLoad = function(window, $) {

    $(".panel-collapsible").each(function() {
        var section = $(this);
        var heading = section.children(".panel-heading");
        heading.click(function(evt) {
            if (!$(evt.target).is("div,a,img")) return;

            section.toggleClass("panel-collapsed");

            $(this).find(".expanded").toggle();
            $(this).find(".collapsed").toggle();
        });
    });

    /*var listInDictContains = function(dict, key, value) {
        if (key in dict) {
            var found = false;
            $.each(dict[key], function(k, v) {
                if (v === value) {
                    found = true;
                    return false; // break
                }
            });
            return found;
        }
        return false;
    };*/

    var addToListInDict = function(dict, key, value) {
        if (key in dict) {
            dict[key].push(value);
        } else {
            dict[key] = [value];
        }
    };

    var getListFromDict = function(dict, key) {
        if (key in dict) return dict[key];
        return [];
    };

    $(".table").each(function() {
        var table = $(this);
        var sections = {};
        table.find(".tablerow").each(function() {
            var toggledBy = ($(this).data("toggledby") || "").split(" ");
            var tr = $(this);
            $.each(toggledBy,
                function(i, toggledByv) {
                    if (toggledByv != "") {
                        addToListInDict(sections, toggledByv, tr);
                    }
                });
        });
        table.find(".tablerow").each(function() {
            var toggles = ($(this).data("toggles") || "").split(" ");
            var rowsToggledByThisRow = [];
            $.each(toggles,
                function(i, togglesv) {
                    if (togglesv != "") {
                        $.merge(rowsToggledByThisRow, getListFromDict(sections, togglesv));
                    }
                });
            if ($(this).data("toggledby")) {
                $(this).hide();
            }
            if (rowsToggledByThisRow.length) {
                // TODO: figure out why this seems to be getting attached twice
                $(this).unbind("click").on("click",
                    function(evt) {
                        if (!$(evt.target).is("img,.tablerow,.tablecell")) return;

                        var expandedClass = ".expanded,.expanded_small";
                        var collapsedClass = ".collapsed,.collapsed_small";

                        $(this).find(expandedClass).toggle();
                        $(this).find(collapsedClass).toggle();

                        var isVisible = $(this).find(expandedClass).is(":visible");

                        $.each(rowsToggledByThisRow,
                            function(i, rowToggled) {
                                rowToggled.toggle(isVisible);

                                $(this).find(expandedClass).toggle(isVisible);
                                $(this).find(collapsedClass).toggle(!isVisible);
                            });
                    });

                $(this).find(".sectionToggle input").each(function() {
                    var toggle = this;
                    var updateRowDeselectedState = function() {
                        var isVisible = toggle.checked;
                        $.each(rowsToggledByThisRow,
                            function(i, rowToggled) {
                                rowToggled.find(".tablecell").toggleClass("deselected", !isVisible);
                            });
                    };
                    updateRowDeselectedState();
                    $(toggle).unbind("change").on("change", updateRowDeselectedState);
                });
            }
        });
    });

};

var sectionsOnLoad = function() {
    _sectionsOnLoad(window, jQuery);
};

if (window.addEventListener) // W3C standard
{
    window.addEventListener("load", sectionsOnLoad, false); // NB **not** 'onload'
} else if (window.attachEvent) // Microsoft
{
    window.attachEvent("onload", sectionsOnLoad);
}