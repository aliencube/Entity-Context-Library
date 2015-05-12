CREATE PROCEDURE [dbo].[AddProduct]
    @Name           AS NVARCHAR(MAX),
    @Description    AS NVARCHAR(MAX),
    @Price          AS DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO [Products] ([Name], [Description], [Price]) VALUES (@Name, @Description, @Price)
END
