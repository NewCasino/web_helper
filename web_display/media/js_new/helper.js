
get_str_from_unix=function (unix_time){
    var date = new Date(parseInt(unix_time));
    date.setHours(date.getHours()+8);
    var result="";
    var month=date.getMonth()+1;
    var day=date.getDay();
    var hour=date.getHours();
    var minute=date.getMinutes();
    var second=date.getSeconds();
    
    var str_year=date.getFullYear()+"";
    var str_month="";
    var str_day="";
    var str_hour="";
    var str_minute="";
    var str_second="";
    
    if(month<10) {str_month="0"+month;} else { str_month=""+month;}
    if(day<10) {str_day="0"+day;} else { str_day=""+day;}
    if(hour<10) {str_hour="0"+hour;} else { str_hour=""+hour;}
    if(minute<10) {str_minute="0"+minute;} else { str_minute=""+minute;}
    if(second<10) {str_second="0"+second;} else { str_second=""+second;}
    result=str_year+"-"+str_month+"-"+str_day+" "+str_hour+":"+str_minute+":"+str_second;
    return result;
}