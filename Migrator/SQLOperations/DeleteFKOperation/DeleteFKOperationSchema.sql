DECLARE @SQL nvarchar(1000) SET @SQL =  'ALTER #table# DROP CONSTRAINT ' + 
												(SELECT NAME FROM  sys.foreign_keys
												 WHERE NAME LIKE 'FK__#table#__#column#%')
EXEC (@SQL)