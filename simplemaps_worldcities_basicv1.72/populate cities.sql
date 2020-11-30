use Messaging;
go
set statistics io on;
set statistics time on;
  DECLARE @g geography;   
  declare @u uniqueidentifier;
SET @g = geography::Point(39.3329465208613, -76.6302030351934, 4326)  
SELECT @g.ToString();  


exec Proc_GetClosestRuggers @g, @u

SELECT TOP(7) *, Coordinates.STDistance(@g)
FROM cities
WHERE Coordinates.STDistance(@g) IS NOT NULL  
ORDER BY Coordinates.STDistance(@g);  

/****** Script for SelectTopNRows command from SSMS  ******/
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

  CREATE TABLE SpatialTable2(id int primary key, object GEOGRAPHY);  
CREATE SPATIAL INDEX SIndx_SpatialTable_geography_col1
   ON Cities(coordinates);  