$(document).ready(function () {
    var _this = $(this);
    var desc = $("#szoveg input").val();
    var count = $("#szam input").val();
    //console.log(desc);
    $("#szoveg input").val("");
    $("#szam input").val("");
    
    $.ajax({
        url: '/penzTarcas/BuildpenzTarcaTable',
        data: {
            desc: desc,
            count: count
        },
        success: function (result) {
            $('#tableDiv').html(result);
            //$("table tr td#isD").each(function (index) {

            //});
            $("table tr td#osszegC").each(function (index) {
                console.log($("table tr td#isD input:checkbox").eq(index).is(":checked"));
                var box = $("table tr td#isD input:checkbox").eq(index).is(":checked");
                if(!box){
                    $(this).css("background", ($(this).html() * 1 >= 0) ? "green" : "red");
                }
                var checked = $('input:checkbox:checked').length;
                var nonchecked = $('input:checkbox').length;
                $("#checkB").html(checked + " / " + nonchecked);
            });
        }
    });

});
