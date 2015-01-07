var ZIndex = function () { 
    return {
        //main function to initiate the module
        init: function () {  
            $('#style_color').attr("href", "assets/css/themes/light.css");
            $.cookie('style_color', 'light');  
            //$('body').addClass('page-sidebar-closed').removeClass('page-sidebar-hover-on');      
            $("#container_data").load("z_chart_dynamic.html"); 
        } 
    };
}();