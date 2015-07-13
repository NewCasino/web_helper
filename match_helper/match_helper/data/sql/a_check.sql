select distinct b.a_event_id,b.total,start_time,team1,team2 
from a_all a,  ( select  count( distinct team1) total ,a_event_id  from a_all group by a_event_id  ) b
where a.a_event_id=b.a_event_id
order by total desc