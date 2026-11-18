MiftahFix Installer - تعليمات الاستخدام
=========================================

1. تأكد أولًا أن النسخة المنشورة موجودة في:

bin\Release\net10.0-windows\win-x64\publish

2. تأكد أن الأيقونة موجودة في:

Assets\app.ico

3. ضع ملف:

MiftahFix.iss

داخل المجلد الرئيسي للمشروع، بجانب:

MiftahFix.csproj

ليصبح الشكل كالتالي:

MiftahFix
│
├── Assets
│   └── app.ico
│
├── bin
│   └── Release
│       └── net10.0-windows
│           └── win-x64
│               └── publish
│                   └── MiftahFix.exe
│
├── MiftahFix.csproj
└── MiftahFix.iss

4. افتح Inno Setup Compiler.

5. اختر:

File > Open

ثم افتح:

MiftahFix.iss

6. اضغط:

Build > Compile

أو اضغط F9.

7. سيظهر ملف التثبيت داخل:

InstallerOutput\MiftahFix_Setup_v1.1.0.exe

ملاحظات:
- ملف التثبيت يعمل على Windows 64-bit.
- البرنامج Self-contained ولا يحتاج إلى تثبيت .NET Runtime.
- سيظهر البرنامج في قائمة إزالة البرامج.
- يمكن إنشاء اختصار سطح المكتب من شاشة التثبيت.
- عند إزالة البرنامج سيظهر خيار لحذف الإعدادات والسجل.