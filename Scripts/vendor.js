$(document).ready(function () {
    
    var setupRaty = function() {
        //raty
        $(".process-raty").each(function() {
            var scoreName = $(this).attr("data-scorename") == null ? "score" : $(this).attr("data-scorename");
            var target = $(this).attr("data-target");
            var score = $(this).attr("data-score") == 0 ? "" : $(this).attr("data-score");
            var readonly = $(this).attr("data-readonly");
            $(this).raty({
                scoreName: scoreName,
                target: target,
                score: score,
                readOnly: readonly,
                hints: ["Poor", "Bad", "Average", "Good", "Awesome"],
                mouseover: function(score, evt) {
                    if (target != null) {
                        $(this).children("img").removeAttr("title");
                    }
                }
            });


        });
    };
    setupRaty();
    //some global ajax events
    $(document).ajaxComplete(function () {
        setupRaty();
    });

});