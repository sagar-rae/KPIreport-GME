USE [HtmlTable]
GO
/****** Object:  StoredProcedure [dbo].[HTMLTableSP]    Script Date: 6/17/2022 10:43:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[HTMLTableSP]
@Id INT = NULL,
@Username NVARCHAR(50) = NULL,
@Name NVARCHAR(50) = NULL,
@Nationality NVARCHAR(50) = NULL,
@Registration INT =NULL,
@GMELoan INT =NULL,
@SimCard INT =NULL,
@GmePass INT =NULL,
@IssueSolved INT =NULL,
@Other INT =NULL,
@DateSelected DATE =NULL,
@DateSelectedLast DATE=NULL,
@Sn INT =NULL,
@Flag NVARCHAR(50)=NULL,
@FlagforUp NVARCHAR(50)=NULL,
@TableValue NVARCHAR(50)=NULL
--@Result NVARCHAR(50)=NULL
AS
BEGIN

IF @Flag='Show'
BEGIN
IF OBJECT_ID('tempdb.dbo.#test') IS NOT NULL 
DROP TABLE #test;
IF @TableValue=0
BEGIN
SELECT 
Id,
Sn,
DateSelected,
username,
name,
nationality,
registration,
gmeloan,
simcard,
gmepass,
IssueSolved,
other,
(registration*1+gmeloan*1+simcard*0.5+gmepass*1+IssueSolved*0.25+other*1) se

INTO #test
FROM EmployeeDetails(NOLOCK) WHERE DateSelected=@DateSelected
SELECT * ,(SELECT CAST(SUM(se) AS FLOAT)/ CAST (COUNT(NAME) AS FLOAT) FROM #test t2  WHERE t1.sn = t2.sn GROUP BY t2.sn) be
FROM #test t1
ORDER BY sn
RETURN
END

SELECT Id,
Sn,
DateSelected,
username,
name,
nationality,
registration,
gmeloan,
simcard,
gmepass,
IssueSolved,
other,
(registration*1+gmeloan*1+simcard*0.5+gmepass*1+IssueSolved*0.25+other*1) se

INTO #test1
FROM EmployeeDetails(NOLOCK) WHERE Sn=@TableValue AND DateSelected=@DateSelected
SELECT * ,
(SELECT CAST(SUM(se) AS FLOAT)/ CAST(COUNT(NAME) AS FLOAT) FROM #test1 t2  WHERE t1.sn = t2.sn GROUP BY t2.sn) be
FROM #test1 t1
ORDER BY sn
END

IF @Flag='Update'
BEGIN
UPDATE EmployeeDetails 
SET
Registration=@Registration,
GMELoan=@GMELoan, 
SimCard=@SimCard,
GmePass=@GmePass,
IssueSolved=@IssueSolved,
Other=@Other
WHERE Id=@Id

IF @@ERROR = 0
BEGIN 
	SELECT 0 ErrorCode,'UPDATED SUCCESSFULLY'Msg;
	RETURN
END

ELSE
BEGIN
	SELECT 1 ErrorCode,'UPDATED FAILED'Msg;
	RETURN
END

END
END
