select a.start_time,a.host,a.client,a.website,a.timespan,b.website,b.timespan,
       a.odd_win,a.odd_draw,a.odd_lose,b.odd_win,b.odd_draw,b.odd_lose
from europe_100 a
left join europe_100 b on a.start_time=b.start_time and a.host=b.host and a.client=b.client
where  a.id in  (select max(id) from europe_100 where start_time>'2014-01-01 00:00:00' group by website,start_time,host,client)
and    b.id in  (select max(id) from europe_100 where start_time>'2014-01-01 00:00:00' group by website,start_time,host,client)
and    a.website ='pinnaclesports'
and    b.website='marathonbet' 