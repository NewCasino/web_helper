select (select count(*) from trade where website='btcchina') NUM,a.* from trade a
where a.website='btcchina'
and tid =(select max(tid)  from trade where website='btcchina')
 