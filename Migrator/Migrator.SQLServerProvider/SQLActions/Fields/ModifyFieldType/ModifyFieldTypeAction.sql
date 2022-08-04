--Person
--	string val


------>

----Person
--	int val



---->



alter table Person add val_temp int 
go

update Person set val_temp = try_convert(int, val)
go

alter table Person drop column val
go

EXEC sp_rename 'Person.val_temp', 'val', 'COLUMN'
go



-------------------------------------
1) przy konwersji z np VARCHAR na INT część wartości się nie zmieni
2) Aktualnie implementacja pozwala tylko na zmianę typów prostych np, z INT32 na INT64 


