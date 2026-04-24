// Egypt governorates + selected cities (EN keys with AR labels).
// Used by checkout governorate/city dropdowns.
(function attachEgyptLocations(global) {
  const data = [
    { key: "Cairo", ar: "القاهرة", en: "Cairo", cities: ["Cairo"] },
    { key: "Giza", ar: "الجيزة", en: "Giza", cities: ["Giza", "6th of October", "Sheikh Zayed"] },
    { key: "Alexandria", ar: "الإسكندرية", en: "Alexandria", cities: ["Alexandria", "Borg El Arab"] },
    { key: "Qalyubia", ar: "القليوبية", en: "Qalyubia", cities: ["Banha", "Qalyub", "Shibin El Qanater", "Tukh", "El Khanka"] },
    { key: "Sharqia", ar: "الشرقية", en: "Sharqia", cities: ["Zagazig", "10th of Ramadan", "Belbeis"] },
    { key: "Dakahlia", ar: "الدقهلية", en: "Dakahlia", cities: ["Mansoura", "Talkha", "Mit Ghamr"] },
    { key: "Gharbia", ar: "الغربية", en: "Gharbia", cities: ["Tanta", "El Mahalla El Kubra"] },
    { key: "Monufia", ar: "المنوفية", en: "Monufia", cities: ["Shebin El Kom", "Sadat City"] },
    { key: "Kafr El Sheikh", ar: "كفر الشيخ", en: "Kafr El Sheikh", cities: ["Kafr El Sheikh", "Desouk"] },
    { key: "Beheira", ar: "البحيرة", en: "Beheira", cities: ["Damanhur", "Kafr El Dawwar", "Rashid"] },
    { key: "Ismailia", ar: "الإسماعيلية", en: "Ismailia", cities: ["Ismailia"] },
    { key: "Suez", ar: "السويس", en: "Suez", cities: ["Suez"] },
    { key: "Port Said", ar: "بورسعيد", en: "Port Said", cities: ["Port Said"] },
    { key: "Damietta", ar: "دمياط", en: "Damietta", cities: ["Damietta", "New Damietta"] },
    { key: "North Sinai", ar: "شمال سيناء", en: "North Sinai", cities: ["Arish"] },
    { key: "South Sinai", ar: "جنوب سيناء", en: "South Sinai", cities: ["Sharm El Sheikh", "Dahab", "Nuweiba"] },
    { key: "Faiyum", ar: "الفيوم", en: "Faiyum", cities: ["Faiyum"] },
    { key: "Beni Suef", ar: "بني سويف", en: "Beni Suef", cities: ["Beni Suef"] },
    { key: "Minya", ar: "المنيا", en: "Minya", cities: ["Minya"] },
    { key: "Asyut", ar: "أسيوط", en: "Asyut", cities: ["Asyut"] },
    { key: "Sohag", ar: "سوهاج", en: "Sohag", cities: ["Sohag"] },
    { key: "Qena", ar: "قنا", en: "Qena", cities: ["Qena"] },
    { key: "Luxor", ar: "الأقصر", en: "Luxor", cities: ["Luxor"] },
    { key: "Aswan", ar: "أسوان", en: "Aswan", cities: ["Aswan"] },
    { key: "Red Sea", ar: "البحر الأحمر", en: "Red Sea", cities: ["Hurghada", "Safaga", "Marsa Alam"] },
    { key: "New Valley", ar: "الوادي الجديد", en: "New Valley", cities: ["Kharga", "Dakhla"] },
    { key: "Matrouh", ar: "مطروح", en: "Matrouh", cities: ["Mersa Matruh", "El Alamein", "Siwa"] }
  ];

  function byKey(key) {
    return data.find(g => g.key === key) || null;
  }

  global.EgyptLocations = { governorates: data, byKey };
})(window);

