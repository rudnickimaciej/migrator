IF NOT EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='#table#_#column#' AND XTYPE='U')
    CREATE TABLE #table#_#column#
		(ID INT PRIMARY KEY,
		#table#ID INT FOREIGN KEY REFERENCES #table#(ID),
		Val #sqlType#
		)
GO
