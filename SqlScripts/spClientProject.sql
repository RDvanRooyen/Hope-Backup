SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        mmandych
-- Create date: Nov 2020
-- Description:    Fetch data for example project by client report, optionally for only one client
-- =============================================
CREATE PROCEDURE [dbo].[spClientProject]
    @clientIdParam int
    ,@includeAllClientsParam bit
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    DECLARE @clientId int = @clientIdParam
    DECLARE @includeAllClients bit = @includeAllClientsParam

    SELECT 
        c.Name AS ClientName, 
        p.Name AS ProjectName, 
        p.StartDate AS StartDate, 
        p.EndDate AS EndDate,
        p.EstimatedRevenue AS EstimatedRevenue
    FROM
        Projects p
        LEFT JOIN 
        Clients c 
        ON p.ClientId = c.ClientId
    WHERE 
        @includeAllClients = 1 
        OR 
        (@clientId IS NULL OR @clientId = c.ClientId)
    ORDER BY 
        c.Name, p.Name
END
GO