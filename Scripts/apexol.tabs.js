(function ($) {
    $.fn.apexolTabs = function () {
        var defaults = { beforeClick: function (index) { return true; }, afterClick: function (index) { return true; }, menuBehaviour: false, clickedMenu: false }

        // extend the options from pre-defined values:
        var options = $.extend(defaults, arguments[0] || {});

        return this.each(function () {

            if ($(this).prop("tagName") != 'UL' && $(this).prop("tagName") != 'ul') {
                return;
            }
            if ($(this).data("apexol_tab_done")) {
                return;
            }
            var tab_panes = new Array();
            var index = 0;
            var ul = $(this);

            ul.children("li").first().addClass("first-tab-item");
            ul.children("li").last().addClass("last-tab-item");

            ul.children("li").children("a").each(function () {

                $($(this).attr("href")).hide(); //hide all divs                
                tab_panes[index++] = $(this).attr("href");

                var arrow = $("<span class='arrow_bottom'></span>");
                var item = $(this).closest("li");
                item.append(arrow);
                arrow.css("top", item.outerHeight());    //append span to all

                $(this).click(function (e) {
                    var result = options.beforeClick.call(this, $(this).parent().index());
                    if (!result)
                        return result;
                    for (var i = 0; i < tab_panes.length; i++) {
                        $(tab_panes[i]).hide();
                    }
                    if ($(this).attr("data-url") != null) {
                        var url = $(this).attr("data-url");
                        var target_container = $($(this).attr("href"));
                        $.ajax({
                            url: url,
                            type: "GET",
                            success: function (data) {
                                target_container.html(data);
                                target_container.show();
                            }
                        });
                    }
                    else {
                        $($(this).attr("href")).show();
                    }
                    ul.children("li").removeClass("current");
                    item.addClass("current");
                    options.afterClick.call(this, $(this).parent().index());
                    e.preventDefault();
                });

                if (options.menuBehaviour && !options.clickedMenu) {
                    $(this).mouseenter(function (e) {
                        for (var i = 0; i < tab_panes.length; i++) {
                            $(tab_panes[i]).hide();
                        }
                        $($(this).attr("href")).show();
                        ul.children("li").removeClass("current");
                        item.addClass("current");
                        e.preventDefault();
                    });
                }

            });
            if ($(this).children("li.current").length == 0) {
                $(this).children("li:first-child").addClass("current");
            }
            if (!options.menuBehaviour)
                $(ul.children("li.current").children("a").first().attr("href")).show();
            ul.data("apexol_tab_done", true);
            ul.find("a").first().trigger("click");
        });
    }
})(jQuery);