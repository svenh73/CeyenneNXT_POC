﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.81.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
public partial class ProductDto : BaseProductDto {
    
    private ProductDto parentField;
    
    private OrganizationDto organizationField;
    
    private BrandDto brandField;
    
    private ProductTypeDto productTypeField;
    
    private VendorProductDto[] vendorProductsField;
    
    /// <remarks/>
    public ProductDto Parent {
        get {
            return this.parentField;
        }
        set {
            this.parentField = value;
        }
    }
    
    /// <remarks/>
    public OrganizationDto Organization {
        get {
            return this.organizationField;
        }
        set {
            this.organizationField = value;
        }
    }
    
    /// <remarks/>
    public BrandDto Brand {
        get {
            return this.brandField;
        }
        set {
            this.brandField = value;
        }
    }
    
    /// <remarks/>
    public ProductTypeDto ProductType {
        get {
            return this.productTypeField;
        }
        set {
            this.productTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("VendorProducts")]
    public VendorProductDto[] VendorProducts {
        get {
            return this.vendorProductsField;
        }
        set {
            this.vendorProductsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class OrganizationDto : BaseNamedDto {
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MediaDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VendorDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrganizationDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseNamedCodeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeGroupDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RelatedProductTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BarcodeTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BrandDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CurrencyTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VATTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MediaTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CultureDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PriceTypeDto))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class BaseNamedDto : BaseDto {
    
    private string nameField;
    
    /// <remarks/>
    public string Name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductBarcodeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(StockDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductPriceDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RelatedProductDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeOptionDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseNamedDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MediaDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VendorDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrganizationDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseNamedCodeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeGroupDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RelatedProductTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BarcodeTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BrandDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CurrencyTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VATTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MediaTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CultureDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PriceTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductAttributeValueDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseProductDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VendorProductDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductDto))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class BaseDto {
    
    private int idField;
    
    private string externalIdentifierField;
    
    /// <remarks/>
    public int ID {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    public string ExternalIdentifier {
        get {
            return this.externalIdentifierField;
        }
        set {
            this.externalIdentifierField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class DescriptionDto {
    
    private CultureDto cultureField;
    
    private string descriptionField;
    
    /// <remarks/>
    public CultureDto Culture {
        get {
            return this.cultureField;
        }
        set {
            this.cultureField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Description {
        get {
            return this.descriptionField;
        }
        set {
            this.descriptionField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CultureDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AttributeGroupDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RelatedProductTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BarcodeTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BrandDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CurrencyTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VATTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(MediaTypeDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CultureDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(PriceTypeDto))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class BaseNamedCodeDto : BaseNamedDto {
    
    private string codeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Code {
        get {
            return this.codeField;
        }
        set {
            this.codeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class AttributeDto : BaseNamedCodeDto {
    
    private int indexField;
    
    private OrganizationDto organizationField;
    
    private AttributeGroupDto attributeGroupField;
    
    private AttributeOptionDto[] optionsField;
    
    private TranslationDto[] translationsField;
    
    private string validationExpressionField;
    
    private AttributeType attributeTypeField;
    
    private AttributeDataType dataTypeField;
    
    /// <remarks/>
    public int Index {
        get {
            return this.indexField;
        }
        set {
            this.indexField = value;
        }
    }
    
    /// <remarks/>
    public OrganizationDto Organization {
        get {
            return this.organizationField;
        }
        set {
            this.organizationField = value;
        }
    }
    
    /// <remarks/>
    public AttributeGroupDto AttributeGroup {
        get {
            return this.attributeGroupField;
        }
        set {
            this.attributeGroupField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Options")]
    public AttributeOptionDto[] Options {
        get {
            return this.optionsField;
        }
        set {
            this.optionsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Translations")]
    public TranslationDto[] Translations {
        get {
            return this.translationsField;
        }
        set {
            this.translationsField = value;
        }
    }
    
    /// <remarks/>
    public string ValidationExpression {
        get {
            return this.validationExpressionField;
        }
        set {
            this.validationExpressionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public AttributeType AttributeType {
        get {
            return this.attributeTypeField;
        }
        set {
            this.attributeTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public AttributeDataType DataType {
        get {
            return this.dataTypeField;
        }
        set {
            this.dataTypeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class AttributeGroupDto : BaseNamedCodeDto {
    
    private OrganizationDto organizationField;
    
    private AttributeGroupDto parentField;
    
    /// <remarks/>
    public OrganizationDto Organization {
        get {
            return this.organizationField;
        }
        set {
            this.organizationField = value;
        }
    }
    
    /// <remarks/>
    public AttributeGroupDto Parent {
        get {
            return this.parentField;
        }
        set {
            this.parentField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class AttributeOptionDto : BaseDto {
    
    private int indexField;
    
    private TranslationDto[] translationsField;
    
    private bool defaultField;
    
    private int valueField;
    
    /// <remarks/>
    public int Index {
        get {
            return this.indexField;
        }
        set {
            this.indexField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Translations")]
    public TranslationDto[] Translations {
        get {
            return this.translationsField;
        }
        set {
            this.translationsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool Default {
        get {
            return this.defaultField;
        }
        set {
            this.defaultField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class TranslationDto {
    
    private bool defaultField;
    
    private CultureDto cultureField;
    
    private string translationField;
    
    /// <remarks/>
    public bool Default {
        get {
            return this.defaultField;
        }
        set {
            this.defaultField = value;
        }
    }
    
    /// <remarks/>
    public CultureDto Culture {
        get {
            return this.cultureField;
        }
        set {
            this.cultureField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Translation {
        get {
            return this.translationField;
        }
        set {
            this.translationField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
public enum AttributeType {
    
    /// <remarks/>
    Value,
    
    /// <remarks/>
    SingleSelect,
    
    /// <remarks/>
    MultiSelect,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
public enum AttributeDataType {
    
    /// <remarks/>
    String,
    
    /// <remarks/>
    Integer,
    
    /// <remarks/>
    Decimal,
    
    /// <remarks/>
    DateTime,
    
    /// <remarks/>
    Boolean,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class RelatedProductTypeDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class BarcodeTypeDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class BrandDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ProductTypeDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CurrencyTypeDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class VATTypeDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class MediaTypeDto : BaseNamedCodeDto {
    
    private TranslationDto[] translationsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Translations")]
    public TranslationDto[] Translations {
        get {
            return this.translationsField;
        }
        set {
            this.translationsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class PriceTypeDto : BaseNamedCodeDto {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ProductBarcodeDto : BaseDto {
    
    private BarcodeTypeDto barcodeTypeField;
    
    private string barcodeField;
    
    /// <remarks/>
    public BarcodeTypeDto BarcodeType {
        get {
            return this.barcodeTypeField;
        }
        set {
            this.barcodeTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Barcode {
        get {
            return this.barcodeField;
        }
        set {
            this.barcodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class StockDto : BaseDto {
    
    private int quantityField;
    
    private StockType stockTypeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int Quantity {
        get {
            return this.quantityField;
        }
        set {
            this.quantityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public StockType StockType {
        get {
            return this.stockTypeField;
        }
        set {
            this.stockTypeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
public enum StockType {
    
    /// <remarks/>
    Internal,
    
    /// <remarks/>
    External,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ProductPriceDto : BaseDto {
    
    private System.DateTime startdateField;
    
    private PriceTypeDto priceTypeField;
    
    private CurrencyTypeDto currencyField;
    
    private VATTypeDto vATField;
    
    private decimal priceField;
    
    private bool includingVATField;
    
    /// <remarks/>
    public System.DateTime Startdate {
        get {
            return this.startdateField;
        }
        set {
            this.startdateField = value;
        }
    }
    
    /// <remarks/>
    public PriceTypeDto PriceType {
        get {
            return this.priceTypeField;
        }
        set {
            this.priceTypeField = value;
        }
    }
    
    /// <remarks/>
    public CurrencyTypeDto Currency {
        get {
            return this.currencyField;
        }
        set {
            this.currencyField = value;
        }
    }
    
    /// <remarks/>
    public VATTypeDto VAT {
        get {
            return this.vATField;
        }
        set {
            this.vATField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Price {
        get {
            return this.priceField;
        }
        set {
            this.priceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool IncludingVAT {
        get {
            return this.includingVATField;
        }
        set {
            this.includingVATField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class RelatedProductDto : BaseDto {
    
    private ProductDto relatedProductField;
    
    private RelatedProductTypeDto relatedProductTypeField;
    
    /// <remarks/>
    public ProductDto RelatedProduct {
        get {
            return this.relatedProductField;
        }
        set {
            this.relatedProductField = value;
        }
    }
    
    /// <remarks/>
    public RelatedProductTypeDto RelatedProductType {
        get {
            return this.relatedProductTypeField;
        }
        set {
            this.relatedProductTypeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ProductAttributeValueDto : BaseDto {
    
    private AttributeDto attributeField;
    
    private AttributeOptionDto attributeOptionField;
    
    private string valueField;
    
    private CultureDto cultureField;
    
    /// <remarks/>
    public AttributeDto Attribute {
        get {
            return this.attributeField;
        }
        set {
            this.attributeField = value;
        }
    }
    
    /// <remarks/>
    public AttributeOptionDto AttributeOption {
        get {
            return this.attributeOptionField;
        }
        set {
            this.attributeOptionField = value;
        }
    }
    
    /// <remarks/>
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
    
    /// <remarks/>
    public CultureDto Culture {
        get {
            return this.cultureField;
        }
        set {
            this.cultureField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlIncludeAttribute(typeof(VendorProductDto))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProductDto))]
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public abstract partial class BaseProductDto : BaseDto {
    
    private ProductAttributeValueDto[] attributeValuesField;
    
    private RelatedProductDto[] relatedProductsField;
    
    private MediaDto[] mediaField;
    
    private ProductBarcodeDto[] barcodesField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AttributeValues")]
    public ProductAttributeValueDto[] AttributeValues {
        get {
            return this.attributeValuesField;
        }
        set {
            this.attributeValuesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("RelatedProducts")]
    public RelatedProductDto[] RelatedProducts {
        get {
            return this.relatedProductsField;
        }
        set {
            this.relatedProductsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Media")]
    public MediaDto[] Media {
        get {
            return this.mediaField;
        }
        set {
            this.mediaField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Barcodes")]
    public ProductBarcodeDto[] Barcodes {
        get {
            return this.barcodesField;
        }
        set {
            this.barcodesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class MediaDto : BaseNamedDto {
    
    private CultureDto cultureField;
    
    private DescriptionDto[] descriptionsField;
    
    private MediaTypeDto mediaTypeField;
    
    private string locationField;
    
    /// <remarks/>
    public CultureDto Culture {
        get {
            return this.cultureField;
        }
        set {
            this.cultureField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Descriptions")]
    public DescriptionDto[] Descriptions {
        get {
            return this.descriptionsField;
        }
        set {
            this.descriptionsField = value;
        }
    }
    
    /// <remarks/>
    public MediaTypeDto MediaType {
        get {
            return this.mediaTypeField;
        }
        set {
            this.mediaTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Location {
        get {
            return this.locationField;
        }
        set {
            this.locationField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class VendorProductDto : BaseProductDto {
    
    private VendorDto vendorField;
    
    private ProductPriceDto[] pricesField;
    
    private StockDto[] stockField;
    
    /// <remarks/>
    public VendorDto Vendor {
        get {
            return this.vendorField;
        }
        set {
            this.vendorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Prices")]
    public ProductPriceDto[] Prices {
        get {
            return this.pricesField;
        }
        set {
            this.pricesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Stock")]
    public StockDto[] Stock {
        get {
            return this.stockField;
        }
        set {
            this.stockField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class VendorDto : BaseNamedDto {
}
