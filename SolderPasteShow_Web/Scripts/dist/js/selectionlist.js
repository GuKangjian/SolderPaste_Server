(function($) {
    //当下拉框有placeholder需要在closeAllFloatdiv中重新新增placeholder初始化
    $.fn.selectionlist = function(options)
    {
        var oSettings = {
            'id'                                :'id',
            'hidden_id'                         :'hidden_id',
            'default_selection'                 :'',
            'pleaseselect'                      :0,
            'div_height'                        :180,
            'li_height'                         :30,
            'pleaseselect_txt'                  :"请选择",
            'data'                              :[
                                                    {
                                                        "k": "1",
                                                        "v": "test1"
                                                    },
                                                    {
                                                        "k": "2",
                                                        "v": "test2"
                                                    },
                                                    {
                                                        "k": "3",
                                                        "v": "test3"
                                                    }
                                                ],
            'showHiddenValue'                   :"1",
            'afterClick'                        :""
        };
        return this.each(function(){
            if(options)
            {
                oSettings = $.extend(oSettings, options);
            }
            oSettings = initSettings(oSettings);
            initList(oSettings);
            initSelected(oSettings);
            $("#" + oSettings.id).attr("float-on", "false");
            $("#" + oSettings.id).children("input").attr("input-type", "selectionlist");
            
            $("#" + oSettings.id).click(function(event){
                event.stopPropagation();
                $('#' + oSettings.id).find('input.ef').focus();
                if($("#" + oSettings.id).attr("float-on") == "true")
                {
                    listShow(false, oSettings.id);
                }
                else
                {
                    listShow(true, oSettings.id);
                    initSelected(oSettings);
                }
            });
            
        })
        
        function initSettings(oSettings)
        {
            var oPrivateInit = {
                'default_selection'                 :'',
                'pleaseselect'                      :0,
                'pleaseselect_txt'                  :"请选择",
                'showHiddenValue'                   :"1",
                'afterClick'                        :function after(sSelection){return;}
            }
            
            $.each(oPrivateInit, function (i, value) {
                if ('' == oSettings[i]) {
                    oSettings[i] = value;
                }
            });
            return oSettings;
        }
        
        function listShow(p_bShow, p_sId)
        {
            if(p_bShow)
            {
                if(typeof closeAllFloatDiv === "function")
                {
                    closeAllFloatDiv($("#" + p_sId),$("#" + p_sId).parents('.flboxwp,.ln,.c,.box,.l,.e,.clearfix'));
                }
                $("#" + p_sId).addClass("on");
                $("#" + p_sId).parents('.flboxwp,.ln,.c,.box,.l,.e,.clearfix').attr("float-index", "true").css("z-index", "4");
                $("#" + p_sId).attr("float-on", "true");
            }
            else
            {
                if(typeof closeAllFloatDiv === "function")
                {
                    closeAllFloatDiv(null, null);
                }
                $("#" + p_sId).removeClass("on");
                $("#" + p_sId).parents('.flboxwp,.ln,.c,.box,.l,.e,.clearfix').attr("float-index", "false").css("z-index", "");
                $("#" + p_sId).attr("float-on", "false");
            }
        }
        
        function initList(oSettings)
        {
            if(oSettings.pleaseselect == 1)
            {
                $("#" + oSettings.id).children("div.ul").append('<span data-value="" class="li">' + oSettings.pleaseselect_txt + '</span>');
            }
            $.each(oSettings.data, function(key, value){
                $("#" + oSettings.id).children("div.ul").append('<span data-value="' + value.k + '" class="li" title="' + value.v + '">' + value.v + '</span>');
                
            });
            $("#" + oSettings.id).keydown(function(event){
                event.preventDefault();
                var key = event.keyCode;
                var listdiv = $("#" + oSettings.id).find("div.ul");
                var preselected = $("#" + oSettings.id).find("span.li.on");
                if($("#" + oSettings.id).attr("float-on") == "false")
                {
                    return;
                }
                switch(key)
                {
                    case 38:
                        if(preselected.length == 0)
                        {
                            listdiv.scrollTop(listdiv.children().length * oSettings.li_height);
                            $("#" + oSettings.id).find("span.li:last").addClass("on");
                        }
                        else
                        {
                            if(preselected.prev().length == 0)
                            {
                                listdiv.scrollTop(listdiv.children().length * oSettings.li_height);
                                preselected.removeClass("on");
                                $("#" + oSettings.id).find("span.li:last").addClass("on");
                            }
                            else
                            {
                                if(preselected.position().top<0 || preselected.position().top>oSettings.div_height)
                                {
                                    listdiv.scrollTop(preselected.prev().position().top);
                                }
                                if(preselected.prev().position().top<0)
                                {
                                    listdiv.scrollTop(listdiv.scrollTop() - oSettings.li_height);
                                }
                                preselected.removeClass("on").prev().addClass("on");
                            }
                        }
                        break;
                    case 40:
                        if(preselected.length == 0)
                        {
                            listdiv.scrollTop(0);
                            $("#" + oSettings.id).find("span.li:first").addClass("on");
                        }
                        else
                        {
                            if(preselected.next().length == 0)
                            {
                                listdiv.scrollTop(0);
                                preselected.removeClass("on");
                                $("#" + oSettings.id).find("span.li:first").addClass("on");
                            }
                            else
                            {
                                if(preselected.position().top<0 || preselected.position().top>oSettings.div_height)
                                {
                                    listdiv.scrollTop(preselected.next().position().top);
                                }
                                if(preselected.next().position().top>oSettings.div_height)
                                {
                                    listdiv.scrollTop(listdiv.scrollTop() + oSettings.li_height);
                                }
                                preselected.removeClass("on").next().addClass("on");
                            }
                        }
                        break;
                    case 13:
                        if(preselected.length == 0)
                        {
                            return;
                        }
                        else
                        {
                            preselected.click();
                        }
                        break;
                    default:
                        break;
                }
            });
            $("#" + oSettings.id).find("div>span.li").click(function(event){
                event.stopPropagation();
                var key = $(this).attr("data-value");
                $('#' + oSettings.hidden_id).val(key);
                $(this).addClass("on");
                $('#' + oSettings.id).children('input.ef').val($(this).text());
                listShow(false, oSettings.id);
                oSettings.afterClick(key);
            });
            initSelected(oSettings);
        }
        
        function initSelected(oSettings)
        {
            $('#' + oSettings.id).find('div>span.on.li').removeClass("on");
            var sHiddenVal = $("#" + oSettings.hidden_id).val();
            if(sHiddenVal == "")
            {
                if(oSettings.default_selection != "")
                {
                    var $selected = $('#' + oSettings.id).find("div>span[data-value='" + oSettings.default_selection + "']");
                    $selected.addClass("on");
                    $('#' + oSettings.id).children('input.ef').val($selected.text());
                    $('#' + oSettings.hidden_id).val($selected.attr("data-value"));
                }
                else
                {
                    if(oSettings.pleaseselect)
                    {
                        $('#' + oSettings.id).find("div>span[data-value='']").addClass("on");
                        $("#" + oSettings.id).children("input.ef").val(oSettings.pleaseselect_txt);
                    }
                }
            }
            else
            {
                var $selected = $('#' + oSettings.id).find("div>span[data-value='" + sHiddenVal + "']");
                $selected.addClass("on");
                if($selected.length > 0)
                {
                    $selected.parent().scrollTop(0);
                    $selected.parent().scrollTop($selected.position().top);
                }
                if(oSettings.showHiddenValue == "1")
                {
                    $('#' + oSettings.id).children('input.ef').val($selected.text());
                }
            }
        }
    }
})(jQuery);

function unsetList(id)
{
    $("#" + id).find("div.ul").empty();
    $("#" + id).unbind("click");
    $("#" + id).unbind("keydown");
}