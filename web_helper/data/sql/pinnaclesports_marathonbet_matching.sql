
----the match data that two sites's  all matching 
DECLARE @START_DATE NVARCHAR(50)  SET @START_DATE='2014-01-01 00:00:00'
select a.start_time,a.host,a.client,a.website,a.timespan,a.odd_win,a.odd_draw,a.odd_lose,a.persent_return,
       b.website,b.timespan,b.odd_win,b.odd_draw,b.odd_lose,b.persent_return
from europe_100 a
left join europe_100 b on a.start_time=b.start_time and a.host=b.host and a.client=b.client
where  a.id in  (select max(id) from europe_100 where  start_time>@START_DATE group by website,start_time,host,client)
and    b.id in  (select max(id) from europe_100 where  start_time>@START_DATE group by website,start_time,host,client)
and    a.website ='pinnaclesports'
and    b.website='marathonbet' 
order by a.start_time


----the count that two sites's distinct log 
--DECLARE @START_DATE NVARCHAR(50)   SET @START_DATE='2014-01-01 00:00:00' 
select count(*) from
(
	select distinct website,start_time,host,client,odd_win,odd_draw,odd_lose
	from  europe_100_log 
	where f_start_time>@START_DATE  and timespan >@START_DATE and website='pinnaclesports'
) a
union all
select count(*) from
(
	select distinct website,start_time,host,client,odd_win,odd_draw,odd_lose
	from  europe_100_log 
	where f_start_time>@START_DATE and timespan >@START_DATE and website='marathonbet'
) b

----the count that two sites team identification persent
--DECLARE @START_DATE NVARCHAR(50)   SET @START_DATE='2014-01-01 00:00:00' 
select website, (case f_state when '1'  then 0 when '2'  then 1  when '4'  then 2 else -1 end) state ,count(*) qty
from  europe_100_log
where f_start_time>@START_DATE and website in ('pinnaclesports','marathonbet')
group by website, (case f_state when '1'  then 0 when '2'  then 1  when '4'  then 2 else -1 end)
order by website, (case f_state when '1'  then 0 when '2'  then 1  when '4'  then 2 else -1 end)



