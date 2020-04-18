-- exec [sp_UpdateCustomer]	3, 'John Doe', 'Brisbane', '2020-01-21 09:30 AM'
CREATE PROCEDURE [dbo].[sp_UpdateCustomer]
	@Id int,
	@Name nvarchar(50),
	@City nvarchar(50) = NULL,
	@UpdatedDate datetime,
	@Result int = NULL OUTPUT
AS        
BEGIN        
	SET NOCOUNT ON;     
	
	BEGIN TRY
		BEGIN TRAN
			UPDATE dbo.Customer
			SET  
				[Name] = @Name,
				City = @City,
				UpdatedDate = @UpdatedDate
			WHERE 
				Id = @Id	
			
			SET @Result  = @@ROWCOUNT; -- SUCCESS
		COMMIT TRAN;
	END TRY
	
	BEGIN CATCH
		 SET @Result = 0 --ERROR
		 ROLLBACK TRAN;
	END CATCH
	
	SELECT @Result AS Result
END