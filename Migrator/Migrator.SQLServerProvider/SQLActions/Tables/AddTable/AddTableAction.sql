--class Person
--	Animal animalId (+)



------EXAMPLE---------


DECLARE @SQL nvarchar(1000) SET @SQL =  'ALTER Person DROP CONSTRAINT ' + 
												(SELECT NAME FROM  sys.foreign_keys
												 WHERE NAME LIKE 'FK__Person__animalId%')
  
EXEC (@SQL)

ALTER TABLE Person DROP COLUMN animalId





----SCHEMA----------


CREATE TABLE #table# (

ID Int PRIMARY KEY IDENTITY(1,1),
--pole proste 
DECLARE @SQL nvarchar(1000) SET @SQL =  'ALTER #table# DROP CONSTRAINT ' + 
												(SELECT NAME FROM  sys.foreign_keys
												 WHERE NAME LIKE 'FK__#table#__#column#%')
 
EXEC (@SQL)


ALTER TABLE #table# DROP COLUMN #column#