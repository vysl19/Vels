# Vels
Rise Technology Challenge Work
-Yapılmayanlar:
.Postresqlle bağlantı yapılmadı(entityframeworkInmemory kullanıldı)
.Unit test yapılmadı.
-Kullanım şekli:
/api/Person post metoduyla kişi eklenir.
/api/Contact post metoduyla ilgili kişi için contact eklenir.
​/api​/Person get metoduyla butun kişilerin bilgisi döner(contact bilgileri hariç)
​/api​/Person/{id} get metoduyla kişi bilgisi kişiye ait contact bilgileri döner.
/api/Report post metoduyla report isteği oluşturulur.
Reportservice projesinde bulunan messagebussubcriber ile report isteklerine bakılır. Kuyrukta bir istek varsa, burdan contact web apiden butun contact listesi çekilir rapor sonucuyla ilgili rapor isteği eşleştirilir.