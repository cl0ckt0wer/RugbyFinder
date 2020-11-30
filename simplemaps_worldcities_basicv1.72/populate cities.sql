use Messaging;
go

Insert Cities
SELECT [city]
      ,[city_ascii]
      ,[lat]
      ,[lng]
      ,[country]
      ,[iso2]
      ,[iso3]
      ,[admin_name]
      ,[capital]
      ,[population]
      ,[id]
	  ,geography::Point([lat], [lng], 4326) as Coordinates

  FROM [Messaging].[dbo].[worldcities]

