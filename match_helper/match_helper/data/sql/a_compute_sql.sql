--select all data
select  d.name,c.name,a.start_Time,a.team1,a.team2,b.type_id,b.m1,b.m2,b.r1,b.r2,b.r3,b.o1,b.o2,b.o3
from a_event a,a_odd b,a_type c,a_website d
where a.id=b.event_id
and   b.type_id=c.id
and   b.website_id=d.id


--one event have one more website data
select * from a_event a
where  (select distinct count(a_website_id) from a_all b where b.a_event_id=a.id)>1