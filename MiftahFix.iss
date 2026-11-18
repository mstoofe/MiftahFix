; ============================================================
; MiftahFix Installer
; الإصدار: 1.1.0
; التقنية: Inno Setup
; ============================================================

#define MyAppName "MiftahFix"
#define MyAppVersion "1.1.0"
#define MyAppPublisher "Mustafa Kamil - مصطفى كامل"
#define MyAppURL "https://www.facebook.com/AynRutba/"
#define MyAppSourceURL "https://github.com/mstoofe/MiftahFix"
#define MyAppCopyright "Copyright © 2026 Mustafa Kamil (مصطفى كامل) - MIT License"
#define MyAppExeName "MiftahFix.exe"
#define MyAppDescription "أداة لتصحيح النصوص المكتوبة باستخدام تخطيط لوحة مفاتيح غير صحيح"
#define MyAppIcon "Assets\app.ico"
#define MyAppPublishFolder "bin\Release\net10.0-windows\win-x64\publish"

[Setup]
AppId={{B52BBE9F-5F65-4C31-8E67-6AA0F04A7B21}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppSourceURL}/releases
AppCopyright={#MyAppCopyright}
AppContact={#MyAppURL}
AppComments={#MyAppDescription}

DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes

OutputDir=InstallerOutput
OutputBaseFilename=MiftahFix_Setup_v{#MyAppVersion}
SetupIconFile={#MyAppIcon}
UninstallDisplayIcon={app}\{#MyAppExeName}

Compression=lzma2/ultra64
SolidCompression=yes

ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=dialog

WizardStyle=modern
ShowLanguageDialog=yes
WizardResizable=no

CloseApplications=yes
RestartApplications=no

AllowNoIcons=yes
CreateUninstallRegKey=yes
UninstallDisplayName={#MyAppName}
Uninstallable=yes

VersionInfoVersion=1.1.0.0
VersionInfoCompany={#MyAppPublisher}
VersionInfoDescription={#MyAppDescription}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}
VersionInfoCopyright={#MyAppCopyright}

SetupLogging=yes

; الترجمة العربية الرسمية متوفرة في Inno Setup بدءاً من الإصدار 6.4.1
#if VER < EncodeVer(6,4,1)
  #error "يلزم Inno Setup 6.4.1 أو أحدث لدعم اللغة العربية. حمّل آخر إصدار من jrsoftware.org"
#endif

[Languages]
; العربية أولاً، ويختار المثبّت تلقائياً حسب لغة Windows، مع نافذة لاختيار اللغة.
; لكل لغة: الترخيص وصفحة "معلومات قبل التثبيت" الخاصة بها.
Name: "arabic"; MessagesFile: "compiler:Languages\Arabic.isl"; \
    LicenseFile: "Installer\License_ar.txt"; \
    InfoBeforeFile: "Installer\Info_ar.txt"
Name: "english"; MessagesFile: "compiler:Default.isl"; \
    LicenseFile: "LICENSE.txt"; \
    InfoBeforeFile: "Installer\Info_en.txt"

[Messages]
; سطر المطوّر في صفحة الترحيب الأولى وصفحة الانتهاء.
arabic.WelcomeLabel2=سيتم تثبيت [name/ver] على جهازك.%n%nأداة تصحح النصوص المكتوبة بلغة لوحة مفاتيح خاطئة بضغطة واحدة.%n%nتطوير: مصطفى كامل%nصُنع بحب في العراق ❤%n%nيُنصح بإغلاق البرامج الأخرى قبل المتابعة.
english.WelcomeLabel2=This will install [name/ver] on your computer.%n%nA tool that fixes text typed with the wrong keyboard layout in one keystroke.%n%nDeveloper: Mustafa Kamil (مصطفى كامل)%nMade with love in Iraq ❤%n%nIt is recommended that you close all other applications before continuing.
arabic.FinishedLabel=اكتمل تثبيت [name] على جهازك.%n%nاكتب ثم اضغط Ctrl + Shift + Space لتصحيح ما كتبته.%n%nشكراً لاستخدامك البرنامج - تابع المطوّر على فيسبوك: facebook.com/AynRutba
english.FinishedLabel=Setup has finished installing [name] on your computer.%n%nType, then press Ctrl + Shift + Space to fix what you typed.%n%nThanks for using MiftahFix - follow the developer on Facebook: facebook.com/AynRutba

[CustomMessages]
arabic.VisitDeveloperTask=زيارة صفحة المطوّر على فيسبوك
english.VisitDeveloperTask=Visit the developer's Facebook page
arabic.AppDescription=أداة لتصحيح النصوص المكتوبة باستخدام تخطيط لوحة مفاتيح غير صحيح
arabic.DesktopIconTask=إنشاء اختصار على سطح المكتب
arabic.AdditionalIcons=اختصارات إضافية:
arabic.LaunchProgramTask=تشغيل MiftahFix بعد انتهاء التثبيت
arabic.DeleteUserData=هل تريد حذف إعدادات البرنامج وسجل التحويلات أيضاً؟

english.AppDescription=Fixes text typed with the wrong keyboard layout (Arabic / English)
english.DesktopIconTask=Create a desktop shortcut
english.AdditionalIcons=Additional shortcuts:
english.LaunchProgramTask=Launch MiftahFix after installation
english.DeleteUserData=Do you also want to delete the program settings and conversion history?

[Tasks]
Name: "desktopicon"; \
    Description: "{cm:DesktopIconTask}"; \
    GroupDescription: "{cm:AdditionalIcons}"; \
    Flags: unchecked

[Files]
Source: "{#MyAppPublishFolder}\*"; \
    DestDir: "{app}"; \
    Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; \
    Filename: "{app}\{#MyAppExeName}"; \
    WorkingDir: "{app}"; \
    IconFilename: "{app}\{#MyAppExeName}"

Name: "{autodesktop}\{#MyAppName}"; \
    Filename: "{app}\{#MyAppExeName}"; \
    WorkingDir: "{app}"; \
    IconFilename: "{app}\{#MyAppExeName}"; \
    Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; \
    Description: "{cm:LaunchProgramTask}"; \
    WorkingDir: "{app}"; \
    Flags: nowait postinstall skipifsilent

Filename: "{#MyAppURL}"; \
    Description: "{cm:VisitDeveloperTask}"; \
    Flags: postinstall shellexec skipifsilent nowait unchecked

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Code]
const
  StartupRegistryPath = 'Software\Microsoft\Windows\CurrentVersion\Run';
  StartupRegistryValue = 'MiftahFix';

function IsApplicationRunning(): Boolean;
var
  ResultCode: Integer;
begin
  Result :=
    Exec(
      'tasklist.exe',
      '/FI "IMAGENAME eq {#MyAppExeName}"',
      '',
      SW_HIDE,
      ewWaitUntilTerminated,
      ResultCode
    ) and (ResultCode = 0);
end;

procedure RemoveStartupRegistryEntry();
begin
  RegDeleteValue(
    HKCU,
    StartupRegistryPath,
    StartupRegistryValue
  );
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  UserDataFolder: string;
  Answer: Integer;
begin
  if CurUninstallStep = usUninstall then
  begin
    RemoveStartupRegistryEntry();
  end;

  if CurUninstallStep = usPostUninstall then
  begin
    UserDataFolder :=
      ExpandConstant('{localappdata}\MiftahFix');

    if DirExists(UserDataFolder) then
    begin
      Answer :=
        MsgBox(
          ExpandConstant('{cm:DeleteUserData}'),
          mbConfirmation,
          MB_YESNO or MB_DEFBUTTON2
        );

      if Answer = IDYES then
      begin
        DelTree(
          UserDataFolder,
          True,
          True,
          True
        );
      end;
    end;
  end;
end;
