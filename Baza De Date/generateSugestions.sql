USE [licenta3]
GO
/****** Object:  StoredProcedure [dbo].[GenerateSugestions]    Script Date: 5/29/2022 2:43:14 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER procedure [dbo].[GenerateSugestions] (
    @reteteCautate nvarchar(max),        /* distributor server name */
	@exceptedUser uniqueidentifier
) 
AS
BEGIN

    SET NOCOUNT ON

	create table #reteteCautate (idReteta uniqueidentifier);
	insert into #reteteCautate
	SELECT value FROM STRING_SPLIT(@reteteCautate, ',');

	declare @nrReteteCautate int;
	select @nrReteteCautate = count(idReteta) from #reteteCautate;


	select x.Id, null "DateCreated", null "DateModified" from (
		select IdUser "Id", IdReteta "reteta" from dbo.Apreciere where IdUser != @exceptedUser and Star = 0 group by IdUser, IdReteta having IdReteta in (select * from #reteteCautate)
	) x 
	
	group by x.Id having count(x.reteta) =  @nrReteteCautate;

END
