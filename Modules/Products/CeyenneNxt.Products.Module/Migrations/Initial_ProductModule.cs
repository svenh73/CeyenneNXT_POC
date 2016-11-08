using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CeyenneNxt.Core.Constants;
using FluentMigrator;

namespace CeyenneNxt.Products.Module.Migrations
{
  [Migration(20160101010000, "Add product tables")]
  public class Initial_ProductModule : Migration
  {
    public override void Up()
    {
      Create.Schema(SchemaConstants.Products);

      //Reference tables

      Create.Table("Organization").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_OrganizationID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_OrganizationExternalIdentifier")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_OrganizationName")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_OrganizationCode");

      Create.Table("Brand").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_BrandID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_BrandExternalIdentifier")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_BrandName")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_BrandCode");

      Create.Table("Vendor").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_VendorID")
        
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_Vendor", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_VendorName")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_VendorCode");

      Create.Table("Culture").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_CultureID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_CultureExternalIdentifier")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_Culture", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_CultureName")
        .WithColumn("Code").AsString(10).NotNullable()
        .WithColumn("Default").AsBoolean().NotNullable();

      Create.Table("BarcodeType").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_BarcodeTypeID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_BarcodeTypeExternalIdentifier")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_BarcodeType", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_BarcodeTypeName")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_BarcodeTypeCode");

      Create.Table("ProductType").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_ProductTypeID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_ProductTypeExternalIdentifier")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_ProductType", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_ProductTypeName")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_ProductTypeCode");

      Create.Table("RelatedProductType").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_ProductRelationTypeID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_ProductRelationTypeExternalIdentifier")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_ProductRelationType", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("Name").AsString(255).NotNullable().Indexed("IX_ProductRelationTypeName")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_ProductRelationTypeCode");

      //Data tables

      Create.Table("AttributeGroup").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_AttributeGroupID")
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_VendorProductExternalIdentifier")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_AttributeGroup", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("Parent").AsInt32().NotNullable().ForeignKey("FK_AttributeGroup_AttributeGroup", SchemaConstants.Products, "AttributeGroup", "ID").Indexed("IX_Parent")
        .WithColumn("Code").AsString(255).NotNullable().Indexed("IX_AttributeGroupCode")
        .WithColumn("Name").AsString(255).NotNullable();

      Create.Table("Product").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_ProductID")
        
        .WithColumn("BrandID").AsInt32().NotNullable().ForeignKey("FK_Brand_Product", SchemaConstants.Products, "Brand", "ID")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_Product", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("ProductTypeID").AsInt32().NotNullable().ForeignKey("FK_ProductType_Product", SchemaConstants.Products, "ProductType", "ID")
        .WithColumn("ParentProductID").AsInt32().Nullable().ForeignKey("FK_Product_Product", SchemaConstants.Products, "Product", "ID")
        .WithColumn("SKU").AsString(50).NotNullable().Indexed("IX_ProductSKU");

      Create.Table("VendorProduct").InSchema(SchemaConstants.Products)
        .WithColumn("ExternalIdentifier").AsString(50).NotNullable().Indexed("IX_VendorProductExternalIdentifier")
        .WithColumn("ProductID").AsInt32().NotNullable().ForeignKey("FK_Product_VendorProduct", SchemaConstants.Products, "Product", "ID").PrimaryKey("PK_VendorProduct")
        .WithColumn("VendorID").AsInt32().NotNullable().ForeignKey("FK_Vendor_VendorProduct", SchemaConstants.Products, "Vendor", "ID").PrimaryKey("PK_VendorProduct");

      Create.Table("ProductBarcode").InSchema(SchemaConstants.Products)
        .WithColumn("BarcodeTypeID").AsInt32().NotNullable().ForeignKey("FK_BarcodeType_ProductBarcode", SchemaConstants.Products, "BarcodeType", "ID").PrimaryKey("PK_ProductBarcode")
        .WithColumn("ProductID").AsInt32().NotNullable().ForeignKey("FK_Product_ProductBarcode", SchemaConstants.Products, "Product", "ID").PrimaryKey("PK_ProductBarcode")
        .WithColumn("VendorID").AsInt32().NotNullable().ForeignKey("FK_Vendor_ProductBarcode", SchemaConstants.Products, "Vendor", "ID").PrimaryKey("PK_ProductBarcode")
        .WithColumn("Barcode").AsString(50).NotNullable().PrimaryKey("PK_VendorProductBarcode")
        .WithColumn("IsDefault").AsBoolean().NotNullable();

      Create.ForeignKey("FK_VendorProduct_VendorProductBarcode").FromTable("ProductBarcode").InSchema(SchemaConstants.Products).ForeignColumns("ProductID", "VendorID").ToTable("VendorProduct").InSchema(SchemaConstants.Products).PrimaryColumns("ProductID", "VendorID");

      Create.Table("RelatedProduct").InSchema(SchemaConstants.Products)
        .WithColumn("RelatedProductTypeID").AsInt32().NotNullable().ForeignKey("FK_RelatedProductType_RelatedProduct", SchemaConstants.Products, "RelatedProductType", "ID").PrimaryKey("PK_RelatedProduct")
        .WithColumn("ProductID").AsInt32().NotNullable().ForeignKey("FK_Product_RelatedProduct_Parent", SchemaConstants.Products, "Product", "ID").PrimaryKey("PK_RelatedProduct")
        .WithColumn("RelatedProductID").AsInt32().NotNullable().ForeignKey("FK_Product_RelatedProduct_Child", SchemaConstants.Products, "Product", "ID").PrimaryKey("PK_RelatedProduct");


      Create.Table("Attribute").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_AttributeID")
        .WithColumn("OrganizationID").AsInt32().NotNullable().ForeignKey("FK_Organization_Attribute", SchemaConstants.Products, "Organization", "ID").Indexed("IX_OrganizationID")
        .WithColumn("AttributeGroupID").AsInt32().NotNullable().ForeignKey("FK_AttributeGroup_Attribute", SchemaConstants.Products, "AttributeGroup", "ID").Indexed("IX_AttributeGroupID")
        .WithColumn("AttributeType").AsInt32().NotNullable().Indexed("IX_AttributeType")
        .WithColumn("DataType").AsInt32().NotNullable().Indexed("IX_DataType")
        .WithColumn("Code").AsString(10).NotNullable().Indexed("IX_AttributeCode")
        .WithColumn("ValidationExpression").AsString(200);

      Create.Table("Translation").InSchema(SchemaConstants.Products)
        .WithColumn("AttributeID").AsInt32().NotNullable().ForeignKey("FK_Attribute_Translation", SchemaConstants.Products, "Attribute", "ID")
        .WithColumn("CultureID").AsInt32().NotNullable().ForeignKey("FK_Culture_Translation", SchemaConstants.Products, "Culture", "ID")
        .WithColumn("Translation").AsString(255).NotNullable();

      Create.UniqueConstraint("AttributeAndCulture").OnTable("Translation").WithSchema(SchemaConstants.Products).Columns("AttributeID", "CultureID");

      Create.Table("AttributeOption").InSchema(SchemaConstants.Products)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_AttributeOptionID")
        .WithColumn("AttributeID").AsInt32().NotNullable().ForeignKey("FK_Attribute_AttributeOption", SchemaConstants.Products, "Attribute", "ID")
        .WithColumn("Code").AsString(255).NotNullable()
        .WithColumn("Index").AsInt32()
        .WithColumn("Value").AsInt32().NotNullable()
        .WithColumn("Default").AsBoolean();

      Create.UniqueConstraint("AttributeAndCode").OnTable("AttributeOption").WithSchema(SchemaConstants.Products).Columns("AttributeID", "Code");

      Create.Table("ProductAttributeValue").InSchema(SchemaConstants.Products)
        .WithColumn("ProductID").AsInt32().NotNullable().ForeignKey("FK_Product_ProductAttributeValue", SchemaConstants.Products, "Product", "ID").PrimaryKey("PK_ProductAttributeValue")
        .WithColumn("AttributeID").AsInt32().NotNullable().ForeignKey("FK_Attribute_ProductAttributeValue", SchemaConstants.Products, "Attribute", "ID").PrimaryKey("PK_ProductAttributeValue")
        .WithColumn("AttributeOptionID").AsInt32().NotNullable()// Todo:.ForeignKey("FK_AttributeOption_ProductAttributeValue", SchemaConstants.Products, "AttributeOption", "ID").PrimaryKey("PK_ProductAttributeValue")
        .WithColumn("CutureID").AsInt32().NotNullable().ForeignKey("FK_Culture_ProductAttributeValue", SchemaConstants.Products, "Culture", "ID").PrimaryKey("PK_ProductAttributeValue")
        .WithColumn("Value").AsCustom("Text").NotNullable();

    }

    public override void Down()
    {
      Delete.Table("ProductAttributeValue").InSchema(SchemaConstants.Products);
      Delete.Table("ProductAttributeOption").InSchema(SchemaConstants.Products);

      Delete.Table("AttributeOptionValue").InSchema(SchemaConstants.Products);
      Delete.Table("AttributeOption").InSchema(SchemaConstants.Products);

      Delete.Table("AttributeGroupAttribute").InSchema(SchemaConstants.Products);

      Delete.Table("AttributeGroupName").InSchema(SchemaConstants.Products);
      Delete.Table("AttributeGroup").InSchema(SchemaConstants.Products);

      Delete.Table("AttributeName").InSchema(SchemaConstants.Products);
      Delete.Table("Attribute").InSchema(SchemaConstants.Products);

      Delete.Table("RelatedProduct").InSchema(SchemaConstants.Products);
      Delete.Table("VendorProductBarcode").InSchema(SchemaConstants.Products);
      Delete.Table("VendorProduct").InSchema(SchemaConstants.Products);
      Delete.Table("Product").InSchema(SchemaConstants.Products);

      Delete.Table("ProductRelationType").InSchema(SchemaConstants.Products);
      Delete.Table("ProductType").InSchema(SchemaConstants.Products);
      Delete.Table("BarcodeType").InSchema(SchemaConstants.Products);
      Delete.Table("AttributeType").InSchema(SchemaConstants.Products);

      Delete.Table("Language").InSchema(SchemaConstants.Products);
      Delete.Table("Vendor").InSchema(SchemaConstants.Products);
      Delete.Table("Brand").InSchema(SchemaConstants.Products);
      Delete.Table("Organization").InSchema(SchemaConstants.Products);


      Delete.Schema(SchemaConstants.Products);
    }
  }
}
