SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER PROCEDURE padb.uspAddBenchmark
    @PortfolioName NVARCHAR(100) = NULL,
    @BenchmarkName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PortfolioID INT;
    EXEC padb.uspGetPortfolioID
        @PortfolioName = @PortfolioName,
        @PortfolioID = @PortfolioID OUTPUT;

    DECLARE @BenchmarkID INT;
    EXEC padb.uspGetPortfolioID
        @PortfolioName = @BenchmarkName,
        @PortfolioID = @BenchmarkID OUTPUT;

    BEGIN TRANSACTION;
        INSERT INTO padb.Benchmark(PortfolioID, BenchmarkID)
        VALUES (@PortfolioID, @BenchmarkID);
    COMMIT;
END
GO