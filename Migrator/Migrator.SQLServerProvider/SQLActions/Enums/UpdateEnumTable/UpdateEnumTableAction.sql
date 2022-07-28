enum Level
{
	a = 1,
	b = 2,
	c = 3
}

----->

enum Level
{
	b = 4,
	c = 5,
	e = 6
}

---->

TRUNCATE TABLE LevelEnum

ALTER TABLE LevelEnum   

DROP CONSTRAINT CHK_ColumnD_DocExc;  
DECLARE @SQL nvarchar(1000) SET @SQL =  'ALTER TABLE LevelEnum DROP CONSTRAINT ' + 
												(SELECT NAME FROM  sys.check_constraints
												 WHERE NAME LIKE 'CK__LevelEnum%')
EXEC (@SQL)


   ALTER TABLE LevelEnum
   ADD CONSTRAINT CK_IDVal_LevelEnum   
   CHECK (
		(ID = 1 and Val = 'a') or
		(ID = 2 and Val = 'b' ) or
		(ID = 3 and Val = 'c' )
	  )
GO  

create table LevelEnum
(
	ID int NOT NULL,
	Value VARCHAR NOT NULL, 
	unique (ID, Val),
	  check (
		(ID = 1 and Val = 'a') or
		(ID = 2 and Val = 'b' ) or
		(ID = 3 and Val = 'c' )
	  )
)

INSERT INTO  LevelEnum
SELECT 1, 'a' UNION
SELECT 2, 'b' UNION
SELECT 3, 'c' 