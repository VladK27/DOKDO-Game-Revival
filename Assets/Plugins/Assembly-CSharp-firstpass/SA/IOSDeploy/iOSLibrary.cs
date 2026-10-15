using System.ComponentModel;

namespace SA.IOSDeploy
{
	public enum iOSLibrary
	{
		[Description("CarrierBundleUtilities.tbd")]
		CarrierBundleUtilities = 0,
		[Description("IOABPLib.tbd")]
		IOABPLib = 1,
		[Description("libAccessibility.tbd")]
		libAccessibility = 2,
		[Description("libacmobileshim.tbd")]
		libacmobileshim = 3,
		[Description("libafc.tbd")]
		libafc = 4,
		[Description("libamsupport.tbd")]
		libamsupport = 5,
		[Description("libAppPatch.tbd")]
		libAppPatch = 6,
		[Description("libarchive.2.tbd")]
		libarchive2 = 7,
		[Description("libarchive.tbd")]
		libarchive = 8,
		[Description("libARI.tbd")]
		libARI = 9,
		[Description("libARIServer.tbd")]
		libARIServer = 10,
		[Description("libassertion_launchd.tbd")]
		libassertion_launchd = 11,
		[Description("libate.tbd")]
		libate = 12,
		[Description("libauthinstall.tbd")]
		libauthinstall = 13,
		[Description("libAWDProtobufBluetooth.tbd")]
		libAWDProtobufBluetooth = 14,
		[Description("libAWDProtobufFacetime.tbd")]
		libAWDProtobufFacetime = 15,
		[Description("libAWDProtobufFacetimeiMessage.tbd")]
		libAWDProtobufFacetimeiMessage = 16,
		[Description("libAWDProtobufGCK.tbd")]
		libAWDProtobufGCK = 17,
		[Description("libAWDProtobufLocation.tbd")]
		libAWDProtobufLocation = 18,
		[Description("libAWDSupport.tbd")]
		libAWDSupport = 19,
		[Description("libAWDSupportFramework.tbd")]
		libAWDSupportFramework = 20,
		[Description("libAXSafeCategoryBundle.tbd")]
		libAXSafeCategoryBundle = 21,
		[Description("libAXSpeechManager.tbd")]
		libAXSpeechManager = 22,
		[Description("libBasebandManager.tbd")]
		libBasebandManager = 23,
		[Description("libBasebandUSB.tbd")]
		libBasebandUSB = 24,
		[Description("libbsm.0.tbd")]
		libbsm0 = 25,
		[Description("libbsm.tbd")]
		libbsm = 26,
		[Description("libbz2.1.0.tbd")]
		libbz210 = 27,
		[Description("libbz2.tbd")]
		libbz2 = 28,
		[Description("libc++.1.tbd")]
		libcPlusPlus1 = 29,
		[Description("libc++.tbd")]
		libcPlusPlus = 30,
		[Description("libc++abi.tbd")]
		libcPlusPlusAbi = 31,
		[Description("libc.tbd")]
		libc = 32,
		[Description("libcharset.1.0.0.tbd")]
		libcharset100 = 33,
		[Description("libcharset.1.tbd")]
		libcharset1 = 34,
		[Description("libcharset.tbd")]
		libcharset = 35,
		[Description("libChineseTokenizer.tbd")]
		libChineseTokenizer = 36,
		[Description("libcmph.tbd")]
		libcmph = 37,
		[Description("libcompression.tbd")]
		libcompression = 38,
		[Description("libcoretls.tbd")]
		libcoretls = 39,
		[Description("libcoretls_cfhelpers.tbd")]
		libcoretls_cfhelpers = 40,
		[Description("libCRFSuite.tbd")]
		libCRFSuite = 41,
		[Description("libCRFSuite0.12.tbd")]
		libCRFSuite012 = 42,
		[Description("libCTLogHelper.tbd")]
		libCTLogHelper = 43,
		[Description("libcupolicy.tbd")]
		libcupolicy = 44,
		[Description("libcurses.tbd")]
		libcurses = 45,
		[Description("libdbm.tbd")]
		libdbm = 46,
		[Description("libDHCPServer.A.tbd")]
		libDHCPServerA = 47,
		[Description("libDHCPServer.tbd")]
		libDHCPServer = 48,
		[Description("libdl.tbd")]
		libdl = 49,
		[Description("libdns_services.tbd")]
		libdns_services = 50,
		[Description("libdscsym.tbd")]
		libdscsym = 51,
		[Description("libedit.2.tbd")]
		libedit2 = 52,
		[Description("libedit.3.0.tbd")]
		libedit30 = 53,
		[Description("libedit.3.tbd")]
		libedit3 = 54,
		[Description("libedit.tbd")]
		libedit = 55,
		[Description("libenergytrace.tbd")]
		libenergytrace = 56,
		[Description("libETLDIAGLoggingDynamic.tbd")]
		libETLDIAGLoggingDynamic = 57,
		[Description("libETLDLFDynamic.tbd")]
		libETLDLFDynamic = 58,
		[Description("libETLDLOADCoreDumpDynamic.tbd")]
		libETLDLOADCoreDumpDynamic = 59,
		[Description("libETLDLOADDynamic.tbd")]
		libETLDLOADDynamic = 60,
		[Description("libETLDMCDynamic.tbd")]
		libETLDMCDynamic = 61,
		[Description("libETLDynamic.tbd")]
		libETLDynamic = 62,
		[Description("libETLEFSDumpDynamic.tbd")]
		libETLEFSDumpDynamic = 63,
		[Description("libETLSAHDynamic.tbd")]
		libETLSAHDynamic = 64,
		[Description("libETLTransportDynamic.tbd")]
		libETLTransportDynamic = 65,
		[Description("libexslt.0.tbd")]
		libexslt0 = 66,
		[Description("libexslt.tbd")]
		libexslt = 67,
		[Description("libextension.tbd")]
		libextension = 68,
		[Description("libform.5.4.tbd")]
		libform54 = 69,
		[Description("libform.tbd")]
		libform = 70,
		[Description("libgcc_s.1.tbd")]
		libgcc_s1 = 71,
		[Description("libgermantok.tbd")]
		libgermantok = 72,
		[Description("libH5Dynamic.tbd")]
		libH5Dynamic = 73,
		[Description("libHDLCDynamic.tbd")]
		libHDLCDynamic = 74,
		[Description("libheimdal-asn1.tbd")]
		libheimdal_asn1 = 75,
		[Description("libiconv.2.4.0.tbd")]
		libiconv240 = 76,
		[Description("libiconv.2.tbd")]
		libiconv2 = 77,
		[Description("libiconv.tbd")]
		libiconv = 78,
		[Description("libicucore.A.tbd")]
		libicucoreA = 79,
		[Description("libicucore.tbd")]
		libicucore = 80,
		[Description("libInFieldCollection.tbd")]
		libInFieldCollection = 81,
		[Description("libinfo.tbd")]
		libinfo = 82,
		[Description("libIOAccessoryManager.tbd")]
		libIOAccessoryManager = 83,
		[Description("libipconfig.tbd")]
		libipconfig = 84,
		[Description("libipsec.A.tbd")]
		libipsecA = 85,
		[Description("libipsec.tbd")]
		libipsec = 86,
		[Description("libktrace.tbd")]
		libktrace = 87,
		[Description("liblangid.tbd")]
		liblangid = 88,
		[Description("libLLVM-C.tbd")]
		libLLVM_C = 89,
		[Description("libLLVM.tbd")]
		libLLVM = 90,
		[Description("liblockdown.tbd")]
		liblockdown = 91,
		[Description("liblizma.5.tbd")]
		liblizma5 = 92,
		[Description("liblizma.tbd")]
		liblizma = 93,
		[Description("libm.tbd")]
		libm = 94,
		[Description("libmarisa.tbd")]
		libmarisa = 95,
		[Description("libMatch.1.tbd")]
		libMatch1 = 96,
		[Description("libMatch.tbd")]
		libMatch = 97,
		[Description("libmav_ipc_router_dynamic.tbd")]
		libmav_ipc_router_dynamic = 98,
		[Description("libmecab_em.tbd")]
		libmecab_em = 99,
		[Description("libmecabra.tbd")]
		libmecabra = 100,
		[Description("libmis.tbd")]
		libmis = 101,
		[Description("libMobileCheckpoint.tbd")]
		libMobileCheckpoint = 102,
		[Description("libMobileGestalt.tbd")]
		libMobileGestalt = 103,
		[Description("libMobileGestaltExtensions.tbd")]
		libMobileGestaltExtensions = 104,
		[Description("libncurses.5.4.tbd")]
		libncurses54 = 105,
		[Description("libncurses.tbd")]
		libncurses = 106,
		[Description("libnetwork.tbd")]
		libnework = 107,
		[Description("libnfshared.tbd")]
		libnfshared = 108,
		[Description("libobjc.A.tbd")]
		libobjcA = 109,
		[Description("libobjc.tbd")]
		libobjc = 110,
		[Description("libomadm.tbd")]
		libomadm = 111,
		[Description("libPCITransport.tbd")]
		libPCITransport = 112,
		[Description("libpoll.tbd")]
		libpoll = 113,
		[Description("libPPM.tbd")]
		libPPM = 114,
		[Description("libprequelite.tbd")]
		libprequelite = 115,
		[Description("libproc.tbd")]
		libproc = 116,
		[Description("libprotobuf.tbd")]
		libprotobuf = 117,
		[Description("libpthead.tbd")]
		libpthead = 118,
		[Description("libQLCharts.tbd")]
		libQLCharts = 119,
		[Description("libresolv.9.tbd")]
		libresolv9 = 120,
		[Description("libresolv.tbd")]
		libresolv = 121,
		[Description("librpcsvc.tbd")]
		librpcsvc = 122,
		[Description("libsandbox.1.tbd")]
		libsandbox1 = 123,
		[Description("libsandbox.tbd")]
		libsandbox = 124,
		[Description("libsp.tbd")]
		libsp = 125,
		[Description("libspindump.tbd")]
		libspindump = 126,
		[Description("libsqlite3.0.tbd")]
		libsqlite30 = 127,
		[Description("libsqlite3.tbd")]
		libsqlite3 = 128,
		[Description("libstdc++.6.0.9.tbd")]
		libstdcPlusPlus_609 = 129,
		[Description("libstdc++.6.tbd")]
		libstdcPlusPlus_6 = 130,
		[Description("libstdc++.tbd")]
		libstdcPlusPlus = 131,
		[Description("libsysdiagnose.tbd")]
		libsysdiagnose = 132,
		[Description("libSystem.B.tbd")]
		libSystemB = 133,
		[Description("libSystem.tbd")]
		libSystem = 134,
		[Description("libsystemstats.tbd")]
		libsystemstats = 135,
		[Description("libtailspin.tbd")]
		libtailspin = 136,
		[Description("libTelephonyBasebandBulkUSBDynamic.tbd")]
		libTelephonyBasebandBulkUSBDynamic = 137,
		[Description("libTelephonyBasebandDynamic.tbd")]
		libTelephonyBasebandDynamic = 138,
		[Description("libTelephonyDebugDynamic.tbd")]
		libTelephonyDebugDynamic = 139,
		[Description("libTelephonyIOKitDynamic.tbd")]
		libTelephonyIOKitDynamic = 140,
		[Description("libTelephonyUSBDynamic.tbd")]
		libTelephonyUSBDynamic = 141,
		[Description("libTelephonyUtilDynamic.tbd")]
		libTelephonyUtilDynamic = 142,
		[Description("libThaiTokenizer.tbd")]
		libThaiTokenizer = 143,
		[Description("libtidy.A.tbd")]
		libtidyA = 144,
		[Description("libtidy.tbd")]
		libtidy = 145,
		[Description("libzupdate.tbd")]
		libzupdate = 146,
		[Description("libutil.tbd")]
		libutil = 147,
		[Description("libutil1.0.tbd")]
		libutil_10 = 148,
		[Description("libWAPI.tbd")]
		libWAPI = 149,
		[Description("libxml2.2.tbd")]
		libxlm22 = 150,
		[Description("libxml2.tbd")]
		libxml2 = 151,
		[Description("libxslt.1.tbd")]
		libxslt1 = 152,
		[Description("libxslt.tbd")]
		libxslt = 153,
		[Description("libz.1.1.3.tbd")]
		libz113 = 154,
		[Description("libz.1.2.5.tbd")]
		libz125 = 155,
		[Description("libz.1.2.8.tbd")]
		libz128 = 156,
		[Description("libz.1.tbd")]
		libz1 = 157,
		[Description("libz.tbd")]
		libz = 158
	}
}
