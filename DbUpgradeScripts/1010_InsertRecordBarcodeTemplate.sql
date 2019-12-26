INSERT INTO [MBarcodeTemplate]
           (  
			 TemplateName,
			 Size,
			 NoOfColumn,
			 DefaultScript,
			 ScriptData,
			 PrinterName,
			 CompanyNo,
			 StatusNo,
			 UserID,
			 UserDate)
     VALUES
           (
'Large',
'40*35 - 4',
5,
'SIZE 108 mm,50 mm
GAP 3 mm,0 mm
REFERENCE 0,0
SPEED 3.0
DENSITY 13
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
DIRECTION 0
SHIFT 0
OFFSET 0 mm
CLS
TEXT 69,268,"0",0,10,9,"VarFirmName"
TEXT 274,211,"0",0,7,9,"VarBestBefore"
TEXT 287,147,"0",0,10,10,"VarRATE"
TEXT 286,181,"0",0,7,8,"VarWeight"
TEXT 247,181,"0",0,7,8,"WT:"
TEXT 232,150,"0",0,7,7,"RATE:"
TEXT 77,152,"0",0,10,10,"VarMRP"
TEXT 32,13,"0",0,7,8,"VarShortDesc"
TEXT 31,157,"0",0,7,7,"MRP:"
TEXT 204,210,"0",0,7,7,"BEST B:"
TEXT 72,211,"0",0,7,9,"VarPackedDate"
TEXT 32,211,"0",0,7,7,"Pkd:"
TEXT 140,242,"0",0,7,7,"VarFreeText1"
TEXT 38,304,"0",0,7,7,"VarFreeText2"
TEXT 38,333,"0",0,7,7,"VarFreeText3"
TEXT 38,361,"0",0,7,7,"VarFreeText4"
TEXT 32,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 32,50,"128M",74,1,0,2,6,"VarBarcode"
TEXT 497,268,"0",0,10,9,"VarFirmNAME"
TEXT 702,211,"0",0,7,9,"VarBestBefore"
TEXT 715,147,"0",0,10,10,"VarRATE"
TEXT 714,181,"0",0,7,8,"VarWeight"
TEXT 675,181,"0",0,7,8,"WT:"
TEXT 660,150,"0",0,7,7,"RATE:"
TEXT 505,152,"0",0,10,10,"VarMRP"
TEXT 460,13,"0",0,7,8,"VarShortDesc"
TEXT 459,157,"0",0,7,7,"MRP:"
TEXT 632,210,"0",0,7,7,"BEST B:"
TEXT 500,211,"0",0,7,9,"VarPackedDate"
TEXT 460,211,"0",0,7,7,"Pkd:"
TEXT 568,242,"0",0,7,7,"VarFreeText1"
TEXT 466,304,"0",0,7,7,"VarFreeText2"
TEXT 466,333,"0",0,7,7,"VarFreeText3"
TEXT 466,361,"0",0,7,7,"VarFreeText4"
TEXT 460,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 460,50,"128M",74,1,0,2,6,"VarBarcode"
PRINT VarPrint,1',


'SIZE 108 mm,50 mm
GAP 3 mm,0 mm
REFERENCE 0,0
SPEED 3.0
DENSITY 13
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
DIRECTION 0
SHIFT 0
OFFSET 0 mm
CLS
TEXT 69,268,"0",0,10,9,"VarFirmName"
TEXT 274,211,"0",0,7,9,"VarBestBefore"
TEXT 287,147,"0",0,10,10,"VarRATE"
TEXT 286,181,"0",0,7,8,"VarWeight"
TEXT 247,181,"0",0,7,8,"WT:"
TEXT 232,150,"0",0,7,7,"RATE:"
TEXT 77,152,"0",0,10,10,"VarMRP"
TEXT 32,13,"0",0,7,8,"VarShortDesc"
TEXT 31,157,"0",0,7,7,"MRP:"
TEXT 204,210,"0",0,7,7,"BEST B:"
TEXT 72,211,"0",0,7,9,"VarPackedDate"
TEXT 32,211,"0",0,7,7,"Pkd:"
TEXT 140,242,"0",0,7,7,"VarFreeText1"
TEXT 38,304,"0",0,7,7,"VarFreeText2"
TEXT 38,333,"0",0,7,7,"VarFreeText3"
TEXT 38,361,"0",0,7,7,"VarFreeText4"
TEXT 32,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 32,50,"128M",74,1,0,2,6,"VarBarcode"
TEXT 497,268,"0",0,10,9,"VarFirmNAME"
TEXT 702,211,"0",0,7,9,"VarBestBefore"
TEXT 715,147,"0",0,10,10,"VarRATE"
TEXT 714,181,"0",0,7,8,"VarWeight"
TEXT 675,181,"0",0,7,8,"WT:"
TEXT 660,150,"0",0,7,7,"RATE:"
TEXT 505,152,"0",0,10,10,"VarMRP"
TEXT 460,13,"0",0,7,8,"VarShortDesc"
TEXT 459,157,"0",0,7,7,"MRP:"
TEXT 632,210,"0",0,7,7,"BEST B:"
TEXT 500,211,"0",0,7,9,"VarPackedDate"
TEXT 460,211,"0",0,7,7,"Pkd:"
TEXT 568,242,"0",0,7,7,"VarFreeText1"
TEXT 466,304,"0",0,7,7,"VarFreeText2"
TEXT 466,333,"0",0,7,7,"VarFreeText3"
TEXT 466,361,"0",0,7,7,"VarFreeText4"
TEXT 460,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 460,50,"128M",74,1,0,2,6,"VarBarcode"
PRINT VarPrint,1',

'TSC TTP-244 Plus (Copy 1)',
1,
3,
1,
'10/20/2018 11:39:05 AM'

 );
/*--------------------------------------------------------------------------------------------------------------------*/

INSERT INTO [MBarcodeTemplate]
           (  
			 TemplateName,
			 Size,
			 NoOfColumn,
			 DefaultScript,
			 ScriptData,
			 PrinterName,
			 CompanyNo,
			 StatusNo,
			 UserID,
			 UserDate)
     VALUES
           (
'Extra Large',
'24*35 - 5',
3,

'SIZE 108 mm,50 mm
GAP 3 mm,0 mm
REFERENCE 0,0
SPEED 3.0
DENSITY 13
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
DIRECTION 0
SHIFT 0
OFFSET 0 mm
CLS
TEXT 69,268,"0",0,10,9,"VarFirmName"
TEXT 274,211,"0",0,7,9,"VarBestBefore"
TEXT 287,147,"0",0,10,10,"VarRATE"
TEXT 286,181,"0",0,7,8,"VarWeight"
TEXT 247,181,"0",0,7,8,"WT:"
TEXT 232,150,"0",0,7,7,"RATE:"
TEXT 77,152,"0",0,10,10,"VarMRP"
TEXT 32,13,"0",0,7,8,"VarShortDesc"
TEXT 31,157,"0",0,7,7,"MRP:"
TEXT 204,210,"0",0,7,7,"BEST B:"
TEXT 72,211,"0",0,7,9,"VarPackedDate"
TEXT 32,211,"0",0,7,7,"Pkd:"
TEXT 140,242,"0",0,7,7,"VarFreeText1"
TEXT 38,304,"0",0,7,7,"VarFreeText2"
TEXT 38,333,"0",0,7,7,"VarFreeText3"
TEXT 38,361,"0",0,7,7,"VarFreeText4"
TEXT 32,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 32,50,"128M",74,1,0,2,6,"VarBarcode"
TEXT 497,268,"0",0,10,9,"VarFirmNAME"
TEXT 702,211,"0",0,7,9,"VarBestBefore"
TEXT 715,147,"0",0,10,10,"VarRATE"
TEXT 714,181,"0",0,7,8,"VarWeight"
TEXT 675,181,"0",0,7,8,"WT:"
TEXT 660,150,"0",0,7,7,"RATE:"
TEXT 505,152,"0",0,10,10,"VarMRP"
TEXT 460,13,"0",0,7,8,"VarShortDesc"
TEXT 459,157,"0",0,7,7,"MRP:"
TEXT 632,210,"0",0,7,7,"BEST B:"
TEXT 500,211,"0",0,7,9,"VarPackedDate"
TEXT 460,211,"0",0,7,7,"Pkd:"
TEXT 568,242,"0",0,7,7,"VarFreeText1"
TEXT 466,304,"0",0,7,7,"VarFreeText2"
TEXT 466,333,"0",0,7,7,"VarFreeText3"
TEXT 466,361,"0",0,7,7,"VarFreeText4"
TEXT 460,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 460,50,"128M",74,1,0,2,6,"VarBarcode"
PRINT VarPrint,1',

'SIZE 108 mm,50 mm
GAP 3 mm,0 mm
REFERENCE 0,0
SPEED 3.0
DENSITY 13
SET PEEL OFF
SET CUTTER OFF
SET PARTIAL_CUTTER OFF
SET TEAR ON
DIRECTION 0
SHIFT 0
OFFSET 0 mm
CLS
TEXT 69,268,"0",0,10,9,"VarFirmName"
TEXT 274,211,"0",0,7,9,"VarBestBefore"
TEXT 287,147,"0",0,10,10,"VarRATE"
TEXT 286,181,"0",0,7,8,"VarWeight"
TEXT 247,181,"0",0,7,8,"WT:"
TEXT 232,150,"0",0,7,7,"RATE:"
TEXT 77,152,"0",0,10,10,"VarMRP"
TEXT 32,13,"0",0,7,8,"VarShortDesc"
TEXT 31,157,"0",0,7,7,"MRP:"
TEXT 204,210,"0",0,7,7,"BEST B:"
TEXT 72,211,"0",0,7,9,"VarPackedDate"
TEXT 32,211,"0",0,7,7,"Pkd:"
TEXT 140,242,"0",0,7,7,"VarFreeText1"
TEXT 38,304,"0",0,7,7,"VarFreeText2"
TEXT 38,333,"0",0,7,7,"VarFreeText3"
TEXT 38,361,"0",0,7,7,"VarFreeText4"
TEXT 32,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 32,50,"128M",74,1,0,2,6,"VarBarcode"
TEXT 497,268,"0",0,10,9,"VarFirmNAME"
TEXT 702,211,"0",0,7,9,"VarBestBefore"
TEXT 715,147,"0",0,10,10,"VarRATE"
TEXT 714,181,"0",0,7,8,"VarWeight"
TEXT 675,181,"0",0,7,8,"WT:"
TEXT 660,150,"0",0,7,7,"RATE:"
TEXT 505,152,"0",0,10,10,"VarMRP"
TEXT 460,13,"0",0,7,8,"VarShortDesc"
TEXT 459,157,"0",0,7,7,"MRP:"
TEXT 632,210,"0",0,7,7,"BEST B:"
TEXT 500,211,"0",0,7,9,"VarPackedDate"
TEXT 460,211,"0",0,7,7,"Pkd:"
TEXT 568,242,"0",0,7,7,"VarFreeText1"
TEXT 466,304,"0",0,7,7,"VarFreeText2"
TEXT 466,333,"0",0,7,7,"VarFreeText3"
TEXT 466,361,"0",0,7,7,"VarFreeText4"
TEXT 460,185,"0",0,7,7,"(INCL. OF ALL TAXES)"
BARCODE 460,50,"128M",74,1,0,2,6,"VarBarcode"
PRINT VarPrint,1',

'TSC TTP-244 Plus (Copy 1)',
1,
3,
1,
'10/20/2018 11:39:05 AM'

 );