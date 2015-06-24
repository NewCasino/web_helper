select  id,timespan,website,start_time,host,client,odd_win,odd_draw,odd_lose,time_zone,time_add,f_league,f_start_time,f_host,f_client,'' group_id,
        ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00),2)  persent_return,
        ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_win),2) persent_win,
        ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_draw),2) persent_draw,
        ROUND(((1 / (1 /CONVERT(float,odd_win) + 1 /CONVERT(float,odd_draw) + 1 / CONVERT(float,odd_lose))) * 100.00)/CONVERT(float,odd_lose),2) persent_lose       
from europe_100_log
where ISNUMERIC(odd_win)=1
and   ISNUMERIC(odd_draw)=1
and   ISNUMERIC(odd_lose)=1 
and   id in (select max(id) from europe_100_log group by website,start_time,host,client)
 