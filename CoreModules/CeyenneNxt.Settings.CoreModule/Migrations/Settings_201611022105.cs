using CeyenneNxt.Core.Constants;
using FluentMigrator;

namespace CeyenneNxt.Settings.CoreModule.Migrations
{
  [Migration(201611022105, "Add setting and settingvalue table")]
  public class Settings_201611022105 : Migration
  {
    public override void Up()
    {
      Create.Schema(SchemaConstants.Default);

      Create.Table("Setting").InSchema(SchemaConstants.Default)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_Setting")
        .WithColumn("Name").AsString(100).NotNullable()
        .WithColumn("Domain").AsString(500).Nullable()
        .WithColumn("DataType").AsInt32()
        .WithColumn("SettingType").AsInt32()
        .WithColumn("Active").AsBoolean();

      Create.Table("Channel").InSchema(SchemaConstants.Default)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_Channel")
        .WithColumn("Name").AsString(100).NotNullable();

      Create.Table("Vendor").InSchema(SchemaConstants.Default)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_Vendor")
        .WithColumn("Name").AsString(100).NotNullable();

      Create.Table("SettingValue").InSchema(SchemaConstants.Default)
        .WithColumn("ID").AsInt32().Identity().PrimaryKey("PK_SettingValue")
        .WithColumn("SettingID").AsInt32().NotNullable().ForeignKey("FK_Setting_SettingValue", SchemaConstants.Default, "Setting", "ID")
        .WithColumn("VendorID").AsInt32().Nullable().ForeignKey("FK_Setting_Vendor", SchemaConstants.Default, "Vendor", "ID")
        .WithColumn("ChannelID").AsInt32().Nullable().ForeignKey("FK_Setting_Channel", SchemaConstants.Default, "Channel", "ID")
        .WithColumn("EnvironmentType").AsInt32().Nullable()
        .WithColumn("Value").AsString(500);
    }

    public override void Down()
    {
      Delete.Table("SettingValue").InSchema(SchemaConstants.Default);

      Delete.Table("Channel").InSchema(SchemaConstants.Default);

      Delete.Table("Vendor").InSchema(SchemaConstants.Default);

      Delete.Table("Setting").InSchema(SchemaConstants.Default);

      Delete.Schema(SchemaConstants.Default);
    }
  }
}
