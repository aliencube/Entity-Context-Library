CREATE PROCEDURE [dbo].[GetProduct]
    @ProductId AS INT
AS
BEGIN
    SELECT * FROM [Products] WHERE [ProductId] = @ProductId
END
