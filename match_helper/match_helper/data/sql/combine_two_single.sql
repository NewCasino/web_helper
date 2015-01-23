select a.sport,a.country,a.competition,a.start_time,a.home,a.away,
       (select odd from s_mbook_market where event_id=a.event_id and market='Match Odds' and runner=a.home and type='back') HOME1,
              (select odd from s_mbook_market where event_id=a.event_id and market='Match Odds' and runner<>a.home and runner<>a.away and type='back') DRAW,
       (select odd from s_mbook_market where event_id=a.event_id and market='Match Odds' and runner=a.away and type='back') AWAY1,
       (select o1 from  s_mb_odds where event_id=b.event_id and bet_type='line') HOME2,
       (select o2 from  s_mb_odds where event_id=b.event_id and bet_type='line') DRAW2,
       (select o3 from  s_mb_odds where event_id=b.event_id and bet_type='line') AWAY2
from  s_mbook_events a,s_mb_events b
where a.home=b.home and a.away=b.away
order by a.start_time