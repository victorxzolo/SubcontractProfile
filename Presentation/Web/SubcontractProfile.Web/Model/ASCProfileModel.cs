using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubcontractProfile.Web.Model
{
    public class ASCProfileModel
    {
        public string resultCode { get; set; }
        public string resultDescription { get; set; }
        public string moreInfo { get; set; }
        public string outStatus { get; set; }
        public List<resultData> resultData { get; set; }
    }

    public class resultData 
    { 
        public List<LocationListModel> LocationList { get; set; }
    }
    public class LocationListModel
    {
        public string outTitle { get; set; }
        public string outCompanyName { get; set; }
        public string outPartnerName { get; set; }
        public string outCompanyShortName { get; set; }
        public string outTaxId { get; set; }
        public string outWTName { get; set; }
        public string outDistChn { get; set; }
        public string outChnSales { get; set; }
        public string outType { get; set; }
        public string outSubType { get; set; }
        public string outBusinessType { get; set; }
        public string outCharacteristic { get; set; }
        public string outLocationCode { get; set; }
        public string outLocationName { get; set; }
        public string outShopArea { get; set; }
        public string outShopType { get; set; }
        public string outOperatorClass { get; set; }
        public string outLocationPhoneNo { get; set; }
        public string outLocationFax { get; set; }
        public string outContractName { get; set; }
        public string outContractPhoneNo { get; set; }
        public string outLocationStatus { get; set; }
        public string outRetailShop { get; set; }
        public string outBusinessRegistration { get; set; }
        public string outVatType { get; set; }
        public string outEffectiveDt { get; set; }
        public string outIdType { get; set; }
        public string outHQFlag { get; set; }
        public string outChnName { get; set; }
        public string outSAPVendorCode { get; set; }
        public string outMobileForService { get; set; }
        public string outLocationRegion { get; set; }
        public string outLocationSubRegion { get; set; }
        public string outPaymentChannelCode { get; set; }
        public string outPaymentChannelName { get; set; }
        public string outLocationRemark { get; set; }
        public string outBusinessName { get; set; }
        public string outPubid { get; set; }
        public string outLocationAbbr { get; set; }
        public List<AddressLocationListModel> addressLocationList { get; set; }
        public List<SAPCustomerListModel> SAPCustomerList { get; set; }
        public List<ASCListModel> ASCList { get; set; }

        public string location_id { get; set; }
    }

    public class AddressLocationListModel
    {
        public string outAddressID { get; set; }
        public string outAddressType { get; set; }
        public string outHouseNo { get; set; }
        public string outMoo { get; set; }
        public string outMooban { get; set; }
        public string outBuilding { get; set; }
        public string outFloor { get; set; }
        public string outRoom { get; set; }
        public string outSoi { get; set; }
        public string outStreet { get; set; }
        public string outProvince { get; set; }
        public string outAmphur { get; set; }
        public string outTumbol { get; set; }
        public string outZipcode { get; set; }
        public string outCountry { get; set; }
        public string outFullAddress { get; set; }
        public string outAddressLastUpd { get; set; }
    }

    public class AddressASCListModel {
        public string outAddressType { get; set; }
        public string outAddressID { get; set; }
        public string outASCHouseNo { get; set; }
        public string outASCMoo { get; set; }
        public string outASCMooban { get; set; }
        public string outASCBuilding { get; set; }
        public string outASCFloor { get; set; }
        public string outASCRoom { get; set; }
        public string outASCSoi { get; set; }
        public string outASCStreet { get; set; }
        public string outASCProvince { get; set; }
        public string outASCAmphur { get; set; }
        public string outASCTumbol { get; set; }
        public string outASCZipcode { get; set; }
        public string outASCCountry { get; set; }
        public string outASCFullAddress { get; set; }
    }

    public class SAPCustomerListModel
    {
        public string outSAPCode { get; set; }
        public string outSAPAccountGroup { get; set; }
        public string outSAPVendorCode { get; set; }
    }
    public class ASCListModel
    {
        public string outMobileNo { get; set; }
        public string outMemberCategory { get; set; }
        public string outIdCardType { get; set; }
        public string outIdNo { get; set; }
        public string outMemberClass { get; set; }
        public string outMemberReason { get; set; }
        public string outASCCode { get; set; }
        public string outRegion { get; set; }
        public string outSubRegion { get; set; }
        public string outUserId { get; set; }
        public string outPosition { get; set; }
        public string outASCEffectiveDt { get; set; }
        public string outASCTitleThai { get; set; }
        public string outASCTitleEng { get; set; }
        public string outGender { get; set; }
        public string outFirstNameThai { get; set; }
        public string outLastNameThai { get; set; }
        public string outASCPartnerName { get; set; }
        public string outFirstNameEng { get; set; }
        public string outLastNameEng { get; set; }
        public string outNickname { get; set; }
        public string outBirthdate { get; set; }
        public string outMainPhone { get; set; }
        public string outMainSocial { get; set; }
        public string outPinCode { get; set; }
        public string outUserName { get; set; }
        public List<AddressASCListModel> addressASCList { get; set; }

    }
}
