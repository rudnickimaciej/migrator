
alter table #table
alter column #fieldName #newFieldType



-------------------------------------
1) Będą lecieć błędy przy konwersji jeśli ktoś zmieni typ z np. INT na VARCHAR, a  w tabeli będą już jakieś dane 
2) Aktualnie implementacja pozwala tylko na zmianę typów prostych np, z INT32 na INT64 


