-----------------------------------------------------------
-----------------------------------------------------------
--------
--------  Vendor Abbreviation is always defaulted first
--------
-----------------------------------------------------------
-----------------------------------------------------------

-- Long Guns
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Model ID 2 APlus + Model ID 2 Web + Caliber/Gauge + Barrel Length +  Capacity
	-- Item Description 2
	-- Finish + Stock + Sight 
	-- Added for Website 
	-- Barrel Type + Chamber + Misc + Action + Choke

-- Hand Guns
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Model ID 2 APlus + Model ID 2 Web + Caliber/Gauge + Barrel Length +  Capacity
	-- Item Description 2
	-- Action Aplus + Finish Aplus + Misc 1 + Misc 2 + Misc 3
	-- Added for Website 
	-- NONE

-- Ammo
	-- Item Description 1
	-- Vendor Abbreviation + Caliber/Gauge + Grain Weight + Bullet Type + Shell Length + Drams + Weight + Shot Shell Type + Family Name + Count + Misc
	-- Item Description 2
	-- Action Aplus + Finish Aplus + Misc 1 + Misc 2 + Misc 3
	-- Added for Website 
	-- NONE

-- Optics Subclass 00
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Firearm Model ID APlus + Firearm Model ID Web + Tube Diameter + Height + Finish
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 01
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Height + Tube Diameter + Lens Color + Finish + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 02
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Firearm Model ID APlus + Firearm Model ID Web + Height + Tube Diameter + Finish + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 03
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Size + Lense Color + Finish + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 04
	-- Item Description 1
	-- Vendor Abbreviation + Type + Firearm Model ID APlus + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 05
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Power + Reticle Type + Finish
	-- Item Description 2
	-- Misc 1 + Misc 2
	-- Added for Website 
	-- NONE

-- Optics Subclass 06
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Maginification + Color + Finish + Size + Prism + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 07
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Maginification + Color + Finish + Size + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 08
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Maginification + Color + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 09
	-- Item Description 1
	-- Vendor Abbreviation + Type + Firearm Model ID APlus + Firearm Model ID Web + Maginification + Reticle + Finish + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 10
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Type + Color + Misc
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE

-- Optics Subclass 99
	-- Item Description 1
	-- Vendor Abbreviation + Model ID Aplus + Model Web + Misc 1 + Misc 2
	-- Item Description 2
	-- NONE
	-- Added for Website 
	-- NONE






SELECT APlusAttributeValue,*
FROM dbo.ItemAttributes ia
INNER JOIN dbo.Attribute_Lookup al ON ia.AttributeLookup_Id = al.AttributeLookup_Id
WHERE ia.item_id = 30340
SET Item_Description = 
SET AplusDescription1 = 
SET AplusDescription2 = 
Select
CASE WHEN LEFT(itm_num,2) = '01' THEN
		ml.abbrev
	WHEN LEFT(itm_num,2) = '02' THEN	
	WHEN LEFT(itm_num,2) = '03' THEN	
	WHEN LEFT(itm_num,2) = '09' THEN	
		Case WHEN subclass_id = '00' THEN	
			WHEN subclass_id = '01' THEN	
			WHEN subclass_id = '02' THEN	
			WHEN subclass_id = '03' THEN	
			WHEN subclass_id = '04' THEN	
			WHEN subclass_id = '05' THEN	
			WHEN subclass_id = '06' THEN	
			WHEN subclass_id = '07' THEN	
			WHEN subclass_id = '08' THEN	
			WHEN subclass_id = '09' THEN	
			WHEN subclass_id = '10' THEN	
			WHEN subclass_id = '99' THEN	
		END
 END 
 DECLARE @Itemid INT
 SET @Itemid = 30304 -- 30340,43368
 DECLARE @Attributes TABLE(pk INT IDENTITY, item_id INT,attributelookup_id INT,attributeType NVARCHAR(50),categoryclass_id NVARCHAR(5),subclass_id NVARCHAR(5))   
 --INSERT INTO @Attributes
 --        ( item_id ,
 --          attributelookup_id ,
 --          attributeType,
--		   categoryclass_id , 
--		   subclass_id
--         )
 SELECT ia.Item_Id, ia.AttributeLookup_Id
 --,at.AttributeType
 ,cc.Category_Id,sc.SubClass_Id,ido.DescriptionField, ido.SystemType, ido.OrderNumber,ido.attributeType_Id
 FROM ims.dbo.ItemAttributes ia
 INNER JOIN IMS.dbo.Items i ON i.Item_id = ia.Item_Id
 INNER JOIN ims.dbo.Attribute_Lookup al ON ia.AttributeLookup_Id = al.AttributeLookup_Id
-- INNER JOIN IMS.dbo.AttributeType at ON al.AttributeType_Id = at.AttributeType_Id
 INNER JOIN ims.dbo.CategoryClass cc ON cc.CategoryClass_Id = i.CategoryClass_Id
 INNER JOIN IMS.dbo.SubClass sc ON	i.SubClassCode_Id = sc.SubClassCode_Id AND cc.CategoryClass_Id = sc.Category_Id
 LEFT OUTER JOIN IMS.dbo.ItemDescriptionOrder ido on ido.CategoryClass_Id = i.CategoryClass_Id --AND ido.SubClass_Id = ISNULL(ido.SubClass_Id,NULL,i.SubClassCode_Id
 WHERE ia.Item_Id = @ItemId
 --SELECT *
 --FROM @Attributes
 ORDER BY ido.DescriptionField,ido.ordernumber

 DECLARE @Desc1 NVARCHAR(31)
 DECLARE @Desc2 NVARCHAR(31)
 DECLARE @APlus_Attribute NVARCHAR(30)
 DECLARE @Web_Attribute NVARCHAR(30)
 DECLARE @ItemDesc NVARCHAR(1000)
 DECLARE @pk INT, @pkmax INT
 DECLARE @alid int
 
 WHILE @pk <= @pkmax
	BEGIN
		SET @alid = (SELECT attributelookup_id FROM @Attributes WHERE pk = @pk)
		SET @APlus_Attribute = (SELECT APlusAttributeValue from dbo.attribute_Lookup WHERE AttributeLookup_Id = @alid)
		SET @Web_Attribute = (SELECT WebsiteAttributeValue from dbo.attribute_Lookup WHERE AttributeLookup_Id = @alid)
		SET @CategoryClass = (SELECT categoryclass_id FROM @Attributes WHERE pk = @pk)
		SET @SubClass = (SELECT subclass_id FROM @Attributes WHERE pk = @pk)
		CASE @CategoryClass = '01' THEN
			ml.abbrev
			WHEN LEFT(itm_num,2) = '02' THEN	
			WHEN LEFT(itm_num,2) = '03' THEN	
			WHEN LEFT(itm_num,2) = '09' THEN	
				CASE WHEN subclass_id = '00' THEN	
					WHEN subclass_id = '01' THEN	
					WHEN subclass_id = '02' THEN	
					WHEN subclass_id = '03' THEN	
					WHEN subclass_id = '04' THEN	
					WHEN subclass_id = '05' THEN	
					WHEN subclass_id = '06' THEN	
					WHEN subclass_id = '07' THEN	
					WHEN subclass_id = '08' THEN	
					WHEN subclass_id = '09' THEN	
					WHEN subclass_id = '10' THEN	
					WHEN subclass_id = '99' THEN	
				END
		 END 
		SELECT @pk = @pk + 1  
	END  






   SELECT *
   FROM dbo.Attribute_Type


   SELECT *
   FROM storefront.dbo.saattachment
   WHERE att_type = 'url'

   UPDATE storefront.dbo.saattachment
   SET att_usage_status = 'Y'
   FROM storefront.dbo.saattachment
   WHERE att_type='url'


   SELECT *
   FROM dbo.ItemAttributes ia
   INNER JOIN dbo.Attribute_Lookup al ON ia.AttributeLookup_Id = al.AttributeLookup_Id
   INNER JOIN dbo.AttributeType at ON al.AttributeType_Id = at.AttributeType_Id
   WHERE ia.Item_Id = 30340


   SELECT abbrev+



WITH ItemDescription
AS
(
-- Anchor member definition
    SELECT ia.item_id,al.AplusAttributeValue+ ' ' +al.websitevalue AS varchar(1000) as "ItemDescription"
	 FROM dbo.ItemAttributes ia
	   INNER JOIN dbo.Attribute_Lookup al ON ia.AttributeLookup_Id = al.AttributeLookup_Id
	   INNER JOIN dbo.AttributeType at ON al.AttributeType_Id = at.AttributeType_Id
	   WHERE ia.Item_Id = 30340
    UNION ALL
-- Recursive member definition
    SELECT ia.item_id, c1.ctg_id, UPPER(c1.ctg_name) AS ctg_name, c1.ctg_level, c1.ctg_parent,UPPER(CAST((sfc.HeirArchry + '==' + c1.ctg_name) AS VARCHAR(1000))) AS "HeirArchry"
    FROM storefront.dbo.sacategory AS c1
	JOIN SFCategory AS sfc ON c1.ctg_parent = sfc.ctg_id
	
    )
-- Statement that executes the CTE
SELECT * FROM SFCategory
ORDER BY [HeirArchry]
GO
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
SELECT APlusAttributeValue,*
FROM dbo.ItemAttributes ia
INNER JOIN dbo.Attribute_Lookup al ON ia.AttributeLookup_Id = al.AttributeLookup_Id
WHERE ia.item_id = 30340



DECLARE @subclasscodeid INT,@itemid INT, @categoryclassid INT
DECLARE @Desc NVARCHAR(15), @ItemDescription1 NVARCHAR(31), @ItemDescription2 NVARCHAR(31), @ItemDescriptionWeb NVARCHAR(1000), @ST NVARCHAR(15)
DECLARE @WebSiteValue NVARCHAR(50),@APlusValue NVARCHAR(15),@ActualValue NVARCHAR(50)
SET @itemid = 30340
SET @subclasscodeid = (SELECT subclasscode_id FROM IMS.dbo.Items WHERE item_id = @itemid)
SET @categoryclassid = (SELECT CategoryClass_Id FROM IMS.dbo.Items WHERE item_id = @itemid)
DECLARE @pk INT, @pkmax int
DECLARE @IDO TABLE (pk INT IDENTITY,descriptionfield NVARCHAR(50), category_id NVARCHAR(5), subclass_id NVARCHAR(5),AttributeType NVARCHAR(50)
					,systemtype NVARCHAR(15),ordernumber INT, WebsiteValue nvarchar(50),APlusValue nvarchar(50),ActualValue nvarchar(50))
INSERT INTO @IDO
SELECT ido.DescriptionField,cc.Category_Id,sc.SubClass_Id,at.AttributeType,ido.SystemType,ido.OrderNumber,al.WebsiteAttributeValue,al.APlusAttributeValue,ia.ActualAttributeValue
FROM IMS.dbo.ItemDescriptionOrder ido 
INNER JOIN dbo.CategoryClass cc ON ido.CategoryClass_Id = cc.CategoryClass_Id
LEFT OUTER JOIN dbo.SubClass sc ON cc.Category_Id = sc.Category_Id AND ido.SubClassCode_Id = sc.SubClassCode_Id
INNER JOIN IMS.dbo.AttributeType at ON at.AttributeType_Id = ido.AttributeType_Id
INNER JOIN IMS.dbo.Attribute_Lookup al ON ido.AttributeType_Id = al.AttributeType_Id
INNER JOIN IMS.dbo.ItemAttributes ia ON ia.AttributeLookup_Id = al.AttributeLookup_Id
WHERE ido.CategoryClass_Id = @categoryclassid
AND ISNULL(ido.SubClassCode_Id,@subclasscodeid) = @subclasscodeid
AND ia.Item_Id = @itemid
ORDER BY ido.DescriptionField,ido.OrderNumber
SELECT *
FROM @IDO
SET @ItemDescription1 = ''
SET @ItemDescription2 = ''
SET @ItemDescriptionWeb = ''
SET @pk = 1
SET @pkmax = (SELECT COUNT(*) FROM @IDO)
WHILE @pk <= @pkmax
	BEGIN
		SET @Desc = (SELECT DescriptionField FROM @IDO WHERE pk = @pk)
		SET @ST = (SELECT systemtype FROM @IDO WHERE pk = @pk)
		SET @WebSiteValue = (SELECT WebsiteValue FROM @IDO WHERE pk = @pk)
		SET @APlusValue = (SELECT APlusValue FROM @IDO WHERE pk = @pk)
		SET @ActualValue = (SELECT ActualValue FROM @IDO WHERE pk = @pk)
	
		IF @Desc = 'Desc1'
			BEGIN
				SET @ItemDescription1 = @ItemDescription1 + ' ' + CASE WHEN @ST = 'Web' THEN
																		@WebSiteValue
																	WHEN @ST = 'APlus' THEN 
																		@APlusValue
																	WHEN @ST = 'Actual' THEN
																		@ActualValue
																	END                                                                  
			END
		IF @Desc = 'Desc2'
			BEGIN
				SET @ItemDescription2 = @ItemDescription2 + ' ' + CASE WHEN @ST = 'Web' THEN
																		@WebSiteValue
																	WHEN @ST = 'APlus' THEN 
																		@APlusValue
																	WHEN @ST = 'Actual' THEN
																		@ActualValue
																	END                                                                  
			END
		IF @Desc = 'Web'
			BEGIN
				SET @ItemDescriptionWeb = @ItemDescriptionWeb + ' ' + CASE WHEN @ST = 'Web' THEN
																		@WebSiteValue
																	WHEN @ST = 'APlus' THEN 
																		@APlusValue
																	WHEN @ST = 'Actual' THEN
																		@ActualValue
																	END                                                                  
			END
		SELECT @pk = @pk + 1 
	END
SELECT @ItemDescription1 AS id1,@ItemDescription2 AS Id2,@ItemDescriptionWeb AS idw    