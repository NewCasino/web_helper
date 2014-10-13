
----the match data that two sites's  all matching 
DECLARE @START_DATE NVARCHAR  SET @START_DATE='2014-01-01 00:00:00'
select a.start_time,a.host,a.client,a.website,a.timespan,a.odd_win,a.odd_draw,a.odd_lose,
       b.website,b.timespan,b.odd_win,b.odd_draw,b.odd_lose
from europe_100 a
left join europe_100 b on a.start_time=b.start_time and a.host=b.host and a.client=b.client
where  a.id in  (select max(id) from europe_100 where  start_time>@START_DATE group by website,start_time,host,client)
and    b.id in  (select max(id) from europe_100 where  start_time>@START_DATE group by website,start_time,host,client)
and    a.website ='pinnaclesports'
and    b.website='marathonbet' 


----the count that two sites's distinct log 
DECLARE @START_DATE NVARCHAR  SET @START_DATE='2014-01-01 00:00:00' 
select count(*) from
(
	select distinct website,start_time,host,client,odd_win,odd_draw,odd_lose
	from  europe_100_log 
	where f_start_time>@START_DATE and website='pinnaclesports'
) a
union all
select count(*) from
(
	select distinct website,start_time,host,client,odd_win,odd_draw,odd_lose
	from  europe_100_log 
	where f_start_time>@START_DATE and website='marathonbet'
) b



