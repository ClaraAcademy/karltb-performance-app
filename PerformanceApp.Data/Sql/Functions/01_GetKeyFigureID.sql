SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE padb;
GO

CREATE OR ALTER FUNCTION padb.ufnGetKeyFigureID(@KeyFigureName NVARCHAR(100))
RETURNS INT
AS
BEGIN
    DECLARE @KeyFigureID INT;

    SELECT @KeyFigureID = KeyFigureID
    FROM padb.KeyFigureInfo
    WHERE KeyFigureName = @KeyFigureName;

    RETURN (@KeyFigureID);
END
GO