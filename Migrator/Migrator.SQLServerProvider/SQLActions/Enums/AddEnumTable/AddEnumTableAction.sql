 enum Level
{
	a = 1,
	b = 2,
	c = 3
}


---->


create table LevelEnum
(
	ID int NOT NULL,
	Value VARCHAR NOT NULL
)


ALTER TABLE LevelEnum
   ADD CONSTRAINT CK_IDVal_LevelEnum   
   UNIQUE (ID, Val),
   CHECK (
		(ID = 1 and Val = 'a') or
		(ID = 2 and Val = 'b' ) or
		(ID = 3 and Val = 'c' )
	  )

INSERT INTO  LevelEnum
SELECT 1, 'a' UNION
SELECT 2, 'b' UNION
SELECT 3, 'c' 