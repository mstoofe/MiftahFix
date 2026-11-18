# MiftahFix — مفتاح فكس

**تصحيح النصوص المكتوبة بلغة لوحة مفاتيح خاطئة بضغطة واحدة.**
كتبت `hgsghl ugd;l` بدل «السلام عليكم»؟ اضغط <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Space</kbd> ويُصحَّح فوراً.

## الميزات
- **تصحيح آخر ما كتبته بدون تحديد**، وضغطة ثانية تعيد النص كما كان.
- **تصحيح النص المحدد** في أي برنامج.
- **تبديل لغة لوحة المفاتيح تلقائياً** بعد التصحيح.
- **كشف الكتابة بلغة خاطئة** أثناء الكتابة: تنبيه أو تصحيح تلقائي.
- سجل للتصحيحات، تشغيل مع Windows، إيقاف مؤقت، وتحديثات تلقائية.

## الخصوصية
ما تكتبه يبقى في ذاكرة الجهاز فقط لتصحيحه، ولا يُحفظ في ملف ولا يُرسل إلى أي مكان.
الكود كاملاً متاح هنا لمن يريد التحقق.

## التحميل
حمّل آخر إصدار من صفحة [Releases](https://github.com/mstoofe/MiftahFix/releases/latest).

## البناء من الكود
يتطلب .NET 10 SDK وWindows.
```
dotnet build
dotnet run
dotnet publish -c Release
```
ثم افتح `MiftahFix.iss` في Inno Setup 6.4.1 أو أحدث لبناء المثبّت.

## المطوّر
**مصطفى كامل — Mustafa Kamil**
[صفحة المطوّر على فيسبوك](https://www.facebook.com/AynRutba/)

صُنع بحب في العراق ❤

## الترخيص
مفتوح المصدر بترخيص [MIT](LICENSE.txt).

---

## English
**MiftahFix** fixes text typed with the wrong keyboard layout (Arabic ↔ English).
Type, then press <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Space</kbd> — or select text and press it.
It also switches the keyboard layout automatically and can detect wrong-layout typing as you type.
Nothing you type is stored or sent anywhere.

Developer: **Mustafa Kamil** · [Facebook](https://www.facebook.com/AynRutba/) · License: MIT
