إزالة أزرار تكبير وتصغير النص

استبدل الملف:
UI\Controls\ModernTextEditor.cs

ولا تحتاج إلى أي تعديل آخر.

تم حذف زري التكبير والتصغير بالكامل من شريط أدوات المحرر.
يبقى التكبير الاختياري عبر Ctrl + عجلة الماوس، لكنه لا يظهر كأزرار في الواجهة.

بعد الاستبدال:
taskkill /F /IM MiftahFix.exe
dotnet clean
dotnet build
dotnet run
