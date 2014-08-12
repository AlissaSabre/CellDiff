; -*- coding: utf-8; -*-

; Setup script for CellDiff: An Excel plug-in to compare cell contents
; This script requires NSIS version 3.0 or later.

RequestExecutionLevel admin
Unicode true
SetCompressor /SOLID /FINAL lzma

; Product Identity and other general config
!define PRODUCT_NAME "CellDiff"
!define PRODUCT_LONG_NAME "CellDiff: An Excel plug-in to compare cell contents"
!define PRODUCT_VERSION "0.6.1.0"
!define PRODUCT_PUBLISHER "Alissa Sabre"
!define PRODUCT_WEB_SITE "https://github.com/AlissaSabre/CellDiff"

!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

!define PRODUCT_README_EN "Readme.html"

!define PRODUCT_SETUP_NAME "CellDiff-${PRODUCT_VERSION}-setup.exe"

; VersionInfo resource for the installer (and uninstaller)
VIProductVersion "${PRODUCT_VERSION}"
VIAddVersionKey ProductVersion  "${PRODUCT_VERSION}"
VIAddVersionKey ProductName     "${PRODUCT_NAME}"
VIAddVersionKey CompanyName     "${PRODUCT_PUBLISHER}"
VIAddVersionKey LegalCopyright  "Written by ${PRODUCT_PUBLISHER}"
VIAddVersionKey FileVersion     "${PRODUCT_VERSION}"
VIAddVersionKey FileDescription "Installer of ${PRODUCT_NAME}"
VIAddVersionKey InternalName    "${PRODUCT_SETUP_NAME}"

!include "FileFunc.nsh"

!include "MUI.nsh"
#include "MUI2.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\win-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\win-uninstall.ico"

; Welcome page
!define MUI_PAGE_CUSTOMFUNCTION_LEAVE on_page_welcome_leave
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_SHOWREADME "$(LocalizedReadme)"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files -- Note that we have no install language selector,
; so adding too many languages here is pointless.  Better to keep the
; list minimum, with the first language to be English.
!insertmacro MUI_LANGUAGE "English"

LangString DotNetNotFound ${LANG_ENGLISH} \
    "${PRODUCT_NAME} requires Microsoft .NET Framework version 4.0, \
     but the installer couldn't find one on this computer.  \
     Please download it from Microsoft website, install it, \
     and then try running ${PRODUCT_SETUP_NAME} again."

Function on_page_welcome_leave
  ; Make sure .NET Framework version 4.0 presents
  push "$0"
  ReadRegStr  $0 HKLM "Software\Microsoft\.NetFramework" "InstallRoot"
  IfErrors ERR
  StrCmp $0 "" ERR
  IfFileExists "$0\..\Framework\v4.0.30319\regasm.exe" OK
Err:
  MessageBox MB_OK "$(DotNetNotFound)"
  Quit
OK:  
  pop $0
FunctionEnd

!define BINARY_DIR "."

Name "${PRODUCT_NAME}"
OutFile "${BINARY_DIR}\${PRODUCT_SETUP_NAME}"
InstallDir "$PROGRAMFILES\Alissa\CellDiff"
ShowInstDetails show
ShowUnInstDetails hide

LangString RegisterMessage ${LANG_ENGLISH}  "Register: ${PRODUCT_NAME}"

LangString RegAsmFailed    ${LANG_ENGLISH}  "RegAsm.exe failed"

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite on

  File    "${BINARY_DIR}\CellDiff.dll"
  File    "${BINARY_DIR}\NetOffice.dll"
  File    "${BINARY_DIR}\ExcelApi.dll"
  File    "${BINARY_DIR}\OfficeApi.dll"
  File    "${BINARY_DIR}\VBIDEApi.dll"
  File /r "${BINARY_DIR}\ja"

  File    "${BINARY_DIR}\${PRODUCT_README_EN}"
  File /r "${BINARY_DIR}\Images"

  ; Run regasm in .NET 4.0
  DetailPrint "$(RegisterMessage)"
  push $0
  ReadRegStr $0 HKLM "Software\Microsoft\.NetFramework" "InstallRoot"
  IfErrors ERR
  StrCmp $0 "" ERR
  IfFileExists "$0\..\Framework\v4.0.30319\regasm.exe" OK
Err:
  MessageBox MB_OK "$(DotNetNotFound)"
  Quit
OK:  
  nsExec::ExecToLog '"$0\..\Framework\v4.0.30319\regasm.exe" "$OUTDIR\CellDiff.dll" /codebase /nologo /silent'
  pop $0
  StrCmp $0 "0" OK2
  MessageBox MB_OK "$(RegAsmFailed): $0"
  Quit
OK2:  
  pop $0

SectionEnd

LangString LocalizedReadme ${LANG_ENGLISH}  "${PRODUCT_README_EN}"

Section -Post

  WriteUninstaller "$INSTDIR\uninst.exe"
  
  WriteRegStr   ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "${PRODUCT_LONG_NAME}"
  WriteRegStr   ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr   ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr   ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr   ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  WriteRegStr   ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Readme" "$INSTDIR\$(LocalizedReadme)"
  WriteRegDWORD ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "NoModify" 0x00000001
  WriteRegDWORD ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "NoRepair" 0x00000001

  Push $0
  Push $1
  Push $2
  ${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
  IntFmt $0 "0x%08X" $0
  WriteRegDWORD ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "EstimatedSize" "$0"
  Pop $2
  Pop $1
  Pop $0

SectionEnd

LangString ConfirmMessage ${LANG_ENGLISH}  "Do you want to uninstall ${PRODUCT_NAME}?"

Function un.onInit
  MessageBox MB_ICONEXCLAMATION|MB_YESNO|MB_DEFBUTTON2 "$(ConfirmMessage)" IDYES +2
  Abort
FunctionEnd

Section Uninstall

  ; Find regasm.exe in .NET 4.0 and use it to unregister the DLL.
  push "$0"
  ReadRegStr $0 HKLM "Software\Microsoft\.NetFramework" "InstallRoot"
  IfErrors SKIP
  StrCmp $0 "" SKIP
  IfFileExists "$0\..\Framework\v4.0.30319\regasm.exe" OK SKIP
OK:  
  nsExec::ExecToLog '"$0\..\Framework\v4.0.30319\regasm.exe" "$INSTDIR\CellDiff.dll" /codebase /nologo /silent /unregister'
  pop $0
SKIP:
  pop $0

  ; Remove the installed files and directory.
  Delete "$INSTDIR\Images\*.png"
  Delete "$INSTDIR\Images\Readme.txt"
  RMDir  "$INSTDIR\Images"
  Delete "$INSTDIR\${PRODUCT_README_EN}"

  Delete "$INSTDIR\ja\CellDiff.resources.dll"
  RMDir  "$INSTDIR\ja"
  Delete "$INSTDIR\CellDiff.dll"
  Delete "$INSTDIR\NetOffice.dll"
  Delete "$INSTDIR\ExcelApi.dll"
  Delete "$INSTDIR\OfficeApi.dll"
  Delete "$INSTDIR\VBIDEApi.dll"

  Delete "$INSTDIR\uninst.exe"
  RMDir  "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

SectionEnd

;;; 
;;; Following is a kind of a _hot_fix_ against NSIS supplied Japanese.nsh
;;; MakeNSIS generates a warning on it, but it is necessary
;LangString MUI_TEXT_FINISH_SHOWREADME ${LANG_JAPANESE} "Readmeを読む(&S)"
